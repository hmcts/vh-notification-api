using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class SeedDataToCreateNotifyTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: nameof(Template),
               columns: new[] { "Id", "NotifyTemplateId", "NotificationType", "MessageType", "Parameters" },
               values: new object[,]
               {
                    {1, "06407ff7-988a-480b-82ae-94d8730e5357", NotificationType.CreateIndividual, MessageType.Email, "Name,Username,Password"},
                    {2, "1dc8deb5-19e3-4ac4-8f17-5930bfdbcec1", NotificationType.CreateRepresentative, MessageType.Email, "Name,Username,Password"},
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(nameof(Template), "Id", 1);
            migrationBuilder.DeleteData(nameof(Template), "Id", 2);
        }
    }
}
