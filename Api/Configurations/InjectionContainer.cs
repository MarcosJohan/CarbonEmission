using Api.Repositories;
using Api.Services;

namespace Api.Configurations;

public static class InjectionContainer
{
    public static void AddServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICarbonEmissionRepository, CarbonEmissionRepository>();
        builder.Services.AddTransient<ICarbonEmissionService, CarbonEmissionService>();
    }
}