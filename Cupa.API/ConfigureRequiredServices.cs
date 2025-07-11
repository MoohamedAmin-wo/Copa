using Cupa.API.Helpers;

namespace Cupa.API
{
    public static class ConfigureRequiredServices
    {
        public static IServiceCollection ConfigureAPILayer(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddControllers();
            services.AddOpenApi();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "COPA - Graduation Project",
                    Contact = new OpenApiContact
                    {
                        Name = "Mohamed Amin",
                        Email = "spark_work@outlook.sa",
                    }
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "Jwt",
                    In = ParameterLocation.Header,
                    Description = "Enter jwt token "
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{new OpenApiSecurityScheme{Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme,Id = "Bearer",},Name = "Bearer",In = ParameterLocation.Header},new List<string>()}});
            });

            services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();
            services.AddEndpointsApiExplorer();
            services.AddCors();
            return services;
        }
    }
}
