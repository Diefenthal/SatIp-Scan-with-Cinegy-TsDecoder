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
    public class RtcpByePacket : RtcpPacket
    {
        public Collection<string> SynchronizationSources { get; private set; }
        public string ReasonForLeaving { get; private set; }

        public override void Parse(byte[] buffer, int offset)
        {
            base.Parse(buffer, offset);
            SynchronizationSources = new Collection<string>();
            var index = 4;

            while (SynchronizationSources.Count < ReportCount)
            {
                SynchronizationSources.Add(Utils.ConvertBytesToString(buffer, offset + index, 4));
                index += 4;
            }

            if (index < Length)
            {
                int reasonLength = buffer[offset + index];
                ReasonForLeaving = Utils.ConvertBytesToString(buffer, offset + index + 1, reasonLength);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("ByeBye .\n");
            sb.AppendFormat("Version : {0} .\n", Version);
            sb.AppendFormat("Padding : {0} .\n", Padding);
            sb.AppendFormat("Report Count : {0} .\n", ReportCount);
            sb.AppendFormat("PacketType: {0} .\n", Type);
            sb.AppendFormat("Length : {0} .\n", Length);
            sb.AppendFormat("SynchronizationSources : {0} .\n", SynchronizationSources);
            sb.AppendFormat("ReasonForLeaving : {0} .\n", ReasonForLeaving);
            sb.AppendFormat(".\n");
            return sb.ToString();
        }
    }
}