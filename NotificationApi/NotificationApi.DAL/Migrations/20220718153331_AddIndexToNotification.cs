using Microsoft.EntityFrameworkCore.Migrations;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddIndexToNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Notification_NotificationType_HearingRefId_ParticipantRefId",
                table: "Notification",
                columns: new[] { "NotificationType", "HearingRefId", "ParticipantRefId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notification_NotificationType_HearingRefId_ParticipantRefId",
                table: "Notification");
        }
    }
}
