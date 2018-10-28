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

namespace SatIp
{
    delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);
    delegate void AddResultDelegate(Channel chan);
    public partial class Satellite : UserControl
    {
        private int _count;
        private readonly TsDecoder _decoder = new TsDecoder();
        private readonly SatIpDevice _device;
        private bool _isScanning;
        private bool _locked;
        private readonly List<IniMappingContext> _mappings = new List<IniMappingContext>();
        private IPEndPoint _remoteEndPoint;
        private Thread _scanThread;
        private AutoResetEvent _scanThreadStopEvent;
        private bool _stopScanning;
        private UdpClient _udpclient;
        private bool nitfound;
        private bool patfound;
        private bool pmtsfound;
        private bool sdtfound;

        public Satellite(SatIpDevice device)
        {
            InitializeComponent();
            _device = device;
        }

        private void Satellite_Load(object sender, EventArgs e)
        {
            if (_device != null && _device.SupportsDVBS)
            {
                #region DVBSSources

                cbxDiseqC.Items.Add("None(Single Lnb)");
                cbxDiseqC.Items.Add("22 KHz (Tone Switch)");
                cbxDiseqC.Items.Add("Diseq c 1.x (A/B/C/D");
                cbxDiseqC.SelectedIndex = 0;

                var app = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var tuningdata = app + "\\TuningData\\Satellite";
                cbxSourceA.Items.Add("- None -");
                cbxSourceA.SelectedIndex = 0;
                cbxSourceB.Items.Add("- None -");
                cbxSourceB.SelectedIndex = 0;
                cbxSourceC.Items.Add("- None -");
                cbxSourceC.SelectedIndex = 0;
                cbxSourceD.Items.Add("- None -");
                cbxSourceD.SelectedIndex = 0;
                foreach (var str2 in Directory.GetFiles(tuningdata))
                {
                    var reader = new IniReader(str2);
                    var str3 = reader.ReadString("SATTYPE", "1");
                    var str4 = reader.ReadString("SATTYPE", "2");
                    if (!cbxSourceA.Items.Contains(str4) && str4 != "")
                    {
                        cbxSourceA.Items.Add(new IniMapping(str3 + " " + str4, str2));
                        cbxSourceB.Items.Add(new IniMapping(str3 + " " + str4, str2));
                        cbxSourceC.Items.Add(new IniMapping(str3 + " " + str4, str2));
                        cbxSourceD.Items.Add(new IniMapping(str3 + " " + str4, str2));
                    }
                }

                UpdateSatelliteSettings();

                #endregion
            }
        }

        private void UpdateSatelliteSettings()
        {
            SuspendLayout();
            if (cbxDiseqC.SelectedIndex == 0)
            {
                lblSourceB.Visible = false;
                cbxSourceB.Visible = false;
            }
            else
            {
                lblSourceB.Visible = true;
                cbxSourceB.Visible = true;
            }

            if (cbxDiseqC.SelectedIndex == 0 || cbxDiseqC.SelectedIndex == 1)
            {
                lblSourceC.Visible = false;
                cbxSourceC.Visible = false;
                lblSourceD.Visible = false;
                cbxSourceD.Visible = false;
            }
            else
            {
                lblSourceC.Visible = true;
                cbxSourceC.Visible = true;
                lblSourceD.Visible = true;
                cbxSourceD.Visible = true;
            }

            ResumeLayout();
        }

        private void CbxDiseqC_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSatelliteSettings();
        }

        private void BtnScan_Click(object sender, EventArgs e)
        {
            if (cbxDiseqC.SelectedIndex == 0)
                if (cbxSourceA.SelectedItem.ToString() != "- None -")
                    _mappings.Add(new IniMappingContext("1", (IniMapping) cbxSourceA.SelectedItem));
            if (cbxDiseqC.SelectedIndex == 1)
            {
                if (cbxSourceA.SelectedItem.ToString() != "- None -")
                    _mappings.Add(new IniMappingContext("1", (IniMapping) cbxSourceA.SelectedItem));
                if (cbxSourceB.SelectedItem.ToString() != "- None -")
                    _mappings.Add(new IniMappingContext("2", (IniMapping) cbxSourceB.SelectedItem));
            }
            else
            {
                if (cbxSourceA.SelectedItem.ToString() != "- None -")
                    _mappings.Add(new IniMappingContext("1", (IniMapping) cbxSourceA.SelectedItem));
                if (cbxSourceB.SelectedItem.ToString() != "- None -")
                    _mappings.Add(new IniMappingContext("2", (IniMapping) cbxSourceB.SelectedItem));
                if (cbxSourceC.SelectedItem.ToString() != "- None -")
                    _mappings.Add(new IniMappingContext("3", (IniMapping) cbxSourceC.SelectedItem));
                if (cbxSourceD.SelectedItem.ToString() != "- None -")
                    _mappings.Add(new IniMappingContext("4", (IniMapping) cbxSourceD.SelectedItem));
            }

            if (_mappings.Count > 0)
            {
                if (_isScanning == false)
                    StartScanThread();
                else
                    _stopScanning = true;
            }
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
            foreach (var mapping in _mappings)
            {
                var reader = new IniReader(mapping.Mapping.File);
                var Count = reader.ReadInteger("DVB", "0", 0);
                try
                {
                    var Index = 1;
                    var source = mapping.Source;
                    string tuning;
                    while (Index <= Count)
                    {
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
                        var percent = (float) Index / Count;
                        percent *= 100f;
                        if (percent > 100f) percent = 100f;
                        SetControlPropertyThreadSafe(pgbSearchResult, "Value", (int) percent);
                        var strArray = reader.ReadString("DVB", Index.ToString()).Split(',');

                        if (strArray[4] == "S2")
                            tuning = string.Format(
                                "src={0}&freq={1}&pol={2}&sr={3}&fec={4}&msys=dvbs2&mtype={5}&plts=on&ro=0.35&pids=0",
                                source, strArray[0], strArray[1].ToLower(), strArray[2].ToLower(), strArray[3],
                                strArray[5].ToLower());
                        else
                            tuning = string.Format("src={0}&freq={1}&pol={2}&sr={3}&fec={4}&msys=dvbs&mtype={5}&pids=0",
                                source, strArray[0], strArray[1].ToLower(), strArray[2], strArray[3],
                                strArray[5].ToLower());

                        RtspStatusCode statuscode;
                        if (string.IsNullOrEmpty(_device.RtspSession.SessionID))
                        {
                            _device.RtspSession.Options();
                            _device.RtspSession.Setup(tuning, TransmissionMode.Unicast);
                            _device.RtspSession.Play(string.Empty);
                            _udpclient = new UdpClient(_device.RtspSession.RtpPort);
                            _remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                            _device.RtspSession.RecieptionInfoChanged += RtspSession_RecieptionInfoChanged;
                            _device.RtspSession.Play(tuning);
                        }
                        else
                        {
                            _device.RtspSession.Play(tuning);
                        }

                        /* Say the Sat>IP server we want Receives the Recieption Details SDP */
                        statuscode = _device.RtspSession.Describe();

                        if (_locked)
                        {
                            while (!patfound || !pmtsfound || !sdtfound)
                            {
                                var receivedbytes = _udpclient.Receive(ref _remoteEndPoint);
                                var rtp = RtpPacket.Decode(receivedbytes);

                                if (rtp.HasPayload)
                                {
                                    Logger.Write(rtp.Payload.ToHexString());
                                    _decoder.AddData(receivedbytes);
                                }

                                //else
                                //{
                                //    for (var i = 0; i < 5; i++)
                                //    {
                                //        if (i >= 5)
                                //        { Index++; }
                                //    }
                                //}
                            }

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
                                    short aacpid = -1;
                                    short dtspid = -1;
                                    short eac3pid = -1;
                                    var desc = _decoder.GetServiceDescriptorForProgramNumber(programMapTable
                                        ?.ProgramNumber);
                                    if (desc != null)
                                    {
                                        if (programMapTable?.EsStreams != null)
                                            foreach (var stream in programMapTable.EsStreams)
                                                if (stream != null)
                                                {
                                                    if (stream.Descriptors.OfType<Ac3Descriptor>().Any())
                                                        ac3pid = stream.ElementaryPid;
                                                    if (stream.Descriptors.OfType<Eac3Descriptor>().Any())
                                                        eac3pid = stream.ElementaryPid;
                                                    if (stream.Descriptors.OfType<AACDescriptor>().Any())
                                                        aacpid = stream.ElementaryPid;
                                                    if (stream.Descriptors.OfType<DTSDescriptor>().Any())
                                                        dtspid = stream.ElementaryPid;
                                                    if (stream.Descriptors.OfType<SubtitlingDescriptor>().Any())
                                                        subpid = stream.ElementaryPid;
                                                    if (stream.Descriptors.OfType<TeletextDescriptor>().Any())
                                                        ttxpid = stream.ElementaryPid;
                                                    switch (stream.StreamType)
                                                    {
                                                        case 0x01
                                                            : // ISO/IEC 11172-2 (MPEG-1 video) in a packetized stream
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
                                                        case 0x03
                                                            : // ISO/IEC 11172-3 (MPEG-1 audio) in a packetized stream
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

                                        var chan = new Channel
                                        {
                                            ServiceType = desc.ServiceType,
                                            ServiceName = desc.ServiceName.Value,
                                            ServiceProvider = desc.ServiceProviderName.Value,
                                            ServiceId = programMapTable.ProgramNumber,
                                            ProgramClockReferenceId = programMapTable.PcrPid,
                                            AudioPid = audioPid,
                                            VideoPid = videoPid,
                                            AC3Pid = ac3pid,
                                            EAC3Pid = eac3pid,
                                            AACPid = aacpid,
                                            DTSPid = dtspid,
                                            TTXPid = ttxpid,
                                            SubTitlePid = subpid
                                        };
                                        AddResults(chan);
                                    }
                                }
                            }
                        }

                        Thread.Sleep(5000);
                        Index++;
                    }
                }
                catch
                {
                }
            }

            _device.RtspSession.TearDown();
            SetControlPropertyThreadSafe(pgbSearchResult, "Value", 100);
            _isScanning = false;
            SetControlPropertyThreadSafe(btnScan, "Text", "Start Search");
            StopScanThread();
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
            }
            else if (args.TableType == TableType.Nit)
            {
                nitfound = true;
                SetControlPropertyThreadSafe(lblNit, "BackColor", Color.Green);
                _device.RtspSession.Play(string.Format("&delpids={0}", args.TablePid));
            }
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

        private void AddResults(Channel chan)
        {
            _count++;

            if (lwResults.InvokeRequired)
            {
                lwResults.Invoke(new AddResultDelegate(AddResults), chan);
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
                lwResults.Items.Add(lstItem);

                label1.Text = string.Format("Services Found; {0}", _count.ToString());
            }
        }
    }
}