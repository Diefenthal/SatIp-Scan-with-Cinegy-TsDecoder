﻿/* Copyright 2018 Kay Diefenthal.

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
    public class IniMapping
    {
        #region Constructor

        public IniMapping(string name, string file)
        {
            Name = name;
            File = file;
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }

        #region Private Fields

        #endregion

        #region Properties

        public string Name { get; set; }

        public string File { get; set; }

        #endregion
    }
}