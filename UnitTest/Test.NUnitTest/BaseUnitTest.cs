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
using Microsoft.Extensions.Configuration;
using Test.Domain;
using Microsoft.EntityFrameworkCore;

namespace Test.NUnitTest
{
    public class BaseUnitTest
    {
        protected IServiceCollection _serviceCollection { get; set; }

        protected IServiceProvider _serviceProvider { get; set; }

        protected IConfigurationRoot _configuration { get; set; }

        public BaseUnitTest()
        {
            _serviceCollection = new ServiceCollection();
            InitConfiguration();
        }

        protected virtual IServiceCollection AddAutoMapper()
        {
            return _serviceCollection.AddAutoMapper(typeof(CustomizeProfile));
        }

        protected virtual void AddDbContext()
        {
            _serviceCollection.AddDbContext<TestDBContext>(options => options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));
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

        protected virtual void InitConfiguration()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
        }
    }
}
