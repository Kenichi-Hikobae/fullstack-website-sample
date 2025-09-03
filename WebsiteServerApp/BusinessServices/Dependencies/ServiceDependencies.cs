using WebsiteServerApp.BusinessServices.Interfaces;
using WebsiteServerApp.BusinessServices.Services;

namespace WebsiteServerApp.BusinessServices.Dependencies;

public static class ServiceDependencies
{
    /// <summary>
    /// Add the services used in the API client.
    /// </summary>
    /// <param name="services">The service object from the web application.</param>
    /// <returns>The service object with the service added.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IOwnerService, OwnerService>();
        services.AddScoped<IPropertyService, PropertyService>();

        return services;
    }
}
