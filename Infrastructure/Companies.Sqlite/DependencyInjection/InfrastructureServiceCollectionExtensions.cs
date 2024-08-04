using Companies.Services.Companies.Read;
using Companies.Sqlite.Read;
using Microsoft.Extensions.DependencyInjection;

namespace Companies.Sqlite.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureConfig(this IServiceCollection services)
    {
        services.AddScoped<ICompanyReadRepository, CompanyReadRepository>();
        return services;
    }
}