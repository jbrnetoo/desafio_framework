using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Api_Compra.Config
{
    public static class SwaggerConfig
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API Compra",
                    Version = "v1",
                    Description = "Desafio sugerido pela Framework como etapa de processo seletivo"
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, "Api_Compra.xml");
                c.IncludeXmlComments(filePath);

                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Auth Header"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference {
                               Type = ReferenceType.SecurityScheme,
                               Id = "basic"
                            }
                        },
                        new string[]{ }
                    }
                });
            });

            return services;
        }
    }
}
