using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Ordsome.ApiGway
{
    public class Startup
    {
        public Startup (IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder ();
            builder.SetBasePath (env.ContentRootPath)
                .AddJsonFile ("appsettings.json")
                .AddJsonFile ("ocelot.json", optional : false, reloadOnChange : true)
                .AddEnvironmentVariables ();

            Configuration = builder.Build ();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices (IServiceCollection services)
        {
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII
                    .GetBytes (Configuration.GetSection ("AppSettings:Secret").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                    };
                });

            services.AddOcelot (Configuration);
        }

        public async void Configure (IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication ();
            await app.UseOcelot ();
        }
    }
}