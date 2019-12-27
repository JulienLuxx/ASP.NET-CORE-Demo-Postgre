using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Core;
using Test.Service;
using Test.Service.IOC;
using Xunit;

namespace Test.XUnitTest
{

    public class ESTmpUnitTest : BaseUnitTest
    {
        private IESSvc _esSvc { get; set; }

        protected override void Init()
        {
            _serviceCollection.Configure<ESConnectionStrings>(_configuration.GetSection("ElasticSearchNodes"));
            _serviceCollection.AddESSvc();
            base.Init();
            _esSvc = _serviceProvider.GetService<IESSvc>();
            var s = _serviceProvider.GetService<IOptions<ESConnectionStrings>>();
        }

        [Fact]
        public void Test()
        {
            _esSvc.Test();
        }
    }
}
