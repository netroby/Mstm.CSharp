﻿using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    internal class MySQLAnalysisProvider : DefaultSQLSyntaxAnalysisProvider
    {


        protected override RelationDictAbstract<NormalWhereRelationEnum> WhereRelationDictData
        {
            get
            {
                return NormalWhereRelationDict.GetInstance();
            }
        }

        protected override RelationDictAbstract<CycleWhereRelationEnum> CycleRelationDictData
        {
            get
            {
                return CycleWhereRelationDict.GetInstance();
            }
        }

        protected override RelationDictAbstract<PointInTimeWhereRelationEnum> PointInTimeRelationDictData
        {
            get
            {
                return PointInTimeWhereRelationDict.GetInstance();
            }
        }

        protected override RelationDictAbstract<StatisticsRelationEnum> StatisticsRelationDictData
        {
            get
            {
                return StatisticsRelationDict.GetInstance();
            }
        }

        protected override string SafeQuote
        {
            get { return "`"; }
        }

    }
}
