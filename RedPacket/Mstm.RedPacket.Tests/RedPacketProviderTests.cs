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
            decimal amount = 300 * 300 + 21;
            //100 * 81 + 17  有3.32  小于最小值3.33
            int packageCount = 100 * 15 + 16;
            decimal ceiling = 21.45M;
            decimal floor = 30;
            int errorCount = 0;
            decimal currentAmount = 0;
            int currentPackageCount = 0;
            //均分红包
            decimal avgPkg = Math.Round(amount / packageCount, 2);
            //最大红包
            decimal maxPkg = Math.Round(avgPkg * (100 + ceiling) / 100, 2);
            //最小红包
            decimal minPkg = Math.Round(avgPkg * (100 - floor) / 100, 2);

            List<decimal> errorList = new List<decimal>();

            while (true)
            {
                var money = RedPacketProvider.GetOneRedPacket(amount, currentAmount, packageCount, currentPackageCount, ceiling, floor);
                if (money <= 0) { break; }
                if (money > maxPkg || money < minPkg)
                {
                    errorList.Add(money);
                    errorCount++;
                }
                currentAmount += money;
                currentPackageCount++;
                System.Console.WriteLine(money + "");
            }

            Assert.AreEqual(amount, currentAmount);
            Assert.IsTrue(errorCount == 0);
        }
    }
}
