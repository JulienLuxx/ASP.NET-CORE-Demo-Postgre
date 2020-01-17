using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Core.IOC;
using Test.Domain;
using Test.Domain.IOC;
using Test.Service.Infrastructure;
using Test.Service.IOC;

namespace Test.XUnitTest
{
    public class BaseUnitTest
    {
        protected IServiceCollection _serviceCollection { get; set; }

        protected IServiceProvider _serviceProvider { get; set; }

        protected IConfigurationRoot _configuration { get; set; }

        protected BaseUnitTest()
        {
            _serviceCollection = new ServiceCollection();
            InitConfiguration();
            //Init();
            //AutofacInit();
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

        protected virtual void Init()
        {
            _serviceCollection.AddAutoMapper(typeof(CustomizeProfile));
            _serviceCollection.AddAllSvc();
            BuilderServiceProvider();
        }

        protected virtual IContainer  AutofacInit()
        {
            _serviceCollection.AddAutoMapper(typeof(CustomizeProfile));
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
