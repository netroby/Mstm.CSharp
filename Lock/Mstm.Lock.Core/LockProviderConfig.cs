using Mstm.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Lock.Core
{
    public class LockProviderConfig : BaseProviderConfig
    {
        public override string ModuleName => "LockProvider";

        private LockProviderConfig(string groupName)
            : base(groupName)
        {

        }

        /// <summary>
        /// 创建一个新的配置
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static LockProviderConfig New(string groupName = null)
        {
            return new LockProviderConfig(groupName);
        }
    }
}
