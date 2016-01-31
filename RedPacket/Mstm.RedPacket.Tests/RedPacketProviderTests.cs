using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mstm.RedPacket.Core;
using System.Collections.Generic;

namespace Mstm.RedPacket.Tests
{
    [TestClass]
    public class RedPacketProviderTests
    {
        [TestMethod]
        public void GetOneRedPacketTestLoop()
        {
            for (int i = 0; i < 100; i++)
            {
                GetOneRedPacketTest();
                RedPacketProvider.Reset();
            }
        }

        [TestMethod]
        public void GetOneRedPacketTestSingle()
        {
            GetOneRedPacketTest();
        }

        private void GetOneRedPacketTest()
        {
            Func<RedPacketConfig> func = new Func<RedPacketConfig>(GetRedPacketConfig);
            var config = func.Invoke();
            int errorCount = 0;
            List<decimal> errorList = new List<decimal>();

            //均分红包
            decimal avgPkg = Math.Round(config.Amount / config.PacketCount, 2);
            //最大红包
            decimal maxPkg = Math.Round(avgPkg * (100 + config.Ceiling) / 100, 2);
            //最小红包
            decimal minPkg = Math.Round(avgPkg * (100 - config.Floor) / 100, 2);


            while (true)
            {
                var money = RedPacketProvider.GetOneRedPacket(func);
                if (money <= 0) { break; }
                if (money > maxPkg || money < minPkg)
                {
                    errorList.Add(money);
                    errorCount++;
                }
                config.CurrentAmount += money;
                config.CurrentPackageCount++;
                System.Console.WriteLine(money + "");
            }

            Assert.AreEqual(config.Amount, config.CurrentAmount);
            Assert.IsTrue(errorCount == 0);
        }




        /// <summary>
        /// 获取红包配置
        /// </summary>
        /// <returns></returns>
        RedPacketConfig GetRedPacketConfig()
        {
            //当前已发红包总金额
            decimal currentAmount = 0;
            //当前已发红包总数
            int currentPackageCount = 0;


            //总金额
            decimal amount = 300 * 300 + 100;
            //100 * 81 + 17  有3.32  小于最小值3.33
            //总红包数
            int packageCount = 250;
            //上限
            decimal ceiling = 20;
            //下限
            decimal floor = 30;
            //均分红包
            decimal avgPkg = Math.Round(amount / packageCount, 2);
            //最大红包
            decimal maxPkg = Math.Round(avgPkg * (100 + ceiling) / 100, 2);
            //最小红包
            decimal minPkg = Math.Round(avgPkg * (100 - floor) / 100, 2);

            RedPacketConfig config = new RedPacketConfig()
            {
                Amount = amount,
                PacketCount = packageCount,
                CurrentAmount = currentAmount,
                CurrentPackageCount = currentPackageCount,
                Ceiling = ceiling,
                Floor = floor
            };
            return config;


        }
    }
}
