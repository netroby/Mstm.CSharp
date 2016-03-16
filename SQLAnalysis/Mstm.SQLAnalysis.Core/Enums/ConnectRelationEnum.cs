using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.Core
{
    public enum ConnectRelationEnum
    {
        [Operator("and")]
        And = 1,

        [Operator("or")]
        Or = 2
    }
}
