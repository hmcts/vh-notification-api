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
                        "a85b7c4b-bc32-41b9-ab3b-0eedc0ff8617", (int) NotificationType.HearingConfirmationJudgeMultiDay,
                        (int) MessageType.Email,
                        "case name,case number,judge,Start Day Month Year,time,number of days,courtroom account username,account password"
                    },
                    {
                        "77f66714-fafc-4642-b505-33517e8a6ba8", (int) NotificationType.HearingConfirmationJohMultiDay,
                        (int) MessageType.Email,
                        "case name,case number,judicial office holder,Start Day Month Year,time,number of days,time"
                    },
                    {
                        "bf20004c-258b-4de9-83a1-d7b260750b9f", (int) NotificationType.HearingConfirmationLipMultiDay,
                        (int) MessageType.Email,
                        "case name,case number,name,Start Day Month Year,time,number of days,time"
                    },
                    {
                        "c06b60b0-ca26-43ee-b6b6-7e5aa5a2b170",
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
