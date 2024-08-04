using Companies.Services.Companies.Read;
using Companies.Services.SharePrices.Read;
using Companies.Sqlite.Read;
using Companies.Sqlite.SharePrices.Read;
using Microsoft.Extensions.DependencyInjection;

namespace Companies.Sqlite.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureConfig(this IServiceCollection services)
    {
        services.AddScoped<ICompanyReadRepository, CompanyReadRepository>();
        services.AddScoped<ISharePriceReadRepository, SharePriceReadRepository>();
        return services;
    }
}