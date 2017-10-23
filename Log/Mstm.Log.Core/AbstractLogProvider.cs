using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Log.Core
{
    public abstract class AbstractLogProvider : ILogProvider
    {

        /// <summary>
        /// 日志组件需要的配置文件名称
        /// </summary>
        public string LogConfigFile { get; private set; }

        public AbstractLogProvider(string logConfigFile)
        {
            LogConfigFile = logConfigFile;
        }

        #region ILogProvider
        public abstract void Debug(object msg);
        public abstract void Debug(object msg, Exception ex);
        public abstract void DebugFormat(string msg, params object[] args);
        public abstract void Error(object msg);
        public abstract void Error(object msg, Exception ex);
        public abstract void ErrorFormat(string msg, params object[] args);
        public abstract void Fatal(object msg);
        public abstract void Fatal(object msg, Exception ex);
        public abstract void FatalFormat(string msg, params object[] args);
        public abstract void Info(object msg);
        public abstract void Info(object msg, Exception ex);
        public abstract void InfoFormat(string msg, params object[] args);
        public abstract void Warn(object msg);
        public abstract void Warn(object msg, Exception ex);
        public abstract void WarnFormat(string msg, params object[] args);
        #endregion
    }
}
