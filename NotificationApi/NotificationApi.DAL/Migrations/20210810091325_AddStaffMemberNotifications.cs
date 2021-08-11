using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddStaffMemberNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var templateFactory = NotifyTemplateFactory.Get();

            migrationBuilder.InsertData(
                nameof(Template),
                new[] { "NotifyTemplateId", "NotificationType", "MessageType", "Parameters" },
                new object[,]
                {
                    {
                        templateFactory.CreateStaffMember, (int) NotificationType.CreateStaffMember,
                        (int) MessageType.Email,
                        "Name,Username,Password"
                    },
                    {
                        templateFactory.HearingAmendmentStaffMember, (int) NotificationType.HearingAmendmentStaffMember,
                        (int) MessageType.Email,
                        "case name,case number,staff member,New Day Month Year,Old Day Month Year,New time,Old time"
                    },
                    {
                        templateFactory.HearingConfirmationStaffMember, (int) NotificationType.HearingConfirmationStaffMember,
                        (int) MessageType.Email,
                        "case name,case number,staff member,day month year,time,username"
                    },
                    {
                        templateFactory.HearingConfirmationStaffMemberMultiDay, (int) NotificationType.HearingConfirmationStaffMemberMultiDay,
                        (int) MessageType.Email,
                        "case name,case number,staff member,Start Day Month Year,time,number of days,username"
                    },
                    {
                        templateFactory.StaffMemberDemoOrTest, (int) NotificationType.StaffMemberDemoOrTest,
                        (int) MessageType.Email,
                        "test type,date,time,case number,staff member"
                    },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.CreateStaffMember
            );

            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.HearingConfirmationStaffMember
            );
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.HearingConfirmationStaffMemberMultiDay
            );
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.HearingAmendmentStaffMember
            );
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int)NotificationType.StaffMemberDemoOrTest
            );
        }
    }
}
