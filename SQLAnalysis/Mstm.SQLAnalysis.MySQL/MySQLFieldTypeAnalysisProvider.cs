using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    internal class MySQLFieldTypeAnalysisProvider : IFieldTypeAnalysisProvider
    {
        public FieldTypeEnum GetFieldTypeEnum(string dbType)
        {
            return FieldTypeEnum.Text;
        }
    }
}
