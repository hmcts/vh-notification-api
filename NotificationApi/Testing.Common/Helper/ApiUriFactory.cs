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

            public static string GetPasswordNotificationByEmail(string email) =>
                $"{ApiRoot}/{email}";

            public static string GetNotificationByHearingAndParticipant(int notificationType, string participantId, string hearingId) =>
               $"{ApiRoot}/{notificationType}/{hearingId}/{participantId}";
        }
    }
}
