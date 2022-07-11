using Microsoft.EntityFrameworkCore.Migrations;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddParametersString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Parameters",
                table: "Notification",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parameters",
                table: "Notification");
        }
    }
}
