using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    internal class MySQLAnalysisProvider : ISQLAnalysisProvider
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

        private PointInTimeWhereRelationDict PointInTimeRelationDictData
        {
            get
            {
                return PointInTimeWhereRelationDict.GetInstance();
            }
        }

        private StatisticsRelationDict StatisticsRelationDictData
        {
            get
            {
                return StatisticsRelationDict.GetInstance();
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
            string relation = WhereRelationDictData[filter.NormalWhereRelation].Replace(Constants.ReplaceValue, filter.WhereValue);
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
            where.AppendFormat("{0}{1}({0}{5}({2})>={3}{0}AND{0}{5}({2})<={4})", Constants.WhiteSpace, EnumUtility.GetEnumOperation(filter.ConnectRelation), filter.FieldName, filter.MinValue, filter.MaxValue, funcName);
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
                if (filterInfo.PointInTimeFilterInfoList != null)
                {
                    where.Append(this.BuildPointInTimeWhere(filterInfo.PointInTimeFilterInfoList));
                }
                if (filterInfo.OrderInfoList != null)
                {
                    where.Append(this.BuildOrder(filterInfo.OrderInfoList));
                }
            }
            return where.ToString();
        }


        public string BuildPointInTimeWhere(List<PointInTimeFilterInfo> filters)
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
                    BuildWhereWithPointInTime(where, filter);
                });
            }
            return where.ToString();
        }


        /// <summary>
        /// 编译时间点类型的where语句
        /// </summary>
        /// <param name="where"></param>
        /// <param name="filter"></param>
        private void BuildWhereWithPointInTime(StringBuilder where, PointInTimeFilterInfo filter)
        {
            //CreateTime >=DATE_ADD(now(),INTERVAL -4 Hour);
            if (where == null || filter == null) { return; }
            string expression = PointInTimeRelationDictData[filter.PointInTimeWhereRelation].Replace(Constants.ReplaceValue, filter.WhereValue.ToString());
            where.AppendFormat("{0}{1}({2}{0}{3})", Constants.WhiteSpace, EnumUtility.GetEnumOperation(filter.ConnectRelation), filter.FieldName, expression);
        }


        public string BuildStatistics(StatisticsInfo info)
        {
            StringBuilder statisticsBuilder = new StringBuilder();
            if (info == null)
            {
                throw new ArgumentException("统计信息不能为空！", "filter");
            }
            //包含不合法的参数则直接抛出异常
            if (info.StatisticsRelation != StatisticsRelationEnum.Count && string.IsNullOrWhiteSpace(info.FieldName))
            {
                throw new ArgumentException("字段名称不能为空！", "FieldName");
            }

            if (string.IsNullOrWhiteSpace(info.TableName))
            {
                throw new ArgumentException("表名不能为空！", "TableName");
            }
            string expression = StatisticsRelationDictData[info.StatisticsRelation];
            statisticsBuilder.Append(expression.Replace(Constants.ReplaceTableName, info.TableName).Replace(Constants.ReplaceFieldName, info.FieldName));
            return statisticsBuilder.ToString();
        }


        public string BuildOrder(List<OrderInfo> orderList)
        {
            StringBuilder orderBuilder = new StringBuilder();
            if (orderList != null && orderList.Count != 0)
            {
                //order by `comment_ID` desc,`comment_post_ID` asc,`comment_author` asc,`comment_author` asc
                orderBuilder.AppendFormat("{0}{1}{0}{2}", Constants.WhiteSpace, Constants.Order, Constants.By);
                orderList.ForEach(order =>
                {
                    if (string.IsNullOrWhiteSpace(order.FieldName))
                    {
                        throw new ArgumentException("无效的排序参数！", "FieldName");
                    }
                    orderBuilder.AppendFormat("{0}`{1}`{0}{2},", Constants.WhiteSpace, order.FieldName, EnumUtility.GetEnumOperation(order.OrderMode));
                });
                //去除最后一个逗号
                orderBuilder.Remove(orderBuilder.Length - 1, 1);
            }
            return orderBuilder.ToString();
        }
    }
}
