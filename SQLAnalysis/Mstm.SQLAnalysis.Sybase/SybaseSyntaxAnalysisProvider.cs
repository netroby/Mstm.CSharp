using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.Sybase
{
    internal class SybaseSyntaxAnalysisProvider : DefaultSQLSyntaxAnalysisProvider
    {
        protected override RelationDictAbstract<NormalWhereRelationEnum> WhereRelationDictData
        {
            get { throw new NotImplementedException(); }
        }

        protected override RelationDictAbstract<CycleWhereRelationEnum> CycleRelationDictData
        {
            get { throw new NotImplementedException(); }
        }

        protected override RelationDictAbstract<PointInTimeWhereRelationEnum> PointInTimeRelationDictData
        {
            get { throw new NotImplementedException(); }
        }

        protected override RelationDictAbstract<StatisticsRelationEnum> StatisticsRelationDictData
        {
            get { throw new NotImplementedException(); }
        }

        protected override string SafeQuote
        {
            get { throw new NotImplementedException(); }
        }
    }
}
