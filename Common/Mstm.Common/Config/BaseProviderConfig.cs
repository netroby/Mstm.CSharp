using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Common.Config
{
    public abstract class BaseProviderConfig : BaseConfig
    {
        public virtual string DefaultGroupName { get { return "Default"; } }
        public abstract string ModuleName { get; }
        public string GroupName { get; private set; }

        public BaseProviderConfig(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName)) { groupName = DefaultGroupName; }
            GroupName = groupName;
        }

        public string AssemblyName
        {
            get { return Config[string.Format("{0}:{1}:AssemblyName", ModuleName, GroupName)]; }
        }

        public string ClassFullName
        {
            get { return Config[string.Format("{0}:{1}:ClassFullName", ModuleName, GroupName)]; }
        }
    }
}
