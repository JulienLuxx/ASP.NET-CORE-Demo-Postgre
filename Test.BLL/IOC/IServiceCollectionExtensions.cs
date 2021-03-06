﻿using Elasticsearch.Net;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Service.Impl;
using Test.Service.Interface;

namespace Test.Service.IOC
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAllSvc(this IServiceCollection services)
        {
            services.AddArticleSvc();
            services.AddArticleTypeSvc();
            services.AddCommentSvc();
            services.AddLogSvc();
            services.AddUserSvc();

            services.AddESSvc();
            return services;
        }

        public static IServiceCollection InitElasticClient(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionPool, CustomizeStaticConnectionPool>();
            services.AddScoped<IElasticClient, CustomizeElasticClient>();
            return services;
        }

        public static IServiceCollection AddArticleSvc(this IServiceCollection services)
        {
            services.AddScoped<IArticleSvc, ArticleSvc>();
            return services;
        }

        public static IServiceCollection AddArticleTypeSvc(this IServiceCollection services)
        {
            services.AddScoped<IArticleTypeSvc, ArticleTypeSvc>();
            return services;
        }

        public static IServiceCollection AddCommentSvc(this IServiceCollection services)
        {
            services.AddScoped<ICommentSvc, CommentSvc>();
            return services;
        }

        public static IServiceCollection AddESSvc(this IServiceCollection services)
        {
            services.AddScoped<IESSvc, ESSvc>();
            return services;
        }

        public static IServiceCollection AddLogSvc(this IServiceCollection services)
        {
            services.AddScoped<ILogSvc, LogSvc>();
            return services;
        }

        public static IServiceCollection AddUserSvc(this IServiceCollection services)
        {
            services.AddScoped<IUserSvc, UserSvc>();
            return services;
        }
    }
}
