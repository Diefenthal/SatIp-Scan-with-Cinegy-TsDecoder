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
using System.Text;

namespace SatIp
{
    /// <summary>
    ///     A simple class that can be used to serialise RTSP requests.
    /// </summary>
    public class RtspRequest
    {
        /// <summary>
        ///     Initialise a new instance of the <see cref="RtspRequest" /> class.
        /// </summary>
        /// <param name="method">The request method.</param>
        /// <param name="uri">The request URI</param>
        /// <param name="majorVersion">The major version number.</param>
        /// <param name="minorVersion">The minor version number.</param>
        public RtspRequest(RtspMethod method, string uri, int majorVersion, int minorVersion)
        {
            Method = method;
            Uri = uri;
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
        }

        /// <summary>
        ///     Get the request method.
        /// </summary>
        public RtspMethod Method { get; }

        /// <summary>
        ///     Get the request URI.
        /// </summary>
        public string Uri { get; }

        /// <summary>
        ///     Get the request major version number.
        /// </summary>
        public int MajorVersion { get; }

        /// <summary>
        ///     Get the request minor version number.
        /// </summary>
        public int MinorVersion { get; }

        /// <summary>
        ///     Get or set the request headers.
        /// </summary>
        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Get or set the request body.
        /// </summary>
        public string Body { get; set; } = string.Empty;

        /// <summary>
        ///     Serialise this request.
        /// </summary>
        /// <returns>raw request bytes</returns>
        public byte[] Serialise()
        {
            var request = new StringBuilder();
            request.AppendFormat("{0} {1} RTSP/{2}.{3}\r\n", Method, Uri, MajorVersion, MinorVersion);
            foreach (var header in Headers) request.AppendFormat("{0}: {1}\r\n", header.Key, header.Value);
            request.AppendFormat("\r\n{0}", Body);
            Logger.Info(request.ToString());
            return Encoding.UTF8.GetBytes(request.ToString());
        }
    }
}