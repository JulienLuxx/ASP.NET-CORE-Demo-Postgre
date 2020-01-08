using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Test.Core;
using Test.Service;
using Test.Service.IOC;
using Xunit;
using System;

namespace Test.XUnitTest
{

    public class ESTmpUnitTest : BaseUnitTest
    {
        private IESSvc _esSvc { get; set; }

        protected override void Init()
        {
            //var esCon = _configuration.GetSection("ElasticSearchAddress").GetSection("Nodes").Get<string[]>();
            _serviceCollection.Configure<ESConnectionStrings>(_configuration.GetSection("ElasticSearchAddress"));
            _serviceCollection.InitElasticClient();
            _serviceCollection.AddESSvc();
            base.Init();
            var 
            _esSvc = _serviceProvider.GetService<IESSvc>();            
            //var escon = _serviceProvider.GetService<IOptions<ESConnectionStrings>>();
            //if (null != escon.Value)
            //{
            //    var uriList = new List<Uri>();
            //    foreach (var node in escon.Value.Nodes)
            //    {
            //        uriList.Add(new Uri(node));
            //    }

            //}
        }

        [Fact]
        public void Test()
        {
            _esSvc.Test();
        }
    }
}
