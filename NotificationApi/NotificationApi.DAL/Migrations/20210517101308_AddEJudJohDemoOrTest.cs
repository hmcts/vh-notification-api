using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddEJudJohDemoOrTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               nameof(Template),
               new[] { "NotifyTemplateId", "NotificationType", "MessageType", "Parameters" },
               new object[,]
               {
                     {
                        "6f16c9df-9ddd-4c4d-a743-7d87853fd753", (int) NotificationType.EJudJohDemoOrTest,
                        (int) MessageType.Email,
                        "case number,test type,judicial office holder,username,date,time"
                    }
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.EJudJohDemoOrTest
            );
        }
    }
}
