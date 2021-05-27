using Microsoft.Extensions.Configuration;

namespace NotificationApi.DAL.Migrations
{
    public static class NotificationConfiguration
    {
        private static NotifyConfiguration _notifyConfiguration;

        public static NotifyConfiguration Get()
        {
            if (_notifyConfiguration != null) return _notifyConfiguration;

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets("4E35D845-27E7-4A19-BE78-CDA896BF907D");

            var config = builder.Build();
            _notifyConfiguration = config.GetSection("NotifyConfiguration").Get<NotifyConfiguration>();

            return _notifyConfiguration;
        }
    }
}
