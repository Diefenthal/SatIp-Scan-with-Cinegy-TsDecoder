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
    internal class RtcpSourceDescriptionPacket : RtcpPacket
    {
        /// <summary>
        ///     Get the list of source descriptions.
        /// </summary>
        public Collection<SourceDescriptionBlock> Descriptions;

        public override void Parse(byte[] buffer, int offset)
        {
            base.Parse(buffer, offset);
            Descriptions = new Collection<SourceDescriptionBlock>();

            var index = 4;

            while (Descriptions.Count < ReportCount)
            {
                var descriptionBlock = new SourceDescriptionBlock();
                descriptionBlock.Process(buffer, offset + index);
                Descriptions.Add(descriptionBlock);
                index += descriptionBlock.BlockLength;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Source Description.\n");
            sb.AppendFormat("Version : {0} .\n", Version);
            sb.AppendFormat("Padding : {0} .\n", Padding);
            sb.AppendFormat("Report Count : {0} .\n", ReportCount);
            sb.AppendFormat("PacketType: {0} .\n", Type);
            sb.AppendFormat("Length : {0} .\n", Length);
            sb.AppendFormat("Descriptions : {0} .\n", Descriptions);

            sb.AppendFormat(".\n");
            return sb.ToString();
        }
    }
}