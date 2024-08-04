using Companies.Contracts;
using Companies.Services.Companies;
using Companies.Services.Companies.Read;
using Companies.Services.SharePrices.Read;
using Microsoft.Extensions.DependencyInjection;

namespace Companies.Services.DependencyInjection;

public static class ServicesServiceCollectionExtensions
{
    public static IServiceCollection AddServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<ICompanyReadService, CompanyReadService>();
        services.AddScoped<ISharePriceReadService, SharePriceReadService>();
        services.AddScoped<ICompanyService, CompanyService>();
        return services;
    }
}
