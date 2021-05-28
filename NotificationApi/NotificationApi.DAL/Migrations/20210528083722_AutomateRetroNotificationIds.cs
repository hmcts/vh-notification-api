using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using System;

namespace NotificationApi.DAL.Migrations
{
    public partial class AutomateRetroNotificationIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var templateFactory = NotifyTemplateFactory.Get();

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.CreateIndividual,
                column: "NotifyTemplateId",
                value: templateFactory.CreateIndividual.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.CreateRepresentative,
                column: "NotifyTemplateId",
                value: templateFactory.CreateRepresentative.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.PasswordReset,
                column: "NotifyTemplateId",
                value: templateFactory.PasswordReset.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationLip,
                column: "NotifyTemplateId",
                value: templateFactory.HearingConfirmationLip.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationRepresentative,
                column: "NotifyTemplateId",
                value: templateFactory.HearingConfirmationRepresentative.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationJudge,
                column: "NotifyTemplateId",
                value: templateFactory.HearingConfirmationJudge.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationJoh,
                column: "NotifyTemplateId",
                value: templateFactory.HearingConfirmationJoh.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationLipMultiDay,
                column: "NotifyTemplateId",
                value: templateFactory.HearingConfirmationLipMultiDay.Value);


            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationRepresentativeMultiDay,
                column: "NotifyTemplateId",
                value: templateFactory.HearingConfirmationRepresentativeMultiDay.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationJudgeMultiDay,
                column: "NotifyTemplateId",
                value: templateFactory.HearingConfirmationJudgeMultiDay.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationJohMultiDay,
                column: "NotifyTemplateId",
                value: templateFactory.HearingConfirmationJohMultiDay.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingAmendmentLip,
                column: "NotifyTemplateId",
                value: templateFactory.HearingAmendmentLip.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingAmendmentRepresentative,
                column: "NotifyTemplateId",
                value: templateFactory.HearingAmendmentRepresentative.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingAmendmentJudge,
                column: "NotifyTemplateId",
                value: templateFactory.HearingAmendmentJudge.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingAmendmentJoh,
                column: "NotifyTemplateId",
                value: templateFactory.HearingAmendmentJoh.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingReminderLip,
                column: "NotifyTemplateId",
                value: templateFactory.HearingReminderLip.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingReminderRepresentative,
                column: "NotifyTemplateId",
                value: templateFactory.HearingReminderRepresentative.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingReminderJoh,
                column: "NotifyTemplateId",
                value: templateFactory.HearingReminderJoh.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationEJudJudge,
                column: "NotifyTemplateId",
                value: templateFactory.HearingConfirmationEJudJudge.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationEJudJudgeMultiDay,
                column: "NotifyTemplateId",
                value: templateFactory.HearingConfirmationEJudJudgeMultiDay.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingAmendmentEJudJudge,
                column: "NotifyTemplateId",
                value: templateFactory.HearingAmendmentEJudJudge.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingAmendmentEJudJoh,
                column: "NotifyTemplateId",
                value: templateFactory.HearingAmendmentEJudJoh.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingReminderEJudJoh,
                column: "NotifyTemplateId",
                value: templateFactory.HearingReminderEJudJoh.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationEJudJoh,
                column: "NotifyTemplateId",
                value: templateFactory.HearingConfirmationEJudJoh.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.HearingConfirmationEJudJohMultiDay,
                column: "NotifyTemplateId",
                value: templateFactory.HearingConfirmationEJudJohMultiDay.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.EJudJohDemoOrTest,
                column: "NotifyTemplateId",
                value: templateFactory.EJudJohDemoOrTest.Value);

            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "NotificationType",
                keyValue: (int)NotificationType.ParticipantDemoOrTest,
                column: "NotifyTemplateId",
                value: templateFactory.ParticipantDemoOrTest.Value);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //
        }
    }
}
