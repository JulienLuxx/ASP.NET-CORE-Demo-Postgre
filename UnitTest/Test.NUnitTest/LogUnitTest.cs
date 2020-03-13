using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Service.Interface;
using Test.Service.QueryModel;

namespace Test.NUnitTest
{
    [TestFixture]
    public class LogUnitTest : BaseUnitTest
    {
        private readonly ILogSvc _logSvc;

        public LogUnitTest() : base()
        {
            AddDbContext();
            BuilderServiceProvider(InitByAutofac());
            _logSvc = _serviceProvider.GetService<ILogSvc>();
        }

        [Test]
        public async Task Test() 
        {
            var param = new LogQueryModel()
            {
                OrderByColumn = "Id",
                IsDesc = false
            };
            var result = await _logSvc.GetPageDataAsync(param);
            Assert.LessOrEqual(result.Data.Count, 20);
        }
    }
}
