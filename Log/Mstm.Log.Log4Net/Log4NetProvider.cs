using log4net;
using log4net.Config;
using log4net.Repository;
using Mstm.Log.Core;
using System;
using System.IO;

namespace Mstm.Log.Log4Net
{
    /// <summary>
    /// ILogProvider日志组件实现类，使用Log4Net实现
    /// </summary>
    public class Log4NetProvider : ILogProvider
    {
        private static ILoggerRepository repository;
        log4net.ILog innerLogger;
        /// <summary>
        /// log4net配置文件名称
        /// </summary>
        public const string Log4NetConfigFile = "log4net.config";

        /// <summary>
        /// 静态构造函数
        /// 加载log4net配置
        /// </summary>
        static Log4NetProvider()
        {
            repository = LogManager.CreateRepository("Log4NetProviderRepository");
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string fileFullPath = basePath + Log4NetConfigFile;
            if (basePath.EndsWith("/") == false && basePath.EndsWith(@"\") == false)
            {
                //Win32NT Unix
                string platform = Environment.OSVersion.Platform.ToString();
                if (platform == "Win32NT")
                {
                    fileFullPath = basePath + @"\" + Log4NetConfigFile;
                }
                else
                {
                    fileFullPath = basePath + "/" + Log4NetConfigFile;
                }
            }
            var file = new FileInfo(fileFullPath);
            XmlConfigurator.Configure(repository, file);
        }

        /// <summary>
        /// 构造寒素
        /// </summary>
        /// <param name="type">当前日志操作所在的类型</param>
        public Log4NetProvider(Type type)
        {
            innerLogger = LogManager.GetLogger(repository.Name, type);
        }

        #region 具体日志操作

        public void Debug(object msg)
        {
            innerLogger.Debug(msg);
        }

        public void Debug(object msg, Exception ex)
        {
            innerLogger.Debug(msg, ex);
        }

        public void DebugFormat(string log, params object[] args)
        {
            innerLogger.DebugFormat(log, args);
        }

        public void Error(object msg)
        {
            innerLogger.Error(msg);
        }

        public void Error(object msg, Exception ex)
        {
            innerLogger.Error(msg, ex);
        }

        public void ErrorFormat(string msg, params object[] args)
        {
            innerLogger.ErrorFormat(msg, args);
        }

        public void Fatal(object msg)
        {
            innerLogger.Fatal(msg);
        }

        public void Fatal(object msg, Exception ex)
        {
            innerLogger.Fatal(msg, ex);
        }

        public void FatalFormat(string msg, params object[] args)
        {
            innerLogger.FatalFormat(msg, args);
        }

        public void Info(object msg)
        {
            innerLogger.Info(msg);
        }

        public void Info(object msg, Exception ex)
        {
            innerLogger.Info(msg, ex);
        }

        public void InfoFormat(string msg, params object[] args)
        {
            innerLogger.InfoFormat(msg, args);
        }

        public void Warn(object msg)
        {
            innerLogger.Warn(msg);
        }

        public void Warn(object msg, Exception ex)
        {
            innerLogger.Warn(msg, ex);
        }

        public void WarnFormat(string msg, params object[] args)
        {
            innerLogger.WarnFormat(msg, args);
        }

        #endregion
    }
}
