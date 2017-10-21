using Mstm.Json.Core;
using Mstm.Json.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.OAuth.Core
{
#if net47
    /// <summary>
    /// 默认Http客户端
    /// 使用微软的HttpClient和Microsoft.AspNet.WebApi.Client相关扩展实现
    /// </summary>
    public class DefaultHttpClient : IHttpClient
    {

        HttpClient client;
        ISerializeProvider jsonProvider;

        public DefaultHttpClient()
        {
            jsonProvider = JsonSerializeProvider.GetProvider();
            client = new HttpClient();
        }

        public TResult GetAsJson<TResult>(string requestUrl)
        {
            HttpResponseMessage response = null;
            response = client.GetAsync(requestUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<TResult>().Result;
                return result;
            }
            else
            {
                var strException = Encoding.UTF8.GetString(response.Content.ReadAsByteArrayAsync().Result);
                throw new Exception(strException);
            }
        }

        public TResult PostAsJson<TResult, TRequestBody>(string requestUrl, TRequestBody data)
        {
            HttpResponseMessage response = null;
            response = client.PostAsJsonAsync<TRequestBody>(requestUrl, data).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<TResult>().Result;
                return result;
            }
            else
            {
                var strException = Encoding.UTF8.GetString(response.Content.ReadAsByteArrayAsync().Result);
                throw new Exception(strException);
            }
        }

        public string GetString(string requestUrl)
        {
            HttpResponseMessage response = null;
            response = client.GetAsync(requestUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            else
            {
                var strException = Encoding.UTF8.GetString(response.Content.ReadAsByteArrayAsync().Result);
                throw new Exception(strException);
            }
        }


        public TResult Post<TResult>(string requestUrl, Dictionary<string, string> data)
        {
            HttpContent content = null;
            if (data == null)
            {
                data = new Dictionary<string, string>();
            }
            content = new FormUrlEncodedContent(data);

            HttpResponseMessage response = null;
            response = client.PostAsync(requestUrl, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var str = response.Content.ReadAsStringAsync().Result;
                TResult result = jsonProvider.DeserializeObject<TResult>(str);
                return result;
            }
            else
            {
                var strException = Encoding.UTF8.GetString(response.Content.ReadAsByteArrayAsync().Result);
                throw new Exception(strException);
            }

        }
    }
#endif
}
