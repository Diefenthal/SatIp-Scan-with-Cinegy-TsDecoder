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