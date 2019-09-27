using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Service.Impl;
using Test.Service.Interface;

namespace Test.XUnitTest
{
    public class TmpUnitTest : BaseUnitTest
    {
        private IUserSvc _userSvc { get; set; }
        public TmpUnitTest() : base() 
        {
            _serviceCollection.AddScoped<IUserSvc, UserSvc>();
        }
    }
}
