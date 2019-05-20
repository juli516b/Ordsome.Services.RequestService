using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using UserService.Application.Commands.Register;
using UserService.Application.Interfaces;
using UserService.Application.Queries.GetRequestsBasedOnUserId;
using UserService.Application.RestClients;
using UserService.Infrastructure.Persistence;
using UserService.WebApi.Filters;

namespace UserService.WebApi
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
            //Add rest clients
            services.AddRestServices();
            // Add AutoMapper
            //  services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            // Add framework services.
            //services.AddTransient<INotificationService, NotificationService>();

            // Add MediatR - muligt at tilføje logging af alle requests via mediatr her.
            services.AddMediatR(typeof(GetRequestsBasedOnUserIdQueryHandler).GetTypeInfo().Assembly);

            // Add DbContext using SQL Server Provider
            services.AddDbContext<IUserServiceDbContext , UserServiceDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("UserServiceDb")));

            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterCommandValidator>());

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            // In production, the Angular files will be served from this directory
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "UserServiceAPI", Version = "v1" }));

            services.AddCors();
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
            }

            app.UseStaticFiles();
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "userapi/docs/{documentName}/swagger.json";
            });
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