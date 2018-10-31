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
    public class RtcpReceiverReportPacket : RtcpPacket
    {
        public string SynchronizationSource { get; private set; }
        public Collection<ReportBlock> ReportBlocks { get; private set; }
        public byte[] ProfileExtension { get; private set; }

        public override void Parse(byte[] buffer, int offset)
        {
            base.Parse(buffer, offset);
            SynchronizationSource = Utils.ConvertBytesToString(buffer, offset + 4, 4);

            ReportBlocks = new Collection<ReportBlock>();
            var index = 8;

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
            sb.AppendFormat("Receiver Report.\n");
            sb.AppendFormat("Version : {0} .\n", Version);
            sb.AppendFormat("Padding : {0} .\n", Padding);
            sb.AppendFormat("Report Count : {0} .\n", ReportCount);
            sb.AppendFormat("PacketType: {0} .\n", Type);
            sb.AppendFormat("Length : {0} .\n", Length);
            sb.AppendFormat("SynchronizationSource : {0} .\n", SynchronizationSource);
            sb.AppendFormat(".\n");
            return sb.ToString();
        }
    }
}