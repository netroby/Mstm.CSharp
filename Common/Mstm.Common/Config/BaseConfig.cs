using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Common.Config
{
    /// <summary>
    /// 配置文件操作基类
    /// </summary>
    public abstract class BaseConfig
    {
        /// <summary>
        /// 当前加载的配置文件信息
        /// </summary>
        protected IConfigurationRoot Config;

        /// <summary>
        /// 配置文件的名称，默认为appsettings.json
        /// </summary>
        public virtual string ConfigFile { get { return "appsettings.json"; } }


        /// <summary>
        /// 构造函数
        /// 加载配置文件并解析
        /// </summary>
        public BaseConfig()
        {
            Config = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile(ConfigFile).Build();
        }
    }
}
