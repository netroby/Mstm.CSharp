using Mstm.Json.Core;
using Mstm.Json.Newtonsoft;
using Mstm.NoSQL.Redis.Core;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.NoSQL.Redis.StackExchange
{
    /// <summary>
    /// StackExchange的Redis客户端提供器
    /// </summary>
    public class StackExchangeRedisProvider : IRedisProvider
    {

        ConnectionMultiplexer _redisConn;
        IDatabase _redisDB;
        ISerializeProvider _serializeProvider;

        public StackExchangeRedisProvider(string connStr, int db = 0)
        {
            _serializeProvider = JsonSerializeProvider.GetProvider();

            if (string.IsNullOrWhiteSpace(connStr))
            {
                throw new ArgumentNullException("connStr", "Redis连接字符串配置错误！");
            }
            _redisConn = ConnectionMultiplexer.Connect(connStr);
            _redisDB = _redisConn.GetDatabase(db);
        }


        #region IRedisString

        public string GetString(string key)
        {
            return _redisDB.StringGet(key);
        }

        public void SetString(string key, string value)
        {
            _redisDB.StringSet(key, value);
        }

        public void SetData<T>(string key, T value)
        {
            var jsonStr = _serializeProvider.SerializeObject(value);
            SetString(key, jsonStr);
        }

        public T GetData<T>(string key)
        {
            try
            {
                string jsonStr = GetString(key);
                return string.IsNullOrWhiteSpace(jsonStr) ? default(T) : _serializeProvider.DeserializeObject<T>(jsonStr);
            }
            catch (Exception)
            {
                return default(T);
            }
        }


        public bool IsExistString(string key)
        {
            bool isExist = _redisDB.KeyExists(key);
            if (isExist)
            {
                isExist = _redisDB.KeyType(key) == RedisType.String;
            }
            return isExist;
        }

        public bool DeleteString(string key)
        {
            return _redisDB.KeyDelete(key);
        }

        public string AppendString(string key, string appendStr)
        {
            _redisDB.StringAppend(key, appendStr);
            return GetString(key);
        }


        public byte[] GetBytes(string key)
        {
            return _redisDB.StringGet(key);
        }


        public void SetBytes(string key, byte[] value)
        {
            _redisDB.StringSet(key, value);
        }


        #endregion



        #region IRedisKey

        public bool IsExistKey(string key)
        {
            return _redisDB.KeyExists(key);
        }


        public long DeleteKey(params string[] keys)
        {
            if (keys == null || keys.Length == 0) { return 0; }
            List<RedisKey> redisKeys = new List<RedisKey>();
            keys.ToList().ForEach((item) =>
            {
                redisKeys.Add(item);
            });
            return _redisDB.KeyDelete(redisKeys.ToArray());
        }

        public RedisDataType GetKeyType(string key)
        {
            return (RedisDataType)_redisDB.KeyType(key);
        }


        public string GetRandomKey()
        {
            return _redisDB.KeyRandom();
        }

        public int GetTTL(string key)
        {
            var timeSpan = _redisDB.KeyTimeToLive(key);
            return timeSpan == null ? 0 : (int)Math.Ceiling(timeSpan.Value.TotalSeconds);
        }

        public bool PersistKey(string key)
        {
            return _redisDB.KeyPersist(key);
        }

        public bool ReNameKeyWithCover(string oldKey, string newKey)
        {
            return _redisDB.KeyRename(oldKey, newKey, When.Always);
        }

        public bool ReNameKeyWithOutCover(string oldKey, string newKey)
        {
            return _redisDB.KeyRename(oldKey, newKey, When.NotExists);
        }

        public bool SetExpireKey(string key, int seconds)
        {
            return _redisDB.KeyExpire(key, TimeSpan.FromSeconds(seconds));
        }

        public bool SetExpireatKey(string key, long timestamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime time = startTime.AddMilliseconds(timestamp);
            return _redisDB.KeyExpire(key, time);
        }

        #endregion
    }
}
