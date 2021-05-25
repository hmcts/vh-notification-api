using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace NotificationApi.DAL.Migrations
{
    public static class NotificationConfiguration
    {
        public static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Path.GetFullPath("../NotificationApi/"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables();

            var config = builder.Build();

            return config.GetSection("NotifyConfiguration");
        }

        public static Guid GetValue(string key) => GetConfiguration().GetValue<Guid>(key);
    }
}
