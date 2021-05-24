using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddJudgeDemoOrTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               nameof(Template),
               new[] { "NotifyTemplateId", "NotificationType", "MessageType", "Parameters" },
               new object[,]
               {
                    {
                        "e2709c28-9e12-4bc5-b2ea-3fc8147e7373", (int) NotificationType.JudgeDemoOrTest,
                        (int) MessageType.Email,
                        "test type,date,time,case number,Judge,courtroom account username"
                    },
                    {
                        "56e9ff91-267f-4154-814a-0281dd100cc6", (int) NotificationType.EJudJudgeDemoOrTest,
                        (int) MessageType.Email,
                        "test type,date,time,case number,Judge,username"
                    }
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.JudgeDemoOrTest
            );
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.EJudJudgeDemoOrTest
            );
        }
    }
}
