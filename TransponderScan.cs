/* Copyright 2018 Kay Diefenthal.

  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Cinegy.TsDecoder.Tables;
using Cinegy.TsDecoder.TransportStream;
using SatIp.Properties;

namespace SatIp
{
    //delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);
    //delegate void AddResultDelegate(Channel chan);
    public partial class TransponderScan : Form
    {
        private int _currentpid;
        private string _currenttunestring;
        private readonly TsDecoder _decoder = new TsDecoder();
        private readonly SatIpDevice _device;
        private bool _isScanning;
        private bool _locked;
        private IPEndPoint _remoteEndPoint;
        private Thread _scanThread;
        private AutoResetEvent _scanThreadStopEvent;
        private bool _stopScanning;
        private UdpClient _udpclient;
        private bool nitfound;
        private readonly TsPacketFactory packetFactory = new TsPacketFactory();
        private bool patfound;
        private bool pmtsfound;
        private bool sdtfound;

        public TransponderScan()
        {
            InitializeComponent();
        }

        public TransponderScan(SatIpDevice device)
        {
            InitializeComponent();
            _device = device;

            #region DeviceInfo

            tbxDeviceType.Text = device.DeviceType;
            tbxFriendlyName.Text = device.FriendlyName;
            tbxManufacture.Text = device.Manufacturer;
            tbxModelDescription.Text = device.ModelDescription;
            tbxUniqueDeviceName.Text = device.UniqueDeviceName;
            pbxDVBC.Image = Resources.dvb_c;
            pbxDVBC.Visible = device.SupportsDVBC;
            pbxDVBS.Image = Resources.dvb_s;
            pbxDVBS.Visible = device.SupportsDVBS;
            pbxDVBT.Image = Resources.dvb_t;
            pbxDVBT.Visible = device.SupportsDVBT;

            try
            {
                var imageUrl =
                    string.Format(device.FriendlyName == "OctopusNet" ? "http://{0}:{1}/{2}" : "http://{0}:{1}{2}",
                        device.BaseUrl.Host, device.BaseUrl.Port, device.GetImage(1));
                pbxManufactureBrand.LoadAsync(imageUrl);
                pbxManufactureBrand.Visible = true;
            }
            catch
            {
                pbxManufactureBrand.Visible = false;
            }

            #endregion
        }

        private void TransponderScan_Load(object sender, EventArgs e)
        {
            var app = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var tuningdata = app + "\\TuningData\\Satellite";
            foreach (var str2 in Directory.GetFiles(tuningdata))
            {
                var satellitesreader = new IniReader(str2);
                var str3 = satellitesreader.ReadString("SATTYPE", "1");
                var str4 = satellitesreader.ReadString("SATTYPE", "2");
                if (!SatellitesCombobox.Items.Contains(str4) && str4 != "")
                    SatellitesCombobox.Items.Add(new IniMapping(str3 + " " + str4, str2));
                SatellitesCombobox.SelectedIndex = 0;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_device != null) _device.Dispose();
        }

        private static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
                control.Invoke(new SetControlPropertyThreadSafeDelegate
                    (SetControlPropertyThreadSafe), control, propertyName, propertyValue);
            else
                control.GetType().InvokeMember(
                    propertyName,
                    BindingFlags.SetProperty,
                    null,
                    control,
                    new[] {propertyValue});
        }

        private void SatellitesCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TransponderCombobox.Items.Clear();
            var Satellites = (IniMapping) SatellitesCombobox.SelectedItem;
            var transponderreader = new IniReader(Satellites.File);
            var Count = transponderreader.ReadInteger("DVB", "0", 0);
            for (var i = 1; i <= Count; i++)
            {
                var strArray = transponderreader.ReadString("DVB", i.ToString()).Split(',');
                TransponderCombobox.Items.Add(new Transponder(int.Parse(strArray[0]), int.Parse(strArray[2]),
                    strArray[3], strArray[1], strArray[4], strArray[5]));
            }

            TransponderCombobox.SelectedIndex = 0;
        }

        private void TransponderCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var transponder = (Transponder) TransponderCombobox.SelectedItem;
            FrequencyTextBox.Text = transponder.Frequency.ToString();
            SymbolrateTextBox.Text = transponder.Symbolrate.ToString();
            ModulationTypeComboBox.Text = transponder.ModulationType;
            ModulationSystemComboBox.Text = transponder.ModulationSystem;
            PolarisationComboBox.Text = transponder.Polarisation;
            FecComboBox.Text = transponder.Fec;
            PilotComboBox.Text = transponder.Pilot;
            RollOffComboBox.Text = transponder.Rolloff.ToString();

            var source = "1";
            _currenttunestring = string.Format(
                "src={0}&freq={1}&pol={2}&sr={3}&fec={4}&msys={5}&mtype={6}&plts=on&ro=0.35&pids={7}",
                source,
                transponder.Frequency,
                transponder.Polarisation,
                transponder.Symbolrate,
                transponder.Fec,
                transponder.ModulationSystem,
                transponder.ModulationType,
                transponder.Pilot,
                transponder.Rolloff,
                _currentpid = 0);
        }

        private void StartScanThread()
        {
            if (_scanThread != null && !_scanThread.IsAlive) StopScanThread();

            if (_scanThread == null)
            {
                _scanThreadStopEvent = new AutoResetEvent(false);
                _scanThread = new Thread(DoScan)
                {
                    Name = "SAT>IP Scan",
                    IsBackground = true,
                    Priority = ThreadPriority.Highest
                };
                _scanThread.Start();
            }
        }

        private void StopScanThread()
        {
            if (_scanThread != null)
            {
                if (!_scanThread.IsAlive)
                    _scanThread.Abort();
                else
                    _scanThreadStopEvent.Set();
                _scanThread = null;
                if (_scanThreadStopEvent != null)
                {
                    _scanThreadStopEvent.Close();
                    _scanThreadStopEvent = null;
                }
            }
        }

        private void DoScan()
        {
            _decoder.TableChangeDetected += _decoder_TableChangeDetected;
            var programMapTables = new List<ProgramMapTable>();
            _isScanning = true;
            _stopScanning = false;
            SetControlPropertyThreadSafe(btnScan, "Text", "Stop Search");
            programMapTables.Clear();
            patfound = false;
            pmtsfound = false;
            nitfound = false;
            sdtfound = false;
            SetControlPropertyThreadSafe(lblPat, "BackColor", Color.LightGreen);
            SetControlPropertyThreadSafe(lblPmt, "BackColor", Color.LightGreen);
            SetControlPropertyThreadSafe(lblSdt, "BackColor", Color.LightGreen);
            SetControlPropertyThreadSafe(lblNit, "BackColor", Color.LightGreen);
            if (_stopScanning) return;

            RtspStatusCode statuscode;
            if (string.IsNullOrEmpty(_device.RtspSession.SessionID))
            {
                _device.RtspSession.Options();
                _device.RtspSession.Setup(_currenttunestring, TransmissionMode.Unicast);
                _device.RtspSession.Play(string.Empty);
                _udpclient = new UdpClient(_device.RtspSession.RtpPort);
                _remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                _device.RtspSession.RecieptionInfoChanged += RtspSession_RecieptionInfoChanged;
                _device.RtspSession.Play(_currenttunestring);
            }
            else
            {
                _device.RtspSession.Play(_currenttunestring);
            }

            /* Say the Sat>IP server we want Receives the Recieption Details SDP */
            statuscode = _device.RtspSession.Describe();

            if (_locked)
            {
                while (!patfound || !pmtsfound || !sdtfound || !nitfound)
                {
                    var receivedbytes = _udpclient.Receive(ref _remoteEndPoint);
                    var rtp = RtpPacket.Decode(receivedbytes);
                    if (rtp.HasPayload)
                    {
                        Logger.Write(rtp.Payload.ToHexString());
                        _decoder.AddData(receivedbytes);
                    }

                    //}
                    lock (_decoder)
                    {
                        programMapTables = _decoder?.ProgramMapTables.OrderBy(p => p.ProgramNumber).ToList();
                        foreach (var programMapTable in programMapTables)
                        {
                            short videoPid = -1;
                            short audioPid = -1;
                            short ac3pid = -1;
                            short ttxpid = -1;
                            short subpid = -1;
                            var desc = _decoder.GetServiceDescriptorForProgramNumber(programMapTable?.ProgramNumber);
                            if (desc != null)
                            {
                                if (programMapTable?.EsStreams != null)
                                    foreach (var stream in programMapTable.EsStreams)
                                        if (stream != null)
                                        {
                                            if (stream.Descriptors.OfType<Ac3Descriptor>().Any())
                                                ac3pid = stream.ElementaryPid;
                                            if (stream.Descriptors.OfType<Eac3Descriptor>().Any())
                                                ac3pid = stream.ElementaryPid;
                                            if (stream.Descriptors.OfType<SubtitlingDescriptor>().Any())
                                                subpid = stream.ElementaryPid;
                                            if (stream.Descriptors.OfType<TeletextDescriptor>().Any())
                                                ttxpid = stream.ElementaryPid;
                                            switch (stream.StreamType)
                                            {
                                                case 0x01: // ISO/IEC 11172-2 (MPEG-1 video) in a packetized stream
                                                case 0x02
                                                    : // ITU-T Rec. H.262 and ISO/IEC 13818-2 (MPEG-2 higher rate interlaced video) in a packetized stream
                                                case 0x1B
                                                    : // ITU-T Rec. H.264 and ISO/IEC 14496-10 (lower bit-rate video) in a packetized stream                                                        
                                                case 0x24
                                                    : // ITU - T Rec.H.265 and ISO/ IEC 23008 - 2(Ultra HD video) in a packetized stream
                                                {
                                                    videoPid = stream.ElementaryPid;
                                                    break;
                                                }
                                                case 0x03: // ISO/IEC 11172-3 (MPEG-1 audio) in a packetized stream
                                                case 0x04
                                                    : // ISO/IEC 13818-3 (MPEG-2 halved sample rate audio) in a packetized stream

                                                {
                                                    audioPid = stream.ElementaryPid;
                                                    break;
                                                }
                                                default:
                                                {
                                                    Console.Write(stream.StreamType.ToString());
                                                    break;
                                                }
                                            }
                                        }

                                var chan = new SatChannel
                                {
                                    ServiceType = desc.ServiceType,
                                    ServiceName = desc.ServiceName.Value,
                                    ServiceProvider = desc.ServiceProviderName.Value,
                                    ServiceId = programMapTable.ProgramNumber,
                                    ProgramClockReferenceId = programMapTable.PcrPid,
                                    AudioPid = audioPid,
                                    VideoPid = videoPid,
                                    AC3Pid = ac3pid,
                                    TTXPid = ttxpid,
                                    SubTitlePid = subpid
                                };
                                AddResults(chan);
                            }
                        }
                    }
                }

                //Thread.Sleep(55000);
                _device.RtspSession.TearDown();
                SetControlPropertyThreadSafe(pgbSearchResult, "Value", 100);
                _isScanning = false;
                SetControlPropertyThreadSafe(btnScan, "Text", "Start Search");
                StopScanThread();
            }
        }

        private void RtspSession_RecieptionInfoChanged(object sender, RecieptionInfoArgs e)
        {
            SetControlPropertyThreadSafe(pgbSignalLevel, "Value", e.Level);
            SetControlPropertyThreadSafe(pgbSignalQuality, "Value", e.Quality);
            _locked = e.Locked;
        }

        private void _decoder_TableChangeDetected(object sender, TableChangedEventArgs args)
        {
            //Called for Pat and Nit 
            if (args.TableType == TableType.Pat)
            {
                patfound = true;
                SetControlPropertyThreadSafe(lblPat, "BackColor", Color.Green);
                _device.RtspSession.Play("&delpids=0");
                foreach (var pid in _decoder.ProgramAssociationTable.Pids)
                    _device.RtspSession.Play(string.Format("&addpids={0}", pid));
                _device.RtspSession.Play("&addpids=17");
            }
            else if (args.TableType == TableType.Pmt)
            {
                _device.RtspSession.Play(string.Format("&delpids={0}", args.TablePid));
            }
            else if (args.TableType == TableType.Sdt)
            {
                pmtsfound = true;
                SetControlPropertyThreadSafe(lblPmt, "BackColor", Color.Green);
                sdtfound = true;
                SetControlPropertyThreadSafe(lblSdt, "BackColor", Color.Green);
                _device.RtspSession.Play(string.Format("&delpids={0}", args.TablePid));
                _device.RtspSession.Play("&addpids=16");
            }
            else if (args.TableType == TableType.Nit)
            {
                nitfound = true;
                SetControlPropertyThreadSafe(lblNit, "BackColor", Color.Green);
                _device.RtspSession.Play(string.Format("&delpids={0}", args.TablePid));
            }
        }

        private void AddResults(SatChannel chan)
        {
            //_count++;

            if (lvResult.InvokeRequired)
            {
                lvResult.Invoke(new AddResultDelegate(AddResults), chan);
            }
            else
            {
                string[] items =
                {
                    chan.ServiceType.ToString(),
                    chan.ServiceName,
                    chan.ServiceProvider,
                    chan.ServiceId.ToString(),
                    chan.ProgramClockReferenceId.ToString(),
                    chan.VideoPid.ToString(),
                    chan.AudioPid.ToString(),
                    chan.AC3Pid.ToString(),
                    chan.TTXPid.ToString(),
                    chan.SubTitlePid.ToString()
                };
                var lstItem = new ListViewItem(items)
                {
                    Checked = true
                };
                lvResult.Items.Add(lstItem);

                //label1.Text = string.Format("Services Found; {0}", _count.ToString());
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (lvResult.Items.Count > 0) lvResult.Items.Clear();
            //StartScanThread();
            _decoder.TableChangeDetected += _decoder_TableChangeDetected;
            var programMapTables = new List<ProgramMapTable>();
            _isScanning = true;
            _stopScanning = false;
            SetControlPropertyThreadSafe(btnScan, "Text", "Stop Search");
            programMapTables.Clear();
            patfound = false;
            pmtsfound = false;
            nitfound = false;
            sdtfound = false;
            SetControlPropertyThreadSafe(lblPat, "BackColor", Color.LightGreen);
            SetControlPropertyThreadSafe(lblPmt, "BackColor", Color.LightGreen);
            SetControlPropertyThreadSafe(lblSdt, "BackColor", Color.LightGreen);
            SetControlPropertyThreadSafe(lblNit, "BackColor", Color.LightGreen);
            if (_stopScanning) return;

            RtspStatusCode statuscode;
            if (string.IsNullOrEmpty(_device.RtspSession.SessionID))
            {
                _device.RtspSession.Options();
                _device.RtspSession.Setup(_currenttunestring, TransmissionMode.Unicast);
                _device.RtspSession.Play(_currenttunestring);
                _udpclient = new UdpClient(_device.RtspSession.RtpPort);
                _remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                _device.RtspSession.RecieptionInfoChanged += RtspSession_RecieptionInfoChanged;
            }
            else
            {
                _device.RtspSession.Play(_currenttunestring);
            }

            /* Say the Sat>IP server we want Receives the Recieption Details SDP */
            statuscode = _device.RtspSession.Describe();

            if (_locked)
            {
                while (!patfound || !pmtsfound || !sdtfound || !nitfound)
                {
                    var receivedbytes = _udpclient.Receive(ref _remoteEndPoint);
                    var rtp = RtpPacket.Decode(receivedbytes);
                    if (rtp.HasPayload)
                    {
                        var packets = packetFactory.GetTsPacketsFromData(rtp.Payload);

                        Logger.Write(rtp.Payload.ToHexString());
                        _decoder.AddData(receivedbytes);
                    }

                    //}
                    lock (_decoder)
                    {
                        programMapTables = _decoder?.ProgramMapTables.OrderBy(p => p.ProgramNumber).ToList();
                        foreach (var programMapTable in programMapTables)
                        {
                            short videoPid = -1;
                            short audioPid = -1;
                            short ac3pid = -1;
                            short ttxpid = -1;
                            short subpid = -1;
                            var desc = _decoder.GetServiceDescriptorForProgramNumber(programMapTable?.ProgramNumber);
                            if (desc != null)
                            {
                                if (programMapTable?.EsStreams != null)
                                    foreach (var stream in programMapTable.EsStreams)
                                        if (stream != null)
                                        {
                                            if (stream.Descriptors.OfType<Ac3Descriptor>().Any())
                                                ac3pid = stream.ElementaryPid;
                                            if (stream.Descriptors.OfType<Eac3Descriptor>().Any())
                                                ac3pid = stream.ElementaryPid;
                                            if (stream.Descriptors.OfType<SubtitlingDescriptor>().Any())
                                                subpid = stream.ElementaryPid;
                                            if (stream.Descriptors.OfType<TeletextDescriptor>().Any())
                                                ttxpid = stream.ElementaryPid;
                                            switch (stream.StreamType)
                                            {
                                                case 0x01: // ISO/IEC 11172-2 (MPEG-1 video) in a packetized stream
                                                case 0x02
                                                    : // ITU-T Rec. H.262 and ISO/IEC 13818-2 (MPEG-2 higher rate interlaced video) in a packetized stream
                                                case 0x1B
                                                    : // ITU-T Rec. H.264 and ISO/IEC 14496-10 (lower bit-rate video) in a packetized stream                                                        
                                                case 0x24
                                                    : // ITU - T Rec.H.265 and ISO/ IEC 23008 - 2(Ultra HD video) in a packetized stream
                                                {
                                                    videoPid = stream.ElementaryPid;
                                                    break;
                                                }
                                                case 0x03: // ISO/IEC 11172-3 (MPEG-1 audio) in a packetized stream
                                                case 0x04
                                                    : // ISO/IEC 13818-3 (MPEG-2 halved sample rate audio) in a packetized stream

                                                {
                                                    audioPid = stream.ElementaryPid;
                                                    break;
                                                }
                                                default:
                                                {
                                                    Console.Write(stream.StreamType.ToString());
                                                    break;
                                                }
                                            }
                                        }

                                var chan = new SatChannel
                                {
                                    ServiceType = desc.ServiceType,
                                    ServiceName = desc.ServiceName.Value,
                                    ServiceProvider = desc.ServiceProviderName.Value,
                                    ServiceId = programMapTable.ProgramNumber,
                                    ProgramClockReferenceId = programMapTable.PcrPid,
                                    AudioPid = audioPid,
                                    VideoPid = videoPid,
                                    AC3Pid = ac3pid,
                                    TTXPid = ttxpid,
                                    SubTitlePid = subpid
                                };
                                AddResults(chan);
                            }
                        }
                    }
                }

                //Thread.Sleep(55000);
                _device.RtspSession.TearDown();
                SetControlPropertyThreadSafe(pgbSearchResult, "Value", 100);
                _isScanning = false;
                SetControlPropertyThreadSafe(btnScan, "Text", "Start Search");
            }
        }
    }

    public class Transponder
    {
        public Transponder(decimal frequency, int symbolrate, string fec, string polarisation, string modulationSystem,
            string modulationType)
        {
            Frequency = frequency;
            Symbolrate = symbolrate;
            Fec = fec.ToLower();
            Polarisation = polarisation.ToLower();
            ModulationSystem = modulationSystem.ToLower();
            ModulationType = modulationType.ToLower();
            if (modulationSystem == "DVB-S")
            {
                ModulationSystem = "dvbs";
                Pilot = "off";
                Rolloff = 0.35;
            }
            else
            {
                ModulationSystem = "dvbs2";
                Pilot = "on";
                Rolloff = 0.35;
            }
        }

        public decimal Frequency { get; set; }

        public int Symbolrate { get; set; }

        public string Fec { get; set; }

        public string Polarisation { get; set; }

        public string ModulationSystem { get; set; }

        public string ModulationType { get; set; }

        public string Pilot { get; set; }

        public double Rolloff { get; set; }

        public override string ToString()
        {
            return string.Format(" {0} - {1} - {2}", Frequency, Symbolrate, Polarisation);
        }
    }
}