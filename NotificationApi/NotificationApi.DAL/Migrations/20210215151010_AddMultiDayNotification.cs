using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddMultiDayNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                nameof(Template),
                new[] {"NotifyTemplateId", "NotificationType", "MessageType", "Parameters"},
                new object[,]
                {
                    {
                        "e07120b8-7fb8-43b6-88d4-909953453f05", (int) NotificationType.HearingConfirmationJudgeMultiDay,
                        (int) MessageType.Email,
                        "case name,case number,judge,Start Day Month Year,time,number of days,courtroom account username,account password"
                    },
                    {
                        "7926b67c-d9f7-4a42-a9aa-674b9ef8783b", (int) NotificationType.HearingConfirmationJohMultiDay,
                        (int) MessageType.Email,
                        "case name,case number,judicial office holder,Start Day Month Year,time,number of days,time"
                    },
                    {
                        "3dc60474-92ea-488f-a9ee-42a1d7c604a2", (int) NotificationType.HearingConfirmationLipMultiDay,
                        (int) MessageType.Email,
                        "case name,case number,name,Start Day Month Year,time,number of days,time"
                    },
                    {
                        "39fb2e4b-6a1a-4bd2-8763-889966dd6be0",
                        (int) NotificationType.HearingConfirmationRepresentativeMultiDay,
                        (int) MessageType.Email,
                        "case name,case number,client name,solicitor name,Start Day Month Year,time,number of days,time"
                    },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingConfirmationJudgeMultiDay
            );

            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingConfirmationJohMultiDay
            );

            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingConfirmationLipMultiDay
            );

            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingConfirmationRepresentativeMultiDay
            );
        }
    }
}
