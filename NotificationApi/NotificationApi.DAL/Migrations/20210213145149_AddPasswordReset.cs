using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddPasswordReset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                nameof(Template),
                new[] {"NotifyTemplateId", "NotificationType", "MessageType", "Parameters"},
                new object[,]
                {
                    {
                        "15f91d90-56f0-480a-957d-11245f9c2cd9", (int) NotificationType.PasswordReset,
                        (int) MessageType.Email, "name,password"
                    },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.PasswordReset
            );
        }
    }
}
