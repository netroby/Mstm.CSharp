using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    public class MySQLAnalysisProvider : ISQLAnalysisProvider
    {
        private WhereRelationDict WhereRelationDictData
        {
            get
            {
                return WhereRelationDict.GetInstance();
            }
        }

        public string BuildWhere(List<FilterModel> filters)
        {
            StringBuilder where = new StringBuilder();
            // where 1 
            where.AppendFormat("{0}{1}{0}{2}", Constants.WhiteSpace, Constants.Where, 1);

            if (filters != null && filters.Count() > 0)
            {
                filters.ForEach(filter =>
                {
                    //包含不合法的参数则直接抛出异常
                    if (string.IsNullOrWhiteSpace(filter.FieldName))
                    {
                        throw new ArgumentException("字段名称不能为空！", "FieldName");
                    }
                    switch (filter.WhereRelation)
                    {
                        case WhereRelationEnum.Equal:
                        case WhereRelationEnum.In:
                        case WhereRelationEnum.NotEqual:
                        case WhereRelationEnum.LessThan:
                        case WhereRelationEnum.LessThanOrEqual:
                        case WhereRelationEnum.MoreThan:
                        case WhereRelationEnum.MoreThanOrEqual:
                            BuildWhereWithNormal(where, filter);
                            break;
                        case WhereRelationEnum.Like:
                        case WhereRelationEnum.LikePrefix:
                        case WhereRelationEnum.LikeSuffix:
                        case WhereRelationEnum.NotLike:
                        case WhereRelationEnum.NotLikePrefix:
                        case WhereRelationEnum.NotLikeSuffix:
                            BuildWhereWithLike(where, filter);
                            break;
                        case WhereRelationEnum.IsNull:
                        case WhereRelationEnum.IsNotNull:
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
        private void BuildWhereWithLike(StringBuilder where, FilterModel filter)
        {
            //for example
            // username like '%明%'
            if (where == null || filter == null) { return; }
            string relation = WhereRelationDictData[filter.WhereRelation].Replace("VALUE", filter.WhereValue);
            where.AppendFormat("{0}{1}{0}({2}{0}{3})", Constants.WhiteSpace, EnumUtility.GetEnumOperation(filter.ConnectRelation), filter.FieldName, relation);
        }


        /// <summary>
        /// 编译IS语句
        /// </summary>
        /// <param name="where"></param>
        /// <param name="filter"></param>
        private void BuildWhereWithIS(StringBuilder where, FilterModel filter)
        {
            //for example
            // username is not null
            if (where == null || filter == null) { return; }
            string relation = WhereRelationDictData[filter.WhereRelation];
            where.AppendFormat("{0}{1}{0}({2}{0}{3})", Constants.WhiteSpace, EnumUtility.GetEnumOperation(filter.ConnectRelation), filter.FieldName, relation);
        }


        /// <summary>
        /// 编译普通语句
        /// </summary>
        /// <param name="where"></param>
        /// <param name="filter"></param>
        private void BuildWhereWithNormal(StringBuilder where, FilterModel filter)
        {
            //for example
            // userid = 1001
            if (where == null || filter == null) { return; }
            string relation = WhereRelationDictData[filter.WhereRelation];
            //处理数据类型之间的区别
            string value = filter.FieldType != FieldTypeEnum.Number ? string.Format("'{0}'", filter.WhereValue) : filter.WhereValue;
            where.AppendFormat("{0}{1}{0}({2}{0}{3}{4})", Constants.WhiteSpace, EnumUtility.GetEnumOperation(filter.ConnectRelation), filter.FieldName, relation, value);
        }
    }
}
