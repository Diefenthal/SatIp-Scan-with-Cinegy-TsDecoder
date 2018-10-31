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

using System.Collections.ObjectModel;
using System.Text;

namespace SatIp
{
    public class RtcpSenderReportPacket : RtcpPacket
    {
        public override void Parse(byte[] buffer, int offset)
        {
            base.Parse(buffer, offset);
            SynchronizationSource = Utils.Convert4BytesToInt(buffer, offset + 4);
            NPTTimeStamp = Utils.Convert8BytesToLong(buffer, offset + 8);
            RTPTimeStamp = Utils.Convert4BytesToInt(buffer, offset + 16);
            SenderPacketCount = Utils.Convert4BytesToInt(buffer, offset + 20);
            SenderOctetCount = Utils.Convert4BytesToInt(buffer, offset + 24);

            ReportBlocks = new Collection<ReportBlock>();
            var index = 28;

            while (ReportBlocks.Count < ReportCount)
            {
                var reportBlock = new ReportBlock();
                reportBlock.Process(buffer, offset + index);
                ReportBlocks.Add(reportBlock);
                index += reportBlock.BlockLength;
            }

            if (index < Length)
            {
                ProfileExtension = new byte[Length - index];

                for (var extensionIndex = 0; index < Length; index++)
                {
                    ProfileExtension[extensionIndex] = buffer[offset + index];
                    extensionIndex++;
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Sender Report.\n");
            sb.AppendFormat("Version : {0} .\n", Version);
            sb.AppendFormat("Padding : {0} .\n", Padding);
            sb.AppendFormat("Report Count : {0} .\n", ReportCount);
            sb.AppendFormat("PacketType: {0} .\n", Type);
            sb.AppendFormat("Length : {0} .\n", Length);
            sb.AppendFormat("SynchronizationSource : {0} .\n", SynchronizationSource);
            sb.AppendFormat("NTP Timestamp : {0} .\n", Utils.NptTimestampToDateTime(NPTTimeStamp));
            sb.AppendFormat("RTP Timestamp : {0} .\n", RTPTimeStamp);
            sb.AppendFormat("Sender PacketCount : {0} .\n", SenderPacketCount);
            sb.AppendFormat("Sender Octet Count : {0} .\n", SenderOctetCount);
            sb.AppendFormat(".\n");
            return sb.ToString();
        }

        #region Properties

        /// <summary>
        ///     Get the synchronization source.
        /// </summary>
        public int SynchronizationSource { get; private set; }

        /// <summary>
        ///     Get the NPT timestamp.
        /// </summary>
        public long NPTTimeStamp { get; private set; }

        /// <summary>
        ///     Get the RTP timestamp.
        /// </summary>
        public int RTPTimeStamp { get; private set; }

        /// <summary>
        ///     Get the packet count.
        /// </summary>
        public int SenderPacketCount { get; private set; }

        /// <summary>
        ///     Get the octet count.
        /// </summary>
        public int SenderOctetCount { get; private set; }

        /// <summary>
        ///     Get the list of report blocks.
        /// </summary>
        public Collection<ReportBlock> ReportBlocks { get; private set; }

        /// <summary>
        ///     Get the profile extension data.
        /// </summary>
        public byte[] ProfileExtension { get; private set; }

        #endregion
    }
}