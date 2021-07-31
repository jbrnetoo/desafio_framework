using Api_Compra.Config;
using Api_Compra.Filtros;
using Data.Context;
using FluentValidation.AspNetCore;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Text;

namespace Api_Compra
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(hostEnvironment.ContentRootPath)
                  .AddJsonFile("appsettings.json", true, true)
                  .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                  .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddRouting();

            services.AddCors();

            services.AddFluentValidation();

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelAttribute));
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddLogging(logging =>
            {
                logging.AddKissLog();
            });

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ComercioContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            VersioningConfig.Configuration(services);

            DependencyInjectionConfig.ResolveDependencies(services);

            SwaggerConfig.Configuration(services);

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Documentação");
                c.InjectStylesheet("/swagger/theme-flattop.css");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseKissLogMiddleware(options =>
            {
                ConfigureKissLog(options);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureKissLog(IOptionsBuilder options)
        {
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is System.NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            RegisterKissLogListeners(options);
        }

        private void RegisterKissLogListeners(IOptionsBuilder options)
        {
            options.Listeners.Add(new RequestLogsApiListener(new Application(
                Configuration["KissLog.OrganizationId"],
                Configuration["KissLog.ApplicationId"])
            )
            {
                ApiUrl = Configuration["KissLog.ApiUrl"]
            });
        }
    }
}
