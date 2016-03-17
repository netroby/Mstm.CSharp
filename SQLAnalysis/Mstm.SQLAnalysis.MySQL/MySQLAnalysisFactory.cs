using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    public class MySQLAnalysisFactory : ISQLAnalysisFactory
    {
        public ISQLAnalysisProvider GetSQLAnalysisProvider()
        {
            return new MySQLAnalysisProvider();
        }
    }
}
