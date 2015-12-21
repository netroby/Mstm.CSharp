using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03装饰模式
{
    /// <summary>
    /// 我们原有一个抽象类Person和他的两个实现Teacher和Doctor
    /// 这里利用装饰模式对其以包装的形式进行扩展
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Doctor("薇薇");
            ClothesA cA = new ClothesA();
            cA.SetComponent(p);
            ClothesB cB = new ClothesB();
            cB.SetComponent(cA);
            ClothesC cC = new ClothesC();
            cC.SetComponent(cB);
            cC.Wear();

            Console.WriteLine("-------------------");

            Person p2 = new Teacher("Bob");
            cB.SetComponent(p2);
            cC.SetComponent(cB);
            cA.SetComponent(cC);
            cA.Wear();
            Console.ReadLine();
        }
    }
}
