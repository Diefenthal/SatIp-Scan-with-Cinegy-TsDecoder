/*  
    Copyright (C) <2007-2017>  <Kay Diefenthal>

    SatIp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SatIp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with SatIp.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SatIp
{
    public static class Utils
    {
        //When the First Epoch will wrap (The real Y2k)
        public static DateTime UtcEpoch2036 = new DateTime(2036, 2, 7, 6, 28, 16, DateTimeKind.Utc);

        public static DateTime UtcEpoch1900 = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime UtcEpoch1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static int ConvertBCDToInt(byte[] byteData, int index, int count)
        {
            var result = 0;
            var shift = 4;

            for (var nibbleIndex = 0; nibbleIndex < count; nibbleIndex++)
            {
                result = result * 10 + ((byteData[index] >> shift) & 0x0f);

                if (shift == 4)
                {
                    shift = 0;
                }
                else
                {
                    shift = 4;
                    index++;
                }
            }

            return result;
        }

        public static string ToHexString(this byte[] hex)
        {
            if (hex == null) return null;
            if (hex.Length == 0) return string.Empty;

            var s = new StringBuilder();
            foreach (var b in hex) s.Append(b.ToString("x2"));
            return s.ToString();
        }

        public static int Convert2BytesToInt(byte[] buffer, int offset)
        {
            var temp = buffer[offset];
            temp = (byte) ((temp * 256) + buffer[offset + 1]);

            return temp;
        }

        public static int Convert3BytesToInt(byte[] buffer, int offset)
        {
            var temp = (int) buffer[offset];
            temp = temp * 256 + buffer[offset + 1];
            temp = temp * 256 + buffer[offset + 2];

            return temp;
        }

        public static int Convert4BytesToInt(byte[] buffer, int offset)
        {
            var temp = (int) buffer[offset];
            temp = temp * 256 + buffer[offset + 1];
            temp = temp * 256 + buffer[offset + 2];
            temp = temp * 256 + buffer[offset + 3];

            return temp;
        }

        public static long Convert4BytesToLong(byte[] buffer, int offset)
        {
            long temp = 0;

            for (var index = 0; index < 4; index++)
                temp = temp * 256 + buffer[offset + index];

            return temp;
        }

        public static long Convert8BytesToLong(byte[] buffer, int offset)
        {
            long temp = 0;

            for (var index = 0; index < 8; index++)
                temp = temp * 256 + buffer[offset + index];

            return temp;
        }

        public static string ConvertBytesToString(byte[] buffer, int offset, int length)
        {
            var reply = new StringBuilder(4);
            for (var index = 0; index < length; index++)
                reply.Append((char) buffer[offset + index]);
            return reply.ToString();
        }

        public static DateTime NptTimestampToDateTime(long nptTimestamp)
        {
            return NptTimestampToDateTime((uint) ((nptTimestamp >> 32) & 0xFFFFFFFF),
                (uint) (nptTimestamp & 0xFFFFFFFF), null);
        }

        public static DateTime NptTimestampToDateTime(uint seconds, uint fractions, DateTime? epoch)
        {
            var ticks = (ulong) (seconds * TimeSpan.TicksPerSecond +
                                 fractions * TimeSpan.TicksPerSecond / 0x100000000L);
            if (epoch.HasValue) return epoch.Value + TimeSpan.FromTicks((long) ticks);
            return (seconds & 0x80000000L) == 0
                ? UtcEpoch2036 + TimeSpan.FromTicks((long) ticks)
                : UtcEpoch1900 + TimeSpan.FromTicks((long) ticks);
        }

        public static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            throw new Exception("Local IP Address Not Found!");
        }
    }
}