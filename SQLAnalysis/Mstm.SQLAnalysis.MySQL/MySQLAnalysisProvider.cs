﻿using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    public class MySQLAnalysisProvider : ISQLAnalysisProvider
    {
        private NormalWhereRelationDict WhereRelationDictData
        {
            get
            {
                return NormalWhereRelationDict.GetInstance();
            }
        }

        private CycleWhereRelationDict CycleRelationDictData
        {
            get
            {
                return CycleWhereRelationDict.GetInstance();
            }
        }


        public string BuildNormalWhere(List<NormalFilterInfo> filters)
        {
            StringBuilder where = new StringBuilder();

            if (filters != null && filters.Count() > 0)
            {
                filters.ForEach(filter =>
                {
                    //包含不合法的参数则直接抛出异常
                    if (string.IsNullOrWhiteSpace(filter.FieldName))
                    {
                        throw new ArgumentException("字段名称不能为空！", "FieldName");
                    }
                    switch (filter.NormalWhereRelation)
                    {
                        case NormalWhereRelationEnum.Equal:
                        case NormalWhereRelationEnum.In:
                        case NormalWhereRelationEnum.NotEqual:
                        case NormalWhereRelationEnum.LessThan:
                        case NormalWhereRelationEnum.LessThanOrEqual:
                        case NormalWhereRelationEnum.MoreThan:
                        case NormalWhereRelationEnum.MoreThanOrEqual:
                            BuildWhereWithNormal(where, filter);
                            break;
                        case NormalWhereRelationEnum.Like:
                        case NormalWhereRelationEnum.LikePrefix:
                        case NormalWhereRelationEnum.LikeSuffix:
                        case NormalWhereRelationEnum.NotLike:
                        case NormalWhereRelationEnum.NotLikePrefix:
                        case NormalWhereRelationEnum.NotLikeSuffix:
                            BuildWhereWithLike(where, filter);
                            break;
                        case NormalWhereRelationEnum.IsNull:
                        case NormalWhereRelationEnum.IsNotNull:
                            BuildWhereWithIS(where, filter);
                            break;
                    }
                });
            }
            return where.ToString();
        }

        /// <summary>
        /// 编译LIKE语句
        /// </summary>
        /// <param name="where"></param>
        /// <param name="filter"></param>
        private void BuildWhereWithLike(StringBuilder where, NormalFilterInfo filter)
        {
            //for example
            // username like '%明%'
            if (where == null || filter == null) { return; }
            string relation = WhereRelationDictData[filter.NormalWhereRelation].Replace("VALUE", filter.WhereValue);
            where.AppendFormat("{0}{1}{0}({2}{0}{3})", Constants.WhiteSpace, EnumUtility.GetEnumOperation(filter.ConnectRelation), filter.FieldName, relation);
        }


        /// <summary>
        /// 编译IS语句
        /// </summary>
        /// <param name="where"></param>
        /// <param name="filter"></param>
        private void BuildWhereWithIS(StringBuilder where, NormalFilterInfo filter)
        {
            //for example
            // username is not null
            if (where == null || filter == null) { return; }
            string relation = WhereRelationDictData[filter.NormalWhereRelation];
            where.AppendFormat("{0}{1}{0}({2}{0}{3})", Constants.WhiteSpace, EnumUtility.GetEnumOperation(filter.ConnectRelation), filter.FieldName, relation);
        }


        /// <summary>
        /// 编译普通语句
        /// </summary>
        /// <param name="where"></param>
        /// <param name="filter"></param>
        private void BuildWhereWithNormal(StringBuilder where, NormalFilterInfo filter)
        {
            //for example
            // userid = 1001
            if (where == null || filter == null) { return; }
            string relation = WhereRelationDictData[filter.NormalWhereRelation];
            //处理数据类型之间的区别
            string value = filter.FieldType != FieldTypeEnum.Number ? string.Format("'{0}'", filter.WhereValue) : filter.WhereValue;
            where.AppendFormat("{0}{1}{0}({2}{0}{3}{4})", Constants.WhiteSpace, EnumUtility.GetEnumOperation(filter.ConnectRelation), filter.FieldName, relation, value);
        }


        public string BuildCycleWhere(List<CycleFilterInfo> filters)
        {
            StringBuilder where = new StringBuilder();
            if (filters != null && filters.Count > 0)
            {
                filters.ForEach(filter =>
                {
                    //包含不合法的参数则直接抛出异常
                    if (string.IsNullOrWhiteSpace(filter.FieldName))
                    {
                        throw new ArgumentException("字段名称不能为空！", "FieldName");
                    }
                    BuildWhereWithCycle(where, filter);
                });
            }
            return where.ToString();
        }


        /// <summary>
        /// 编译周期类型的where语句
        /// </summary>
        /// <param name="where"></param>
        /// <param name="filter"></param>
        private void BuildWhereWithCycle(StringBuilder where, CycleFilterInfo filter)
        {
            if (where == null || filter == null) { return; }
            string funcName = CycleRelationDictData[filter.CycleRelation];
            where.AppendFormat("{0}{1}({0}{5}({2})>={3}{0}AND{0}{5}({2})<={4})", Constants.WhiteSpace, filter.ConnectRelation, filter.FieldName, filter.MinValue, filter.MaxValue, funcName);
        }




        public string BuildWhere(FilterInfo filterInfo)
        {
            StringBuilder where = new StringBuilder();
            // where 1 
            where.AppendFormat("{0}{1}{0}{2}", Constants.WhiteSpace, Constants.Where, 1);

            if (filterInfo != null)
            {
                if (filterInfo.NormalFilterInfoList != null)
                {
                    where.Append(this.BuildNormalWhere(filterInfo.NormalFilterInfoList));
                }
                if (filterInfo.CycleFilterInfoList != null)
                {
                    where.Append(this.BuildCycleWhere(filterInfo.CycleFilterInfoList));
                }
            }
            return where.ToString();
        }
    }
}
