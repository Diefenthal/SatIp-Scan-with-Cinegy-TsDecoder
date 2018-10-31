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
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SatIp
{
    public class RtcpListener : IDisposable
    {
        public delegate void PacketReceivedHandler(object sender, RtcpPacketReceivedArgs e);

        private bool _disposed;
        private readonly IPEndPoint _multicastEndPoint;
        private Thread _rtcpListenerThread;
        private AutoResetEvent _rtcpListenerThreadStopEvent;
        private IPEndPoint _serverEndPoint;
        private readonly TransmissionMode _transmissionMode;
        private readonly UdpClient _udpClient;

        public RtcpListener(string address, int port, TransmissionMode mode)
        {
            _transmissionMode = mode;
            switch (mode)
            {
                case TransmissionMode.Unicast:
                    _udpClient = new UdpClient(new IPEndPoint(IPAddress.Parse(address), port));
                    _serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    break;
                case TransmissionMode.Multicast:
                    _multicastEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
                    _serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    _udpClient = new UdpClient();
                    _udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                    _udpClient.ExclusiveAddressUse = false;
                    _udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));
                    _udpClient.JoinMulticastGroup(_multicastEndPoint.Address);
                    break;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RtcpListener()
        {
            Dispose(false);
        }

        public void StartRtcpListenerThread()
        {
            if (_rtcpListenerThread != null && !_rtcpListenerThread.IsAlive) StopRtcpListenerThread();

            if (_rtcpListenerThread == null)
            {
                Logger.Info("SAT>IP : starting new RTCP listener thread");
                _rtcpListenerThreadStopEvent = new AutoResetEvent(false);
                _rtcpListenerThread = new Thread(RtcpListenerThread);
                _rtcpListenerThread.Name = "SAT>IP tuner  RTCP listener";
                _rtcpListenerThread.IsBackground = true;
                _rtcpListenerThread.Priority = ThreadPriority.Lowest;
                _rtcpListenerThread.Start();
            }
        }

        public void StopRtcpListenerThread()
        {
            if (_rtcpListenerThread != null)
            {
                if (!_rtcpListenerThread.IsAlive)
                {
                    Logger.Warn("SAT>IP : aborting old RTCP listener thread");
                    _rtcpListenerThread.Abort();
                }
                else
                {
                    _rtcpListenerThreadStopEvent.Set();
                    if (!_rtcpListenerThread.Join(400 * 2))
                    {
                        Logger.Warn("SAT>IP : failed to join RTCP listener thread, aborting thread");
                        _rtcpListenerThread.Abort();
                    }
                }

                _rtcpListenerThread = null;
                if (_rtcpListenerThreadStopEvent != null)
                {
                    _rtcpListenerThreadStopEvent.Close();
                    _rtcpListenerThreadStopEvent = null;
                }
            }
        }

        private void RtcpListenerThread()
        {
            try
            {
                var receivedGoodBye = false;
                try
                {
                    //_udpClient.Client.ReceiveTimeout = 400;
                    var serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    while (!receivedGoodBye && !_rtcpListenerThreadStopEvent.WaitOne(1))
                    {
                        var packets = _udpClient.Receive(ref serverEndPoint);
                        if (packets == null) continue;

                        var offset = 0;
                        while (offset < packets.Length)
                            switch (packets[offset + 1])
                            {
                                case 200: //sr
                                    var sr = new RtcpSenderReportPacket();
                                    sr.Parse(packets, offset);
                                    offset += sr.Length;
                                    break;
                                case 201: //rr
                                    var rr = new RtcpReceiverReportPacket();
                                    rr.Parse(packets, offset);
                                    offset += rr.Length;
                                    break;
                                case 202: //sd
                                    var sd = new RtcpSourceDescriptionPacket();
                                    sd.Parse(packets, offset);
                                    offset += sd.Length;
                                    break;
                                case 203: // bye
                                    var bye = new RtcpByePacket();
                                    bye.Parse(packets, offset);
                                    receivedGoodBye = true;
                                    OnPacketReceived(new RtcpPacketReceivedArgs(bye));
                                    offset += bye.Length;
                                    break;
                                case 204: // app
                                    var app = new RtcpAppPacket();
                                    app.Parse(packets, offset);
                                    OnPacketReceived(new RtcpPacketReceivedArgs(app));
                                    offset += app.Length;
                                    break;
                            }
                    }
                }
                finally
                {
                    switch (_transmissionMode)
                    {
                        case TransmissionMode.Multicast:
                            _udpClient.DropMulticastGroup(_multicastEndPoint.Address);
                            _udpClient.Close();
                            break;
                        case TransmissionMode.Unicast:
                            _udpClient.Close();
                            break;
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                Logger.Error("SAT>IP : RTCP listener thread exception", ex);
                return;
            }

            Logger.Warn("SAT>IP : RTCP listener thread stopping");
        }

        public event PacketReceivedHandler PacketReceived;

        protected void OnPacketReceived(RtcpPacketReceivedArgs args)
        {
            if (PacketReceived != null) PacketReceived(this, args);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    StopRtcpListenerThread();
            _disposed = true;
        }

        public class RtcpPacketReceivedArgs : EventArgs
        {
            public RtcpPacketReceivedArgs(object packet)
            {
                Packet = packet;
            }

            public object Packet { get; }
        }
    }
}