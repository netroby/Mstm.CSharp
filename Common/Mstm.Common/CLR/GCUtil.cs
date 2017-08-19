using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Common.CLR
{
    public class GCUtil
    {
        public static GCHandle GetHandler(object obj)
        {
            return GCHandle.Alloc(obj);
        }
    }
}
