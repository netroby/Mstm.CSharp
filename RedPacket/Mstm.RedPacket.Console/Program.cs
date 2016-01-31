﻿using Mstm.RedPacket.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mstm.RedPacket.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            RunWithMultiThread();
        }


        /// <summary>
        /// 单一线程下运行
        /// </summary>
        static void RunWithSingleThread()
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
            System.Console.WriteLine("Sum:{0},Max:{1},Min:{2},Avg:{3},Error:{4},Count:{5}", Math.Round(currentAmount, 2), maxPkg, minPkg, avgPkg, errorCount, currentPackageCount);
            foreach (var item in errorList)
            {
                //System.Console.WriteLine("错误:{0}",item);
            }
            System.Console.ReadKey();
        }


        /// <summary>
        /// 多线程下运行
        /// </summary>
        static void RunWithMultiThread()
        {
            decimal amount = 300 * 300 + 100;
            //100 * 81 + 17  有3.32  小于最小值3.33
            int packageCount = 250;
            decimal ceiling = 20;
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

            //var m = RedPacketProvider.GetOneRedPacket(amount, currentAmount, packageCount, currentPackageCount, ceiling, floor);
            //System.Console.WriteLine(m + "");

            for (int i = 0; i < packageCount + 10; i++)
            {
                Thread thread = new Thread(() =>
                {
                    var money = RedPacketProvider.GetOneRedPacket(amount, currentAmount, packageCount, currentPackageCount, ceiling, floor);
                    currentAmount += money;
                    currentPackageCount++;
                    System.Console.WriteLine(money + "");
                    if (i == 100)
                    {
                        RedPacketProvider.Reset();
                    }

                    if (i == 60)
                    {
                        RedPacketProvider.Reset();
                    }

                    if (i == 210)
                    {
                        RedPacketProvider.Reset();
                    }
                });

                thread.Start();
            }

            System.Console.WriteLine("Sum:{0}", currentAmount);
            System.Console.ReadKey();
        }

    }
}
