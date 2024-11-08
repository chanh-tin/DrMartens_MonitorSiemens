#define VIRTUAL_MODEx

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using Serilog.Events;
using Microsoft.EntityFrameworkCore;
using System.IO;
using SourceBaseBE.MainService.Services;
using SourceBaseBE.MainService.Services.Generate;
using iSoft.Common.Util;
using iSoft.Common.ConfigsNS;
using iSoft.Common.Cronjobs.DefaultCronjobs;
using iSoft.ElasticSearch.Services;
using SourceBaseBE.Database.DBContexts;
using Microsoft.Extensions.Logging;
using iSoft.Common.ExtensionMethods;
using iSoft.RabbitMQ.Services;
using iSoft.DBLibrary.DBConnections.Factory;
using iSoft.DBLibrary.DBConnections.Interfaces;
using iSoft.RabbitMQ;
using Prometheus;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using iSoft.Redis.Services;
using iSoft.DBLibrary.DBConnections;
using iSoft.InfluxDB.Services;
using iSoft.InfluxDB.Cronjobs;
using SourceBaseBE.CommonFunc.HealthCheck;
using Microsoft.AspNetCore.Routing;
using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SourceBaseBE.CommonFunc.Cronjobs;
using System.Net.Http;

namespace SourceBaseBE.ServerApp
{
    public class Startup
    {

        public static ILoggerFactory loggerFactory = null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                //.WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] ({SourceContext:lj}) - {Message}{NewLine}{Exception}");
                .WriteTo.Console(new SerilogFormat());

            bool minimumLevelFlag = ConfigUtil.GetAppSetting<bool>("AppSettings:MinimumLevelErrorFlag");
            if (minimumLevelFlag)
            {
                loggerConfiguration.MinimumLevel.Error();
            }

            Log.Logger = loggerConfiguration.CreateLogger();

            loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Warning)
                           .AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information)
                           .AddConsole()
                           .AddDebug()
                           .ClearProviders();
                }
            );
            loggerFactory.AddSerilog(Log.Logger);

            string startStr = File.ReadAllText("start.txt");
            Log.Logger.Information(startStr);

            // configure jwt authentication
            string authenticationSecret = CommonConfig.GetConfig().AuthenticationSecret;
            var key = Encoding.ASCII.GetBytes(authenticationSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddSingleton(provider => new InfluxDBService(CommonConfig.GetConfig().InfluxDBConfig));
            services.AddScoped(provider =>
              {
                  IDBConnectionCustom dBConnectionCustom = DBConnectionFactory.CreateDBConnection(CommonConfig.GetConfig().MasterDatabaseConfig);
                  return new CommonDBContext(dBConnectionCustom);
              });
            services.AddScoped<GenerateSourceService>();
            services.AddScoped<GenTemplateService>();
            services.AddScoped<Example002Service>();
            services.AddScoped<Example001Service>();
            services.AddScoped<Example003Service>();
            services.AddScoped<UserService>();
            services.AddScoped<ElasticSearchService>();

            services.AddCors();
            services.AddControllers();

            services.AddControllersWithViews();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseHttpMetrics();

            // global cors policy
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Content-Disposition")
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseRouter(routes =>
                {
                    routes.MapGet("/", async context =>
                    {
                        //context.Response.Redirect("/api/v1/dashboard");
                        context.Response.Redirect("/Home/Setting");
                    });
                });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
