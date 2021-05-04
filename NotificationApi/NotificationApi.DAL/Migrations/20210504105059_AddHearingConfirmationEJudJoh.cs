using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddHearingConfirmationEJudJoh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               nameof(Template),
               new[] { "NotifyTemplateId", "NotificationType", "MessageType", "Parameters" },
               new object[,]
               {
                     {
                        "4b6c39a5-88ab-475c-9e3a-62be1778a16d", (int) NotificationType.HearingConfirmationEJudJoh,
                        (int) MessageType.Email,
                        "case name,case number,judicial office holder,day month year,time"
                    },
                    {
                        "ed0f327e-324b-446c-bb0a-48bea0f67786", (int) NotificationType.HearingConfirmationEJudJohMultiDay,
                        (int) MessageType.Email,
                        "case name,case number,judicial office holder,Start Day Month Year,time,number of days"
                    }
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.HearingConfirmationEJudJohMultiDay
            );

            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.HearingConfirmationEJudJoh
            );
        }
    }
}
