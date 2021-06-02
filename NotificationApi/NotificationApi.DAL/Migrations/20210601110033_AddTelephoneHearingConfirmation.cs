using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddTelephoneHearingConfirmation : Migration
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
                        templateFactory.TelephoneHearingConfirmation, (int) NotificationType.TelephoneHearingConfirmation,
                        (int) MessageType.Email,
                        "case name,case number,name,day month year,time"
                    },
                    {
                        templateFactory.TelephoneHearingConfirmationMultiDay, (int) NotificationType.TelephoneHearingConfirmationMultiDay,
                        (int) MessageType.Email,
                        "case name,case number,name,day month year,time,number of days"
                    }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.TelephoneHearingConfirmation
            );

            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.TelephoneHearingConfirmationMultiDay
            );
        }
    }
}
