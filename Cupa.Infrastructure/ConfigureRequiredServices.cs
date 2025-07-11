namespace Cupa.Infrastructure
{
    public static class ConfigureRequiredServices
    {
        public static IServiceCollection ConfigureInfrastructureLayer(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var connection = builder.Configuration.GetConnectionString("CopaConnection")
                ?? throw new NullReferenceException("Can't find Connection strings");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(180);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Tokens.ChangeEmailTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders()
             .AddTokenProvider(TokenOptions.DefaultEmailProvider, typeof(EmailTokenProvider<ApplicationUser>));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClubRepository, ClubRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IPictureRepository, PictureRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IClubPlayerRepository, ClubPlayerRepository>();
            services.AddScoped<ISuccessStroriesRepository, SuccessStoriesRepository>();

            return services;
        }
    }
}