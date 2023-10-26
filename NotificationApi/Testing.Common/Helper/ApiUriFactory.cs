namespace Testing.Common.Helper
{
    public static class ApiUriFactory
    {
        public static class NotificationEndpoints
        {
            private const string ApiRoot = "notification";
            public static string CreateNewEmailNotification => $"{ApiRoot}";
            public static string UpdateNotification => $"{ApiRoot}/callback";
            public static string GetTemplateByNotificationType(int notificationType) =>
                $"{ApiRoot}/template/{notificationType}";
        }
        
        public static class ParticipantNotificationEndpoints
        {
            public static string SendParticipantWelcomeEmail => "participant-welcome-email";
        }
    }
}
