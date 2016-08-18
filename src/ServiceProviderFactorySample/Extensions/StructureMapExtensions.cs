using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace ServiceProviderFactorySample
{
    public static class StructureMapExtensions
    {
        public static IServiceCollection AddStructureMap(this IServiceCollection services, Action<ConfigurationExpression> configure = null)
        {
            return services.AddSingleton<IServiceProviderFactory<IContainer>>(new StructureMapServiceProviderFactory(configure));
        }

        public static IWebHostBuilder UseStructureMap(this IWebHostBuilder builder, Action<ConfigurationExpression> configure = null)
        {
            return builder.ConfigureServices(services => services.AddStructureMap(configure));
        }

        private class StructureMapServiceProviderFactory : IServiceProviderFactory<IContainer>
        {
            public StructureMapServiceProviderFactory(Action<ConfigurationExpression> configure)
            {
                Configure = configure ?? (config => { });
            }

            private Action<ConfigurationExpression> Configure { get; }

            public IContainer CreateBuilder(IServiceCollection services)
            {
                var container = new Container(Configure);

                container.Populate(services);

                return container;
            }

            public IServiceProvider CreateServiceProvider(IContainer container)
            {
                return container.GetInstance<IServiceProvider>();
            }
        }
    }
}