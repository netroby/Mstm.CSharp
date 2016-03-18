using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mstm.SQLAnalysis.Core;
using Mstm.SQLAnalysis.MySQL;
using Shouldly;

namespace Mstm.SQLAnalysis.UnitTests
{

    [TestClass]
    public class MySQLDbStructAnalysisProviderTests
    {

        IDbStructAnalysisProvider _provider;

        /// <summary>
        /// 单个测试开始前
        /// </summary>
        [TestInitialize]
        public void MethodInit()
        {
            ISQLAnalysisFactory factory = new MySQLAnalysisFactory();
            _provider = factory.GetSQLAnalysisProvider();
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
        /// 构建获取当前所有的数据库名的sql语句测试
        /// </summary>
        [TestMethod]
        public void BuildSelectDatabasesTest()
        {
            string sql = _provider.BuildSelectDatabases();
            string expectSql = "SELECT SCHEMA_NAME FROM information_schema.SCHEMATA";
            sql.ShouldBe(expectSql);
        }


        /// <summary>
        /// 构建获取指定数据库下的所有表名的sql语句测试
        /// </summary>
        [TestMethod]
        public void BuildSelectTablesTest()
        {
            string dbName = "userdb";
            string sql = _provider.BuildSelectTables(dbName);
            string expectSql = string.Format("SELECT `TABLE_NAME` FROM information_schema.`TABLES` WHERE `TABLE_SCHEMA`='{0}'", dbName);
            sql.ShouldBe(expectSql);
        }


        /// <summary>
        /// 构建获取当前表中所有字段信息的sql语句测试
        /// </summary>
        [TestMethod]
        public void BuildSelectFieldsTest()
        {
            string dbName = "userdb";
            string tableName = "userinfo";
            string sql = _provider.BuildSelectFields(dbName, tableName);
            string expectSql = string.Format("SELECT `COLUMN_NAME`,`DATA_TYPE` FROM information_schema.`COLUMNS` WHERE `TABLE_SCHEMA`='{0}' and `TABLE_NAME`='{1}'", dbName, tableName);
            sql.ShouldBe(expectSql);
        }
    }
}
