using Microsoft.Extensions.Configuration;
using Mstm.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Lock.Distributed.ZK
{
    public class ZKConfig : BaseConfig
    {
        public const string DefaultGroup = "DefaultLockerZK";
        public const string ModuleName = "Zookeeper";
        public string GroupName { get; private set; }

        private ZKConfig(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName)) { groupName = DefaultGroup; }
            GroupName = groupName;
        }

        public static ZKConfig New(string groupName = null)
        {
            return new ZKConfig(groupName);
        }

        public string ConnStr
        {
            get
            {
                return this.Config[string.Format("{0}:{1}:ConnStr", ModuleName, GroupName)];
            }
        }

        public string LockZKRoot
        {
            get
            {
                return this.Config[string.Format("{0}:{1}:LockZKRoot", ModuleName, GroupName)];
            }
        }

        public string LockZKValue
        {
            get
            {
                return this.Config[string.Format("{0}:{1}:LockZKValue", ModuleName, GroupName)];
            }
        }
    }
}
