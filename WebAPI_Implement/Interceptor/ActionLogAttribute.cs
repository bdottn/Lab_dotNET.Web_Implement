using log4net;
using log4net.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI_Implement.Interceptor
{
    /// <summary>
    /// Action 攔截器
    /// </summary>
    sealed class ActionLogAttribute : ActionFilterAttribute
    {
        private readonly ILog log;

        public ActionLogAttribute()
        {
            this.log = LogManager.GetLogger(typeof(ActionLogAttribute));
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            this.LogMessage(Level.Info, "接收請求", actionContext);

            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // 發生例外時才會有值，因 Service 層以下有攔截器處理，正常只會捕捉到 Controller 的例外
            var exception = actionExecutedContext.Exception;

            if (exception != null)
            {
                var errorMessages = new List<string>();

                while (exception != null)
                {
                    errorMessages.Add(exception.Message);
                    exception = exception.InnerException;
                }

                this.LogMessage(Level.Fatal, $"請求處理發生例外：{string.Join("。", errorMessages)}", actionExecutedContext.ActionContext);
            }
            else if (actionExecutedContext.Response.IsSuccessStatusCode)
            {
                // Response 為 2xx 時的情況
                this.LogMessage(Level.Info, "請求處理成功", actionExecutedContext.ActionContext);
            }
            else
            {
                // Response 不為 2xx 時的情況
                var responseMessage = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;

                this.LogMessage(Level.Error, $"請求處理失敗：{responseMessage}", actionExecutedContext.ActionContext);
            }

            base.OnActionExecuted(actionExecutedContext);
        }

        private void LogMessage(Level level, string message, HttpActionContext actionContext)
        {
            var requestType = actionContext.Request.Method.Method;
            var requestUri = HttpUtility.UrlDecode(actionContext.Request.RequestUri.OriginalString);

            var messages = new List<string>()
            {
                "WebAPI",
                $"{actionContext.ActionDescriptor.ControllerDescriptor.ControllerType.Name}.{actionContext.ActionDescriptor.ActionName}",
                $"{message}【{requestType}】{requestUri}"
            };

            if (actionContext.ActionArguments != null)
            {
                messages.Add(JsonConvert.SerializeObject(actionContext.ActionArguments, new StringEnumConverter()));
            }

            // WebAPI | ControllerType.ActionName | Message【RequestType】RequestUri | Arg
            var messageString = string.Join(" | ", messages);

            if (level == Level.Info)
            {
                this.log.Info(messageString);
            }
            else if (level == Level.Error)
            {
                this.log.Error(messageString);
            }
            else if (level == Level.Fatal)
            {
                this.log.Fatal(messageString);
            }
        }
    }
}