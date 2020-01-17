using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Core.Encrypt;

namespace Test.Core.IOC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEncryptUtil(this IServiceCollection services)
        {
            services.AddScoped<IEncryptUtil, EncryptUtil>();
            return services;
        }
    }
}
