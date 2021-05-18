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
                        "6f16c9df-9ddd-4c4d-a743-7d87853fd753", (int) NotificationType.EJudJudgeDemoOrTest,
                        (int) MessageType.Email,
                        "test type,date,time,case number,judicial office holder,username"
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
