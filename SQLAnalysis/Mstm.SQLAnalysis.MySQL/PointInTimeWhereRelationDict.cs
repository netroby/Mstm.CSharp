using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    internal class PointInTimeWhereRelationDict : WhereRelationDictAbstract<PointInTimeWhereRelationEnum>
    {
        private static PointInTimeWhereRelationDict _instance;



        public static PointInTimeWhereRelationDict GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PointInTimeWhereRelationDict();
            }
            return _instance;
        }

        protected override Dictionary<PointInTimeWhereRelationEnum, string> InitDict()
        {
            var dict = new Dictionary<PointInTimeWhereRelationEnum, string>() { 
                {   PointInTimeWhereRelationEnum.BeforeYear,           "<= DATE_ADD(NOW(),INTERVAL -VALUE YEAR)"      },
                {   PointInTimeWhereRelationEnum.BeforeMonth,          "<= DATE_ADD(NOW(),INTERVAL -VALUE MONTH)"     },
                {   PointInTimeWhereRelationEnum.BeforeDay,            "<= DATE_ADD(NOW(),INTERVAL -VALUE DAY)"       },
                {   PointInTimeWhereRelationEnum.BeforeHour,           "<= DATE_ADD(NOW(),INTERVAL -VALUE HOUR)"      },
                {   PointInTimeWhereRelationEnum.InYear,               ">= DATE_ADD(NOW(),INTERVAL -VALUE YEAR)"      },
                {   PointInTimeWhereRelationEnum.InMonth,              ">= DATE_ADD(NOW(),INTERVAL -VALUE MONTH)"     },
                {   PointInTimeWhereRelationEnum.InDay,                ">= DATE_ADD(NOW(),INTERVAL -VALUE DAY)"       },
                {   PointInTimeWhereRelationEnum.InHour,               ">= DATE_ADD(NOW(),INTERVAL -VALUE HOUR)"      },
            };
            return dict;
        }
    }
}
