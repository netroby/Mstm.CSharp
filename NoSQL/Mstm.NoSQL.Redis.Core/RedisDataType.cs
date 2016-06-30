using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.NoSQL.Redis.Core
{

    /// <summary>
    /// Redis数据类型枚举
    /// </summary>
    public enum RedisDataType
    {
        None = 0,
        String = 1,
        List = 2,
        Set = 3,
        SortedSet = 4,
        Hash = 5,
        Unknown = 6,
    }
}
