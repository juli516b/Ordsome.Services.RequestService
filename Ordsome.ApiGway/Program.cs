using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Ordsome.ApiGway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) => 
        
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:7000")
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true,
                            true)
                        .AddJsonFile("ocelot.json", false, true)
                        .AddEnvironmentVariables();
                })
                .ConfigureServices(s =>
                {
                    s
                        .AddOcelot();
                })
                .Configure(a =>
                {
                    a.UseOcelot().Wait();
                })
                .UseIIS()
                .Build();
        }
    }