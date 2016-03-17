﻿using System;
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


        /// <summary>
        /// 测试构建完整where语句
        /// </summary>
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

            List<PointInTimeFilterInfo> pointInTimeList = new List<PointInTimeFilterInfo>() { 
                new PointInTimeFilterInfo(){
                    ConnectRelation= ConnectRelationEnum.And,
                    FieldName="CreatedTime",
                    WhereValue=200,
                    PointInTimeWhereRelation= PointInTimeWhereRelationEnum.BeforeDay
                },
                new PointInTimeFilterInfo(){
                    ConnectRelation= ConnectRelationEnum.And,
                    FieldName="ModifiedTime",
                    WhereValue=1,
                    PointInTimeWhereRelation= PointInTimeWhereRelationEnum.BeforeYear
                }
            };

            filterInfo.CycleFilterInfoList = cycleList;
            filterInfo.NormalFilterInfoList = normalList;
            filterInfo.PointInTimeFilterInfoList = pointInTimeList;

            string sql = _provider.BuildWhere(filterInfo);
        }



        /// <summary>
        /// 测试构建标准的不带where的筛选语句
        /// </summary>
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



        /// <summary>
        /// 测试构建周期类型的不带where的筛选语句
        /// </summary>
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



        /// <summary>
        /// 测试构建时间点类型的不带where的筛选语句
        /// </summary>
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
                    WhereValue=1,
                    PointInTimeWhereRelation= PointInTimeWhereRelationEnum.BeforeYear
                },
            };
            string sql = _provider.BuildPointInTimeWhere(list);
        }



        /// <summary>
        /// 测试构建Max统计语句
        /// </summary>
        [TestMethod]
        public void BuildStatisticsTest_Max()
        {
            StatisticsFilterInfo filter = new StatisticsFilterInfo()
            {
                FieldName = "UserId",
                TableName = "UserInfo",
                StatisticsRelation = StatisticsRelationEnum.Max
            };
            string sql = _provider.BuildStatistics(filter);
        }



        /// <summary>
        /// 测试构建Count统计语句
        /// </summary>
        [TestMethod]
        public void BuildStatisticsTest_Count()
        {
            StatisticsFilterInfo filter = new StatisticsFilterInfo()
            {
                TableName = "UserInfo",
                StatisticsRelation = StatisticsRelationEnum.Count
            };
            string sql = _provider.BuildStatistics(filter);
        }


        /// <summary>
        /// 测试构建带统计的简单where语句
        /// </summary>
        [TestMethod]
        public void BuildWhereWithStatisticsTest()
        {
            //筛选出用户Id大于平均值的,并且密保问题不为空的用户
            StatisticsFilterInfo statisticsFilter = new StatisticsFilterInfo()
            {
                TableName = "UserInfo",
                FieldName = "UserId",
                StatisticsRelation = StatisticsRelationEnum.Avg
            };

            string avgSql = _provider.BuildStatistics(statisticsFilter);

            var normalList = new List<NormalFilterInfo>() { 
            new NormalFilterInfo(){
                FieldName="UserId",
                FieldType= FieldTypeEnum.Number,
                ConnectRelation= ConnectRelationEnum.And,
                NormalWhereRelation= NormalWhereRelationEnum.MoreThan,
                WhereValue=avgSql
            },
            new NormalFilterInfo(){
                FieldName="PwdQuestion",
                FieldType= FieldTypeEnum.Text,
                ConnectRelation= ConnectRelationEnum.And,
                NormalWhereRelation= NormalWhereRelationEnum.IsNotNull,
            }
            };
            FilterInfo filterInfo = new FilterInfo();
            filterInfo.NormalFilterInfoList = normalList;

            string sql = _provider.BuildWhere(filterInfo);
        }
    }
}
