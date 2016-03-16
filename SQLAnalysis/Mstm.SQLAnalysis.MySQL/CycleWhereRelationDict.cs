using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    internal class CycleWhereRelationDict
    {
        private static Dictionary<CycleWhereRelationEnum, string> _dict;
        private static CycleWhereRelationDict _instance;

        static CycleWhereRelationDict()
        {
            _dict = new Dictionary<CycleWhereRelationEnum, string>() { 
                {   CycleWhereRelationEnum.Month,           "Month"           },
                {   CycleWhereRelationEnum.Week,            "DAYOFWEEK"       },
                {   CycleWhereRelationEnum.Day,             "Day"             },
                {   CycleWhereRelationEnum.Hour,            "HOUR"            },
            };
        }



        public string this[CycleWhereRelationEnum index]
        {
            get
            {
                if (_dict == null || _dict.ContainsKey(index) == false)
                {
                    throw new Exception("不支持的筛选条件！" + index.ToString());
                }
                return _dict[index];
            }
        }

        public static CycleWhereRelationDict GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CycleWhereRelationDict();
            }
            return _instance;
        }
    }
}
