namespace Testing.Common.Helper
{
    public static class ApiUriFactory
    {
        public static class NotificationEndpoints
        {
            private const string ApiRoot = "notification";
            public static string UpdateNotification => $"{ApiRoot}";

            public static string GetTemplateByNotificationType(int notificationType) =>
                $"{ApiRoot}/template/{notificationType}";
        }
    }
}
