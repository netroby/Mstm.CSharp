using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Common.Text
{
    /// <summary>
    /// Base64相关工具类
    /// </summary>
    public class Base64Util
    {
        /// <summary>
        /// 默认使用utf8作为编码
        /// </summary>
        public readonly static Encoding DefaultEncoding = Encoding.UTF8;

        /// <summary>
        /// 将字符串转化为Base64编码
        /// </summary>
        /// <param name="text">带编码的文本</param>
        /// <param name="encoding">文本的编码格式，默认参见<see cref="Base64Util.DefaultEncoding"/></param>
        /// <returns>Base64编码结果</returns>
        public static string GetBase64String(string text, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = DefaultEncoding;
            }
            byte[] bytes = encoding.GetBytes(text);
            string base64Str = Convert.ToBase64String(bytes);
            return base64Str;
        }

        /// <summary>
        /// 还原Base64编码文本
        /// </summary>
        /// <param name="base64Str">Base64编码文本</param>
        /// <param name="encoding">文本的编码格式，默认参见<see cref="Base64Util.DefaultEncoding"/></param>
        /// <returns>Base64编码的原始文本</returns>
        public static string FromBase64String(string base64Str, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = DefaultEncoding;
            }
            byte[] bytes = Convert.FromBase64String(base64Str);
            string text = encoding.GetString(bytes);
            return text;
        }
    }
}
