using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Core.IOC;
using Test.Domain;
using Test.Service.Impl;
using Test.Service.Interface;
using Xunit;

namespace Test.XUnitTest
{
    public class TmpUnitTest : BaseUnitTest
    {
        private IUserSvc _userSvc { get; set; }
        public TmpUnitTest() : base()
        {
            _serviceCollection.AddEncryptUtil();
            AddDbContext();
            BuilderServiceProvider();
            _userSvc = _serviceProvider.GetService<IUserSvc>();
        }

        [Fact]
        public void Test()
        {
            
        }
    }
}
