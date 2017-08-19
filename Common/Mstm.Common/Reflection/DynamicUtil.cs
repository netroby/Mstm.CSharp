using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Common.Reflection
{
    public class DynamicUtil
    {
        public static dynamic GetDynamicObject()
        {
            return new ExpandoObject();
        }
    }
}
