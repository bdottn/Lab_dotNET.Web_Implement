using EntityOperation.Protocol;
using Operation.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Caching;
using System.Xml.Linq;

namespace EntityOperation
{
    sealed class Config : IConfig
    {
        // 設計模式：Singleton
        private static readonly Lazy<Config> lazySingleton = new Lazy<Config>(() => new Config());

        private static ObjectCache cache = MemoryCache.Default;

        private const string configKey = "LabConfig";

        private readonly string configPath;

        private Config()
        {
            string configPath = ConfigurationManager.AppSettings[configKey];

            // 不同類型專案取得 bin 目錄位置的方法不一樣。WinForm、UnitTest 專案用 BaseDirectory；WebApi、Mvc 專案用 RelativeSearchPath
            // RelativeSearchPath 在 WinForm、UnitTest 專案會是 null，可以用來判斷專案類型
            var binDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;

            this.configPath = Path.IsPathRooted(configPath) ? configPath : Path.Combine(binDirectory, configPath);
        }

        private XDocument ConfigDocument
        {
            get
            {
                if (cache[configKey] == null)
                {
                    this.SetCachePolicy();
                }

                return (XDocument)cache[configKey];
            }
        }

        private void SetCachePolicy()
        {
            var cachePolicy = new CacheItemPolicy();

            // 設定快取的清除規則：當監控的檔案內容更新時，清除快取
            cachePolicy.ChangeMonitors.Add(new HostFileChangeMonitor(new List<string>() { this.configPath, }));

            cache.Set(configKey, XDocument.Load(this.configPath), cachePolicy);
        }

        internal static Config Instance
        {
            get
            {
                return lazySingleton.Value;
            }
        }

        public string MSSQLConnectionString
        {
            get
            {
                return this.ConfigDocument.Root.Element("MSSQLConnectionString").Value;
            }
        }

        public WebAPICredential AcceptedCredential
        {
            get
            {
                var element = this.ConfigDocument.Root.Element("AcceptedCredential");

                return
                    new WebAPICredential()
                    {
                        Key = element.Attribute("UserName").Value,
                        Value = element.Attribute("Password").Value,
                    };
            }
        }
    }
}