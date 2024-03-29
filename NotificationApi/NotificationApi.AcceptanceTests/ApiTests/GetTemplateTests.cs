using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.Contract;
using NUnit.Framework;

namespace NotificationApi.AcceptanceTests.ApiTests
{
    public class GetTemplateTests : AcApiTest
    {
        [TestCase(NotificationType.CreateIndividual)]
        [TestCase(NotificationType.CreateRepresentative)]
        [TestCase(NotificationType.HearingConfirmationLip)]
        [TestCase(NotificationType.HearingConfirmationRepresentative)]
        [TestCase(NotificationType.HearingConfirmationJudge)]
        [TestCase(NotificationType.HearingConfirmationJoh)]
        [TestCase(NotificationType.HearingConfirmationLipMultiDay)]
        [TestCase(NotificationType.HearingConfirmationRepresentativeMultiDay)]
        [TestCase(NotificationType.HearingConfirmationJudgeMultiDay)]
        [TestCase(NotificationType.HearingConfirmationJohMultiDay)]
        [TestCase(NotificationType.HearingAmendmentLip)]
        [TestCase(NotificationType.HearingAmendmentRepresentative)]
        [TestCase(NotificationType.HearingAmendmentJudge)]
        [TestCase(NotificationType.HearingAmendmentJoh)]
        [TestCase(NotificationType.HearingReminderLip)]
        [TestCase(NotificationType.HearingReminderRepresentative)]
        [TestCase(NotificationType.HearingReminderJoh)]
        [TestCase(NotificationType.HearingConfirmationEJudJudge)]
        [TestCase(NotificationType.HearingConfirmationEJudJudgeMultiDay)]
        [TestCase(NotificationType.HearingAmendmentEJudJudge)]
        [TestCase(NotificationType.HearingAmendmentEJudJoh)]
        [TestCase(NotificationType.HearingReminderEJudJoh)]
        [TestCase(NotificationType.HearingConfirmationEJudJoh)]
        [TestCase(NotificationType.HearingConfirmationEJudJohMultiDay)]
        [TestCase(NotificationType.ParticipantDemoOrTest)]
        [TestCase(NotificationType.EJudJohDemoOrTest)]
        [TestCase(NotificationType.JudgeDemoOrTest)]
        [TestCase(NotificationType.EJudJudgeDemoOrTest)]
        [TestCase(NotificationType.TelephoneHearingConfirmation)]
        [TestCase(NotificationType.TelephoneHearingConfirmationMultiDay)]
        [TestCase(NotificationType.CreateStaffMember)]
        [TestCase(NotificationType.HearingAmendmentStaffMember)]
        [TestCase(NotificationType.HearingConfirmationStaffMember)]
        [TestCase(NotificationType.HearingConfirmationStaffMemberMultiDay)]
        [TestCase(NotificationType.StaffMemberDemoOrTest)]
        [TestCase(NotificationType.NewHearingReminderLIP)]
        [TestCase(NotificationType.NewHearingReminderRepresentative)]
        [TestCase(NotificationType.NewHearingReminderJOH)]
        [TestCase(NotificationType.NewHearingReminderEJudJoh)]
        [TestCase(NotificationType.NewUserLipWelcome)]
        [TestCase(NotificationType.NewUserLipConfirmation)]
        [TestCase(NotificationType.NewUserLipConfirmationMultiDay)]
        [TestCase(NotificationType.ExistingUserLipConfirmation)]
        [TestCase(NotificationType.ExistingUserLipConfirmationMultiDay)]
        [TestCase(NotificationType.NewHearingReminderLipSingleDay)]
        [TestCase(NotificationType.NewHearingReminderLipMultiDay)]
        [TestCase(NotificationType.NewUserRepresentativeWelcome)]
        [TestCase(NotificationType.NewUserRepresentativeConfirmation)]
        [TestCase(NotificationType.NewUserRepresentativeConfirmationMultiDay)]
        [TestCase(NotificationType.ExistingUserRepresentativeConfirmation)]
        [TestCase(NotificationType.ExistingUserRepresentativeConfirmationMultiDay)]
        [TestCase(NotificationType.NewHearingReminderRepresentativeSingleDay)]
        [TestCase(NotificationType.NewHearingReminderRepresentativeMultiDay)]
        public async Task should_return_okay_and_template(NotificationType notificationType)
        {
            // act
            var response = await NotificationApiClient.GetTemplateByNotificationTypeAsync(notificationType);
        
            // assert
            response.Should().NotBeNull();
            response.NotificationType.Should().Be(notificationType);
            response.Parameters.Should().NotBeNullOrWhiteSpace();
            response.NotifyTemplateId.Should().NotBeEmpty();
            response.Id.Should().BePositive();
            var notifyTemplate = await NotifyClient.GetTemplateByIdAsync(response.NotifyTemplateId.ToString());
            notifyTemplate.Should().NotBeNull();
        }
    }
}
