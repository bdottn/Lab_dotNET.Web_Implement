﻿using Autofac;
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
    static class AutofacConfiguration
    {
        public static void Configure(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // AutoMapper
            builder.RegisterAutoMapper(Assembly.Load("Model.Mapper"));

            // EntityOperation
            builder.RegisterGeneric(typeof(SQLRepository<>)).As(typeof(ISQLRepository<>));
            builder.RegisterGeneric(typeof(SQLQueryOperation<>)).As(typeof(ISQLQueryOperation<>));

            // Service
            builder.RegisterType<ExceptionInterceptor>();
            builder.RegisterType<CustomerService>().As<ICustomerService>().EnableInterfaceInterceptors().InterceptedBy(typeof(ExceptionInterceptor)); ;

            // WebAPI_Implement
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}