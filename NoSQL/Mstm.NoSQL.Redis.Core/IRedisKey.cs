using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.NoSQL.Redis.Core
{

    /// <summary>
    /// 提供Key相关操作接口
    /// </summary>
    public interface IRedisKey
    {
        /// <summary>
        /// 判断指定的键是否已经存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>键是否存在</returns>
        bool IsExistKey(string key);
    }
}
