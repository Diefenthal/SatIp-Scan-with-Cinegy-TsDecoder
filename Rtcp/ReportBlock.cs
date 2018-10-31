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
    public class ReportBlock
    {
        /// <summary>
        ///     Get the length of the block.
        /// </summary>
        public int BlockLength => 24;

        /// <summary>
        ///     Get the synchronization source.
        /// </summary>
        public string SynchronizationSource { get; private set; }

        /// <summary>
        ///     Get the fraction lost.
        /// </summary>
        public int FractionLost { get; private set; }

        /// <summary>
        ///     Get the cumulative packets lost.
        /// </summary>
        public int CumulativePacketsLost { get; private set; }

        /// <summary>
        ///     Get the highest number received.
        /// </summary>
        public int HighestNumberReceived { get; private set; }

        /// <summary>
        ///     Get the inter arrival jitter.
        /// </summary>
        public int InterArrivalJitter { get; private set; }

        /// <summary>
        ///     Get the timestamp of the last report.
        /// </summary>
        public int LastReportTimeStamp { get; private set; }

        /// <summary>
        ///     Get the delay since the last report.
        /// </summary>
        public int DelaySinceLastReport { get; private set; }

        /// <summary>
        ///     Unpack the data in a packet.
        /// </summary>
        /// <param name="buffer">The buffer containing the packet.</param>
        /// <param name="offset">The offset to the first byte of the packet within the buffer.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public void Process(byte[] buffer, int offset)
        {
            SynchronizationSource = Utils.ConvertBytesToString(buffer, offset, 4);
            FractionLost = buffer[offset + 4];
            CumulativePacketsLost = Utils.Convert3BytesToInt(buffer, offset + 5);
            HighestNumberReceived = Utils.Convert4BytesToInt(buffer, offset + 8);
            InterArrivalJitter = Utils.Convert4BytesToInt(buffer, offset + 12);
            LastReportTimeStamp = Utils.Convert4BytesToInt(buffer, offset + 16);
            DelaySinceLastReport = Utils.Convert4BytesToInt(buffer, offset + 20);
        }
    }
}