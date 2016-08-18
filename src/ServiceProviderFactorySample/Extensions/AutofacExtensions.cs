using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceProviderFactorySample
{
    public static class AutofacExtensions
    {
        public static IServiceCollection AddAutofac(this IServiceCollection services)
        {
            return services.AddSingleton<IServiceProviderFactory<ContainerBuilder>, AutofacServiceProviderFactory>();
        }

        public static IWebHostBuilder UseAutofac(this IWebHostBuilder builder)
        {
            return builder.ConfigureServices(services => services.AddAutofac());
        }

        private class AutofacServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
        {
            public ContainerBuilder CreateBuilder(IServiceCollection services)
            {
                var containerBuilder = new ContainerBuilder();

                containerBuilder.Populate(services);

                return containerBuilder;
            }

            public IServiceProvider CreateServiceProvider(ContainerBuilder builder)
            {
                return new AutofacServiceProvider(builder.Build());
            }
        }
    }
}