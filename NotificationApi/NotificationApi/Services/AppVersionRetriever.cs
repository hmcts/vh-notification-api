using System.Reflection;

namespace NotificationApi.Services;

public static class AppVersionRetriever
{
    /// <summary>
    /// Get the current version of the application (major.minor.build)
    /// </summary>
    /// <returns>current app version</returns>
    public static string GetAppVersion()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version;
        return version == null ? "Unknown" : $"{version.Major}.{version.Minor}.{version.Build}";
    }
}
