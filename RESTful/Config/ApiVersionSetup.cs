/******************************************
 * AUTHOR:          Shah-MI
 * CREATION:       2023-08-12
 ******************************************/


using Microsoft.AspNetCore.Mvc;

namespace RESTful.Config;

public static class ApiVersionSetup
{
    public static IServiceCollection AddApiVersion(this IServiceCollection services)
    {
        services.AddApiVersioning(z =>
        {
            z.AssumeDefaultVersionWhenUnspecified = true;
            z.DefaultApiVersion = new ApiVersion(1, 0);
            z.ReportApiVersions = true;
        });
        return services;
    }
}