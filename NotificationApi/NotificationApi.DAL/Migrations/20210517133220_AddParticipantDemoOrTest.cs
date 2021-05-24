using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddParticipantDemoOrTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                nameof(Template),
                new[] { "NotifyTemplateId", "NotificationType", "MessageType", "Parameters" },
                new object[,]
                {
                    {
                        "f0898ba0-3c26-40c6-b311-1ec4b0e5b01c", (int) NotificationType.ParticipantDemoOrTest,
                        (int) MessageType.Email,
                        "test type,case number,date,name,username,time"
                    }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.ParticipantDemoOrTest
            );
        }
    }
}
