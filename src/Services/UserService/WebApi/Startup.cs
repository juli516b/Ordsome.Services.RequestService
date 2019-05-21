using System.Reflection;
using Application.Interfaces;
using Application.Queries.GetRequestsBasedOnUserId;
using Application.RestClients;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordsome.Services.CrossCuttingConcerns.Extensions;
using Ordsome.Services.CrossCuttingConcerns.Mediatr;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRestServices();
            services.AddMediatR(typeof(GetRequestsBasedOnUserIdQueryHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddDbContext<IUserServiceDbContext, UserServiceDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("UserServiceDb")));
            services.AddCustomMvc();
            services.AddSwaggerSettings(Configuration);
            services.AddCors();
            services.AddAuthenticationSettings(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSwagger(c => { c.RouteTemplate = "userapi/docs/{documentName}/swagger.json"; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/userapi/docs/v1/swagger.json", "UserAPI");
                c.RoutePrefix = "userapi/docs";
            });

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}