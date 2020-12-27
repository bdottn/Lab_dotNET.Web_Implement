using System.Web;
using System.Web.Http;

namespace WebAPI_Implement
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AutofacConfiguration.Configure(GlobalConfiguration.Configuration);
        }
    }
}