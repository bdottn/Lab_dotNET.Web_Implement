using Autofac;
using Autofac.Extras.DynamicProxy;
using Autofac.Integration.WebApi;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using EntityOperation;
using EntityOperation.Protocol;
using Service;
using Service.Interceptor;
using Service.Protocol;
using System.Reflection;
using System.Web.Http;

namespace WebAPI_Implement
{
    /// <summary>
    /// Autofac 組態設定檔
    /// </summary>
    static class AutofacConfiguration
    {
        /// <summary>
        /// 配置
        /// </summary>
        public static void Configure(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // AutoMapper
            builder.RegisterAutoMapper(Assembly.Load("Model.Mapper"));

            // EntityOperation
            builder.RegisterInstance(Config.Instance).As<IConfig>();
            builder.RegisterGeneric(typeof(SQLQueryOperation<>)).As(typeof(ISQLQueryOperation<>));
            builder.RegisterGeneric(typeof(SQLRepository<>)).As(typeof(ISQLRepository<>));

            // Service
            builder.RegisterType<ExceptionInterceptor>();
            builder.RegisterType<CertificationService>().As<ICertificationService>().EnableInterfaceInterceptors().InterceptedBy(typeof(ExceptionInterceptor)); ;
            builder.RegisterType<CustomerService>().As<ICustomerService>().EnableInterfaceInterceptors().InterceptedBy(typeof(ExceptionInterceptor)); ;

            // WebAPI_Implement
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}