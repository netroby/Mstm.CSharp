using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mstm.SQLAnalysis.Core;
using Mstm.SQLAnalysis.MySQL;
using System.Collections.Generic;

namespace Mstm.SQLAnalysis.UnitTests
{
    [TestClass]
    public class MySQLAnalysisProviderTests
    {
        ISQLAnalysisProvider _provider;

        /// <summary>
        /// 单个测试开始前
        /// </summary>
        [TestInitialize]
        public void MethodInit()
        {
            _provider = new MySQLAnalysisProvider();
        }


        /// <summary>
        /// 单个测试结束后
        /// </summary>
        [TestCleanup]
        public void MethodDispose()
        {
            _provider = null;
        }


        [TestMethod]
        public void BuildWhereTest()
        {
            var list = new List<FilterModel>() { 
            new FilterModel(){
                FieldName="UserName",
                FieldType= FieldTypeEnum.Text,
                ConnectRelation= ConnectRelationEnum.Or,
                WhereRelation= WhereRelationEnum.LikePrefix,
                WhereValue="张"
            },
            new FilterModel(){
                FieldName="UserId",
                FieldType= FieldTypeEnum.Number,
                ConnectRelation= ConnectRelationEnum.And,
                WhereRelation= WhereRelationEnum.Equal,
                WhereValue="1501290940000503"
            },

            new FilterModel(){
                FieldName="PwdQuestion",
                FieldType= FieldTypeEnum.Text,
                ConnectRelation= ConnectRelationEnum.And,
                WhereRelation= WhereRelationEnum.IsNotNull,
            },

            new FilterModel(){
                FieldName="MobilePhone",
                FieldType= FieldTypeEnum.Text,
                ConnectRelation= ConnectRelationEnum.And,
                WhereRelation= WhereRelationEnum.IsNull,
            },

            new FilterModel(){
                FieldName="CreatedTime",
                FieldType= FieldTypeEnum.DateTime,
                ConnectRelation= ConnectRelationEnum.And,
                WhereRelation= WhereRelationEnum.MoreThan,
                WhereValue="2014-5-8 12:12:11"
            },

            };

            string sql = _provider.BuildWhere(list);

        }
    }
}
