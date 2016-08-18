using System;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceProviderFactorySample
{
    public class Startup
    {
        public void ConfigureServics(IServiceCollection services)
        {
            // Add services here that have extension methods on IServiceCollection
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            // Add services using your custom container here. In this case autofac
            containerBuilder.RegisterType<Foo>().AsImplementedInterfaces().SingleInstance();
        }

        public void Configure(IApplicationBuilder app, Lazy<IFoo> foo)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Hello World! {foo.Value.GetHashCode()}");
            });
        }

        public class Foo : IFoo
        {

        }

        public interface IFoo
        {

        }
    }
}
