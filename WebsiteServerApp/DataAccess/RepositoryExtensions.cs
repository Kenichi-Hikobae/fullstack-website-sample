using WebsiteServerApp.DataAccess.Interfaces;
using WebsiteServerApp.DataAccess.Repositories;

namespace WebsiteServerApp.DataAccess;

public static class RepositoryExtensions
{
    /// <summary>
    /// Add the repositories used in the API client.
    /// </summary>
    /// <param name="services">The service object from the web application.</param>
    /// <returns>The service object with the repositories added.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();

        return services;
    }
}
