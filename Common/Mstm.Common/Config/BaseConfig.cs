using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Common.Config
{
    public abstract class BaseConfig
    {
        protected IConfigurationRoot Config;
        public virtual string ConfigFile { get { return "appsettings.json"; } }

        public BaseConfig()
        {
            Config = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile(ConfigFile).Build();
        }
    }
}
