using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using System;

namespace NotificationApi.DAL.Migrations
{
    public partial class AutomateRetroNotificationIds : Migration
    {
        private Guid _createIndividual;
        private Guid _createRepresentative;
        private Guid _passwordReset;
        private Guid _hearingConfirmationLip;
        private Guid _hearingConfirmationRepresentative;
        private Guid _hearingConfirmationJudge;
        private Guid _hearingConfirmationJoh;
        private Guid _hearingConfirmationLipMultiDay;
        private Guid _hearingConfirmationRepresentativeMultiDay;
        private Guid _hearingConfirmationJudgeMultiDay;
        private Guid _hearingConfirmationJohMultiDay;
        private Guid _hearingAmendmentLip;
        private Guid _hearingAmendmentRepresentative;
        private Guid _hearingAmendmentJudge;
        private Guid _hearingAmendmentJoh;
        private Guid _hearingReminderLip;
        private Guid _hearingReminderRepresentative;
        private Guid _hearingReminderJoh;
        private Guid _hearingConfirmationEJudJudge;
        private Guid _hearingConfirmationEJudJudgeMultiDay;
        private Guid _hearingAmendmentEJudJudge;
        private Guid _hearingAmendmentEJudJoh;
        private Guid _hearingReminderEJudJoh;
        private Guid _hearingConfirmationEJudJoh;
        private Guid _hearingConfirmationEJudJohMultiDay;
        private Guid _eJudJohDemoOrTest;
        private Guid _participantDemoOrTest;

        public AutomateRetroNotificationIds()
        {
            var templateFactory = NotifyTemplateFactory.Get();
            _createIndividual = templateFactory.CreateIndividual.Value;
            _createRepresentative = templateFactory.CreateRepresentative.Value;
            _passwordReset = templateFactory.PasswordReset.Value;
            _hearingConfirmationLip = templateFactory.HearingConfirmationLip.Value;
            _hearingConfirmationRepresentative = templateFactory.HearingConfirmationRepresentative.Value;
            _hearingConfirmationJudge = templateFactory.HearingConfirmationJudge.Value;
            _hearingConfirmationJoh = templateFactory.HearingConfirmationJoh.Value;
            _hearingConfirmationLipMultiDay = templateFactory.HearingConfirmationLipMultiDay.Value;
            _hearingConfirmationRepresentativeMultiDay = templateFactory.HearingConfirmationRepresentativeMultiDay.Value;
            _hearingConfirmationJudgeMultiDay = templateFactory.HearingConfirmationJudgeMultiDay.Value;
            _hearingConfirmationJohMultiDay = templateFactory.HearingConfirmationJohMultiDay.Value;
            _hearingAmendmentLip = templateFactory.HearingAmendmentLip.Value;
            _hearingAmendmentRepresentative = templateFactory.HearingAmendmentRepresentative.Value;
            _hearingAmendmentJudge = templateFactory.HearingAmendmentJudge.Value;
            _hearingAmendmentJoh = templateFactory.HearingAmendmentJoh.Value;
            _hearingReminderLip = templateFactory.HearingReminderLip.Value;
            _hearingReminderRepresentative = templateFactory.HearingReminderRepresentative.Value;
            _hearingReminderJoh = templateFactory.HearingReminderJoh.Value;
            _hearingConfirmationEJudJudge = templateFactory.HearingConfirmationEJudJudge.Value;
            _hearingConfirmationEJudJudgeMultiDay = templateFactory.HearingConfirmationEJudJudgeMultiDay.Value;
            _hearingAmendmentEJudJudge = templateFactory.HearingAmendmentEJudJudge.Value;
            _hearingAmendmentEJudJoh = templateFactory.HearingAmendmentEJudJoh.Value;
            _hearingReminderEJudJoh = templateFactory.HearingReminderEJudJoh.Value;
            _hearingConfirmationEJudJoh = templateFactory.HearingConfirmationEJudJoh.Value;
            _hearingConfirmationEJudJohMultiDay = templateFactory.HearingConfirmationEJudJohMultiDay.Value;
            _eJudJohDemoOrTest = templateFactory.EJudJohDemoOrTest.Value;
            _participantDemoOrTest = templateFactory.ParticipantDemoOrTest.Value;
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.CreateIndividual,
                column: "NotifyTemplateId",
                value: _createIndividual);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.CreateRepresentative,
                column: "NotifyTemplateId",
                value: _createRepresentative);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.PasswordReset,
                column: "NotifyTemplateId",
                value: _passwordReset);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationLip,
                column: "NotifyTemplateId",
                value: _hearingConfirmationLip);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationRepresentative,
                column: "NotifyTemplateId",
                value: _hearingConfirmationRepresentative);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationJudge,
                column: "NotifyTemplateId",
                value: _hearingConfirmationJudge);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationJoh,
                column: "NotifyTemplateId",
                value: _hearingConfirmationJoh);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationLipMultiDay,
                column: "NotifyTemplateId",
                value: _hearingConfirmationLipMultiDay);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationRepresentativeMultiDay,
                column: "NotifyTemplateId",
                value: _hearingConfirmationRepresentativeMultiDay);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationJudgeMultiDay,
                column: "NotifyTemplateId",
                value: _hearingConfirmationJudgeMultiDay);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationJohMultiDay,
                column: "NotifyTemplateId",
                value: _hearingConfirmationJohMultiDay);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingAmendmentLip,
                column: "NotifyTemplateId",
                value: _hearingAmendmentLip);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingAmendmentRepresentative,
                column: "NotifyTemplateId",
                value: _hearingAmendmentRepresentative);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingAmendmentJudge,
                column: "NotifyTemplateId",
                value: _hearingAmendmentJudge);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingAmendmentJoh,
                column: "NotifyTemplateId",
                value: _hearingAmendmentJoh);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingReminderLip,
                column: "NotifyTemplateId",
                value: _hearingReminderLip);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingReminderRepresentative,
                column: "NotifyTemplateId",
                value: _hearingReminderRepresentative);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingReminderJoh,
                column: "NotifyTemplateId",
                value: _hearingReminderJoh);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationEJudJudge,
                column: "NotifyTemplateId",
                value: _hearingConfirmationEJudJudge);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationEJudJudgeMultiDay,
                column: "NotifyTemplateId",
                value: _hearingConfirmationEJudJudgeMultiDay);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingAmendmentEJudJudge,
                column: "NotifyTemplateId",
                value: _hearingAmendmentEJudJudge);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingAmendmentEJudJoh,
                column: "NotifyTemplateId",
                value: _hearingAmendmentEJudJoh);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingReminderEJudJoh,
                column: "NotifyTemplateId",
                value: _hearingReminderEJudJoh);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationEJudJoh,
                column: "NotifyTemplateId",
                value: _hearingConfirmationEJudJoh);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationEJudJohMultiDay,
                column: "NotifyTemplateId",
                value: _hearingConfirmationEJudJohMultiDay);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.EJudJohDemoOrTest,
                column: "NotifyTemplateId",
                value: _eJudJohDemoOrTest);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.ParticipantDemoOrTest,
                column: "NotifyTemplateId",
                value: _participantDemoOrTest);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //
        }
    }
}
