using Microsoft.Extensions.Configuration;
using Mstm.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Log.Core
{
    /// <summary>
    /// 日志配置信息
    /// </summary>
    public class LogConfig : BaseProviderConfig
    {
        /// <summary>
        /// 当前模块名称
        /// </summary>
        public override string ModuleName => "Logger";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groupName">组名称</param>
        private LogConfig(string groupName)
            : base(groupName)
        {
        }

        /// <summary>
        /// 创建一个新的配置
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <returns></returns>
        public static LogConfig New(string groupName = null)
        {
            return new LogConfig(groupName);
        }
    }
}
