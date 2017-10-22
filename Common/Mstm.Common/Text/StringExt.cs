using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class StringExt
    {
        public static byte[] Bytes(this string source)
        {
            return System.Text.Encoding.UTF8.GetBytes(source);
        }

        public static string Str(this byte[] bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public static string Str(this IEnumerable<byte> bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes.ToArray());
        }
    }
}
