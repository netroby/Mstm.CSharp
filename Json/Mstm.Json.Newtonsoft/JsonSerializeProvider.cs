using Mstm.Json.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Json.Newtonsoft
{

    /// <summary>
    /// Json.Net序列化器实现
    /// </summary>
    public class JsonSerializeProvider : ISerializeProvider
    {
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
