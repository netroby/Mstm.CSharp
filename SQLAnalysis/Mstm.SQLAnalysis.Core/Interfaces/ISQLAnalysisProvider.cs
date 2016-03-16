using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.Core
{
    public interface ISQLAnalysisProvider
    {

        /// <summary>
        /// 构建where语句
        /// </summary>
        /// <param name="filters">所有筛选条件集合</param>
        /// <returns>带where的完整where语句</returns>
        string BuildWhere(List<FilterModel> filters);
    }
}
