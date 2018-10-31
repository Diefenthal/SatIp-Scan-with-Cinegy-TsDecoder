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

using System;

namespace SatIp
{
    public class CableTuner : Tuner
    {
        public override TunerType Type => TunerType.Cable;

        public override int Index => throw new NotImplementedException();

        public override int SignalLevel => throw new NotImplementedException();

        public override int SignalQuality => throw new NotImplementedException();

        public override bool SignalLocked => throw new NotImplementedException();

        public override void Tune(string parameters)
        {
            throw new NotImplementedException();
        }
    }
}