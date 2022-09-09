using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Events;

namespace N6.Bsjc.ReportDocumentViewer
{
    public class Program
    {
        public static int Main(string[] args)
        {
			Log.Logger = new LoggerConfiguration()
#if DEBUG
		   .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
		   .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
		   .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
		   .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
		   .Enrich.FromLogContext()
		   .ReadFrom.Configuration(BuildConfiguration())
#if DEBUG
		   .WriteTo.Async(c => c.Console())
#endif
		   .CreateLogger();

			try
			{
				Log.Information("Starting web host.");
				CreateHostBuilder(args).Build().Run();
				return 0;
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Host terminated unexpectedly!");
				return 1;
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAutofac()
                .UseSerilog();

		private static IConfigurationRoot BuildConfiguration()
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", false, true)
				.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true, true)
				.Build();

			return configuration;
		}
	}
}
