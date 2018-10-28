﻿/* Copyright 2017 Cinegy GmbH.

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

using System.Collections.Generic;
using Cinegy.TsDecoder.TransportStream;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Cinegy.TsDecoder.Tables
{
    public class ServiceDescriptionTable : Table
    {
        public ushort TransportStreamId { get; set; }
        public byte VersionNumber { get; set; }
        public bool CurrentNextIndicator { get; set; }
        public byte SectionNumber { get; set; }
        public byte LastSectionNumber { get; set; }
        public bool ItemsIncomplete { get; set; } = true;
        public ushort OriginalNetworkId { get; set; }
        public List<ServiceDescriptionItem> Items { get; set; }
    }

    public class ServiceDescriptionItem
    {
        public byte DescriptorLength;
        public ushort DescriptorsLoopLength;
        public byte DvbDescriptorTag;
        public bool EitPresentFollowingFlag;
        public bool EitScheduleFlag;
        public bool FreeCaMode;
        public byte RunningStatus;
        public ushort ServiceId;
        public List<Descriptor> Descriptors { get; set; }
    }
}