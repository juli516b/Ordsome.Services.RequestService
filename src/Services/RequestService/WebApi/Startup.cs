using System.Reflection;
using Application.Infrastructure;
using Application.Interfaces;
using Application.Queries.Requests.GetAnswersByRequestId;
using Application.RestClients;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Ordsome.Services.CrossCuttingConcerns.Extensions;

namespace WebApi
{
    //TODO - maybe make a crosscutting concern for startup. A kind of framework for new services
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<INotificationService, NotificationService>();

            // Add MediatR - muligt at tilføje logging af alle requests via mediatr her.
            services.AddMediatR(typeof(GetAnswersByRequestIdQueryHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));


            // Add DbContext using SQL Server Provider
            services.AddDbContext<IRequestServiceDbContext, RequestServiceDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RequestDbService")));

            // Adds Fluentvalidation, filter, and suppressModelStateInvalidFilter
            services.AddCustomMvc();

            // Swagger
            services.AddSwaggerSettings(Configuration);

            // Security

            services.AddAuthenticationSettings(Configuration);
            
            services.AddRestServices();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSwagger(c => { c.RouteTemplate = "requestapi/docs/{documentName}/swagger.json"; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/requestapi/docs/v1/swagger.json", "RequestAPI");
                c.RoutePrefix = "requestapi/docs";
            });
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}