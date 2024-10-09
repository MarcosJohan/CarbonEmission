using Api.Repositories;
using Api.Services;

namespace Api.Configurations;

public static class InjectionContainer
{
    public static void AddServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICarbonEmissionRepository, CarbonEmissionRepository>();
        builder.Services.AddTransient<ICarbonEmissionService, CarbonEmissionService>();
        builder.Services.AddTransient<IAuthService, AuthService>();
        builder.Services.AddTransient<IReportRepository, ReportRepository>();
        builder.Services.AddTransient<IReportService, ReportService>();
        
    }
}