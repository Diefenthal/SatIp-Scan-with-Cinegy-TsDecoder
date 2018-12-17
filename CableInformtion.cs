using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cinegy.TsDecoder.TransportStream;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Cinegy.TsDecoder.Tables;
using System.Reflection;
using System.IO;

namespace SatIp
{
    public partial class CableInformtion : UserControl
    {
        private SatIpDevice _device;
        private bool _isScanning = false;
        private bool _stopScanning = false;
        private TsDecoder _decoder = new TsDecoder();
        int _count = 0;
        private AutoResetEvent _scanThreadStopEvent = null;
        private Thread _scanThread;
        private bool _locked;
        private IPEndPoint _remoteEndPoint;
        private UdpClient _udpclient;
        private bool patfound;
        private bool pmtsfound;
        private bool nitfound;
        private bool sdtfound;
        List<IniMappingContext> _mappings = new List<IniMappingContext>();
        public CableInformtion()
        {
            InitializeComponent();
        }
        public CableInformtion(SatIpDevice device)
        {
            InitializeComponent();
            _device = device;
        }
        private void StartScanThread()
        {
            if (_scanThread != null && !_scanThread.IsAlive)
            {
                StopScanThread();
            }

            if (_scanThread == null)
            {
                _scanThreadStopEvent = new AutoResetEvent(false);
                _scanThread = new Thread(new ThreadStart(DoScan))
                {
                    Name = "SAT>IP Scan",
                    IsBackground = true,
                    Priority = ThreadPriority.Highest,
                };
                _scanThread.Start();
            }
        }
        private void StopScanThread()
        {
            if (_scanThread != null)
            {
                if (!_scanThread.IsAlive)
                {
                    _scanThread.Abort();
                }
                else
                {
                    _scanThreadStopEvent.Set();
                }
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
            List<ProgramMapTable> programMapTables = new List<ProgramMapTable>();
            _isScanning = true;
            _stopScanning = false;
            SetControlPropertyThreadSafe(btnScan, "Text", "Stop Search");
            foreach (var mapping in _mappings)
            {
                IniReader reader = new IniReader(mapping.Mapping.File);
                var Count = reader.ReadInteger("DVB", "0", 0);
                try
                {
                    var Index = 1;
                    string source = mapping.Source;
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
                        float percent = ((float)(Index)) / Count;
                        percent *= 100f;
                        if (percent > 100f) percent = 100f;
                        SetControlPropertyThreadSafe(pgbSearchResult, "Value", (int)percent);
                        string[] strArray = reader.ReadString("DVB", Index.ToString()).Split(new char[] { ',' });

                        if (strArray[4] == "C2")
                        {
                            //rtsp://192.168.128.5/?freq=793.982&bw=8&msys=dvbc2&c2tft=0&ds=0&plp=0&pids=0,16,100,308,256
                            tuning = string.Format("freq={0}&bw={1}&msys=dvbc2&c2tft={2}&ds={3}&plp={4}&pids=0", source, strArray[0].ToString(), strArray[1].ToLower().ToString(), strArray[2].ToLower().ToString(), strArray[3].ToString(), strArray[5].ToLower().ToString());
                        }
                        else
                        {
                            //rtsp://192.168.128.5/?freq=623.25&msys=dvbc&mtype=256qam&sr=6900&specinv=0&pids=0,16,50,201,301
                            tuning = string.Format("freq={0}&msys=dvbc&mtype={1}&sr={2}&specinv={3}&pids=0", source, strArray[0].ToString(), strArray[1].ToLower().ToString(), strArray[2].ToString(), strArray[3].ToString(), strArray[5].ToLower().ToString());
                        }

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
                            while ((!patfound) || (!pmtsfound) || (!sdtfound))
                            {
                                var receivedbytes = _udpclient.Receive(ref _remoteEndPoint);
                                RtpPacket rtp = RtpPacket.Decode(receivedbytes);
                                if (rtp.HasPayload)
                                {
                                    Logger.Write(Utils.ToHexString(rtp.Payload));
                                    _decoder.AddData(receivedbytes);
                                }
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
                                    var desc = _decoder.GetServiceDescriptorForProgramNumber(programMapTable?.ProgramNumber);
                                    if (desc != null)
                                    {
                                        if (programMapTable?.EsStreams != null)
                                        {
                                            foreach (var stream in programMapTable.EsStreams)
                                            {
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
                                                        case 0x01: // ISO/IEC 11172-2 (MPEG-1 video) in a packetized stream
                                                        case 0x02: // ITU-T Rec. H.262 and ISO/IEC 13818-2 (MPEG-2 higher rate interlaced video) in a packetized stream
                                                        case 0x1B: // ITU-T Rec. H.264 and ISO/IEC 14496-10 (lower bit-rate video) in a packetized stream                                                        
                                                        case 0x24: // ITU - T Rec.H.265 and ISO/ IEC 23008 - 2(Ultra HD video) in a packetized stream
                                                            {
                                                                videoPid = stream.ElementaryPid;
                                                                break;
                                                            }
                                                        case 0x03: // ISO/IEC 11172-3 (MPEG-1 audio) in a packetized stream
                                                        case 0x04: // ISO/IEC 13818-3 (MPEG-2 halved sample rate audio) in a packetized stream

                                                            {
                                                                audioPid = stream.ElementaryPid;
                                                                break;
                                                            }
                                                        default:
                                                            { Console.Write(stream.StreamType.ToString()); break; }

                                                    }
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
                foreach (short pid in _decoder.ProgramAssociationTable.Pids)
                { _device.RtspSession.Play(string.Format("&addpids={0}", pid)); }
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
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate
                (SetControlPropertyThreadSafe),
                new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(
                    propertyName,
                    BindingFlags.SetProperty,
                    null,
                    control,
                    new object[] { propertyValue });
            }
        }
        private void AddResults(Channel chan)
        {
            if (lwResults.InvokeRequired)
            {
                lwResults.Invoke(new AddResultDelegate(AddResults), new object[] { chan });
            }
            else
            {
                _count++;
                string[] items = new string[]
                {
                        chan.ServiceType.ToString(),
                    chan.ServiceName,
                    chan.ServiceProvider,
                    chan.ServiceId.ToString(),
                    chan.ProgramClockReferenceId.ToString(),
                    chan.VideoPid.ToString(),
                    chan.AudioPid.ToString(),
                    chan.AC3Pid.ToString(),
                    chan.EAC3Pid.ToString(),
                    chan.AACPid.ToString(),
                    chan.DTSPid.ToString(),
                    chan.TTXPid.ToString(),
                    chan.SubTitlePid.ToString()


                };
                ListViewItem lstItem = new ListViewItem(items)
                {
                    Checked = true,
                };
                lwResults.Items.Add(lstItem);

                label1.Text = string.Format("Services Found; {0}", _count.ToString());
            }
        }
        private void Cable_Load(object sender, EventArgs e)
        {
            if ((_device != null) && (_device.SupportsDVBC))
            {
                #region DVBCSources                
                cbxSourceA.Items.Add("- None -");
                cbxSourceA.SelectedIndex = 0;
                var app = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var tuningdata = app + "\\TuningData\\Cable";

                foreach (var str2 in Directory.GetFiles(tuningdata))
                {
                    IniReader reader = new IniReader(str2);
                    var str3 = reader.ReadString("CABTYPE", "1");
                    var str4 = reader.ReadString("CABTYPE", "2");
                    if (!cbxSourceA.Items.Contains(str4) && (str4 != ""))
                    {
                        cbxSourceA.Items.Add(new IniMapping(str3 + " " + str4, str2));

                    }
                }

                #endregion
            }
        }
        private void BtnScan_Click(object sender, EventArgs e)
        {

            if (cbxSourceA.SelectedItem.ToString() != "- None -")
            { _mappings.Add(new IniMappingContext("1", (IniMapping)cbxSourceA.SelectedItem)); }

            if (_mappings.Count > 0)
            {
                if (_isScanning == false)
                {
                    StartScanThread();
                }
                else
                {
                    _stopScanning = true;
                }
            }
        }
    }
}