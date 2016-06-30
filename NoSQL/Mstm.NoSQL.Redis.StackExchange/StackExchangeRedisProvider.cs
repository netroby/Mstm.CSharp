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

        public StackExchangeRedisProvider(string connStr, int db)
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
            RedisKey[] redisKeys = keys.Cast<RedisKey>().ToArray();
            return _redisDB.KeyDelete(redisKeys);
        }

        public RedisDataType GetKeyType(string key)
        {
            return (RedisDataType)_redisDB.KeyType(key);
        }

        public List<string> GetKeys(string pattern)
        {
            throw new NotImplementedException();
        }

        public string GetRandomKey()
        {
            return _redisDB.KeyRandom();
        }

        public int GetTTL(string key)
        {
            throw new NotImplementedException();
        }

        public bool PersistKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool ReNameKeyWithCover(string oldKey, string newKey)
        {
            throw new NotImplementedException();
        }

        public bool ReNameKeyWithOutCover(string oldKey, string newKey)
        {
            throw new NotImplementedException();
        }

        public bool SetExpireKey(string key, int seconds)
        {
            throw new NotImplementedException();
        }

        public bool SetExpireatKey(string key, int timestamp)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
