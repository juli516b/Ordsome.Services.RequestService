using System.Linq;
using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ordsome.Services.CrossCuttingConcerns.Filters;
using Swashbuckle.AspNetCore.Swagger;
using FluentValidation;

namespace Ordsome.Services.CrossCuttingConcerns.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthenticationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(configuration.GetSection("AppSettings:Secret").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
        public static void AddSwaggerSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = configuration.GetSection("Swagger:Name").Value, Version = configuration.GetSection("Swagger:Version").Value});
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }
        
        public static void AddCustomMvc(this IServiceCollection services)
        {
            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<IValidator>();
                    fv.LocalizationEnabled = false;
                });

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        }
    }
}