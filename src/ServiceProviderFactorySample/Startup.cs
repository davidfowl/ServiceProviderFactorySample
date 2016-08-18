using System;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ServiceProviderFactorySample
{
    public class Startup
    {
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
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
