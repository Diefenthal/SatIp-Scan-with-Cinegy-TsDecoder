﻿/*  
    Copyright (C) <2007-2017>  <Kay Diefenthal>

    SatIp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SatIp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with SatIp.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SatIp
{
    public class RtpListener : IDisposable
    {
        public delegate void PacketReceivedHandler(object sender, RtpPacketReceivedArgs e);

        private bool _disposed;
        private readonly IPEndPoint _multicastEndPoint;
        private Thread _rtpListenerThread;
        private AutoResetEvent _rtpListenerThreadStopEvent;
        private IPEndPoint _serverEndPoint;
        private readonly TransmissionMode _transmissionMode;
        private readonly UdpClient _udpClient;

        public RtpListener(string address, int port, TransmissionMode mode)
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
                    _serverEndPoint = null;
                    _udpClient = new UdpClient();
                    _udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                    _udpClient.ExclusiveAddressUse = false;
                    _udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, _multicastEndPoint.Port));
                    _udpClient.JoinMulticastGroup(_multicastEndPoint.Address);
                    break;
            }

            //StartRtpListenerThread();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RtpListener()
        {
            Dispose(false);
        }

        public void StartRtpListenerThread()
        {
            // Kill the existing thread if it is in "zombie" state.
            if (_rtpListenerThread != null && !_rtpListenerThread.IsAlive) StopRtpListenerThread();

            if (_rtpListenerThread == null)
            {
                Logger.Info("SAT>IP : starting new RTP listener thread");
                _rtpListenerThreadStopEvent = new AutoResetEvent(false);
                _rtpListenerThread = new Thread(RtpListenerThread);
                _rtpListenerThread.Name = "SAT>IP tuner  RTP listener";
                _rtpListenerThread.IsBackground = true;
                _rtpListenerThread.Priority = ThreadPriority.Lowest;
                _rtpListenerThread.Start();
            }
        }

        public void StopRtpListenerThread()
        {
            if (_rtpListenerThread != null)
            {
                if (!_rtpListenerThread.IsAlive)
                {
                    Logger.Warn("SAT>IP : aborting old RTP listener thread");
                    _rtpListenerThread.Abort();
                }
                else
                {
                    _rtpListenerThreadStopEvent.Set();
                    if (!_rtpListenerThread.Join(400 * 2))
                    {
                        Logger.Warn("SAT>IP : failed to join RTP listener thread, aborting thread");
                        _rtpListenerThread.Abort();
                    }
                }

                _rtpListenerThread = null;
                if (_rtpListenerThreadStopEvent != null)
                {
                    _rtpListenerThreadStopEvent.Close();
                    _rtpListenerThreadStopEvent = null;
                }
            }
        }

        private void RtpListenerThread()
        {
            try
            {
                try
                {
                    while (!_rtpListenerThreadStopEvent.WaitOne(1))
                    {
                        var receivedbytes = _udpClient.Receive(ref _serverEndPoint);
                        var packet = RtpPacket.Decode(receivedbytes);
                        OnPacketReceived(new RtpPacketReceivedArgs(packet));
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
                Logger.Error("SAT>IP : RTP listener thread exception", ex);
                return;
            }

            Logger.Warn("SAT>IP : RTP listener thread stopping");
        }

        public event PacketReceivedHandler PacketReceived;

        protected void OnPacketReceived(RtpPacketReceivedArgs args)
        {
            if (PacketReceived != null) PacketReceived(this, args);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    StopRtpListenerThread();
            _disposed = true;
        }

        public class RtpPacketReceivedArgs : EventArgs
        {
            public RtpPacketReceivedArgs(RtpPacket packet)
            {
                Packet = packet;
            }

            public RtpPacket Packet { get; }
        }
    }
}