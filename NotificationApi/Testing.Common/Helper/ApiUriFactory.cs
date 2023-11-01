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
            public static string SendParticipantSingleDayHearingConfirmationForNewUserEmail => "participant-single-day-hearing-confirmation-email-new-user";
            public static string SendParticipantMultiDayHearingConfirmationForNewUserEmail => "participant-multi-day-hearing-confirmation-email-new-user";
            public static string SendParticipantSingleDayHearingConfirmationForExistingUserEmail => "participant-single-day-hearing-confirmation-email-existing-user";
            public static string SendParticipantMultiDayHearingConfirmationForExistingUserEmail => "participant-multi-day-hearing-confirmation-email-existing-user";
            public static string SendSingleDayHearingReminderEmail => "participant-single-day-hearing-reminder-email";
            public static string SendMultiDayHearingReminderEmail => "participant-multi-day-hearing-reminder-email";
            
        }
    }
}
