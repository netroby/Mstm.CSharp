using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.NoSQL.Redis.Core
{

    /// <summary>
    /// Json.Net序列化器实现
    /// </summary>
    public class JsonSerializeProvider : ISerializeProvider
    {
        private static ISerializeProvider _provider;

        private JsonSerializeProvider() { }

        public static ISerializeProvider GetProvider()
        {
            if (_provider == null)
            {
                _provider = new JsonSerializeProvider();
            }
            return _provider;
        }
        public string SerializeObject<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public T DeserializeObject<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
    }
}
