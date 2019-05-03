using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Ordsome.ApiGway
{
    public class Startup
    {

        private readonly IHostingEnvironment _env;
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            _env = env;
            Configuration = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // var audienceConfig = Configuration.GetSection("Audience");

            // var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
            // var tokenValidationParameters = new TokenValidationParameters
            // {
            //     ValidateIssuerSigningKey = true,
            //     IssuerSigningKey = signingKey,
            //     ValidateIssuer = true,
            //     ValidateAudience = true,
            //     ValidateLifetime = true,
            //     ClockSkew = TimeSpan.Zero,
            //     RequireExpirationTime = true,
            // };

            // services.AddAuthentication(o =>
            // {
            //     o.DefaultAuthenticateScheme = "Token";
            // })
            // .AddJwtBearer("Token", x =>
            //  {
            //      x.RequireHttpsMetadata = false;
            //      x.TokenValidationParameters = tokenValidationParameters;
            //  });

            services.AddOcelot();
            services.AddSwaggerForOcelot(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseSwaggerForOcelotUI(Configuration)
                .UseOcelot()
                .Wait();
        }
    }
}