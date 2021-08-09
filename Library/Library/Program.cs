using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseSerilog(((context, config) =>

                   config.Enrich.FromLogContext()
                  .Enrich.WithMachineName()
                  .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{MachineName}] - {Message}{NewLine}{Exception}")
                  .WriteTo.Logger(logger =>
                       logger.Filter.ByIncludingOnly(f => f.Level == Serilog.Events.LogEventLevel.Error)
                       .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day,
                       outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{MachineName}] - {Message}{NewLine}{Exception}"))
        ))
            .UseStartup<Startup>();
    }
}

