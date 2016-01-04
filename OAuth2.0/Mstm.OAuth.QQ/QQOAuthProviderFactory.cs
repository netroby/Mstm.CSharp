﻿using Mstm.OAuth.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.OAuth.QQ
{
    /// <summary>
    /// QQOAuth服务共工厂
    /// </summary>
    public class QQOAuthProviderFactory : IOAuthProviderFactory
    {

        public IOAuthProvider GetThirdPartyProvider()
        {
            return new QQOAuthProvider();
        }
    }
}
