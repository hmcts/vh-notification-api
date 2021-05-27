using System.IO;
using Microsoft.Extensions.Configuration;

namespace NotificationApi.DAL.Migrations
{
    public static class NotifyTemplateFactory
    {
        private static NotifyTemplates _notifyTemplates = null;

        public static NotifyTemplates Get()
        {
            if (_notifyTemplates == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .AddUserSecrets("4E35D845-27E7-4A19-BE78-CDA896BF907D");

                var config = builder.Build();
                _notifyTemplates = config.GetSection("NotifyConfiguration").Get<NotifyTemplates>();
            }

            return _notifyTemplates;
        }
    }
}
