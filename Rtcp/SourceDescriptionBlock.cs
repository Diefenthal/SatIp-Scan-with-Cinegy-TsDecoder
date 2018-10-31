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

namespace SatIp
{
    internal class SourceDescriptionBlock
    {
        private int blockLength;

        /// <summary>
        ///     Get the list of source descriptioni items.
        /// </summary>
        public Collection<SourceDescriptionItem> Items;

        /// <summary>
        ///     Get the length of the block.
        /// </summary>
        public int BlockLength => blockLength + blockLength % 4;

        /// <summary>
        ///     Get the synchronization source.
        /// </summary>
        public string SynchronizationSource { get; private set; }

        public void Process(byte[] buffer, int offset)
        {
            SynchronizationSource = Utils.ConvertBytesToString(buffer, offset, 4);
            Items = new Collection<SourceDescriptionItem>();
            var index = 4;
            var done = false;
            do
            {
                var item = new SourceDescriptionItem();
                item.Process(buffer, offset + index);

                if (item.Type != 0)
                {
                    Items.Add(item);
                    index += item.ItemLength;
                    blockLength += item.ItemLength;
                }
                else
                {
                    blockLength++;
                    done = true;
                }
            } while (!done);
        }
    }
}