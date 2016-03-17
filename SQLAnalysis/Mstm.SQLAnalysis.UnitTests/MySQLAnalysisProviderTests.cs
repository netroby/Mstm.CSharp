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
        public void BuildNormalWhereTest()
        {
            var list = new List<NormalFilterInfo>() { 
            new NormalFilterInfo(){
                FieldName="UserName",
                FieldType= FieldTypeEnum.Text,
                ConnectRelation= ConnectRelationEnum.Or,
                NormalWhereRelation= NormalWhereRelationEnum.LikePrefix,
                WhereValue="张"
            },
            new NormalFilterInfo(){
                FieldName="UserId",
                FieldType= FieldTypeEnum.Number,
                ConnectRelation= ConnectRelationEnum.And,
                NormalWhereRelation= NormalWhereRelationEnum.Equal,
                WhereValue="1501290940000503"
            },

            new NormalFilterInfo(){
                FieldName="PwdQuestion",
                FieldType= FieldTypeEnum.Text,
                ConnectRelation= ConnectRelationEnum.And,
                NormalWhereRelation= NormalWhereRelationEnum.IsNotNull,
            },

            new NormalFilterInfo(){
                FieldName="MobilePhone",
                FieldType= FieldTypeEnum.Text,
                ConnectRelation= ConnectRelationEnum.And,
                NormalWhereRelation= NormalWhereRelationEnum.IsNull,
            },

            new NormalFilterInfo(){
                FieldName="CreatedTime",
                FieldType= FieldTypeEnum.DateTime,
                ConnectRelation= ConnectRelationEnum.And,
                NormalWhereRelation= NormalWhereRelationEnum.MoreThan,
                WhereValue="2014-5-8 12:12:11"
            },

            };

            string sql = _provider.BuildNormalWhere(list);

        }



        [TestMethod]
        public void BuildCycleWhereTest()
        {
            var list = new List<CycleFilterInfo>() { 
                new CycleFilterInfo(){
                    FieldName="CreatedTime",
                    ConnectRelation= ConnectRelationEnum.And,
                    CycleRelation= CycleWhereRelationEnum.Month,
                    MaxValue=10,
                    MinValue=2
                },
                new CycleFilterInfo(){
                    FieldName="CreatedTime",
                    ConnectRelation= ConnectRelationEnum.And,
                    CycleRelation= CycleWhereRelationEnum.Day,
                    MaxValue=29,
                    MinValue=14
                },
                new CycleFilterInfo(){
                    FieldName="CreatedTime",
                    ConnectRelation= ConnectRelationEnum.And,
                    CycleRelation= CycleWhereRelationEnum.Week,
                    MaxValue=7,
                    MinValue=2
                },
                new CycleFilterInfo(){
                    FieldName="CreatedTime",
                    ConnectRelation= ConnectRelationEnum.And,
                    CycleRelation= CycleWhereRelationEnum.Hour,
                    MaxValue=20,
                    MinValue=9
                },
            };
            string sql = _provider.BuildCycleWhere(list);
        }



        [TestMethod]
        public void BuildWhereTest()
        {
            FilterInfo filterInfo = new FilterInfo();
            List<NormalFilterInfo> normalList = new List<NormalFilterInfo>() { 
                new NormalFilterInfo(){
                    ConnectRelation= ConnectRelationEnum.And,
                    FieldName="UserName",
                    FieldType= FieldTypeEnum.Text,
                    NormalWhereRelation= NormalWhereRelationEnum.IsNotNull,
                },
                new NormalFilterInfo(){
                    ConnectRelation= ConnectRelationEnum.And,
                    FieldName="Email",
                    FieldType= FieldTypeEnum.Text,
                    NormalWhereRelation= NormalWhereRelationEnum.Like,
                    WhereValue="123"
                }
            };
            List<CycleFilterInfo> cycleList = new List<CycleFilterInfo>() { 
                new CycleFilterInfo(){
                    ConnectRelation= ConnectRelationEnum.And,
                    CycleRelation= CycleWhereRelationEnum.Day,
                    MinValue=10,
                    MaxValue=20,
                    FieldName="CreatedTime"
                },
                new CycleFilterInfo(){
                    ConnectRelation= ConnectRelationEnum.And,
                    CycleRelation= CycleWhereRelationEnum.Month,
                    MinValue=6,
                    MaxValue=12,
                    FieldName="CreatedTime"
                },
            };
            filterInfo.CycleFilterInfoList = cycleList;
            filterInfo.NormalFilterInfoList = normalList;
            string sql = _provider.BuildWhere(filterInfo);
        }


        [TestMethod]
        public void BuildPointInTimeWhereTest()
        {
            List<PointInTimeFilterInfo> list = new List<PointInTimeFilterInfo>() { 
                new PointInTimeFilterInfo(){
                    ConnectRelation= ConnectRelationEnum.And,
                    FieldName="CreatedTime",
                    WhereValue=200,
                    PointInTimeWhereRelation= PointInTimeWhereRelationEnum.BeforeDay
                },
                new PointInTimeFilterInfo(){
                    ConnectRelation= ConnectRelationEnum.And,
                    FieldName="ModifiedTime",
                    WhereValue=2,
                    PointInTimeWhereRelation= PointInTimeWhereRelationEnum.BeforeYear
                },
            };
            string sql = _provider.BuildPointInTimeWhere(list);
        }
    }
}
