using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    internal class NormalWhereRelationDict
    {
        private static Dictionary<NormalWhereRelationEnum, string> _dict;
        private static NormalWhereRelationDict _instance;

        static NormalWhereRelationDict()
        {
            _dict = new Dictionary<NormalWhereRelationEnum, string>() { 
                {   NormalWhereRelationEnum.Equal,                 "="                  },
                {   NormalWhereRelationEnum.In,                    "IN"                 },
                {   NormalWhereRelationEnum.IsNotNull,             "IS NOT NULL"        },
                {   NormalWhereRelationEnum.IsNull,                "IS NULL"            },
                {   NormalWhereRelationEnum.LessThan,              "<"                  },
                {   NormalWhereRelationEnum.LessThanOrEqual,       "<="                 },
                {   NormalWhereRelationEnum.Like,                  "LIKE '%VALUE%'"     },
                {   NormalWhereRelationEnum.LikePrefix,            "LIKE '%VALUE'"      },
                {   NormalWhereRelationEnum.LikeSuffix,            "LIKE 'VALUE%'"      },
                {   NormalWhereRelationEnum.MoreThan,              ">"                  },
                {   NormalWhereRelationEnum.MoreThanOrEqual,       ">="                 },
                {   NormalWhereRelationEnum.NotEqual,              "<>"                 },
                {   NormalWhereRelationEnum.NotLike,               "NOT LIKE '%VALUE%'" },
                {   NormalWhereRelationEnum.NotLikePrefix,         "NOT LIKE '%VALUE'"  },
                {   NormalWhereRelationEnum.NotLikeSuffix,         "NOT LIKE 'VALUE%'"  },
            };
        }



        public string this[NormalWhereRelationEnum index]
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

        public static NormalWhereRelationDict GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NormalWhereRelationDict();
            }
            return _instance;
        }
    }
}
