using Microsoft.Extensions.Configuration;
using System;

namespace NotificationApi.DAL.Migrations
{
    public static class NotificationConfiguration
    {
        public static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.template.json", optional: true);

            var config = builder.Build();

            return config.GetSection("NotifyConfiguration");
        }

        public static Guid GetValue(string key) => GetConfiguration().GetValue<Guid>(key);
    }
}
