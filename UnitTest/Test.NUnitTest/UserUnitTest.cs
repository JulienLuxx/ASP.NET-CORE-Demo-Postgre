using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Service.Dto;
using Test.Service.Interface;

namespace Test.NUnitTest
{
    [TestFixture]
    public class UserUnitTest : BaseUnitTest
    {
        private readonly IUserSvc _userSvc;

        public UserUnitTest() : base()
        {
            AddDbContext();
            BuilderServiceProvider(InitByAutofac());
            _userSvc = _serviceProvider.GetService<IUserSvc>();
        }

        [Test]
        public async Task AddSingleAsync()
        {
        }

        [Test]
        public async Task RegisterAsync()
        {
            var param = new RegisterDto()
            {
                Name = "Test",
                Password = "123456",
                Mobile = "156 ",
                MailBox = "156@qq.com"
            };
            var result = await _userSvc.RegisterAsync(param);
            Assert.IsTrue(result.ActionResult);
        }
    }
}
