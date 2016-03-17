using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.Core
{

    /// <summary>
    /// SQL解析工厂接口
    /// </summary>
    public interface ISQLAnalysisFactory
    {
        ISQLAnalysisProvider GetSQLAnalysisProvider();
    }
}
