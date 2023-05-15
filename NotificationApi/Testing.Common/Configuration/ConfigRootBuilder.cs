using Microsoft.Extensions.Configuration;

namespace Testing.Common.Configuration;

public static class ConfigRootBuilder
{
    private const string UserSecretId = "4E35D845-27E7-4A19-BE78-CDA896BF907D";
    public static IConfigurationRoot Build(string userSecretId = UserSecretId)
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Production.json", true) // CI write variables in the pipeline to this file
            .AddUserSecrets(userSecretId)
            .AddEnvironmentVariables()
            .Build();
    }
}
