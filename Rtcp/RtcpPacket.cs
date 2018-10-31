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

namespace SatIp
{
    public abstract class RtcpPacket
    {
        public int Version { get; private set; }
        public bool Padding { get; private set; }
        public int ReportCount { get; private set; }
        public int Type { get; private set; }
        public int Length { get; private set; }

        public virtual void Parse(byte[] buffer, int offset)
        {
            Version = buffer[offset] >> 6;
            Padding = (buffer[offset] & 0x20) != 0;
            ReportCount = buffer[offset] & 0x1f;
            Type = buffer[offset + 1];
            Length = Utils.Convert2BytesToInt(buffer, offset + 2) * 4 + 4;
        }
    }
}