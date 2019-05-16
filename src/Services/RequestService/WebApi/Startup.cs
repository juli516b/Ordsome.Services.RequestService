using System.Linq;
using System.Reflection;
using System.Text;
using Application.Interfaces;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using RequestService.Application.Commands.Requests.RequestCreation;
using RequestService.Application.Interfaces;
using RequestService.Application.Queries.Requests.GetAnswersByRequestId;
using RequestService.Application.RestClients;
using RequestService.Infrastructure;
using RequestService.Infrastructure.AutoMapper;
using RequestService.Infrastructure.Persistence;
using RequestService.WebApi.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace RequestService.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            // Add framework services.
            services.AddTransient<INotificationService, NotificationService>();

            // Add MediatR - muligt at tilføje logging af alle requests via mediatr her.
            services.AddMediatR(typeof(GetAnswersByRequestIdQueryHandler).GetTypeInfo().Assembly);

            // Add DbContext using SQL Server Provider
            services.AddDbContext<IRequestServiceDbContext, RequestServiceDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RequestDbService")));

            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv =>
                    {
                        fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                        fv.RegisterValidatorsFromAssemblyContaining<CreateRequestCommandValidator>();
                        fv.LocalizationEnabled = false;
                    });

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "RequestServiceApi", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            // Security

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(Configuration.GetSection("AppSettings:Secret").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                    };
                });

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
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "requestapi/docs/{documentName}/swagger.json";

            });
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