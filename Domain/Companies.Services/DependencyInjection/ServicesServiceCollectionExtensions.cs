using Companies.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Companies.Services;

public static class ServicesServiceCollectionExtensions
{
    public static IServiceCollection AddServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<ICompanyReadService, CompanyReadService>();
        return services;
    }
}
