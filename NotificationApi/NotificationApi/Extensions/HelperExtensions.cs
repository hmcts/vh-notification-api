namespace NotificationApi.Extensions;

public static class HelperExtensions
{
    public static bool IsJudiciaryUsername(this string username) => username.Contains("judiciary", StringComparison.CurrentCultureIgnoreCase);
}
