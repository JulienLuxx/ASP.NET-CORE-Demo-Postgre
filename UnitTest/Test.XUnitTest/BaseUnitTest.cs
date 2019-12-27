using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Domain;
using Test.Service.Infrastructure;
using Test.Service.IOC;

namespace Test.XUnitTest
{
    public class BaseUnitTest
    {
        protected ServiceCollection _serviceCollection { get; set; }

        protected ServiceProvider _serviceProvider { get; set; }

        protected IConfigurationRoot _configuration { get; set; }

        protected BaseUnitTest()
        {
            _serviceCollection = new ServiceCollection();
            InitConfiguration();
            Init();
        }

        protected virtual void AddDbContext()
        {
            _serviceCollection.AddDbContext<TestDBContext>(options => options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));
        }

        protected virtual void BuilderServiceProvider()
        {
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        protected virtual void Init()
        {
            _serviceCollection.AddAutoMapper(typeof(CustomizeProfile));
            _serviceCollection.AddAllSvc();
            BuilderServiceProvider();
        }

        protected virtual void InitConfiguration()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
        }
    }
}
