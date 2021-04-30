using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddHearingReminderEJudJohNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               nameof(Template),
               new[] { "NotifyTemplateId", "NotificationType", "MessageType", "Parameters" },
               new object[,]
               {
                    {
                        "b5e2e627-398f-4fbf-8853-121d3007e043", (int) NotificationType.HearingReminderEJudJoh,
                        (int) MessageType.Email,
                        "case name,case number,judicial office holder,day month year,time"
                    }
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.HearingReminderEJudJoh
            );
        }
    }
}
