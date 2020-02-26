using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Test.Service.Infrastructure;
using Autofac;
using Test.Core.IOC;
using Test.Domain.IOC;
using Test.Service.IOC;
using Autofac.Extensions.DependencyInjection;

namespace Test.NUnitTest
{
    public class BaseUnitTest
    {
        protected IServiceCollection _serviceCollection { get; set; }

        protected IServiceProvider _serviceProvider { get; set; }

        public BaseUnitTest()
        {
            _serviceCollection = new ServiceCollection();
        }

        protected virtual IServiceCollection AddAutoMapper()
        {
            return _serviceCollection.AddAutoMapper(typeof(CustomizeProfile));
        }

        protected virtual void BuilderServiceProvider()
        {
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        protected virtual void BuilderServiceProvider(IContainer container)
        {
            _serviceProvider = new AutofacServiceProvider(container);
        }

        protected virtual IContainer InitByAutofac()
        {
            AddAutoMapper();
            var builder = new ContainerBuilder();
            builder.RegisterModule<UtilModule>();
            builder.RegisterModule<DomainServiceModule>();
            builder.RegisterModule<ServiceModule>();
            builder.Populate(_serviceCollection);
            return builder.Build();
        }
    }
}
