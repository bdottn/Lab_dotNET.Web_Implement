using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using EntityOperation;
using EntityOperation.Protocol;
using Model.Mapper.OperationToView;
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

            // AutoMapper
            builder.RegisterAutoMapper(typeof(CustomerMapToCustomerInfo).Assembly);

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