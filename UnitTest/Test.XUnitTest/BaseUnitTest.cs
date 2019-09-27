using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        protected BaseUnitTest()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddAutoMapper(typeof(CustomizeProfile));
            _serviceCollection.AddAllSvc();
        }

        protected void AddDbContext()
        {
            _serviceCollection.AddDbContext<TestDBContext>(options => options.UseNpgsql("Host=47.244.228.240;Port=5233;Database=TestDB;Username=root;Password=2134006;"));
        }

        protected void BuilderServiceProvider()
        {
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }
    }
}
