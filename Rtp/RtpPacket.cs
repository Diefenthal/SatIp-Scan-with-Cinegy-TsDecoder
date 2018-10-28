/*  
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
using System.Collections.ObjectModel;
using System.Text;

namespace SatIp
{
    public class RtpPacket
    {
        private static readonly int MinHeaderLength = 12;
        public int ExtensionHeaderId;
        public int ExtensionHeaderLength;
        public int HeaderSize = MinHeaderLength;

        public int Version { get; set; }
        public bool Padding { get; set; }
        public bool Extension { get; set; }
        public int ContributingSourceCount { get; set; }
        public bool Marker { get; set; }
        public int PayloadType { get; set; }
        public int SequenceNumber { get; set; }
        public long TimeStamp { get; set; }
        public long SynchronizationSource { get; set; }
        public Collection<string> ContributingSources { get; private set; }
        public bool HasPayload { get; set; }
        public byte[] Payload { get; set; }

        public static RtpPacket Decode(byte[] buffer)
        {
            var packet = new RtpPacket
            {
                Version = buffer[0] >> 6,
                Padding = (buffer[0] & 0x20) != 0,
                Extension = (buffer[0] & 0x10) != 0,
                ContributingSourceCount = buffer[0] & 0x0f,

                Marker = (buffer[1] & 0x80) != 0,
                PayloadType = buffer[1] & 0x7f,

                SequenceNumber = Utils.Convert2BytesToInt(buffer, 2),
                TimeStamp = Utils.Convert4BytesToLong(buffer, 4),
                SynchronizationSource = Utils.Convert4BytesToLong(buffer, 8)
            };
            var index = 12;

            if (packet.ContributingSourceCount != 0)
            {
                packet.ContributingSources = new Collection<string>();

                while (packet.ContributingSources.Count < packet.ContributingSourceCount)
                {
                    packet.ContributingSources.Add(Utils.ConvertBytesToString(buffer, index, 4));
                    index += 4;
                }
            }

            var dataoffset = 0;
            if (!packet.Extension)
            {
                dataoffset = index;
            }
            else
            {
                packet.ExtensionHeaderId = Utils.Convert2BytesToInt(buffer, index);
                packet.ExtensionHeaderLength = Utils.Convert2BytesToInt(buffer, index + 2);
                dataoffset = index + packet.ExtensionHeaderLength + 4;
            }

            var dataLength = buffer.Length - dataoffset;
            if (dataLength > dataoffset)
            {
                packet.HasPayload = true;
                packet.Payload = new byte[dataLength];
                Array.Copy(buffer, dataoffset, packet.Payload, 0, dataLength);
            }
            else
            {
                packet.HasPayload = false;
            }

            return packet;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("RTP Packet");
            sb.AppendFormat("Version: {0} \n", Version);
            sb.AppendFormat("Padding: {0} \n", Padding);
            sb.AppendFormat("Extension: {0} \n", Extension);
            sb.AppendFormat("Contributing Source Identifiers Count: {0} \n", ContributingSourceCount);
            sb.AppendFormat("Marker: {0} \n", Marker);
            sb.AppendFormat("Payload Type: {0} \n", PayloadType);
            sb.AppendFormat("Sequence Number: {0} \n", SequenceNumber);
            sb.AppendFormat("Timestamp: {0} .\n", TimeStamp);
            sb.AppendFormat("Synchronization Source Identifier: {0} \n", SynchronizationSource);
            sb.AppendFormat("\n");
            return sb.ToString();
        }
    }
}