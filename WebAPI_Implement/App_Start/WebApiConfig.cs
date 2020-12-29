using System.Web.Http;

namespace WebAPI_Implement
{
    /// <summary>
    /// WebApi 組態設定檔
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // 屬性路由對應
            config.MapHttpAttributeRoutes();
        }
    }
}