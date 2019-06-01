using System.Reflection;
using Application.Commands.Answers.VoteOnAnswer;
using Application.Infrastructure.Mappings;
using Application.Interfaces;
using Application.Queries.Requests.GetAnswersByRequestId;
using Application.RestClients;
using FluentValidation.AspNetCore;
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
            services.AddTransient<INotificationService, NotificationService>();
            services.AddMediatR(typeof(GetAnswersByRequestIdQueryHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient<IRequestMappings, RequestMappings>();
            services.AddDbContext<IRequestServiceDbContext, RequestServiceDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RequestDbService")));
            services.AddCustomMvc().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<VoteOnAnswerCommandValidator>();
                fv.LocalizationEnabled = false;
            });
            services.AddSwaggerSettings(Configuration);
            services.AddCors();
            services.AddAuthenticationSettings(Configuration);
            services.AddRestServices();
        }

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
            app.UseSwagger(c => c.RouteTemplate = "requestapi/docs/{documentName}/swagger.json");
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/requestapi/docs/v1/swagger.json", "RequestAPI");
                c.RoutePrefix = "requestapi/docs";
            });

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}