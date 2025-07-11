using CloudinaryDotNet;

namespace Cupa.Application;
public static class ConfigureRequiredServices
{
    public static IServiceCollection ConfigureApplicationLayer(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        services.AddTransient<IEmailSender, EmailSender>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IFilesServices, FilesServices>();
        services.AddScoped<IMegaApiClient, MegaApiClient>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IClubManagmentServices, ClubManagmentServices>();

        services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
        services.Configure<Mail>(builder.Configuration.GetSection("MailSettings"));
        services.Configure<ImageKitAccount>(builder.Configuration.GetSection("ImageKitAccount"));
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.RequireHttpsMetadata = false;
            opt.SaveToken = true;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
}