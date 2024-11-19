#define VIRTUAL_MODEx

using iSoft.AspNetCore.CommonFunc.HealthCheck;
using iSoft.AspNetCore.Cronjobs;
using iSoft.AspNetCore.Services;
using iSoft.Common.ConfigsNS;
using iSoft.Common.Cronjobs.DefaultCronjobs;
using iSoft.Common.ExtensionMethods;
using iSoft.Common.Util;
using iSoft.Database.DBContexts;
using iSoft.DBLibrary.DBConnections;
using iSoft.DBLibrary.DBConnections.Factory;
using iSoft.DBLibrary.DBConnections.Interfaces;
using iSoft.ElasticSearch.Services;
using iSoft.RabbitMQ.Services;
using iSoft.Redis.Cronjobs;
using iSoft.Redis.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Prometheus;
using Serilog;
using Serilog.Events;
using SourceBaseBE.Database.DBContexts;
using SourceBaseBE.MainService.Services;
using System;
using System.IO;
using System.Text;

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
      string authenticationSecret = CommonConfig.AuthenticationSecret;
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
      services.AddScoped<BaseDBContext>(provider =>
        {
          IDBConnectionCustom dBConnectionCustom = DBConnectionFactory.CreateDBConnection(CommonConfig.MasterDatabaseConfig);
          return new CommonDBContext(dBConnectionCustom);
        });

      services.AddScoped<CommonDBContext>(provider =>
      {
        IDBConnectionCustom dBConnectionCustom = DBConnectionFactory.CreateDBConnection(CommonConfig.MasterDatabaseConfig);
        return new CommonDBContext(dBConnectionCustom);
      });

      // Bug: Cannot write DateTime with Kind=Unspecified to PostgreSQL type 'timestamp with time zone', only UTC is supported.
      //IDBConnectionCustom dBConnectionCustom = DBConnectionFactory.CreateDBConnection(CommonConfig.MasterDatabaseConfig);
      //CommonDBContext.CreateDatabase(dBConnectionCustom);

      services.AddScoped<LanguageSystemService>();
      services.AddScoped<LanguageSupportService>();
      services.AddScoped<Example001Service>();
      services.AddScoped<Example002Service>();
      services.AddScoped<Example003Service>();
      services.AddScoped<Example001TransService>();
      services.AddScoped<DataBlockService>();
      services.AddScoped<PlcService>();
      services.AddScoped<TagService>();
      /*[GEN-44]*/

      services.AddHostedService<CheckConnectRedisJob>();
      services.AddHostedService<CheckConnectRedisSupportJob>();
      services.AddHostedService<GCCollectCronjob>();
      services.AddHostedService<ResetMetricsCronjob>();
      //services.AddHostedService<RefreshConnectivityDataJob>();
      //services.AddHostedService(provider =>
      //{

      //    return new CheckShiftService(loggerFactory);
      //});
      services.AddScoped(provider =>
      {
        return new ElasticSearchService(loggerFactory, CommonConfig.ElasticSearchConfig);
      });

      //services.AddHostedService(provider =>
      //{
      //    var rabbitMQService = new RabbitMQService(CommonConfig.RabbitMQConfig);
      //    var elasticSearchService = new ElasticSearchService(loggerFactory, CommonConfig.ElasticSearchConfig);
      //    var logger = provider.GetRequiredService<ILogger<SyncOeeDataHostService>>();
      //    return new SyncOeeDataHostService(CommonConfig.MasterDatabaseConfig, elasticSearchService, rabbitMQService, ExchangeName.SourceBaseBEEnvEx, logger);
      //});

      services.AddSingleton(provider =>
      {
        //var influxDBService = provider.GetRequiredService<InfluxDBService>();
        return new RabbitMQService(CommonConfig.RabbitMQConfig);
      });

      //services.AddHostedService<TestInfluxDBCronjob>();

      //// Add for realtime topic
      //services.AddHostedService(provider =>
      //{
      //    //var influxDBService = provider.GetRequiredService<InfluxDBService>();
      //    var rabbitMQService = provider.GetRequiredService<RabbitMQService>();
      //    var logger = provider.GetRequiredService<ILogger<RealtimeDataConsumerHostedService>>();
      //    return new RealtimeDataConsumerHostedService(logger, rabbitMQService, TopicName.RealtimeDataTopic);
      //});

      //services.AddHostedService(provider =>
      //{
      //    var rabbitMQService = provider.GetRequiredService<RabbitMQService>();
      //    var logger = provider.GetRequiredService<ILogger<UpdateMachineBlockDataConsumerHostedService>>();
      //    return new UpdateMachineBlockDataConsumerHostedService(CommonConfig.MasterDatabaseConfig, logger, rabbitMQService, TopicName.OEEDataInputTopic, oeePoint);

      //services.AddHostedService(provider =>
      //{
      //    var rabbitMQService = provider.GetRequiredService<RabbitMQService>();
      //    //var oeePoint = provider.GetRequiredService<OEEPointStore>();
      //    var logger = provider.GetRequiredService<ILogger<UpdateMachineBlockDataConsumerHostedService>>();
      //    var shiftCheckPointLogger = provider.GetRequiredService<ILogger<ShiftCheckPointService>>();
      //    var elasticSearchService = new ElasticSearchService(loggerFactory, CommonConfig.ElasticSearchConfig);
      //    return new UpdateMachineBlockDataConsumerHostedService(CommonConfig.MasterDatabaseConfig,
      //        logger,
      //        rabbitMQService,
      //        QueueConfigImp.Instance.OEEDataInputTopicProperty,
      //        QueueConfigImp.Instance.ConnectivityEx,
      //        shiftCheckPointLogger,
      //        elasticSearchService
      //        );
      //});

#if VIRTUAL_MODE
#else
      //services.AddHostedService(provider =>
      //{
      //    var logger = provider.GetRequiredService<ILogger<CheckSyncConfigHostedService>>();
      //    return new CheckSyncConfigHostedService(CommonConfig.MasterDatabaseConfig, logger);
      //});
#endif

      //services.AddHostedService(provider =>
      //{
      //    var rabbitMQService = new RabbitMQService(CommonConfig.RabbitMQConfig);
      //    var logger = provider.GetRequiredService<ILogger<CheckOeeTimeOutHostedService>>();
      //    return new CheckOeeTimeOutHostedService(CommonConfig.MasterDatabaseConfig,
      //        logger,
      //        rabbitMQService,
      //        QueueConfigImp.Instance.ConnectivityEx
      //        );
      //});

      services.AddMetricServer(options =>
      {
        options.Port = 12004;
      });
      services.AddHealthChecks()
          .AddNpgSql(PostgresDbConnection.GetConnectionString(CommonConfig.MasterDatabaseConfig), "SELECT 1;", null, "DRMT.MMS.Main-Postgres")
          .AddCheck<ServiceHealthCheck>("DRMT.MMS.Main")
          .ForwardToPrometheus();

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

      //app.ApplicationServices.UseScheduler(scheduler =>
      //{
      //  scheduler
      //      .Schedule<WriteRandomPlaneAltitudeInvocable>()
      //      .EveryFiveSeconds();
      //});

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

      //if (env.IsDevelopment())
      //{
      //	app.UseDeveloperExceptionPage();
      //	app.UseSwagger();
      //	app.UseSwaggerUI(c =>
      //	{
      //		c.SwaggerEndpoint("v1/swagger.json", "SourceBaseBE-BE V1");
      //	});
      //}

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
        //endpoints.MapMetrics("/api/v1/metrics").RequireAuthorization(a => a.RequireRole("Root"));
        //endpoints.MapHealthChecks("/api/v1/_health", new HealthCheckOptions
        //{
        //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        //}).RequireAuthorization(a => a.RequireRole("Root"));
      });

      CachedFunc.SetRedisConfig(CommonConfig.RedisConfig);
      CachedFunc.FlushAll();
    }
  }

}
