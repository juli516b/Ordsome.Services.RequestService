using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace RequestService.WebApi
{
    public class Program
    {
        public static void Main (string[] args)
        {
            CreateWebHostBuilder (args).Run ();
        }

        public static IWebHost CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseUrls ($"http://localhost:7001")
            .UseStartup<Startup> ()
            .Build ();
    }
}