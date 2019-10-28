using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Map;
using Test.Service.QueryModel;
using Xunit;

namespace Test.XUnitTest
{
    public class CoreTestUnitTest : BaseUnitTest 
    {
        private IMapUtil _mapUtil { get; set; }

        public CoreTestUnitTest() : base() 
        {
            _serviceCollection.AddScoped<IMapUtil, MapUtil>();
            BuilderServiceProvider();
            _mapUtil = _serviceProvider.GetService<IMapUtil>();
        }

        [Fact]
        public async Task Test()
        {
            var qModel = new LogQueryModel()
            {
                PageSize = 10
            };
            //var t = MapExtensions.GetDescription()
            Assert.Equal(1, 1);
        }

        [Fact]
        public void EntityToDictionaryTest()
        {
            var qModel = new LogQueryModel()
            {
                PageSize = 10
            };
            var dict = _mapUtil.EntityToDictionary(qModel);
            Assert.Equal(dict["pageSize"], 10.ToString());
        }
    }
}
