using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddTelephoneHearingConfirmationLip : Migration
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
                        templateFactory.TelephoneHearingConfirmationLip, (int) NotificationType.TelephoneHearingConfirmationLip,
                        (int) MessageType.Email,
                        "case name, case number, name, Day Month Year, time"
                    },
                    {
                        templateFactory.TelephoneHearingConfirmationLipMultiDay, (int) NotificationType.TelephoneHearingConfirmationLipMultiDay,
                        (int) MessageType.Email,
                        "case name, case number, name, Day Month Year, time, number of days"
                    }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.TelephoneHearingConfirmationLip
            );

            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.TelephoneHearingConfirmationLipMultiDay
            );
        }
    }
}
