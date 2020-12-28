using System.Web;
using System.Web.Http;
using WebAPI_Implement.Interceptor;

namespace WebAPI_Implement
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AutofacConfiguration.Configure(GlobalConfiguration.Configuration);

            log4netConfiguration.Register();

            GlobalConfiguration.Configuration.Filters.Add(new ActionLogAttribute());
        }
    }
}