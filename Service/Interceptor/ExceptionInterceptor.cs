using Castle.DynamicProxy;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Service.Model;
using System;
using System.Collections.Generic;

namespace Service.Interceptor
{
    /// <summary>
    /// 例外攔截器
    /// </summary>
    sealed class ExceptionInterceptor : IInterceptor
    {
        private readonly ILog log;

        public ExceptionInterceptor()
        {
            this.log = LogManager.GetLogger(typeof(ExceptionInterceptor));
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();

                this.LogMessage(invocation, invocation.ReturnValue as ServiceResult);
            }
            catch (Exception ex)
            {
                var errorMessages = new List<string>();

                while (ex != null)
                {
                    errorMessages.Add(ex.Message);
                    ex = ex.InnerException;
                }

                var serviceResult = Activator.CreateInstance(invocation.Method.ReturnType) as ServiceResult;

                serviceResult.ResultType = ServiceResultType.Exception;
                serviceResult.Message = string.Join("。", errorMessages);

                invocation.ReturnValue = serviceResult;

                this.LogMessage(invocation, serviceResult);
            }
        }

        private void LogMessage(IInvocation invocation, ServiceResult serviceResult)
        {
            var messages = new List<string>()
            {
                "Service",
                $"{invocation.TargetType.Name}.{invocation.Method.Name}",
                serviceResult.ResultType == ServiceResultType.Success ? "成功": serviceResult.Message,
            };

            // Log 層級設在 DEBUG 或 ALL 時，記錄參數。Log 層級設在 INFO 以上時，非 Success 的結果才記錄參數
            if (this.log.IsDebugEnabled || serviceResult.ResultType != ServiceResultType.Success)
            {
                messages.Add(FormatArgs(invocation));
            }

            // Service | MethodType.MethodName | ServiceMessage | Arg
            var message = string.Join(" | ", messages);

            switch (serviceResult.ResultType)
            {
                case ServiceResultType.Success:
                    this.log.Info(message);
                    break;

                case ServiceResultType.Warning:
                    this.log.Warn(message);
                    break;

                case ServiceResultType.Fail:
                    this.log.Error(message);
                    break;

                case ServiceResultType.Exception:
                    this.log.Fatal(message);
                    break;

                default:
                    break;
            }
        }

        private static string FormatArgs(IInvocation invocation)
        {
            if (invocation.Arguments == null || invocation.Arguments.Length == 0)
            {
                return null;
            }

            var args = new Dictionary<string, object>();
            var methodParams = invocation.Method.GetParameters();

            for (int idx = 0; idx < invocation.Arguments.Length; idx++)
            {
                args.Add(methodParams[idx].Name, invocation.Arguments[idx]);
            }

            return JsonConvert.SerializeObject(args, new StringEnumConverter());
        }
    }
}