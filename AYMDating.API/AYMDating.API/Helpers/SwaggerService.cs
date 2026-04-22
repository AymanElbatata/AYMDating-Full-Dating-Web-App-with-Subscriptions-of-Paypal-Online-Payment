using Microsoft.OpenApi.Models;

namespace AYMDating.API.Helpers
{
    public static class SwaggerService
    {
        public static IServiceCollection AddSwaggerDocumentaion(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "AYMDating", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "AYM JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                opt.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[]{"Bearer"} }
                };

                opt.AddSecurityRequirement(securityRequirement);
            });

            return services;
        }
    }
}
