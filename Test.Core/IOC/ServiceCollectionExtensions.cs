using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Core.Encrypt;

namespace Test.Core.IOC
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection AddEncryptUtil(this ServiceCollection services)
        {
            services.AddScoped<IEncryptUtil, EncryptUtil>();
            return services;
        }
    }
}
