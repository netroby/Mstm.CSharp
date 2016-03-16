using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    internal class WhereRelationDict
    {
        private static Dictionary<WhereRelationEnum, string> _dict;
        private static WhereRelationDict _instance;

        static WhereRelationDict()
        {
            _dict = new Dictionary<WhereRelationEnum, string>() { 
                {   WhereRelationEnum.Equal,                 "="                  },
                {   WhereRelationEnum.In,                    "IN"                 },
                {   WhereRelationEnum.IsNotNull,             "IS NOT NULL"        },
                {   WhereRelationEnum.IsNull,                "IS NULL"            },
                {   WhereRelationEnum.LessThan,              "<"                  },
                {   WhereRelationEnum.LessThanOrEqual,       "<="                 },
                {   WhereRelationEnum.Like,                  "LIKE '%VALUE%'"     },
                {   WhereRelationEnum.LikePrefix,            "LIKE '%VALUE'"      },
                {   WhereRelationEnum.LikeSuffix,            "LIKE 'VALUE%'"      },
                {   WhereRelationEnum.MoreThan,              ">"                  },
                {   WhereRelationEnum.MoreThanOrEqual,       ">="                 },
                {   WhereRelationEnum.NotEqual,              "<>"                 },
                {   WhereRelationEnum.NotLike,               "NOT LIKE '%VALUE%'" },
                {   WhereRelationEnum.NotLikePrefix,         "NOT LIKE '%VALUE'"  },
                {   WhereRelationEnum.NotLikeSuffix,         "NOT LIKE 'VALUE%'"  },
            };
        }



        public string this[WhereRelationEnum index]
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

        public static WhereRelationDict GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WhereRelationDict();
            }
            return _instance;
        }
    }
}
