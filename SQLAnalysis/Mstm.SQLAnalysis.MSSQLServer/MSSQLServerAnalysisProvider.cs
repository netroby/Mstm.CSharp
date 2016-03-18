using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MSSQLServer
{
    internal class MSSQLServerAnalysisProvider : MSSQLServerDbStructAnalysisProvider, ISQLAnalysisProvider
    {
        public string BuildWhere(FilterInfo filterInfo)
        {
            throw new NotImplementedException();
        }

        public string BuildNormalWhere(List<NormalFilterInfo> filters)
        {
            throw new NotImplementedException();
        }

        public string BuildCycleWhere(List<CycleFilterInfo> filters)
        {
            throw new NotImplementedException();
        }

        public string BuildPointInTimeWhere(List<PointInTimeFilterInfo> filters)
        {
            throw new NotImplementedException();
        }

        public string BuildStatistics(StatisticsInfo info)
        {
            throw new NotImplementedException();
        }

        public string BuildOrder(List<OrderInfo> orderList)
        {
            throw new NotImplementedException();
        }

        public string BuildSelect(string source, List<string> fieldList = null)
        {
            throw new NotImplementedException();
        }
    }
}
