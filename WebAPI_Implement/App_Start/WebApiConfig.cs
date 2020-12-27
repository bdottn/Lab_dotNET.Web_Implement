using System.Web.Http;

namespace WebAPI_Implement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // 屬性路由對應
            config.MapHttpAttributeRoutes();
        }
    }
}