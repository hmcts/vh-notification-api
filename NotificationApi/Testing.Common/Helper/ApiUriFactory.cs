namespace Testing.Common.Helper
{
    public static class ApiUriFactory
    {
        public static class NotificationEndpoints
        {
            private const string ApiRoot = "notification";
            public static string CreateNewEmailNotificationResponse => $"{ApiRoot}";
            public static string UpdateNotification => $"{ApiRoot}/callback";
            public static string GetTemplateByNotificationType(int notificationType) =>
                $"{ApiRoot}/template/{notificationType}";
        }
    }
}
