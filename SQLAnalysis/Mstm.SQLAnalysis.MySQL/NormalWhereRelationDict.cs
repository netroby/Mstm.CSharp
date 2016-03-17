using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    internal class NormalWhereRelationDict : WhereRelationDictAbstract<NormalWhereRelationEnum>
    {
        private static NormalWhereRelationDict _instance;


        public static NormalWhereRelationDict GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NormalWhereRelationDict();
            }
            return _instance;
        }


        protected override Dictionary<NormalWhereRelationEnum, string> InitDict()
        {
            var dict = new Dictionary<NormalWhereRelationEnum, string>() { 
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

            return dict;
        }
    }
}
