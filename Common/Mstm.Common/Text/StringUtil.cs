using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Common.Text
{
    public class StringUtil
    {
        public SecureString GetSecureString(string text)
        {
            SecureString secure = new SecureString();
            var chars = text.ToCharArray();
            chars.ToList().ForEach(c => secure.AppendChar(c));
            return secure;
        }

        public string FromSecureString(SecureString secureStr)
        {
            string text = "";
            unsafe
            {
                IntPtr ptr = new IntPtr();
                try
                {
                    ptr = Marshal.SecureStringToCoTaskMemUnicode(secureStr);
                    Char* pc = (Char*)ptr;

                    for (int i = 0; pc[i] != 0; i++)
                    {
                        text += pc[i];
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    Marshal.ZeroFreeCoTaskMemUnicode(ptr);
                }
            }
            return text;
        }
    }
}
