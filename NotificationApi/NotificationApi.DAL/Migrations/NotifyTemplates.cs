using System;

namespace NotificationApi.DAL.Migrations
{
    public class NotifyTemplates
    {
        public Guid? CreateIndividual { get; set; }
        public Guid? CreateRepresentative { get; set; }
        public Guid? PasswordReset { get; set; }
        public Guid? HearingConfirmationLip { get; set; }
        public Guid? HearingConfirmationRepresentative { get; set; }
        public Guid? HearingConfirmationJudge { get; set; }
        public Guid? HearingConfirmationJoh { get; set; }
        public Guid? HearingConfirmationLipMultiDay { get; set; }
        public Guid? HearingConfirmationRepresentativeMultiDay { get; set; }
        public Guid? HearingConfirmationJudgeMultiDay { get; set; }
        public Guid? HearingConfirmationJohMultiDay { get; set; }
        public Guid? HearingAmendmentLip { get; set; }
        public Guid? HearingAmendmentRepresentative { get; set; }
        public Guid? HearingAmendmentJudge { get; set; }
        public Guid? HearingAmendmentJoh { get; set; }
        public Guid? HearingReminderLip { get; set; }
        public Guid? HearingReminderRepresentative { get; set; }
        public Guid? HearingReminderJoh { get; set; }
        public Guid? HearingConfirmationEJudJudge { get; set; }
        public Guid? HearingConfirmationEJudJudgeMultiDay { get; set; }
        public Guid? HearingAmendmentEJudJudge { get; set; }
        public Guid? HearingAmendmentEJudJoh { get; set; }
        public Guid? HearingReminderEJudJoh { get; set; }
        public Guid? HearingConfirmationEJudJoh { get; set; }
        public Guid? HearingConfirmationEJudJohMultiDay { get; set; }
        public Guid? EJudJohDemoOrTest { get; set; }
        public Guid? EJudJudgeDemoOrTest { get; set; }
        public Guid? JudgeDemoOrTest { get; set; }
        public Guid? ParticipantDemoOrTest { get; set; }
        public Guid? TelephoneHearingConfirmation { get; set; }
        public Guid? TelephoneHearingConfirmationMultiDay { get; set; }
        public Guid? CreateStaffMember { get; set; }
        public Guid? HearingAmendmentStaffMember { get; set; }
        public Guid? HearingConfirmationStaffMember { get; set; }
        public Guid? HearingConfirmationStaffMemberMultiDay { get; set; }
        public Guid? StaffMemberDemoOrTest { get; set; }
    }
}
