using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;

namespace NotificationApi.DAL.Migrations
{
    public partial class UpdateTemplateIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "Id",
                keyValue: 1,
                columns: new [] {"NotifyTemplateId", "Parameters"},
                values: new object[] {
                "94d06843-4608-4cda-9933-9d0f3d7ce535",
                "name,username,random password"
            } );
            
            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "Id",
                keyValue: 2,
                columns: new [] {"NotifyTemplateId", "Parameters"},
                values: new object[] {
                    "e61575fc-05c8-40da-b7a7-f7b1d04ff2db",
                    "name,username,random password"
                } );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "Id",
                keyValue: 1,
                columns: new [] {"NotifyTemplateId", "Parameters"},
                values: new object[] {
                    "06407ff7-988a-480b-82ae-94d8730e5357",
                    "Name,Username,Password"
                } );
            
            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "Id",
                keyValue: 2,
                columns: new [] {"Parameters"},
                values: new object[] {
                    "Name,Username,Password"
                } );
        }
    }
}
