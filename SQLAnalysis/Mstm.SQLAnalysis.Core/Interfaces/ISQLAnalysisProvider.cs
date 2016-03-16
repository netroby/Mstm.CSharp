using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.Core
{

    /// <summary>
    /// SQL语句构建接口
    /// </summary>
    public interface ISQLAnalysisProvider
    {

        /// <summary>
        /// 构建普通的where语句
        /// </summary>
        /// <param name="filters">所有筛选条件集合</param>
        /// <returns>不带where的筛选语句</returns>
        string BuildNormalWhere(List<NormalFilterInfo> filters);


        /// <summary>
        /// 构建周期类型的where语句
        /// </summary>
        /// <param name="filters">所有筛选条件集合</param>
        /// <returns>不带where的筛选语句</returns>
        string BuildCycleWhere(List<CycleFilterInfo> filters);


        /// <summary>
        /// 构建完整的where语句
        /// </summary>
        /// <param name="filterInfo">筛选条件</param>
        /// <returns>带where的完整的筛选语句</returns>
        string BuildWhere(FilterInfo filterInfo);

    }
}
