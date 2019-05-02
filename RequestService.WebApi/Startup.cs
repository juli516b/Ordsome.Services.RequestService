using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestService.Application.Commands.Requests.RequestCreation;
using RequestService.Application.Interfaces;
using RequestService.Application.Queries.Requests.GetRequests;
using RequestService.Infrastructure;
using RequestService.Infrastructure.AutoMapper;
using RequestService.Infrastructure.Persistence;
using RequestService.WebApi.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;

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
            services.AddDbContext<RequestServiceDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RequestDbService")));

            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRequestCommandValidator>());

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            // In production, the Angular files will be served from this directory
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "RequestServiceApi", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Sample API");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseMvc();        
        }
    }
}
