using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddEJudJudgeNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                nameof(Template),
                new[] {"NotifyTemplateId", "NotificationType", "MessageType", "Parameters"},
                new object[,]
                {
                    {
                        "28b905ea-eca2-48d3-88ea-b87d578306e9", (int) NotificationType.HearingConfirmationEJudJudge,
                        (int) MessageType.Email,
                        "case name,case number,judge,day month year,time"
                    },
                    {
                        "152f75bc-291c-4420-9411-f39fe8a433ed", (int) NotificationType.HearingConfirmationEJudJudgeMultiDay,
                        (int) MessageType.Email,
                        "case name,case number,judge,Start Day Month Year,time,number of days"
                    },
                    {
                        "2ff7b633-e285-4e25-9029-83012dd448ba", (int) NotificationType.HearingAmendmentEJudJudge,
                        (int) MessageType.Email,
                        "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time"
                    }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingConfirmationEJudJudge
            );

            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingConfirmationEJudJudgeMultiDay
            );

            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingAmendmentEJudJudge
            );
        }
    }
}
