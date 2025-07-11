namespace Cupa.MidatR;
public static class ConfigureRequriedServices
{
    public static IServiceCollection ConfigureMeditaRLayer(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssemblies(typeof(TokenRequestHandler).Assembly));
        services.AddValidatorsFromAssembly(typeof(CreateClubPlayerValidator).Assembly);
        return services;
    }
}