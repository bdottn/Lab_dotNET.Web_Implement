using Service.Model;
using Service.Protocol;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI_Implement.Interceptor
{
    /// <summary>
    /// Authorization 攔截器
    /// </summary>
    sealed class AuthorizationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var service = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ICertificationService)) as ICertificationService;

            var authorization = actionContext.Request.Headers.Authorization;

            if (authorization?.Parameter == null || authorization.Scheme != "Basic")
            {
                this.UnauthorizedContext(actionContext);

                return;
            }

            var result = service.Authenticate(authorization.Parameter);

            if (result.ResultType == ServiceResultType.Success)
            {
                base.OnAuthorization(actionContext);
            }
            else
            {
                this.UnauthorizedContext(actionContext);

                return;
            }
        }

        private void UnauthorizedContext(HttpActionContext actionContext)
        {
            var host = actionContext.Request.RequestUri.DnsSafeHost;

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", $"Basic realm=\"{host}\"");
        }
    }
}