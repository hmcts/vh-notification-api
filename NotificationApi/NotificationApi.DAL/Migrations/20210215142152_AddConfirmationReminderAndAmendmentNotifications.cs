using Microsoft.EntityFrameworkCore.Migrations;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Migrations
{
    public partial class AddConfirmationReminderAndAmendmentNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                nameof(Template),
                new[] {"NotifyTemplateId", "NotificationType", "MessageType", "Parameters"},
                new object[,]
                {
                    {
                        "6489a0f3-538f-49bc-86c5-b2787c71062a", (int) NotificationType.HearingConfirmationJudge,
                        (int) MessageType.Email, "case name,case number,judge,day month year,time,courtroom account username,account password"
                    },
                    {
                        "7daf5de2-256d-400f-854d-b8ea06df2b84", (int) NotificationType.HearingConfirmationJoh,
                        (int) MessageType.Email, "case name,case number,judicial office holder,Day Month Year,time"
                    },
                    {
                        "2cf31323-3533-404d-9b52-73b33ce4a1be", (int) NotificationType.HearingConfirmationLip,
                        (int) MessageType.Email, "case name,case number,name,Day Month Year,time"
                    },
                    {
                        "2d39ff6c-d036-4de0-a953-f148f9839f3c", (int) NotificationType.HearingConfirmationRepresentative,
                        (int) MessageType.Email, "case name,case number,client name,solicitor name,Day Month Year,time"
                    },

                    {
                        "c0744bd9-4d9b-4f1a-ba3e-148c216fe1ea", (int) NotificationType.HearingReminderJoh,
                        (int) MessageType.Email, "case name,case number,judicial office holder,day month year,time,username"
                    },
                    {
                        "385f78f3-0e77-45a5-a1b0-6ce793a9cdb2", (int) NotificationType.HearingReminderLip,
                        (int) MessageType.Email, "case name,case number,name,day month year,time,username"
                    },
                    {
                        "7c90feb4-f819-40b8-98ce-f83332b48014", (int) NotificationType.HearingReminderRepresentative,
                        (int) MessageType.Email, "case name,case number,client name,solicitor name,day month year,time,username"
                    },
                    
                    {
                        "7c90e8da-727b-4614-a3c6-b05219920cde", (int) NotificationType.HearingAmendmentJudge,
                        (int) MessageType.Email, "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password"
                    },
                    {
                        "21a701ad-89d3-451a-9a87-544057541909", (int) NotificationType.HearingAmendmentJoh,
                        (int) MessageType.Email, "case name,case number,judicial office holder,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password"
                    },
                    {
                        "02817327-5533-4051-ae69-3609ddeba8fb", (int) NotificationType.HearingAmendmentLip,
                        (int) MessageType.Email, "case name,case number,name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password"
                    },
                    {
                        "5299ac1f-bf42-4c68-82bd-e9c0f0ee51ba", (int) NotificationType.HearingAmendmentRepresentative,
                        (int) MessageType.Email, "case name,case number,client name,solicitor name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password"
                    }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingConfirmationJudge
            );
            
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingConfirmationJoh
            );
            
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingConfirmationLip
            );
            
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingConfirmationRepresentative
            );
            
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingReminderJoh
            );
            
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingReminderLip
            );
            
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingReminderRepresentative
            );
            
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingConfirmationRepresentative
            );
            
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingAmendmentJudge
            );
            
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingAmendmentJoh
            );
            
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingAmendmentLip
            );
            
            migrationBuilder.DeleteData(
                nameof(Template),
                "NotificationType",
                (int) NotificationType.HearingAmendmentRepresentative
            );
        }
    }
}
