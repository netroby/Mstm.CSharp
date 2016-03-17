using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    internal class CycleWhereRelationDict : WhereRelationDictAbstract<CycleWhereRelationEnum>
    {
        private static CycleWhereRelationDict _instance;


        public static CycleWhereRelationDict GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CycleWhereRelationDict();
            }
            return _instance;
        }


        protected override Dictionary<CycleWhereRelationEnum, string> InitDict()
        {
            var dict = new Dictionary<CycleWhereRelationEnum, string>() { 
                {   CycleWhereRelationEnum.Month,           "Month"           },
                {   CycleWhereRelationEnum.Week,            "DAYOFWEEK"       },
                {   CycleWhereRelationEnum.Day,             "Day"             },
                {   CycleWhereRelationEnum.Hour,            "HOUR"            },
            };
            return dict;
        }
    }
}
