﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05工厂方法模式
{
    class AliPayOAuthServiceFactory : IFactory
    {

        public IOAuthService GetOAuthService()
        {
            return new AliPayOAuthService();
        }
    }
}
