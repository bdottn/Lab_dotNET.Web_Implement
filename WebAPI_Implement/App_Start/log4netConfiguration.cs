using log4net.Config;
using System;
using System.Configuration;
using System.IO;

namespace WebAPI_Implement
{
    /// <summary>
    /// log4net 組態設定檔
    /// </summary>
    static class log4netConfiguration
    {
        /// <summary>
        /// 註冊
        /// </summary>
        public static void Register()
        {
            var configPath = ConfigurationManager.AppSettings["log4net"].ToString();

            var fullPath =
                Path.IsPathRooted(configPath)
                    ? configPath
                    : Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath, configPath);

            XmlConfigurator.ConfigureAndWatch(new FileInfo(fullPath));
        }
    }
}