using Autofac;
using Autofac.Integration.WebApi;
using EntityOperation;
using EntityOperation.Protocol;
using Service;
using Service.Protocol;
using System.Reflection;
using System.Web.Http;

namespace WebAPI_Implement
{
    static class AutofacConfiguration
    {
        public static void Configure(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // EntityOperation
            builder.RegisterGeneric(typeof(SQLRepository<>)).As(typeof(ISQLRepository<>));

            // Service
            builder.RegisterType<CustomerService>().As<ICustomerService>();

            // WebAPI_Implement
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}