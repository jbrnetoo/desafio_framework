using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Api_Compra.Config
{
    public static class VersioningConfig
    {
        public static IServiceCollection Configuration(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(c =>
            {
                c.GroupNameFormat = "'v'VVV";
                c.SubstituteApiVersionInUrl = true;
                c.AssumeDefaultVersionWhenUnspecified = true;
                c.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddApiVersioning(c =>
            {
                c.ReportApiVersions = true;
                c.AssumeDefaultVersionWhenUnspecified = true;
                c.DefaultApiVersion = new ApiVersion(1, 0);
            });

            return services;
        }
    }
}
