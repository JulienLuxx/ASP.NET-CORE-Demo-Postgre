using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Core;
using Test.Domain;
using Test.Domain.Entity;
using Test.Domain.Extend;
using Test.Service.Dto;
using Test.Service.Impl;
using Test.Service.Infrastructure;
using Test.Service.Interface;
using Test.Service.QueryModel;
using Xunit;

namespace Test.XUnitTest
{
    public class TestUnitTest : BaseUnitTest
    {
        Mock<DbSet<ArticleType>> _mockSet { get; set; }
        Mock<TestDBContext> _mockContext { get; set; }
        IArticleTypeSvc _mockSvc { get; set; }
        public TestUnitTest() : base()
        {
            _serviceCollection.AddScoped<IDbContextExtendSvc, DbContextExtendSvc>();
            BuilderServiceProvider();
            var mapper = _serviceProvider.GetService<IMapper>();
            var dbContextExtendSvc = _serviceProvider.GetService<IDbContextExtendSvc>();
            var list = new List<ArticleType>()
            {
                new ArticleType(){ Id=1,Name="1",EditerName="123",CreateTime=DateTime.Now},
                new ArticleType(){ Id=1,Name="2",EditerName="223",CreateTime=DateTime.Now},
            };
            _mockSet = new Mock<DbSet<ArticleType>>().SetupList(list);
            _mockContext = new Mock<TestDBContext>();
            _mockContext.Setup(x => x.ArticleType).Returns(_mockSet.Object);
            _mockSvc = new ArticleTypeSvc(mapper, _mockContext.Object, dbContextExtendSvc);
        }

        [Fact]
        public void AddSingleTest()
        {
            var data = new ArticleTypeDto() { Name = "251", EditerName = "test", CreateTime = DateTime.Now };
            _mockSvc.AddSingle(data);

            _mockContext.Verify(x => x.Add(It.IsAny<ArticleType>()), Times.Once());
            _mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Fact]
        public async Task GetPageDatasAsyncTest()
        {
            var result= await _mockSvc.GetPageDatasAsync(new ArticleTypeQueryModel() { OrderByColumn="CreateTime"});
            Assert.Equal(2, result.Count);
        }
    }
}
