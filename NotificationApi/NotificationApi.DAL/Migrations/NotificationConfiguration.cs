using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace NotificationApi.DAL.Migrations
{
    public static class NotificationConfiguration
    {
        private static NotifySection _notifySection = null;

        public static NotifySection Get()
        {
            if (_notifySection == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .AddUserSecrets("4E35D845-27E7-4A19-BE78-CDA896BF907D");

                var config = builder.Build();
                _notifySection = config.GetSection("NotifyConfiguration").Get<NotifySection>();
            }

            return _notifySection;
        }
    }
}
