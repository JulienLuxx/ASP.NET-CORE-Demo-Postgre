using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Service.Impl;
using Test.Service.Interface;

namespace Test.Service.IOC
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection AddAllSvc(this ServiceCollection services)
        {
            services.AddScoped<IArticleSvc, ArticleSvc>();
            services.AddScoped<IArticleTypeSvc, ArticleTypeSvc>();
            services.AddScoped<ICommentSvc, CommentSvc>();
            services.AddScoped<ILogSvc, LogSvc>();
            services.AddScoped<IUserSvc, UserSvc>();
            return services;
        }

        public static ServiceCollection AddArticleSvc(this ServiceCollection services)
        {
            services.AddScoped<IArticleSvc, ArticleSvc>();
            return services;
        }

        public static ServiceCollection AddArticleTypeSvc(this ServiceCollection services)
        {
            services.AddScoped<IArticleTypeSvc, ArticleTypeSvc>();
            return services;
        }

        public static ServiceCollection AddCommentSvc(this ServiceCollection services)
        {
            services.AddScoped<ICommentSvc, CommentSvc>();
            return services;
        }

        public static ServiceCollection AddLogSvc(this ServiceCollection services)
        {
            services.AddScoped<ILogSvc, LogSvc>();
            return services;
        }

        public static ServiceCollection AddUserSvc(this ServiceCollection services)
        {
            services.AddScoped<IUserSvc, UserSvc>();
            return services;
        }
    }
}
