using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace UserService.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseUrls($"http://localhost:7002")
            .UseStartup<Startup>()
            .Build();
    }
}