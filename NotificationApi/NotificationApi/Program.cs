using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration.KeyPerFile;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace NotificationApi
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        protected Program()
        {
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            const string vhInfraCore = "vh-infra-core";
            const string vhNotificationApi = "vh-notification-api";
            var keyVaults = new[] { vhInfraCore, vhNotificationApi };

            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((configBuilder) =>
                {
                    
                    foreach (var keyVault in keyVaults)
                    {
                        var filePath = $"/mnt/secrets/{keyVault}";
                        if (Directory.Exists(filePath))
                        {
                            configBuilder.Add(GetKeyPerFileSource(filePath));    
                        }
                        
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseIISIntegration();
                    webBuilder.UseStartup<Startup>();
                });
        }
        
        private static KeyPerFileConfigurationSource GetKeyPerFileSource(string filePath)
        {
            return new KeyPerFileConfigurationSource
            {
                FileProvider = new PhysicalFileProvider(filePath),
                Optional = true,
                ReloadOnChange = true,
                SectionDelimiter = "--" // Set your custom delimiter here
            };
        }
    }
    
    
}
