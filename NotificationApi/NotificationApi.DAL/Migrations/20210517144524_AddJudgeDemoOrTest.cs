using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using System;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddJudgeDemoOrTest : Migration
    {
        private Guid _judgeDemoOrTest;
        private Guid _ejudJudgeDemoOrTest;
        private readonly IConfiguration _configuration = NotificationConfiguration.GetConfiguration();

        public AddJudgeDemoOrTest()
        {
            _judgeDemoOrTest = NotificationConfiguration.GetValue(_configuration, nameof(NotificationType.JudgeDemoOrTest));
            _ejudJudgeDemoOrTest = NotificationConfiguration.GetValue(_configuration, nameof(NotificationType.EJudJudgeDemoOrTest));
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               nameof(Template),
               new[] { "NotifyTemplateId", "NotificationType", "MessageType", "Parameters" },
               new object[,]
               {
                    {
                        _ejudJudgeDemoOrTest, (int) NotificationType.EJudJudgeDemoOrTest,
                        (int) MessageType.Email,
                        "test type,date,time,case number,Judge"
                    },
                    {
                        _judgeDemoOrTest, (int) NotificationType.JudgeDemoOrTest,
                        (int) MessageType.Email,
                        "test type,date,time,case number,Judge,courtroom account username"
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
