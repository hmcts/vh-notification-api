namespace NotificationApi.Contract
{
    // Public contract for NotificationApi.Domain.Enums.NotificationType
    public enum NotificationType
    {
        CreateIndividual = 1,
        CreateRepresentative = 2,
        PasswordReset = 3,
        HearingConfirmationLip = 4,
        HearingConfirmationRepresentative = 5,
        HearingConfirmationJudge = 6,
        HearingConfirmationJoh = 7,
        HearingConfirmationLipMultiDay = 8,
        HearingConfirmationRepresentativeMultiDay = 9,
        HearingConfirmationJudgeMultiDay = 10,
        HearingConfirmationJohMultiDay = 11,
        HearingAmendmentLip = 12,
        HearingAmendmentRepresentative = 13,
        HearingAmendmentJudge = 14,
        HearingAmendmentJoh = 15,
        HearingReminderLip = 16,
        HearingReminderRepresentative = 17,
        HearingReminderJoh = 18,
        HearingConfirmationEJudJudge = 19,
        HearingConfirmationEJudJudgeMultiDay = 20,
        HearingAmendmentEJudJudge = 21,
        HearingAmendmentEJudJoh = 22,
        HearingReminderEJudJoh = 23,
        HearingConfirmationEJudJoh = 24,
        HearingConfirmationEJudJohMultiDay = 25
    }
}
