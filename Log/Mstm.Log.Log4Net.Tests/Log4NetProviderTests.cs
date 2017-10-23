using Mstm.Log.Core;
using System;
using Xunit;

namespace Mstm.Log.Core.Test
{
    public class Log4NetProviderTests
    {
        [Fact]
        public void GetLoggerTest()
        {
            var logger = LogFactory.GetProvider<Log4NetProviderTests>();
            logger.Info("hello Info");
            logger.Debug("hello Debug");
            logger.Warn("hello Warn");
            logger.Error("hello Error");
            logger.Fatal("hello Fatal");
        }
    }
}
