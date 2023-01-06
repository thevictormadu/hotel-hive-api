using Microsoft.Extensions.Configuration;
using Raven.Client.Documents;
using Serilog;
using Serilog.Events;
using System.Security.Cryptography.X509Certificates;

namespace HotelManagement.Api.Extensions
{
    public static class LogSettingsExtension
    {
        public static void SetupSerilog(IConfiguration config)
        {
            DocumentStore ravenStore = new()
            {
                Urls = new string[] { config["RavenDBConfigurations:ConnectionURL"] },
                Database = config["RavenDBConfigurations:DatabaseName"]
            };

            ravenStore.Certificate = new X509Certificate2(config["RavenDBConfigurations:CertificateFilePath"],
                config["RavenDBConfigurations:Password"], X509KeyStorageFlags.MachineKeySet);

            ravenStore.Initialize();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: ".\\Logs\\log-.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information
                )
                .WriteTo.RavenDB(ravenStore)
                .CreateLogger();
        }
    }
}
