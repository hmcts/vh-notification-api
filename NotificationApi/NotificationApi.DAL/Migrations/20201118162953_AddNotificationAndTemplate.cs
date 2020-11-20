using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddNotificationAndTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Payload = table.Column<string>(nullable: false),
                    DeliveryStatus = table.Column<int>(nullable: false, defaultValue: 0),
                    NotificationType = table.Column<int>(nullable: false),
                    ParticipantRefId = table.Column<Guid>(nullable: false),
                    HearingRefId = table.Column<Guid>(nullable: false),
                    ExternalId = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ToEmail = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotifyTemplateId = table.Column<Guid>(nullable: false),
                    NotificationType = table.Column<int>(nullable: false),
                    MessageType = table.Column<int>(nullable: false),
                    Parameters = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Template");
        }
    }
}
