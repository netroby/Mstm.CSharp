﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.Core
{
    public class RuleModel
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }


        /// <summary>
        /// 字段类型
        /// </summary>
        public int FieldType { get; set; }


        /// <summary>
        /// 查询关系
        /// </summary>
        public WhereRelationEnum WhereRelation { get; set; }


        /// <summary>
        /// 查询关系值
        /// </summary>
        public string WhereValue { get; set; }


        /// <summary>
        /// 连接关系
        /// </summary>
        public ConnectRelationEnum ConnectRelation { get; set; }
    }
}
