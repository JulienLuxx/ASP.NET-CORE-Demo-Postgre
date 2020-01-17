using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Service.Interface;
using Xunit;

namespace Test.XUnitTest
{
    public class LogUnitTest : BaseUnitTest
    {
        private readonly ILogSvc _logSvc;
        public LogUnitTest() : base()
        {
            AddDbContext();
            BuilderServiceProvider(AutofacInit());
            _logSvc=_serviceProvider.GetService<ILogSvc>();
        }

        [Fact]
        public void Test()
        { }
    }
}
