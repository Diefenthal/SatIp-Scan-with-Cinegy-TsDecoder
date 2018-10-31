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

using System.Text;

namespace SatIp
{
    internal class RtcpAppPacket : RtcpPacket
    {
        /// <summary>
        ///     Get the synchronization source.
        /// </summary>
        public int SynchronizationSource { get; private set; }

        /// <summary>
        ///     Get the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Get the identity.
        /// </summary>
        public int Identity { get; private set; }

        /// <summary>
        ///     Get the variable data portion.
        /// </summary>
        public string Data { get; private set; }

        public override void Parse(byte[] buffer, int offset)
        {
            base.Parse(buffer, offset);
            SynchronizationSource = Utils.Convert4BytesToInt(buffer, offset + 4);
            Name = Utils.ConvertBytesToString(buffer, offset + 8, 4);
            Identity = Utils.Convert2BytesToInt(buffer, offset + 12);

            var dataLength = Utils.Convert2BytesToInt(buffer, offset + 14);
            if (dataLength != 0)
                Data = Utils.ConvertBytesToString(buffer, offset + 16, dataLength);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Application Specific.\n");
            sb.AppendFormat("Version : {0} .\n", Version);
            sb.AppendFormat("Padding : {0} .\n", Padding);
            sb.AppendFormat("Report Count : {0} .\n", ReportCount);
            sb.AppendFormat("PacketType: {0} .\n", Type);
            sb.AppendFormat("Length : {0} .\n", Length);
            sb.AppendFormat("SynchronizationSource : {0} .\n", SynchronizationSource);
            sb.AppendFormat("Name : {0} .\n", Name);
            sb.AppendFormat("Identity : {0} .\n", Identity);
            sb.AppendFormat("Data : {0} .\n", Data);
            sb.AppendFormat(".\n");
            return sb.ToString();
        }
    }
}