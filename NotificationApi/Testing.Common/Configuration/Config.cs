using NotificationApi.Common.Configuration;

namespace Testing.Common.Configuration
{
    public class Config
    {
        public AzureAdConfiguration AzureAdConfiguration { get; set; }
        public ConnectionStringsConfig DbConnection { get; set; }
        public ServicesConfiguration ServicesConfig { get; set; }
    }
}
