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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SatIp
{
    /// <summary>
    ///     A simple class that can be used to deserialise RTSP responses.
    /// </summary>
    public class RtspResponse
    {
        private static readonly Regex RegexStatusLine =
            new Regex(@"RTSP/(\d+)\.(\d+)\s+(\d+)\s+([^.]+?)\r\n(.*)", RegexOptions.Singleline);

        /// <summary>
        ///     Initialise a new instance of the <see cref="RtspResponse" /> class.
        /// </summary>
        private RtspResponse()
        {
        }

        /// <summary>
        ///     Get the response major version number.
        /// </summary>
        public int MajorVersion { get; private set; } = 1;

        /// <summary>
        ///     Get the response minor version number.
        /// </summary>
        public int MinorVersion { get; private set; }

        /// <summary>
        ///     Get the response status code.
        /// </summary>
        public RtspStatusCode StatusCode { get; private set; }

        /// <summary>
        ///     Get the response reason phrase.
        /// </summary>
        public string ReasonPhrase { get; private set; }

        /// <summary>
        ///     Get the response headers.
        /// </summary>
        public IDictionary<string, string> Headers { get; private set; }

        /// <summary>
        ///     Get the response body.
        /// </summary>
        public string Body { get; set; }

        public bool HasBody { get; set; } = false;

        /// <summary>
        ///     Deserialise/parse an RTSP response.
        /// </summary>
        /// <param name="responseBytes">The raw response bytes.</param>
        /// <param name="responseByteCount">The number of valid bytes in the response.</param>
        /// <returns>a response object</returns>
        public static RtspResponse Deserialise(byte[] responseBytes, int responseByteCount)
        {
            var response = new RtspResponse();
            var responseString = Encoding.UTF8.GetString(responseBytes, 0, responseByteCount);
            Logger.Info(responseString);
            var m = RegexStatusLine.Match(responseString);
            if (m.Success)
            {
                response.MajorVersion = int.Parse(m.Groups[1].Captures[0].Value);
                response.MinorVersion = int.Parse(m.Groups[2].Captures[0].Value);
                response.StatusCode = (RtspStatusCode) int.Parse(m.Groups[3].Captures[0].Value);
                response.ReasonPhrase = m.Groups[4].Captures[0].Value;
                responseString = m.Groups[5].Captures[0].Value;
            }

            var sections = responseString.Split(new[] {"\r\n\r\n"}, StringSplitOptions.None);

            response.Body = sections[1];

            var headers = sections[0].Split(new[] {"\r\n"}, StringSplitOptions.None);
            response.Headers = new Dictionary<string, string>();
            foreach (var headerInfo in headers.Select(header => header.Split(':')))
                response.Headers.Add(headerInfo[0], headerInfo[1].Trim());
            return response;
        }
    }
}