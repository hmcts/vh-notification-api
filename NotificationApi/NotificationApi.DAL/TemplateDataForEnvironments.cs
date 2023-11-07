using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NotificationApi.Common;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL
{
    [ExcludeFromCodeCoverage]
    public class TemplateDataForEnvironments
    {
        private static Template CreateTemplate(string guid, NotificationType notificationType, MessageType messageType, string parameters)
        {
            return new Template(new Guid(guid), notificationType,
                messageType, parameters);
        }

        private readonly IList<Template> _sourceTemplatesDev = new List<Template>()
        {
            {
                CreateTemplate("5C862F23-F12A-463E-A140-1C9C8D44EC0A", NotificationType.CreateIndividual,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.UserName},{NotifyParams.RandomPassword}")
            },
            {
                CreateTemplate("E913CED8-CFA2-49EC-8DA4-9353F0105D97", NotificationType.CreateRepresentative,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.UserName},{NotifyParams.RandomPassword}")
            },
            {
                CreateTemplate("C4D0001F-1706-483C-AB64-0DCB53EB8216", NotificationType.PasswordReset,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.Password}")
            },
            {
                CreateTemplate("811125FE-4CAB-4829-88ED-D3E7D4689CDD",
                    NotificationType.HearingConfirmationJudge, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Judge},{NotifyParams.DayMonthYear},{NotifyParams.Time},{NotifyParams.CourtroomAccountUserName}")
            },
            {
                CreateTemplate("1D4ACED7-4A0B-4610-B4C7-71F6C35B4143", NotificationType.HearingConfirmationJoh,
                    MessageType.Email, $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.JudicialOfficeHolder},{NotifyParams.DayMonthYear},{NotifyParams.Time}")
            },
            {
                CreateTemplate("1F507F21-CEE6-430F-AB75-AA95980E369F", NotificationType.HearingConfirmationLip,
                    MessageType.Email, $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Name},{NotifyParams.DayMonthYear},{NotifyParams.Time}")
            },
            {
                CreateTemplate("7CF9B49B-02D6-41C2-BCD0-71E2E9655731",
                    NotificationType.HearingConfirmationRepresentative, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.ClientName},{NotifyParams.SolicitorName},{NotifyParams.DayMonthYear},{NotifyParams.Time}")
            },
            {
                CreateTemplate("A95A5C70-1E86-4DD6-8972-1993DFB21A18", NotificationType.HearingReminderJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time,username")
            },
            {
                CreateTemplate("04A2DD3D-06BA-462B-A866-7FC802AAE69A", NotificationType.HearingReminderLip,
                    MessageType.Email, "case name,case number,name,day month year,time,username")
            },
            {
                CreateTemplate("64AAA327-B087-4142-BE1A-94238D9248EA",
                    NotificationType.HearingReminderRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,day month year,time,username")
            },
            {
                CreateTemplate("3210895A-C096-4029-B43E-9FDE4642A254", NotificationType.HearingAmendmentJudge,
                    MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Judge},{NotifyParams.NewDayMonthYear},{NotifyParams.NewTime},{NotifyParams.OldDayMonthYear},{NotifyParams.OldTime},{NotifyParams.CourtroomAccountUserName}")
            },
            {
                CreateTemplate("715017DB-24B5-4117-A47D-0F935054A5A0", NotificationType.HearingAmendmentJoh,
                    MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.JudicialOfficeHolder},{NotifyParams.NewDayMonthYear},{NotifyParams.NewTime},{NotifyParams.OldDayMonthYear},{NotifyParams.OldTime}")
            },
            {
                CreateTemplate("197D2B04-A600-41AE-BF68-8021D6EA0057", NotificationType.HearingAmendmentLip,
                    MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Name},{NotifyParams.NewDayMonthYear},{NotifyParams.NewTime},{NotifyParams.OldDayMonthYear},{NotifyParams.OldTime}")
            },
            {
                CreateTemplate("70C29995-D6D6-48D4-AB1B-6F957A776F30",
                    NotificationType.HearingAmendmentRepresentative, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.ClientName},{NotifyParams.SolicitorName},{NotifyParams.NewDayMonthYear},{NotifyParams.NewTime},{NotifyParams.OldDayMonthYear},{NotifyParams.OldTime}")
            },
            {
                CreateTemplate("04CD937D-C6EB-4932-A040-469123AFEF67",
                    NotificationType.HearingConfirmationJudgeMultiDay, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Judge},{NotifyParams.StartDayMonthYear},{NotifyParams.Time},{NotifyParams.NumberOfDays},{NotifyParams.CourtroomAccountUserName}")
            },
            {
                CreateTemplate("94A12C16-B291-4F5E-89F0-7AF625F5F51B",
                    NotificationType.HearingConfirmationJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days,time")
            },
            {
                CreateTemplate("F7E9BE27-5E7A-439D-B52E-4CFC56C9DD86",
                    NotificationType.HearingConfirmationLipMultiDay, MessageType.Email,
                    "case name,case number,name,Start Day Month Year,time,number of days,time")
            },
            {
                CreateTemplate("8E12EFD7-A6CB-4E43-8757-39CD1D292946",
                    NotificationType.HearingConfirmationRepresentativeMultiDay, MessageType.Email,
                    "case name,case number,client name,solicitor name,Start Day Month Year,time,number of days,time")
            },
            {
                CreateTemplate("ED3A7CC4-9F9B-4E3D-A522-5FEFCB59CA01",
                    NotificationType.HearingConfirmationEJudJudge, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Judge},{NotifyParams.DayMonthYear},{NotifyParams.Time}")
            },
            {
                CreateTemplate("2326B59A-4170-45DB-9638-789CA002FC20",
                    NotificationType.HearingConfirmationEJudJudgeMultiDay, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Judge},{NotifyParams.StartDayMonthYear},{NotifyParams.Time},{NotifyParams.NumberOfDays}")
            },
            {
                CreateTemplate("339EDA5B-2E21-4D75-A768-75648E801738",
                    NotificationType.HearingAmendmentEJudJudge, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Judge},{NotifyParams.NewDayMonthYear},{NotifyParams.NewTime},{NotifyParams.OldDayMonthYear},{NotifyParams.OldTime}")
            },
            {
                CreateTemplate("924E9BAE-0566-43DC-BE26-AAAB4B11F3C5", NotificationType.HearingAmendmentEJudJoh,
                    MessageType.Email, $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.JudicialOfficeHolder},{NotifyParams.NewDayMonthYear},{NotifyParams.NewTime},{NotifyParams.OldDayMonthYear},{NotifyParams.OldTime}")
            },
            {
                CreateTemplate("45C68EC6-D869-45E4-BC83-E547620E05F9", NotificationType.HearingReminderEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time")
            },
            {
                CreateTemplate("B71B697D-2AC4-4FFD-82C5-C270CD2AA22B",
                    NotificationType.HearingConfirmationEJudJoh, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.JudicialOfficeHolder},{NotifyParams.DayMonthYear},{NotifyParams.Time}")
            },
            {
                CreateTemplate("A9CE6ED1-6618-411F-B100-E1335FED558B",
                    NotificationType.HearingConfirmationEJudJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days")
            },
            {
                CreateTemplate("B9D88AB8-6FE7-4FBA-98B6-3CA5E9EBBD31", NotificationType.EJudJohDemoOrTest,
                    MessageType.Email, "case number,test type,judicial office holder,username,date,time")
            },
            {
                CreateTemplate("B00E330B-14F7-4FF1-A511-824F309EC958", NotificationType.ParticipantDemoOrTest,
                    MessageType.Email, "test type,case number,date,name,username,time")
            },
            {
                CreateTemplate("F966FE18-C817-46EB-8F19-BBE04CE0D940", NotificationType.EJudJudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge")
            },
            {
                CreateTemplate("E2D7E609-C42A-4C48-8EE3-C6F853267185", NotificationType.JudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge,courtroom account username")
            },
            {
                CreateTemplate("FC7485B2-36E8-4ED8-A529-3C7777B0212B",
                    NotificationType.TelephoneHearingConfirmation, MessageType.Email,
                    "case name,case number,name,day month year,time")
            },
            {
                CreateTemplate("9EADD247-7B88-4D9F-892B-3F9C62AF67A5",
                    NotificationType.TelephoneHearingConfirmationMultiDay, MessageType.Email,
                    "case name,case number,name,day month year,time,number of days")
            },
            {
                CreateTemplate("F521458D-EA14-445B-9FC3-1FA94EF55DC6", NotificationType.CreateStaffMember,
                    MessageType.Email, "Name,Username,Password")
            },
            {
                CreateTemplate("4E8FBBB4-E501-4509-83E2-C0BBB42B9C3C",
                    NotificationType.HearingAmendmentStaffMember, MessageType.Email,
                    "case name,case number,staff member,New Day Month Year,Old Day Month Year,New time,Old time")
            },
            {
                CreateTemplate("F8467D59-FFFB-44A5-85CE-AD5AE0180F37",
                    NotificationType.HearingConfirmationStaffMember, MessageType.Email,
                    "case name,case number,staff member,day month year,time,username")
            },
            {
                CreateTemplate("C869C829-DF92-4BF9-A378-9EDF0C3518C2",
                    NotificationType.HearingConfirmationStaffMemberMultiDay, MessageType.Email,
                    "case name,case number,staff member,Start Day Month Year,time,number of days,username")
            },
            {
                CreateTemplate("0A755E44-E29E-4BB6-81F2-648553E552BF", NotificationType.StaffMemberDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,staff member")
            },
            {
                CreateTemplate("E4636FBE-6466-4394-9775-A98FED84FD23", NotificationType.NewHearingReminderLIP,
                    MessageType.Email, $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.DayMonthYear},{NotifyParams.Time},{NotifyParams.UserName}")
            },
            {
                CreateTemplate("1FB464A6-EB9F-404E-824E-41A6153E9B17",
                    NotificationType.NewHearingReminderRepresentative,
                    MessageType.Email,
                    "case name, case number, client name, solicitor name, day month year, time, username")
            },
            {
                CreateTemplate("4A6FB490-4CB7-4441-9B00-CCA636A2C455", NotificationType.NewHearingReminderJOH,
                    MessageType.Email, $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.JudicialOfficeHolder},{NotifyParams.DayMonthYear},{NotifyParams.Time},{NotifyParams.UserName}")
            },
            {
                CreateTemplate("1e683018-cd1f-4c41-83c8-3686b697655e", NotificationType.NewHearingReminderEJudJoh,
                    MessageType.Email, $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.JudicialOfficeHolder},{NotifyParams.DayMonthYear},{NotifyParams.Time},{NotifyParams.UserName}")
            },
            {
                CreateTemplate("6c9be8bd-9aaa-468c-ad73-340fb0919b21", NotificationType.NewUserLipWelcome,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber}")
            },
            {
                CreateTemplate("625bb8c7-b70b-4fde-867e-9e365285d756", NotificationType.NewUserLipConfirmation,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.DayMonthYear},{NotifyParams.StartTime},{NotifyParams.UserName},{NotifyParams.RandomPassword},{NotifyParams.DayMonthYearCy}")
            },
            {
                CreateTemplate("14917a84-8a87-4e5e-82d2-6d3402ce1395",
                    NotificationType.NewUserLipConfirmationMultiDay,
                    MessageType.Email, $"{NotifyParams.DayMonthYear},{NotifyParams.StartTime},{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.NumberOfDays},{NotifyParams.UserName},{NotifyParams.RandomPassword},{NotifyParams.DayMonthYearCy}")
            },
            {
                CreateTemplate("7458e52d-3954-4f33-bd2d-0a7d2de295fc",
                    NotificationType.ExistingUserLipConfirmation,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.DayMonthYear},{NotifyParams.StartTime},{NotifyParams.UserName},{NotifyParams.DayMonthYearCy}")
            },
            {
                CreateTemplate("3d83ee22-71ef-47f6-9557-bdaf0c0eecae",
                    NotificationType.ExistingUserLipConfirmationMultiDay,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.DayMonthYear},{NotifyParams.StartTime},{NotifyParams.UserName},{NotifyParams.DayMonthYearCy},{NotifyParams.NumberOfDays}")
            },
            {
                CreateTemplate("cc5cbdca-6614-484d-8b2d-5446ebccb47b",
                    NotificationType.NewHearingReminderLipSingleDay,
                    MessageType.Email,
                    $"{NotifyParams.DayMonthYear},{NotifyParams.StartTime},{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.UserName},{NotifyParams.DayMonthYearCy}")
            },
            {
                CreateTemplate("5f1140fe-605e-49f7-a165-014f704f5d95",
                    NotificationType.NewHearingReminderLipMultiDay,
                    MessageType.Email,
                    $"{NotifyParams.DayMonthYear},{NotifyParams.StartTime},{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.NumberOfDays},{NotifyParams.UserName},{NotifyParams.DayMonthYearCy}")
            },
        };

        private readonly IList<Template> _sourceTemplatesProd = new List<Template>()
        {
            {
                CreateTemplate("145DD703-6B4E-4570-BC48-DCE1F10E76C7", NotificationType.CreateIndividual,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.UserName},{NotifyParams.RandomPassword}")
            },
            {
                CreateTemplate("1DC8DEB5-19E3-4AC4-8F17-5930BFDBCEC1", NotificationType.CreateRepresentative,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.UserName},{NotifyParams.RandomPassword}")
            },
            {
                CreateTemplate("38C8EC36-88A0-40A2-8393-4B889D678EA8", NotificationType.PasswordReset,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.Password}")
            },
            {
                CreateTemplate("9041A2C4-563F-4B9E-9093-BAAB516856F8",
                    NotificationType.HearingConfirmationJudge, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Judge},{NotifyParams.DayMonthYear},{NotifyParams.Time},{NotifyParams.CourtroomAccountUserName}")
            },
            {
                CreateTemplate("92C99AA1-A50E-4E54-910E-6CD0E1BFD090", NotificationType.HearingConfirmationJoh,
                    MessageType.Email, $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.JudicialOfficeHolder},{NotifyParams.DayMonthYear},{NotifyParams.Time}")
            },
            {
                CreateTemplate("513D2EC4-854A-4D7D-9784-2C01FBC4042B", NotificationType.HearingConfirmationLip,
                    MessageType.Email, $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Name},{NotifyParams.DayMonthYear},{NotifyParams.Time}")
            },
            {
                CreateTemplate("FC3D1AD8-055C-4F93-8B75-F2BE78D72797",
                    NotificationType.HearingConfirmationRepresentative, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.ClientName},{NotifyParams.SolicitorName},{NotifyParams.DayMonthYear},{NotifyParams.Time}")
            },
            {
                CreateTemplate("C9FCA388-1FD1-4042-8BD3-B1A4CA507E05", NotificationType.HearingReminderJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time,username")
            },
            {
                CreateTemplate("35B079B6-0FB5-4D73-9A49-8FBABFEF59F6", NotificationType.HearingReminderLip,
                    MessageType.Email, "case name,case number,name,day month year,time,username")
            },
            {
                CreateTemplate("80BE2D08-0FE6-4391-B60B-FD0CF770F9D5",
                    NotificationType.HearingReminderRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,day month year,time,username")
            },
            {
                CreateTemplate("CA63D787-0378-4F8B-8994-0659D95FE273", NotificationType.HearingAmendmentJudge,
                    MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Judge},{NotifyParams.NewDayMonthYear},{NotifyParams.NewTime},{NotifyParams.OldDayMonthYear},{NotifyParams.OldTime},{NotifyParams.CourtroomAccountUserName}")
            },
            {
                CreateTemplate("2D10D852-DB3D-4715-978E-23B2FD4145FE", NotificationType.HearingAmendmentJoh,
                    MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.JudicialOfficeHolder},{NotifyParams.NewDayMonthYear},{NotifyParams.NewTime},{NotifyParams.OldDayMonthYear},{NotifyParams.OldTime}")
            },
            {
                CreateTemplate("02817327-5533-4051-AE69-3609DDEBA8FB", NotificationType.HearingAmendmentLip,
                    MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Name},{NotifyParams.NewDayMonthYear},{NotifyParams.NewTime},{NotifyParams.OldDayMonthYear},{NotifyParams.OldTime}")
            },
            {
                CreateTemplate("5299AC1F-BF42-4C68-82BD-E9C0F0EE51BA",
                    NotificationType.HearingAmendmentRepresentative, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.ClientName},{NotifyParams.SolicitorName},{NotifyParams.NewDayMonthYear},{NotifyParams.NewTime},{NotifyParams.OldDayMonthYear},{NotifyParams.OldTime}")
            },
            {
                CreateTemplate("E07120B8-7FB8-43B6-88D4-909953453F05",
                    NotificationType.HearingConfirmationJudgeMultiDay, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Judge},{NotifyParams.StartDayMonthYear},{NotifyParams.Time},{NotifyParams.NumberOfDays},{NotifyParams.CourtroomAccountUserName}")
            },
            {
                CreateTemplate("7926B67C-D9F7-4A42-A9AA-674B9EF8783B",
                    NotificationType.HearingConfirmationJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days,time")
            },
            {
                CreateTemplate("3DC60474-92EA-488F-A9EE-42A1D7C604A2",
                    NotificationType.HearingConfirmationLipMultiDay, MessageType.Email,
                    "case name,case number,name,Start Day Month Year,time,number of days,time")
            },
            {
                CreateTemplate("39FB2E4B-6A1A-4BD2-8763-889966DD6BE0",
                    NotificationType.HearingConfirmationRepresentativeMultiDay, MessageType.Email,
                    "case name,case number,client name,solicitor name,Start Day Month Year,time,number of days,time")
            },
            {
                CreateTemplate("D8396E28-1423-4672-B0EA-FFB6B40A1057",
                    NotificationType.HearingConfirmationEJudJudge, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Judge},{NotifyParams.DayMonthYear},{NotifyParams.Time}")
            },
            {
                CreateTemplate("47D34025-5421-41F6-A326-B11A87E93122",
                    NotificationType.HearingConfirmationEJudJudgeMultiDay, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Judge},{NotifyParams.StartDayMonthYear},{NotifyParams.Time},{NotifyParams.NumberOfDays}")
            },
            {
                CreateTemplate("867C4F32-E60E-4A39-94A2-AFB0B0E4CB53",
                    NotificationType.HearingAmendmentEJudJudge, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.Judge},{NotifyParams.NewDayMonthYear},{NotifyParams.NewTime},{NotifyParams.OldDayMonthYear},{NotifyParams.OldTime}")
            },
            {
                CreateTemplate("CB0021C4-0919-49D3-AA47-42C7E8094244", NotificationType.HearingAmendmentEJudJoh,
                    MessageType.Email, $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.JudicialOfficeHolder},{NotifyParams.NewDayMonthYear},{NotifyParams.NewTime},{NotifyParams.OldDayMonthYear},{NotifyParams.OldTime}")
            },
            {
                CreateTemplate("22C8A32C-E30E-4337-A7A8-31D247B831B2", NotificationType.HearingReminderEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time")
            },
            {
                CreateTemplate("30BD2A94-81EA-4A6B-9AA8-1236898395DA",
                    NotificationType.HearingConfirmationEJudJoh, MessageType.Email,
                    $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.JudicialOfficeHolder},{NotifyParams.DayMonthYear},{NotifyParams.Time}")
            },
            {
                CreateTemplate("DECAA307-87B3-4522-A92B-B8C0718633CE",
                    NotificationType.HearingConfirmationEJudJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days")
            },
            {
                CreateTemplate("926E2989-ABEF-4B73-BD21-05A8CD8A701C", NotificationType.EJudJohDemoOrTest,
                    MessageType.Email, "case number,test type,judicial office holder,username,date,time")
            },
            {
                CreateTemplate("0198935B-C183-4688-8773-E6C9F3C2BB2D", NotificationType.ParticipantDemoOrTest,
                    MessageType.Email, "test type,case number,date,name,username,time")
            },
            {
                CreateTemplate("4746C5A6-9334-4076-B50F-2E7A17B1FE40", NotificationType.EJudJudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge")
            },
            {
                CreateTemplate("F2FD1181-E1F4-4B67-B581-502D2BB10D75", NotificationType.JudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge,courtroom account username")
            },
            {
                CreateTemplate("9E0E6CE6-239B-4C52-BC43-586B1653E900",
                    NotificationType.TelephoneHearingConfirmation, MessageType.Email,
                    "case name,case number,name,day month year,time")
            },
            {
                CreateTemplate("B8A9E86B-38BA-437F-9C6D-B6CD58914EEF",
                    NotificationType.TelephoneHearingConfirmationMultiDay, MessageType.Email,
                    "case name,case number,name,day month year,time,number of days")
            },
            {
                CreateTemplate("D42913FB-2E8D-4CC8-A411-80766D3F7ABE", NotificationType.CreateStaffMember,
                    MessageType.Email, "Name,Username,Password")
            },
            {
                CreateTemplate("F81B074A-E30E-46BA-9AD3-C064585BE50E",
                    NotificationType.HearingAmendmentStaffMember, MessageType.Email,
                    "case name,case number,staff member,New Day Month Year,Old Day Month Year,New time,Old time")
            },
            {
                CreateTemplate("31A8DB4E-EA54-437F-A481-BE5409B76B1C",
                    NotificationType.HearingConfirmationStaffMember, MessageType.Email,
                    "case name,case number,staff member,day month year,time,username")
            },
            {
                CreateTemplate("CFC47A3D-C90B-4AE3-B469-06DD9F6167E4",
                    NotificationType.HearingConfirmationStaffMemberMultiDay, MessageType.Email,
                    "case name,case number,staff member,Start Day Month Year,time,number of days,username")
            },
            {
                CreateTemplate("90DE8F9C-444B-4C3C-AE87-D6A06CFF903B", NotificationType.StaffMemberDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,staff member")
            },
            {
                CreateTemplate("c29e6297-0201-4efe-823e-128a6e6a2a55", NotificationType.NewHearingReminderLIP,
                    MessageType.Email, $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.DayMonthYear},{NotifyParams.Time},{NotifyParams.UserName}")
            },
            {
                CreateTemplate("a92de80a-6d96-413d-b515-904fdbbf2de8",
                    NotificationType.NewHearingReminderRepresentative, MessageType.Email,
                    "case name, case number, client name, solicitor name, day month year, time, username")
            },
            {
                CreateTemplate("1abe2b66-87de-44a9-8e2b-fb82ec9d361f", NotificationType.NewHearingReminderJOH,
                    MessageType.Email, $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.JudicialOfficeHolder},{NotifyParams.DayMonthYear},{NotifyParams.Time},{NotifyParams.UserName}")
            },
            {
                CreateTemplate("7718d416-d223-4f9c-a6c3-4f4e484e1ced", NotificationType.NewHearingReminderEJudJoh,
                    MessageType.Email, $"{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.JudicialOfficeHolder},{NotifyParams.DayMonthYear},{NotifyParams.Time},{NotifyParams.UserName}")
            },
            {
                CreateTemplate("dd4d6c03-0dc5-474c-82ed-0382c9f725c1", NotificationType.NewHearingReminderEJudJoh,
                    MessageType.Email, "name, case name, case number")
            },
            {
                CreateTemplate("dd4d6c03-0dc5-474c-82ed-0382c9f725c1", NotificationType.NewUserLipWelcome,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber}")
            },
            {
                CreateTemplate("f3b2bde2-a207-42a1-ac31-42afc4c565c5", NotificationType.NewUserLipConfirmation,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.DayMonthYear},{NotifyParams.StartTime},{NotifyParams.UserName},{NotifyParams.RandomPassword},{NotifyParams.DayMonthYearCy}")
            },
            {
                CreateTemplate("1fec5768-db1e-469a-87f0-ca0171907fcf",
                    NotificationType.NewUserLipConfirmationMultiDay,
                    MessageType.Email, $"{NotifyParams.DayMonthYear},{NotifyParams.StartTime},{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.NumberOfDays},{NotifyParams.UserName},{NotifyParams.RandomPassword},{NotifyParams.DayMonthYearCy}")
            },
            {
                CreateTemplate("d150f04b-bce7-4532-b7c3-2ad02550a0ca",
                    NotificationType.ExistingUserLipConfirmation,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.DayMonthYear},{NotifyParams.StartTime},{NotifyParams.UserName},{NotifyParams.DayMonthYearCy}")
            },
            {
                CreateTemplate("fb7a7bc9-b0f2-498b-b9b8-c9171238d5c2",
                    NotificationType.ExistingUserLipConfirmationMultiDay,
                    MessageType.Email, $"{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.DayMonthYear},{NotifyParams.StartTime},{NotifyParams.UserName},{NotifyParams.DayMonthYearCy},{NotifyParams.NumberOfDays}")
            },
            {
                CreateTemplate("f810856a-3cff-4774-a063-d087c32eb6a6",
                    NotificationType.NewHearingReminderLipSingleDay,
                    MessageType.Email, $"{NotifyParams.DayMonthYear},{NotifyParams.StartTime},{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.UserName},{NotifyParams.DayMonthYearCy}")
            },
            {
                CreateTemplate("9417d80d-d424-400c-aa45-6d3b578fcd66",
                    NotificationType.NewHearingReminderLipMultiDay,
                    MessageType.Email,
                    $"{NotifyParams.DayMonthYear},{NotifyParams.StartTime},{NotifyParams.Name},{NotifyParams.CaseName},{NotifyParams.CaseNumber},{NotifyParams.NumberOfDays},{NotifyParams.UserName},{NotifyParams.DayMonthYearCy}")
            },
        };


        public IList<Template> Get(string environment)
        {
            switch (environment.ToLower())
            {
                //SDS Environments
                case "stg":
                case "ithc":
                case "test":
                case "demo":
                case "dev":
                case "lower":
                    return _sourceTemplatesDev;
                case "prod":
                    return _sourceTemplatesProd;
                default:
                    throw new ArgumentException(
                        $"Environment variable {environment} is not set - unable to find the list of templates");
            }
        }
    }
}
