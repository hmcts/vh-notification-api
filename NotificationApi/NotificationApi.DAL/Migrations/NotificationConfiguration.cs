using Microsoft.Extensions.Configuration;
using System;

namespace NotificationApi.DAL.Migrations
{
    public static class NotificationConfiguration
    {
        private static IConfiguration _notifyConfiguration;

        public static IConfiguration GetConfiguration()
        {
            if(_notifyConfiguration == null)
            {
                var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                _notifyConfiguration = builder.Build();
            }

            return _notifyConfiguration.GetSection("NotifyConfiguration");
        }

        public static Guid GetValue(string key) => GetConfiguration().GetValue<Guid>(key);
    }
}
