using log4net;
using System;
using System.IO;

namespace Appium.WeChat.WebAPI.Tools
{
    public static class Loger
    {
        public static readonly ILog logger = LogManager.GetLogger("Default");

        static Loger()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs\\log4net.config")));
            logger.Info("日志工具初始化...");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        public static void Info(object message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Info(message);
            }
            else
            {
                logger.Info(message, ex);
            }
        }

        public static void Debug(object message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Debug(message);
            }
            else
            {
                logger.Debug(message, ex);
            }
        }

        public static void Error(object message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Error(message);
            }
            else
            {
                logger.Error(message, ex);
            }
        }

        public static void Fatal(object message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Fatal(message);
            }
            else
            {
                logger.Fatal(message, ex);
            }
        }
    }
}
