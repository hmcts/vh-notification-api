namespace NotificationApi.Domain.Enums
{
    // Public contract at NotificationApi.Contract.NotificationType
    public enum NotificationType
    {
        CreateIndividual = 1,
        CreateRepresentative = 2,
        PasswordReset = 3,
        HearingConfirmationLip = 4,
        HearingConfirmationRepresentative = 5,
        HearingConfirmationJudge = 6,
        HearingAmendmentLip = 7,
        HearingAmendmentRepresentative = 8,
        HearingAmendmentJudge = 9,
    }
}
