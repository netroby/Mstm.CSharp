using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.Core
{
    public class SelectModel
    {

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }


        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbTypeEnum DbType { get; set; }

        /// <summary>
        /// 规则集合
        /// </summary>
        public List<RuleModel> RuleList { get; set; }


    }
}
