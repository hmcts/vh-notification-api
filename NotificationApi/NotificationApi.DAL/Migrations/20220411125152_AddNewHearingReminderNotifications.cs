using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddNewHearingReminderNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var templateFactory = NotifyTemplateFactory.Get();

            migrationBuilder.InsertData(
                nameof(Template),
                new[] { "NotifyTemplateId", "NotificationType", "MessageType", "Parameters" },
                new object[,]
                {
                    {
                        templateFactory.NewHearingReminderLIP, (int) NotificationType.NewHearingReminderLIP,
                        (int) MessageType.Email,
                        "case name, case number, name, day month year, time"
                    },
                    {
                        templateFactory.NewHearingReminderRepresentative, (int) NotificationType.NewHearingReminderRepresentative,
                        (int) MessageType.Email,
                        "case name, case number, client name, solicitor name, day month year, time"
                    },
                    {
                        templateFactory.NewHearingReminderJOH, (int) NotificationType.NewHearingReminderJOH,
                        (int) MessageType.Email,
                        "case name, case number, judicial office holder, day month year, time"
                    },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.NewHearingReminderLIP
            );

            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.NewHearingReminderRepresentative
            );
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.NewHearingReminderJOH
            );

        }
    }
}
