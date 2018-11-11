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
    internal class Channel 
    {
        public decimal Frequency { get; set; }
        public int Sypmbolrate { get; set; }
        public int Polarization { get; set; }
        public int ModulationSystem { get; set; }
        public int ModulationType { get ; set ; }
        public short ServiceType { get; set; }
        public string ServiceName { get; set; }
        public string ServiceProvider { get; set; }
        public ushort ServiceId { get; set; }
        public ushort ProgramClockReferenceId { get; set; }
        public short VideoPid { get; set; }
        public short AudioPid { get; set; }
        public short AC3Pid { get; set; }
        public short EAC3Pid { get; set; }
        public short AACPid { get; set; }
        public short DTSPid { get; set; }
        public short TTXPid { get; set; }
        public short SubTitlePid { get; set; }
    }
}