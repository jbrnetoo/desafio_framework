using Api_Compra.Models;
using Data.Context;
using Data.Repository;
using Domain.Interfaces;
using Domain.Services;
using FluentValidation;
using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Api_Compra.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ComercioContext>();
            services.AddScoped<IFrutaRepository, FrutaRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped((context) => Logger.Factory.Get());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IValidator<DtoFruta>, DtoFrutaValidator>();
            return services;
        }
    }
}
