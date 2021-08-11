using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NpgsqlTypes;
using Serilog;
using System.Collections.Generic;

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
            {
                config.ReadFrom.Configuration(context.Configuration);
            })).UseStartup<Startup>();
    }
}





//config
//                 .Enrich.FromLogContext()
//                 .Enrich.WithMachineName()
//                 .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{MachineName}] - {Message}{NewLine}{Exception}")
//                 .WriteTo.Logger(logger =>
//                      logger.Filter.ByIncludingOnly(f => f.Level == Serilog.Events.LogEventLevel.Error)
//                      .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day,
//                      outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{MachineName}] - {Message}{NewLine}{Exception}"))
//                      .WriteTo.PostgreSQL("User ID=postgres;Password=993;Server=localhost;Port=5432;Database=Library", "Logs",
//                          new Dictionary<string, ColumnWriterBase>()
//                            {
//                                {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
//                                {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
//                                {"level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
//                                {"raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
//                                {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
//                                {"properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
//                                {"props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
//                                {"machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "1") }
//                           }, needAutoCreateTable: true, levelSwitch: new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Error)
