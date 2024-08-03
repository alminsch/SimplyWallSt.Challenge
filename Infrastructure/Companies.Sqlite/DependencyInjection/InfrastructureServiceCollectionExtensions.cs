using Companies.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Companies.Sqlite;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureConfig(this IServiceCollection services)
    {
        services.AddScoped<ICompanyReadRepository, CompanyReadRespository>();
        return services;
    }
}