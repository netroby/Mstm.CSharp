using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.Sybase
{
    internal class SybaseFieldTypeAnalysisProvider : IFieldTypeAnalysisProvider
    {
        public FieldTypeEnum GetFieldTypeEnum(string dbType)
        {
            return FieldTypeEnum.Text;
        }

    }
}
