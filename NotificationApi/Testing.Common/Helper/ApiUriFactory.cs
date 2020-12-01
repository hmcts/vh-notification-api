namespace Testing.Common.Helper
{
    public static class ApiUriFactory
    {
        public static class NotificationEndpoints
        {
            private const string ApiRoot = "Notification";
            public static string CreateNewEmailNotificationResponse() => $"{ApiRoot}/email";
            public static string UpdateNotification => $"{ApiRoot}";
        }
    }
}
