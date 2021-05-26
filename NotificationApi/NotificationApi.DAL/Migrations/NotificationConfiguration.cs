using Microsoft.Extensions.Configuration;
using System;

namespace NotificationApi.DAL.Migrations
{
    public static class NotificationConfiguration
    {
        private static IConfiguration _configuration;

        public static IConfiguration GetConfiguration()
        {
            if(_configuration == null)
            {
                var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.template.json", optional: true);

                _configuration = builder.Build();
            }

            return _configuration.GetSection("NotifyConfiguration");
        }

        public static Guid GetValue(string key) => GetConfiguration().GetValue<Guid>(key);
    }
}
