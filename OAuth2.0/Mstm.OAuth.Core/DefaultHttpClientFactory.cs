using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.OAuth.Core
{

    /// <summary>
    /// 默认Http客户端工厂
    /// </summary>
    public class DefaultHttpClientFactory : IHttpClientFactory
    {

        public IHttpClient GetHttpClient()
        {
#if net47
            return new DefaultHttpClient();
#else
            throw new Exception("当前框架暂未实现对应的HttpClient");
#endif
        }
    }
}
