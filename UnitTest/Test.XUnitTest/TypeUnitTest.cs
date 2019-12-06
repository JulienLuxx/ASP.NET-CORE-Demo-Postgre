using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Domain;
using Test.Domain.Entity;
using Test.Service.Interface;

namespace Test.XUnitTest
{
    public class TypeUnitTest
    {
        protected Mock<TestDBContext> _mockDBContext { get; set; }

        protected Mock<DbSet<ArticleType>> _mockSet { get; set; }

        protected IArticleTypeSvc _mockSvc { get; set; }

        public TypeUnitTest()
        {
            _mockDBContext = new Mock<TestDBContext>();
        }
    }
}
