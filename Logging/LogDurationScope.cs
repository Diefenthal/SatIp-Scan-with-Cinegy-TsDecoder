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
    public class LogDurationScope : IDisposable
    {
        private readonly string _name;
        private readonly DateTime _startDateTime;

        public LogDurationScope(string name)
        {
            _name = name;
            _startDateTime = DateTime.Now;
        }

        public static LogDurationScope Start(string name)
        {
            return new LogDurationScope(name);
        }

        #region IDisposable Pattern

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    Logger.Verbose("{0}, duration: {1}", _name, (DateTime.Now - _startDateTime).TotalMilliseconds);
            _disposed = true;
        }

        ~LogDurationScope()
        {
            Dispose(false);
        }

        #endregion
    }
}