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
    /// <summary>
    ///     The class that describes a source description item.
    /// </summary>
    public class SourceDescriptionItem
    {
        /// <summary>
        ///     Get the type.
        /// </summary>
        public int Type { get; private set; }

        /// <summary>
        ///     Get the text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        ///     Get the length of the item.
        /// </summary>
        public int ItemLength => Text.Length + 2;

        /// <summary>
        ///     Unpack the data in a packet.
        /// </summary>
        /// <param name="buffer">The buffer containing the packet.</param>
        /// <param name="offset">The offset to the first byte of the packet within the buffer.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public void Process(byte[] buffer, int offset)
        {
            Type = buffer[offset];
            if (Type != 0)
            {
                int length = buffer[offset + 1];
                Text = Utils.ConvertBytesToString(buffer, offset + 2, length);
            }
        }
    }
}