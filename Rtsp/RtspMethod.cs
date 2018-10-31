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

using System.Collections.Generic;

namespace SatIp
{
    /// <summary>
    ///     Standard RTSP request methods.
    /// </summary>
    public sealed class RtspMethod
    {
        private static readonly IDictionary<string, RtspMethod> _values = new Dictionary<string, RtspMethod>();

        public static readonly RtspMethod Describe = new RtspMethod("DESCRIBE");
        public static readonly RtspMethod Announce = new RtspMethod("ANNOUNCE");
        public static readonly RtspMethod GetParameter = new RtspMethod("GET_PARAMETER");
        public static readonly RtspMethod Options = new RtspMethod("OPTIONS");
        public static readonly RtspMethod Pause = new RtspMethod("PAUSE");
        public static readonly RtspMethod Play = new RtspMethod("PLAY");
        public static readonly RtspMethod Record = new RtspMethod("RECORD");
        public static readonly RtspMethod Redirect = new RtspMethod("REDIRECT");
        public static readonly RtspMethod Setup = new RtspMethod("SETUP");
        public static readonly RtspMethod SetParameter = new RtspMethod("SET_PARAMETER");
        public static readonly RtspMethod Teardown = new RtspMethod("TEARDOWN");

        private readonly string _name;

        private RtspMethod(string name)
        {
            _name = name;
            _values.Add(name, this);
        }

        public static ICollection<RtspMethod> Values => _values.Values;

        public override int GetHashCode()
        {
            return _name != null ? _name.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return _name;
        }

        public override bool Equals(object obj)
        {
            var method = obj as RtspMethod;
            if (method != null && this == method) return true;
            return false;
        }

        public static explicit operator RtspMethod(string name)
        {
            RtspMethod value;
            if (!_values.TryGetValue(name, out value)) return null;
            return value;
        }

        public static implicit operator string(RtspMethod method)
        {
            return method._name;
        }
    }
}