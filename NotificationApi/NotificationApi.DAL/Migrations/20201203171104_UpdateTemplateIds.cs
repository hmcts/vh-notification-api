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
                "145dd703-6b4e-4570-bc48-dce1f10e76c7",
                "name,username,random password"
            } );
            
            migrationBuilder.UpdateData(
                table: nameof(Template),
                keyColumn: "Id",
                keyValue: 2,
                columns: new [] {"Parameters"},
                values: new object[] {
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
