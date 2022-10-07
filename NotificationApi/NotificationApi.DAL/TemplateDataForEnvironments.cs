using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NotificationApi
{
    [ExcludeFromCodeCoverage]
    public class TemplateDataForEnvironments
    {
        private readonly IList<Template> _sourceTemplatesDev = new List<Template>()
        {
            {
                new Template(new Guid("5C862F23-F12A-463E-A140-1C9C8D44EC0A"), NotificationType.CreateIndividual,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("E913CED8-CFA2-49EC-8DA4-9353F0105D97"), NotificationType.CreateRepresentative,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("C4D0001F-1706-483C-AB64-0DCB53EB8216"), NotificationType.PasswordReset,
                    MessageType.Email, "name,password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("811125FE-4CAB-4829-88ED-D3E7D4689CDD"),
                    NotificationType.HearingConfirmationJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("1D4ACED7-4A0B-4610-B4C7-71F6C35B4143"), NotificationType.HearingConfirmationJoh,
                    MessageType.Email, "case name,case number,judicial office holder,Day Month Year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("1F507F21-CEE6-430F-AB75-AA95980E369F"), NotificationType.HearingConfirmationLip,
                    MessageType.Email, "case name,case number,name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("7CF9B49B-02D6-41C2-BCD0-71E2E9655731"),
                    NotificationType.HearingConfirmationRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("A95A5C70-1E86-4DD6-8972-1993DFB21A18"), NotificationType.HearingReminderJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("04A2DD3D-06BA-462B-A866-7FC802AAE69A"), NotificationType.HearingReminderLip,
                    MessageType.Email, "case name,case number,name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("64AAA327-B087-4142-BE1A-94238D9248EA"), NotificationType.HearingReminderRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("3210895A-C096-4029-B43E-9FDE4642A254"), NotificationType.HearingAmendmentJudge,
                    MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("715017DB-24B5-4117-A47D-0F935054A5A0"), NotificationType.HearingAmendmentJoh,
                    MessageType.Email,
                    "case name,case number,judicial office holder,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("197D2B04-A600-41AE-BF68-8021D6EA0057"), NotificationType.HearingAmendmentLip,
                    MessageType.Email,
                    "case name,case number,name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("70C29995-D6D6-48D4-AB1B-6F957A776F30"),
                    NotificationType.HearingAmendmentRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("04CD937D-C6EB-4932-A040-469123AFEF67"),
                    NotificationType.HearingConfirmationJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("94A12C16-B291-4F5E-89F0-7AF625F5F51B"),
                    NotificationType.HearingConfirmationJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("F7E9BE27-5E7A-439D-B52E-4CFC56C9DD86"),
                    NotificationType.HearingConfirmationLipMultiDay, MessageType.Email,
                    "case name,case number,name,Start Day Month Year,time,number of days,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("8E12EFD7-A6CB-4E43-8757-39CD1D292946"),
                    NotificationType.HearingConfirmationRepresentativeMultiDay, MessageType.Email,
                    "case name,case number,client name,solicitor name,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("ED3A7CC4-9F9B-4E3D-A522-5FEFCB59CA01"),
                    NotificationType.HearingConfirmationEJudJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("2326B59A-4170-45DB-9638-789CA002FC20"),
                    NotificationType.HearingConfirmationEJudJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("339EDA5B-2E21-4D75-A768-75648E801738"),
                    NotificationType.HearingAmendmentEJudJudge, MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("924E9BAE-0566-43DC-BE26-AAAB4B11F3C5"), NotificationType.HearingAmendmentEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("45C68EC6-D869-45E4-BC83-E547620E05F9"), NotificationType.HearingReminderEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("B71B697D-2AC4-4FFD-82C5-C270CD2AA22B"),
                    NotificationType.HearingConfirmationEJudJoh, MessageType.Email,
                    "case name,case number,judicial office holder,day month year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("A9CE6ED1-6618-411F-B100-E1335FED558B"),
                    NotificationType.HearingConfirmationEJudJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("B9D88AB8-6FE7-4FBA-98B6-3CA5E9EBBD31"), NotificationType.EJudJohDemoOrTest,
                    MessageType.Email, "case number,test type,judicial office holder,username,date,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("B00E330B-14F7-4FF1-A511-824F309EC958"), NotificationType.ParticipantDemoOrTest,
                    MessageType.Email, "test type,case number,date,name,username,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("F966FE18-C817-46EB-8F19-BBE04CE0D940"), NotificationType.EJudJudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("E2D7E609-C42A-4C48-8EE3-C6F853267185"), NotificationType.JudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge,courtroom account username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("FC7485B2-36E8-4ED8-A529-3C7777B0212B"),
                    NotificationType.TelephoneHearingConfirmation, MessageType.Email,
                    "case name,case number,name,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("9EADD247-7B88-4D9F-892B-3F9C62AF67A5"),
                    NotificationType.TelephoneHearingConfirmationMultiDay, MessageType.Email,
                    "case name,case number,name,day month year,time,number of days", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("F521458D-EA14-445B-9FC3-1FA94EF55DC6"), NotificationType.CreateStaffMember,
                    MessageType.Email, "Name,Username,Password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("4E8FBBB4-E501-4509-83E2-C0BBB42B9C3C"),
                    NotificationType.HearingAmendmentStaffMember, MessageType.Email,
                    "case name,case number,staff member,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("F8467D59-FFFB-44A5-85CE-AD5AE0180F37"),
                    NotificationType.HearingConfirmationStaffMember, MessageType.Email,
                    "case name,case number,staff member,day month year,time,username", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("C869C829-DF92-4BF9-A378-9EDF0C3518C2"),
                    NotificationType.HearingConfirmationStaffMemberMultiDay, MessageType.Email,
                    "case name,case number,staff member,Start Day Month Year,time,number of days,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("0A755E44-E29E-4BB6-81F2-648553E552BF"), NotificationType.StaffMemberDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,staff member", DateTime.UtcNow, DateTime.UtcNow)
            },
            { 
                new Template(new Guid("E4636FBE-6466-4394-9775-A98FED84FD23"),NotificationType.NewHearingReminderLIP,
                MessageType.Email,"case name, case number, name, day month year, time, username",DateTime.UtcNow,
                DateTime.UtcNow ) 
            },
            { 
                new Template(new Guid("1FB464A6-EB9F-404E-824E-41A6153E9B17"),NotificationType.NewHearingReminderRepresentative,
                MessageType.Email,"case name, case number, client name, solicitor name, day month year, time, username",
                DateTime.UtcNow,DateTime.UtcNow ) 
            },
            { 
                new Template(new Guid("4A6FB490-4CB7-4441-9B00-CCA636A2C455"),NotificationType.NewHearingReminderJOH,
                MessageType.Email,"case name, case number, judicial office holder, day month year, time, username",
                DateTime.UtcNow,DateTime.UtcNow ) 
            },
            {
                new Template(new Guid("1e683018-cd1f-4c41-83c8-3686b697655e"),NotificationType.NewHearingReminderEJUD,
                MessageType.Email,"case name, case number, judicial office holder, day month year, time, username",
                DateTime.UtcNow,DateTime.UtcNow )
            },
        };

        private readonly IList<Template> _sourceTemplatesPreview = new List<Template>()
        {
            {
                new Template(new Guid("94D06843-4608-4CDA-9933-9D0F3D7CE535"), NotificationType.CreateIndividual,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("E61575FC-05C8-40DA-B7A7-F7B1D04FF2DB"), NotificationType.CreateRepresentative,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("15F91D90-56F0-480A-957D-11245F9C2CD9"), NotificationType.PasswordReset,
                    MessageType.Email, "name,password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("6489A0F3-538F-49BC-86C5-B2787C71062A"),
                    NotificationType.HearingConfirmationJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("7DAF5DE2-256D-400F-854D-B8EA06DF2B84"), NotificationType.HearingConfirmationJoh,
                    MessageType.Email, "case name,case number,judicial office holder,Day Month Year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("2CF31323-3533-404D-9B52-73B33CE4A1BE"), NotificationType.HearingConfirmationLip,
                    MessageType.Email, "case name,case number,name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("2D39FF6C-D036-4DE0-A953-F148F9839F3C"),
                    NotificationType.HearingConfirmationRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("C0744BD9-4D9B-4F1A-BA3E-148C216FE1EA"), NotificationType.HearingReminderJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("385F78F3-0E77-45A5-A1B0-6CE793A9CDB2"), NotificationType.HearingReminderLip,
                    MessageType.Email, "case name,case number,name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("7C90FEB4-F819-40B8-98CE-F83332B48014"),
                    NotificationType.HearingReminderRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("7C90E8DA-727B-4614-A3C6-B05219920CDE"), NotificationType.HearingAmendmentJudge,
                    MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("21A701AD-89D3-451A-9A87-544057541909"), NotificationType.HearingAmendmentJoh,
                    MessageType.Email,
                    "case name,case number,judicial office holder,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("7F09A0C3-0AF6-49F1-85B4-259511FAD474"), NotificationType.HearingAmendmentLip,
                    MessageType.Email,
                    "case name,case number,name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("79BBD6C3-14BA-4C8E-BD7C-10CEDFDFE3F0"),
                    NotificationType.HearingAmendmentRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("A85B7C4B-BC32-41B9-AB3B-0EEDC0FF8617"),
                    NotificationType.HearingConfirmationJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("77F66714-FAFC-4642-B505-33517E8A6BA8"),
                    NotificationType.HearingConfirmationJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("BF20004C-258B-4DE9-83A1-D7B260750B9F"),
                    NotificationType.HearingConfirmationLipMultiDay, MessageType.Email,
                    "case name,case number,name,Start Day Month Year,time,number of days,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("C06B60B0-CA26-43EE-B6B6-7E5AA5A2B170"),
                    NotificationType.HearingConfirmationRepresentativeMultiDay, MessageType.Email,
                    "case name,case number,client name,solicitor name,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("28B905EA-ECA2-48D3-88EA-B87D578306E9"),
                    NotificationType.HearingConfirmationEJudJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("152F75BC-291C-4420-9411-F39FE8A433ED"),
                    NotificationType.HearingConfirmationEJudJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("2FF7B633-E285-4E25-9029-83012DD448BA"),
                    NotificationType.HearingAmendmentEJudJudge, MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("CA353650-C43B-46E4-890C-B8DAD47A825A"), NotificationType.HearingAmendmentEJudJoh,
                    MessageType.Email, "case name,case number,judge,day month year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("B5E2E627-398F-4FBF-8853-121D3007E043"), NotificationType.HearingReminderEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("4B6C39A5-88AB-475C-9E3A-62BE1778A16D"),
                    NotificationType.HearingConfirmationEJudJoh, MessageType.Email,
                    "case name,case number,judicial office holder,day month year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("ED0F327E-324B-446C-BB0A-48BEA0F67786"),
                    NotificationType.HearingConfirmationEJudJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("6F16C9DF-9DDD-4C4D-A743-7D87853FD753"), NotificationType.EJudJohDemoOrTest,
                    MessageType.Email, "case number,test type,judicial office holder,date,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("F0898BA0-3C26-40C6-B311-1EC4B0E5B01C"), NotificationType.ParticipantDemoOrTest,
                    MessageType.Email, "test type,case number,date,name,username,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("56E9FF91-267F-4154-814A-0281DD100CC6"), NotificationType.EJudJudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("E2709C28-9E12-4BC5-B2EA-3FC8147E7373"), NotificationType.JudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge,courtroom account username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("720E569B-4FBD-4529-8530-6A51C46EBD87"),
                    NotificationType.TelephoneHearingConfirmation, MessageType.Email,
                    "case name,case number,name,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("E870E3BF-8C3D-477C-BFC1-EB4776ABBF66"),
                    NotificationType.TelephoneHearingConfirmationMultiDay, MessageType.Email,
                    "case name,case number,name,day month year,time,number of days", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("15C78693-F4F1-48E2-A3A0-0C04DF40586C"), NotificationType.CreateStaffMember,
                    MessageType.Email, "Name,Username,Password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("F11C7848-8D7B-4045-9C6E-15E7C9EB9060"),
                    NotificationType.HearingAmendmentStaffMember, MessageType.Email,
                    "case name,case number,staff member,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("CD2F20EB-5A32-42B9-A267-135D635FC9C7"),
                    NotificationType.HearingConfirmationStaffMember, MessageType.Email,
                    "case name,case number,staff member,day month year,time,username", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("5CCD4DD9-388B-402B-9D58-36720B4346BA"),
                    NotificationType.HearingConfirmationStaffMemberMultiDay, MessageType.Email,
                    "case name,case number,staff member,Start Day Month Year,time,number of days,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("3FCA3D41-4129-465D-BBD7-31DAC01D3CA5"), NotificationType.StaffMemberDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,staff member", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("BB116814-0D4E-4F4B-86B7-E04A437DB31F"), NotificationType.NewHearingReminderLIP,
                    MessageType.Email, "case name, case number, name, day month year, time, username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("17D911FE-3B90-4443-B113-CB4626179DE9"),
                    NotificationType.NewHearingReminderRepresentative, MessageType.Email,
                    "case name, case number, client name, solicitor name, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("31955C3E-4664-4D7B-A2D3-35CD01B7E923"), NotificationType.NewHearingReminderJOH,
                    MessageType.Email, "case name, case number, judicial office holder, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("3e4168c3-04fd-43fc-a863-6dcc38512cf0"),NotificationType.NewHearingReminderEJUD,
                MessageType.Email,"case name, case number, judicial office holder, day month year, time, username",
                DateTime.UtcNow,DateTime.UtcNow )
            },
        };

        private readonly IList<Template> _sourceTemplatesAAT = new List<Template>()
        {
            {
                new Template(new Guid("C7D41520-FA99-42A6-AF89-C99DD750252C"), NotificationType.CreateIndividual,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("B1635497-A4A5-4139-94BE-083A8DC7058D"), NotificationType.CreateRepresentative,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("C3DDD83A-E5EB-4C24-906A-3E7907FC08C3"), NotificationType.PasswordReset,
                    MessageType.Email, "name,password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("C80E52DD-CAE1-452C-BC8D-EDCD3EC7D71E"),
                    NotificationType.HearingConfirmationJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("6857660F-430C-457F-ACE5-DB048EA62CBD"), NotificationType.HearingConfirmationJoh,
                    MessageType.Email, "case name,case number,judicial office holder,Day Month Year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("8C9F0EEB-5962-4670-B576-9434FCC17DE6"), NotificationType.HearingConfirmationLip,
                    MessageType.Email, "case name,case number,name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("CF25DDCE-A4DD-4570-BB20-A996B17C3031"),
                    NotificationType.HearingConfirmationRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("CDB2D5C4-15E8-4A10-8C82-C65E877A30C0"), NotificationType.HearingReminderJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("FCADCCDA-ED33-470C-BAFF-5E065F5D7ADD"), NotificationType.HearingReminderLip,
                    MessageType.Email, "case name,case number,name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("D4DA0280-CF81-41D3-89BB-8326096BE333"),
                    NotificationType.HearingReminderRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("B6B8BD0D-4019-4451-B19E-DA7D645CB9E0"), NotificationType.HearingAmendmentJudge,
                    MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("812589D7-3E36-4C01-8CA2-CBB67597752C"), NotificationType.HearingAmendmentJoh,
                    MessageType.Email,
                    "case name,case number,judicial office holder,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("0E284B71-0C8C-4A4D-85EF-B986723F8212"), NotificationType.HearingAmendmentLip,
                    MessageType.Email,
                    "case name,case number,name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("5FAB1114-172D-4DBB-876E-66EF0AE8FBD3"),
                    NotificationType.HearingAmendmentRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("A764673E-8EB0-4096-ABF1-963808244EA6"),
                    NotificationType.HearingConfirmationJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("59A1CB05-24D8-4043-AA8E-761ED8D50429"),
                    NotificationType.HearingConfirmationJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("F825BD34-CABB-4E3A-9A51-C7DEF1299F65"),
                    NotificationType.HearingConfirmationLipMultiDay, MessageType.Email,
                    "case name,case number,name,Start Day Month Year,time,number of days,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("586FD254-4F9F-4FAA-AD48-38FCF8D92A5A"),
                    NotificationType.HearingConfirmationRepresentativeMultiDay, MessageType.Email,
                    "case name,case number,client name,solicitor name,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("3C53CB77-BD09-481A-82C0-B015A6925615"),
                    NotificationType.HearingConfirmationEJudJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("0818EEDD-1CBA-46F2-8362-9FEB8C8215C1"),
                    NotificationType.HearingConfirmationEJudJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("FF62B0DC-4E10-4D02-B58D-7A2A1A0CAB30"),
                    NotificationType.HearingAmendmentEJudJudge, MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("A1FD5BA6-E915-47DC-A2FC-765C83F99C16"), NotificationType.HearingAmendmentEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("CDBC2F9F-D480-4218-B6D9-765F307CDFEA"), NotificationType.HearingReminderEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("0F77F8EC-BACA-4F55-99CB-64E371755548"),
                    NotificationType.HearingConfirmationEJudJoh, MessageType.Email,
                    "case name,case number,judicial office holder,day month year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("38661950-B4C7-465B-888D-2D3EAB0F666B"),
                    NotificationType.HearingConfirmationEJudJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("256FD96C-E177-4727-BD82-FD0BA1558EE1"), NotificationType.EJudJohDemoOrTest,
                    MessageType.Email, "case number,test type,judicial office holder,username,date,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("AB4CF5AE-5CEE-4939-B901-6200CC1DBC94"), NotificationType.ParticipantDemoOrTest,
                    MessageType.Email, "test type,case number,date,name,username,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("A7C402BE-15CE-4B41-AA0B-F4D44A4685A8"), NotificationType.EJudJudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("6CDA97F8-E58B-4ECA-BC53-6F0C1D3FC538"), NotificationType.JudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge,courtroom account username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("CDDE03E6-A997-43B9-9233-DE5626CCD6A2"),
                    NotificationType.TelephoneHearingConfirmation, MessageType.Email,
                    "case name,case number,name,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("9C276903-55C2-4A71-AF11-127996CEB54D"),
                    NotificationType.TelephoneHearingConfirmationMultiDay, MessageType.Email,
                    "case name,case number,name,day month year,time,number of days", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("A992739D-6D03-48D5-B07D-E57049F8B4EE"), NotificationType.CreateStaffMember,
                    MessageType.Email, "Name,Username,Password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("29D9A491-207D-42ED-8EFC-ED8F64C103B1"),
                    NotificationType.HearingAmendmentStaffMember, MessageType.Email,
                    "case name,case number,staff member,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("CE4CC40F-2B59-44D7-BF88-4160196A178B"),
                    NotificationType.HearingConfirmationStaffMember, MessageType.Email,
                    "case name,case number,staff member,day month year,time,username", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("7EE06C78-D186-4F7A-ADF6-E155EDAAFF96"),
                    NotificationType.HearingConfirmationStaffMemberMultiDay, MessageType.Email,
                    "case name,case number,staff member,Start Day Month Year,time,number of days,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("880151FE-88C5-4F97-904A-923333DDCEB9"), NotificationType.StaffMemberDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,staff member", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("E6E998E7-7185-40E4-8D4A-817D48988047"), NotificationType.NewHearingReminderLIP,
                    MessageType.Email, "case name, case number, name, day month year, time, username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("8E6C8F86-DA54-442F-9820-C09727552A99"),
                    NotificationType.NewHearingReminderRepresentative, MessageType.Email,
                    "case name, case number, client name, solicitor name, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("8A80D059-D075-45B2-88E0-3B2066E3189A"), NotificationType.NewHearingReminderJOH,
                    MessageType.Email, "case name, case number, judicial office holder, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("03cd0de3-2f67-44cc-85a5-6c60e4046c8d"),NotificationType.NewHearingReminderEJUD,
                MessageType.Email,"case name, case number, judicial office holder, day month year, time, username",
                DateTime.UtcNow,DateTime.UtcNow )
            },
        };

        private readonly IList<Template> _sourceTemplatesTest1 = new List<Template>()
        {
            {
                new Template(new Guid("7AFBE513-CF4B-4919-860B-A9BDC0B3967E"), NotificationType.CreateIndividual,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("001260B8-E144-4841-9583-7F8D4BBAB43D"), NotificationType.CreateRepresentative,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("FB6E43B6-BD9A-43D6-A2C6-712EA5202B10"), NotificationType.PasswordReset,
                    MessageType.Email, "name,password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("CEEAD508-DF7E-4CCC-94E2-8571AA6AA0A5"),
                    NotificationType.HearingConfirmationJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("C0A3B041-1C4B-4252-B3A1-2DCF5194B0E4"), NotificationType.HearingConfirmationJoh,
                    MessageType.Email, "case name,case number,judicial office holder,Day Month Year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("C6777D56-BF1A-45E6-98A7-2AE829659876"), NotificationType.HearingConfirmationLip,
                    MessageType.Email, "case name,case number,name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("4C5B6E67-C531-4387-B09A-659C8C900B84"),
                    NotificationType.HearingConfirmationRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("907249EA-9DB9-4B82-BB39-46594C1D8CB4"), NotificationType.HearingReminderJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("D69D1F27-4BF2-4C88-BE0D-35CA78B069A4"), NotificationType.HearingReminderLip,
                    MessageType.Email, "case name,case number,name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("E4E344EF-CB42-4906-A2B1-0282512E9B1D"),
                    NotificationType.HearingReminderRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("DCA27D07-6804-4493-9661-79263F2FDDBA"), NotificationType.HearingAmendmentJudge,
                    MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("F38144F7-5FBB-4010-AD3D-08199F642566"), NotificationType.HearingAmendmentJoh,
                    MessageType.Email,
                    "case name,case number,judicial office holder,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("7AE3F1DB-DC84-473D-BD43-D4ABEC5F273C"), NotificationType.HearingAmendmentLip,
                    MessageType.Email,
                    "case name,case number,name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("0B1CF0C2-8140-4254-8606-A357FC4CB118"),
                    NotificationType.HearingAmendmentRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("C4F2DEB1-153D-44D8-BB2A-5A66586EC081"),
                    NotificationType.HearingConfirmationJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("1A2D5C41-C37F-41F7-8B96-9B7EAAF2AF3F"),
                    NotificationType.HearingConfirmationJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("2B9514BE-29AA-4CAA-A6D1-94A58D8CED8F"),
                    NotificationType.HearingConfirmationLipMultiDay, MessageType.Email,
                    "case name,case number,name,Start Day Month Year,time,number of days,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("A330590B-06ED-45EF-9087-FA6E089A7542"),
                    NotificationType.HearingConfirmationRepresentativeMultiDay, MessageType.Email,
                    "case name,case number,client name,solicitor name,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("B623B2EA-90AC-473A-84DF-633148672FAC"),
                    NotificationType.HearingConfirmationEJudJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("8EFA06A3-3CB5-4982-B75B-6E5A2C586F48"),
                    NotificationType.HearingConfirmationEJudJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("D469C281-C7A7-47DD-A28C-B1BBD1260515"),
                    NotificationType.HearingAmendmentEJudJudge, MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("0DA28BBF-3A0B-47E7-B034-5A1753B21254"), NotificationType.HearingAmendmentEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("67210F08-CB4D-428C-9308-48CB75CDA0C3"), NotificationType.HearingReminderEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("394D70EC-AFF4-40F4-A205-2A96FDEB0248"),
                    NotificationType.HearingConfirmationEJudJoh, MessageType.Email,
                    "case name,case number,judicial office holder,day month year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("A2A87317-9B0E-4D65-9CA5-9A9189EB986C"),
                    NotificationType.HearingConfirmationEJudJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("7AC81033-D0F0-4165-9797-85FDD5A11FA0"), NotificationType.EJudJohDemoOrTest,
                    MessageType.Email, "case number,test type,judicial office holder,username,date,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("AD0E8AD1-C9C2-4E5B-83DA-4719DC6DE613"), NotificationType.ParticipantDemoOrTest,
                    MessageType.Email, "test type,case number,date,name,username,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("4E72CBE8-F54F-4039-834C-F325C4E946E9"), NotificationType.EJudJudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("0D85126D-5A33-4E2A-8F34-C017EC48FC53"), NotificationType.JudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge,courtroom account username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("DB6AA43E-9B68-4622-B636-3C89EBD693DC"),
                    NotificationType.TelephoneHearingConfirmation, MessageType.Email,
                    "case name,case number,name,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("E3AFA6DB-AC02-4724-82B2-2F975CE381A3"),
                    NotificationType.TelephoneHearingConfirmationMultiDay, MessageType.Email,
                    "case name,case number,name,day month year,time,number of days", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("74DFCF02-7BBD-4E56-AC4D-805A503107F6"), NotificationType.CreateStaffMember,
                    MessageType.Email, "Name,Username,Password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("EA46CCD0-5EFF-4F99-AA1A-094A66B18ADE"),
                    NotificationType.HearingAmendmentStaffMember, MessageType.Email,
                    "case name,case number,staff member,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("8EF4D46C-CDB6-4C09-88BA-F33E0C89478A"),
                    NotificationType.HearingConfirmationStaffMember, MessageType.Email,
                    "case name,case number,staff member,day month year,time,username", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("B4C960C8-144C-49E2-B9E4-601F12ED6A79"),
                    NotificationType.HearingConfirmationStaffMemberMultiDay, MessageType.Email,
                    "case name,case number,staff member,Start Day Month Year,time,number of days,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("F6023ACA-B9BE-4CF0-9D88-078A7BCC9B98"), NotificationType.StaffMemberDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,staff member", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("468DA234-9602-4759-895B-248329EDE20E"), NotificationType.NewHearingReminderLIP,
                    MessageType.Email, "case name, case number, name, day month year, time, username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("D0145ADE-2EC7-4FDF-A5B8-4C85318BDF36"),
                    NotificationType.NewHearingReminderRepresentative, MessageType.Email,
                    "case name, case number, client name, solicitor name, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("33DB272D-D961-4596-AFC0-C19B109D390B"), NotificationType.NewHearingReminderJOH,
                    MessageType.Email, "case name, case number, judicial office holder, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("62d3105b-d746-4b06-9cf3-c43b507682f1"),NotificationType.NewHearingReminderEJUD,
                MessageType.Email,"case name, case number, judicial office holder, day month year, time, username",
                DateTime.UtcNow,DateTime.UtcNow )
            },
        };

        private readonly IList<Template> _sourceTemplatesDemo = new List<Template>()
        {
            {
                new Template(new Guid("6CF6266A-385C-4DD7-9AAE-EF79B7E7E8A0"), NotificationType.CreateIndividual,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("0A916EC6-B361-48B9-8244-BDCFBE6C85D2"), NotificationType.CreateRepresentative,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("DCE5A91D-EB1A-4765-B42A-A6FCE5CDE264"), NotificationType.PasswordReset,
                    MessageType.Email, "name,password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("3B878842-3D0B-4535-B244-C7B8740752D2"),
                    NotificationType.HearingConfirmationJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("119227B1-5A8E-421B-942C-4CE259D0AA21"), NotificationType.HearingConfirmationJoh,
                    MessageType.Email, "case name,case number,judicial office holder,Day Month Year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("1CFA6F87-4DD8-4BDD-AFBE-E6128163BA48"), NotificationType.HearingConfirmationLip,
                    MessageType.Email, "case name,case number,name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("2B235C52-A283-4255-89AF-48EB92BE392B"),
                    NotificationType.HearingConfirmationRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("6ACEA005-8017-4613-A8AF-3DE07A795F95"), NotificationType.HearingReminderJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("E9DC6393-5F37-4029-B5A0-C8DC2B8BB592"), NotificationType.HearingReminderLip,
                    MessageType.Email, "case name,case number,name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("7D38527C-626B-4567-8E8B-6D84217E9E35"),
                    NotificationType.HearingReminderRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("9244C5C8-0180-4682-BE7B-C651D8EEBD9C"), NotificationType.HearingAmendmentJudge,
                    MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("C46102FB-7B81-4FD1-90F5-CF3FAF07B7FC"), NotificationType.HearingAmendmentJoh,
                    MessageType.Email,
                    "case name,case number,judicial office holder,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("4C5A123F-98D3-46ED-9679-4F2149C213AC"), NotificationType.HearingAmendmentLip,
                    MessageType.Email,
                    "case name,case number,name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("A0523641-83B5-490E-B3E5-C057DC8157E9"),
                    NotificationType.HearingAmendmentRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("70233D81-374A-4979-9D20-979EA0FB16C7"),
                    NotificationType.HearingConfirmationJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("A86F4664-7522-4D69-8211-20160C7CAEFC"),
                    NotificationType.HearingConfirmationJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("377D144E-71E2-454C-AFDB-DA7CF20D5EFA"),
                    NotificationType.HearingConfirmationLipMultiDay, MessageType.Email,
                    "case name,case number,name,Start Day Month Year,time,number of days,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("466F5E42-FFDE-49D5-9560-0D4EE1A41CC5"),
                    NotificationType.HearingConfirmationRepresentativeMultiDay, MessageType.Email,
                    "case name,case number,client name,solicitor name,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("736D0A34-2C73-4EF7-B81F-E5E54C1AD643"),
                    NotificationType.HearingConfirmationEJudJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("281AE9B9-5AD9-41E4-9F65-13BCB1AD503C"),
                    NotificationType.HearingConfirmationEJudJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("46139247-0115-43FF-AB5C-80C9313185C2"),
                    NotificationType.HearingAmendmentEJudJudge, MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("8EB038AB-8AAF-4283-88DE-763013D8DC27"), NotificationType.HearingAmendmentEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("A42091EC-8212-49E4-9F6E-FC3F30DF70AB"), NotificationType.HearingReminderEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("E9B4F5DE-9885-43F4-9634-08E547E07586"),
                    NotificationType.HearingConfirmationEJudJoh, MessageType.Email,
                    "case name,case number,judicial office holder,day month year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("38BF640A-5C9A-4029-A0FC-3C249A305789"),
                    NotificationType.HearingConfirmationEJudJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("5A363017-69CF-416E-AB52-4C54CB11E3FF"), NotificationType.EJudJohDemoOrTest,
                    MessageType.Email, "case number,test type,judicial office holder,username,date,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("2BAD84BF-D3AB-4B89-A552-2D941E24FB77"), NotificationType.ParticipantDemoOrTest,
                    MessageType.Email, "test type,case number,date,name,username,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("C079F4C6-C2BB-45E7-B085-A302194FDE02"), NotificationType.EJudJudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("DBE13771-E98E-47B1-B579-85E7A111A249"), NotificationType.JudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge,courtroom account username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("680D7CE6-6B14-45E2-AA17-AD11F2701AC0"),
                    NotificationType.TelephoneHearingConfirmation, MessageType.Email,
                    "case name,case number,name,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("CAF7D73E-8AE3-4878-B65F-E13E5FDE13E3"),
                    NotificationType.TelephoneHearingConfirmationMultiDay, MessageType.Email,
                    "case name,case number,name,day month year,time,number of days", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("D668FF24-88CF-48F4-ABEF-EA088A52D7C2"), NotificationType.CreateStaffMember,
                    MessageType.Email, "Name,Username,Password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("81120F81-5D83-4D75-B593-8ED0FFB22E96"),
                    NotificationType.HearingAmendmentStaffMember, MessageType.Email,
                    "case name,case number,staff member,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("6FCB4FE4-6FBB-4EE4-ACE9-84BD0354AA8D"),
                    NotificationType.HearingConfirmationStaffMember, MessageType.Email,
                    "case name,case number,staff member,day month year,time,username", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("FCAA9930-57F3-445A-AF8F-18588B58D901"),
                    NotificationType.HearingConfirmationStaffMemberMultiDay, MessageType.Email,
                    "case name,case number,staff member,Start Day Month Year,time,number of days,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("8936925C-5BB6-4723-AC77-2F10DC2A3E1E"), NotificationType.StaffMemberDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,staff member", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("a70bf7d6-d5b2-41bd-8bc0-a2193287887b"), NotificationType.NewHearingReminderLIP,
                    MessageType.Email, "case name, case number, name, day month year, time, username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("972b8de5-455a-409c-a839-81dae8edf984"),
                    NotificationType.NewHearingReminderRepresentative, MessageType.Email,
                    "case name, case number, client name, solicitor name, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("3f8359da-d714-4bf3-ba16-4ecf09d11895"), NotificationType.NewHearingReminderJOH,
                    MessageType.Email, "case name, case number, judicial office holder, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("cd49e07f-419e-4d1f-9904-6f1f8e9f2ce5"),NotificationType.NewHearingReminderEJUD,
                MessageType.Email,"case name, case number, judicial office holder, day month year, time, username",
                DateTime.UtcNow,DateTime.UtcNow )
            },
        };

        private readonly IList<Template> _sourceTemplatesPreProd = new List<Template>()
        {
            {
                new Template(new Guid("2AF71CB4-C74D-4614-97E2-6ACC82AF4AD9"), NotificationType.CreateIndividual,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("0A88256A-78E8-45BA-BD9B-A3EA6096A189"), NotificationType.CreateRepresentative,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("9F1EE9C2-6FB5-4B0C-9067-6C4969EF49EC"), NotificationType.PasswordReset,
                    MessageType.Email, "name,password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("390AD497-4737-48E4-A2BD-0370F9B0EE78"),
                    NotificationType.HearingConfirmationJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("7E5B7416-012B-4C26-B5FF-E218B29916B5"), NotificationType.HearingConfirmationJoh,
                    MessageType.Email, "case name,case number,judicial office holder,Day Month Year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("37E0BD31-CB80-421A-86BD-2DB2613D9D9B"), NotificationType.HearingConfirmationLip,
                    MessageType.Email, "case name,case number,name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("246098B2-D17D-44E9-84FC-C460ED68EC77"),
                    NotificationType.HearingConfirmationRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("171B21E6-792A-48BD-886E-6BF5E73967B3"), NotificationType.HearingReminderJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("AD9FFDBE-C23A-4498-AD7F-ACD96098397F"), NotificationType.HearingReminderLip,
                    MessageType.Email, "case name,case number,name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("2F2D6F89-5BC2-42D7-BA9E-54CCF8FC3DAE"),
                    NotificationType.HearingReminderRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("280E7109-66B7-4F75-B270-47BB715737DF"), NotificationType.HearingAmendmentJudge,
                    MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("A5404D6D-7535-4589-9670-57D789127B86"), NotificationType.HearingAmendmentJoh,
                    MessageType.Email,
                    "case name,case number,judicial office holder,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("65BF22BF-1B76-496F-8961-6E86C228FDF6"), NotificationType.HearingAmendmentLip,
                    MessageType.Email,
                    "case name,case number,name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("1219A560-D3B0-4F7B-BF0B-E74DA0960CFB"),
                    NotificationType.HearingAmendmentRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("0961EE8B-193F-4308-8431-64C84CA1AB7E"),
                    NotificationType.HearingConfirmationJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("CD0F5B6B-D01B-4E95-B9DF-4CE3E7A6999E"),
                    NotificationType.HearingConfirmationJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("EF40725E-2DB4-46EC-B943-5BA909899595"),
                    NotificationType.HearingConfirmationLipMultiDay, MessageType.Email,
                    "case name,case number,name,Start Day Month Year,time,number of days,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("B5E43298-466C-4613-B0DA-25A8639F1FB7"),
                    NotificationType.HearingConfirmationRepresentativeMultiDay, MessageType.Email,
                    "case name,case number,client name,solicitor name,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("471DAA4A-6284-4AA3-903F-BCDE8FB470FA"),
                    NotificationType.HearingConfirmationEJudJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("5F54F783-6409-4B55-AA5A-7E53AD8D9122"),
                    NotificationType.HearingConfirmationEJudJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("5D952C10-D0C9-47A2-BF60-B4E4205BA550"),
                    NotificationType.HearingAmendmentEJudJudge, MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("3629DC8A-3CBB-441F-B8E3-27B07308437C"), NotificationType.HearingAmendmentEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("F3E4851D-CC60-4405-A8CC-233C069D1736"), NotificationType.HearingReminderEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("A1BD3820-B93D-407E-BF75-12807EA44EF3"),
                    NotificationType.HearingConfirmationEJudJoh, MessageType.Email,
                    "case name,case number,judicial office holder,day month year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("1FC20D60-B140-44BC-B8BE-CAC8B155EAA9"),
                    NotificationType.HearingConfirmationEJudJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("A050723D-B56D-417A-8B10-93E59776BA50"), NotificationType.EJudJohDemoOrTest,
                    MessageType.Email, "case number,test type,judicial office holder,username,date,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("0620B0DD-65B2-4A00-B0D7-5A4974AD5593"), NotificationType.ParticipantDemoOrTest,
                    MessageType.Email, "test type,case number,date,name,username,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("E30863CB-CC71-47AC-8385-981E2C62AC20"), NotificationType.EJudJudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("4E7C403D-7704-4C88-BEB1-A7F59DCB0A84"), NotificationType.JudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge,courtroom account username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("4F0D2D40-BF89-4F8A-BBBC-546981FFF3F0"),
                    NotificationType.TelephoneHearingConfirmation, MessageType.Email,
                    "case name,case number,name,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("A27A6B81-F84D-4316-B687-DCD70C6A4BF5"),
                    NotificationType.TelephoneHearingConfirmationMultiDay, MessageType.Email,
                    "case name,case number,name,day month year,time,number of days", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("4E892FD0-9FC7-49D4-A6B8-DE52D1534E17"), NotificationType.CreateStaffMember,
                    MessageType.Email, "Name,Username,Password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("18F5EC94-2604-45B4-88D7-490992928BF9"),
                    NotificationType.HearingAmendmentStaffMember, MessageType.Email,
                    "case name,case number,staff member,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("8416FCBE-D54F-4DEA-AE26-47E1872083F8"),
                    NotificationType.HearingConfirmationStaffMember, MessageType.Email,
                    "case name,case number,staff member,day month year,time,username", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("AB63D393-47F4-4088-A8D9-94EA749F7B71"),
                    NotificationType.HearingConfirmationStaffMemberMultiDay, MessageType.Email,
                    "case name,case number,staff member,Start Day Month Year,time,number of days,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("B4DE99E1-009C-4FF0-8704-491BC744EF43"), NotificationType.StaffMemberDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,staff member", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("0ff35153-29d4-44bf-8774-5d977f07e056"), NotificationType.NewHearingReminderLIP,
                    MessageType.Email, "case name, case number, name, day month year, time, username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("78cadd8f-81db-40b2-9096-836b92698fd7"),
                    NotificationType.NewHearingReminderRepresentative, MessageType.Email,
                    "case name, case number, client name, solicitor name, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("8e5e3059-b369-4049-bfa3-af94c4e24391"), NotificationType.NewHearingReminderJOH,
                    MessageType.Email, "case name, case number, judicial office holder, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("df2324a3-26bd-4d16-8cb2-afd2a83e1c62"),NotificationType.NewHearingReminderEJUD,
                MessageType.Email,"case name, case number, judicial office holder, day month year, time, username",
                DateTime.UtcNow,DateTime.UtcNow )
            },
        };

        private readonly IList<Template> _sourceTemplatesProd = new List<Template>()
        {
            {
                new Template(new Guid("145DD703-6B4E-4570-BC48-DCE1F10E76C7"), NotificationType.CreateIndividual,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("1DC8DEB5-19E3-4AC4-8F17-5930BFDBCEC1"), NotificationType.CreateRepresentative,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("38C8EC36-88A0-40A2-8393-4B889D678EA8"), NotificationType.PasswordReset,
                    MessageType.Email, "name,password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("9041A2C4-563F-4B9E-9093-BAAB516856F8"),
                    NotificationType.HearingConfirmationJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("92C99AA1-A50E-4E54-910E-6CD0E1BFD090"), NotificationType.HearingConfirmationJoh,
                    MessageType.Email, "case name,case number,judicial office holder,Day Month Year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("513D2EC4-854A-4D7D-9784-2C01FBC4042B"), NotificationType.HearingConfirmationLip,
                    MessageType.Email, "case name,case number,name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("FC3D1AD8-055C-4F93-8B75-F2BE78D72797"),
                    NotificationType.HearingConfirmationRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("C9FCA388-1FD1-4042-8BD3-B1A4CA507E05"), NotificationType.HearingReminderJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("35B079B6-0FB5-4D73-9A49-8FBABFEF59F6"), NotificationType.HearingReminderLip,
                    MessageType.Email, "case name,case number,name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("80BE2D08-0FE6-4391-B60B-FD0CF770F9D5"),
                    NotificationType.HearingReminderRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("CA63D787-0378-4F8B-8994-0659D95FE273"), NotificationType.HearingAmendmentJudge,
                    MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("2D10D852-DB3D-4715-978E-23B2FD4145FE"), NotificationType.HearingAmendmentJoh,
                    MessageType.Email,
                    "case name,case number,judicial office holder,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("02817327-5533-4051-AE69-3609DDEBA8FB"), NotificationType.HearingAmendmentLip,
                    MessageType.Email,
                    "case name,case number,name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("5299AC1F-BF42-4C68-82BD-E9C0F0EE51BA"),
                    NotificationType.HearingAmendmentRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("E07120B8-7FB8-43B6-88D4-909953453F05"),
                    NotificationType.HearingConfirmationJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("7926B67C-D9F7-4A42-A9AA-674B9EF8783B"),
                    NotificationType.HearingConfirmationJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("3DC60474-92EA-488F-A9EE-42A1D7C604A2"),
                    NotificationType.HearingConfirmationLipMultiDay, MessageType.Email,
                    "case name,case number,name,Start Day Month Year,time,number of days,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("39FB2E4B-6A1A-4BD2-8763-889966DD6BE0"),
                    NotificationType.HearingConfirmationRepresentativeMultiDay, MessageType.Email,
                    "case name,case number,client name,solicitor name,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("D8396E28-1423-4672-B0EA-FFB6B40A1057"),
                    NotificationType.HearingConfirmationEJudJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("47D34025-5421-41F6-A326-B11A87E93122"),
                    NotificationType.HearingConfirmationEJudJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("867C4F32-E60E-4A39-94A2-AFB0B0E4CB53"),
                    NotificationType.HearingAmendmentEJudJudge, MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("CB0021C4-0919-49D3-AA47-42C7E8094244"), NotificationType.HearingAmendmentEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("22C8A32C-E30E-4337-A7A8-31D247B831B2"), NotificationType.HearingReminderEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("30BD2A94-81EA-4A6B-9AA8-1236898395DA"),
                    NotificationType.HearingConfirmationEJudJoh, MessageType.Email,
                    "case name,case number,judicial office holder,day month year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("DECAA307-87B3-4522-A92B-B8C0718633CE"),
                    NotificationType.HearingConfirmationEJudJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("926E2989-ABEF-4B73-BD21-05A8CD8A701C"), NotificationType.EJudJohDemoOrTest,
                    MessageType.Email, "case number,test type,judicial office holder,username,date,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("0198935B-C183-4688-8773-E6C9F3C2BB2D"), NotificationType.ParticipantDemoOrTest,
                    MessageType.Email, "test type,case number,date,name,username,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("4746C5A6-9334-4076-B50F-2E7A17B1FE40"), NotificationType.EJudJudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("F2FD1181-E1F4-4B67-B581-502D2BB10D75"), NotificationType.JudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge,courtroom account username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("9E0E6CE6-239B-4C52-BC43-586B1653E900"),
                    NotificationType.TelephoneHearingConfirmation, MessageType.Email,
                    "case name,case number,name,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("B8A9E86B-38BA-437F-9C6D-B6CD58914EEF"),
                    NotificationType.TelephoneHearingConfirmationMultiDay, MessageType.Email,
                    "case name,case number,name,day month year,time,number of days", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("D42913FB-2E8D-4CC8-A411-80766D3F7ABE"), NotificationType.CreateStaffMember,
                    MessageType.Email, "Name,Username,Password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("F81B074A-E30E-46BA-9AD3-C064585BE50E"),
                    NotificationType.HearingAmendmentStaffMember, MessageType.Email,
                    "case name,case number,staff member,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("31A8DB4E-EA54-437F-A481-BE5409B76B1C"),
                    NotificationType.HearingConfirmationStaffMember, MessageType.Email,
                    "case name,case number,staff member,day month year,time,username", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("CFC47A3D-C90B-4AE3-B469-06DD9F6167E4"),
                    NotificationType.HearingConfirmationStaffMemberMultiDay, MessageType.Email,
                    "case name,case number,staff member,Start Day Month Year,time,number of days,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("90DE8F9C-444B-4C3C-AE87-D6A06CFF903B"), NotificationType.StaffMemberDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,staff member", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("c29e6297-0201-4efe-823e-128a6e6a2a55"), NotificationType.NewHearingReminderLIP,
                    MessageType.Email, "case name, case number, name, day month year, time, username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("a92de80a-6d96-413d-b515-904fdbbf2de8"),
                    NotificationType.NewHearingReminderRepresentative, MessageType.Email,
                    "case name, case number, client name, solicitor name, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("1abe2b66-87de-44a9-8e2b-fb82ec9d361f"), NotificationType.NewHearingReminderJOH,
                    MessageType.Email, "case name, case number, judicial office holder, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("7718d416-d223-4f9c-a6c3-4f4e484e1ced"),NotificationType.NewHearingReminderEJUD,
                MessageType.Email,"case name, case number, judicial office holder, day month year, time, username",
                DateTime.UtcNow,DateTime.UtcNow )
            },
        };

        private readonly IList<Template> _sourceTemplatesSandbox = new List<Template>()
        {
            {
                new Template(new Guid("5DAFAA65-DD15-4FF1-9709-B4EC79E28109"), NotificationType.CreateIndividual,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("254C2827-B931-41D9-A134-87EECE8CEFC2"), NotificationType.CreateRepresentative,
                    MessageType.Email, "name,username,random password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("B960795B-AF58-483E-A25C-722155E3A1DE"), NotificationType.PasswordReset,
                    MessageType.Email, "name,password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("22EA73BB-31E1-46B5-B02C-C97D1C74608B"),
                    NotificationType.HearingConfirmationJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("F3B0A36C-4D9E-4805-999E-BD8B2ACD6506"), NotificationType.HearingConfirmationJoh,
                    MessageType.Email, "case name,case number,judicial office holder,Day Month Year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("79026354-7790-4BCA-B62C-F7171D3A6F38"), NotificationType.HearingConfirmationLip,
                    MessageType.Email, "case name,case number,name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("F3A3116B-BEDD-49ED-9977-74A81EB2B05A"),
                    NotificationType.HearingConfirmationRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,Day Month Year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("4BC7572D-60CC-4C7C-BDC2-73E94B6609A7"), NotificationType.HearingReminderJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("414CCB68-0077-4409-B50D-DCD995BC9A64"), NotificationType.HearingReminderLip,
                    MessageType.Email, "case name,case number,name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("E4E74D79-F6C1-4E91-8745-3831D69D3ECB"),
                    NotificationType.HearingReminderRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,day month year,time,username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("DE5CB558-9D62-4054-B73A-15BD52655B26"), NotificationType.HearingAmendmentJudge,
                    MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("DF178486-7770-41A6-9B9E-4B1C0A1017E3"), NotificationType.HearingAmendmentJoh,
                    MessageType.Email,
                    "case name,case number,judicial office holder,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("D6557723-A70E-40A0-A906-9181CDCB86BB"), NotificationType.HearingAmendmentLip,
                    MessageType.Email,
                    "case name,case number,name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("A80D6EE0-E9B4-415B-B187-194034A04065"),
                    NotificationType.HearingAmendmentRepresentative, MessageType.Email,
                    "case name,case number,client name,solicitor name,New Day Month Year,Old Day Month Year,New time,Old time,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("F66336E6-C371-47C0-A538-D3797DF575DE"),
                    NotificationType.HearingConfirmationJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days,courtroom account username,account password",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("4EB2A8D5-9556-4306-8B82-6213809446BA"),
                    NotificationType.HearingConfirmationJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("3BB37FA1-A882-4BD7-8427-FE3F4D00E0F5"),
                    NotificationType.HearingConfirmationLipMultiDay, MessageType.Email,
                    "case name,case number,name,Start Day Month Year,time,number of days,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("9F3A1020-E5BC-466E-B31D-D95F6B554072"),
                    NotificationType.HearingConfirmationRepresentativeMultiDay, MessageType.Email,
                    "case name,case number,client name,solicitor name,Start Day Month Year,time,number of days,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("5AEF0B6A-D746-4A99-B679-EA75EAFB1648"),
                    NotificationType.HearingConfirmationEJudJudge, MessageType.Email,
                    "case name,case number,judge,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("80969A3C-4B60-46BA-9163-11F764A4BA4B"),
                    NotificationType.HearingConfirmationEJudJudgeMultiDay, MessageType.Email,
                    "case name,case number,judge,Start Day Month Year,time,number of days", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("A63D188E-F21C-4D71-B107-536632D83C1A"),
                    NotificationType.HearingAmendmentEJudJudge, MessageType.Email,
                    "case name,case number,judge,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("D205E122-7758-4EE6-8DDA-39DE202EA176"), NotificationType.HearingAmendmentEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("67569EE5-8CDF-4A51-B7AF-9B1DCF7800F7"), NotificationType.HearingReminderEJudJoh,
                    MessageType.Email, "case name,case number,judicial office holder,day month year,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("6992E482-8BB4-4D41-A511-CB39D1DE080B"),
                    NotificationType.HearingConfirmationEJudJoh, MessageType.Email,
                    "case name,case number,judicial office holder,day month year,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("0DB88FE3-DD38-41A7-AA44-C2C6ADD31006"),
                    NotificationType.HearingConfirmationEJudJohMultiDay, MessageType.Email,
                    "case name,case number,judicial office holder,Start Day Month Year,time,number of days",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("F6D035BF-FD2F-44D2-902E-FA698E8F396F"), NotificationType.EJudJohDemoOrTest,
                    MessageType.Email, "case number,test type,judicial office holder,username,date,time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("E5879631-1CFD-4439-8A02-95B612B1E719"), NotificationType.ParticipantDemoOrTest,
                    MessageType.Email, "test type,case number,date,name,username,time", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("F17C499F-8183-4F2B-920C-3473CCE51D45"), NotificationType.EJudJudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("080ADD48-4F87-4FD7-B878-05EEBCC469C4"), NotificationType.JudgeDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,Judge,courtroom account username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("1BDFD439-5923-4E00-9001-602A5FFDA801"),
                    NotificationType.TelephoneHearingConfirmation, MessageType.Email,
                    "case name,case number,name,day month year,time", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("EE4A9A9A-EA1E-4C07-B488-D94CFA2ACD07"),
                    NotificationType.TelephoneHearingConfirmationMultiDay, MessageType.Email,
                    "case name,case number,name,day month year,time,number of days", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("C8F47717-3001-408C-B1F3-91002F5F5A89"), NotificationType.CreateStaffMember,
                    MessageType.Email, "Name,Username,Password", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("4719EC05-E46C-413F-86C0-EF7DC3AE2340"),
                    NotificationType.HearingAmendmentStaffMember, MessageType.Email,
                    "case name,case number,staff member,New Day Month Year,Old Day Month Year,New time,Old time",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("7D79D219-A0B9-44E4-91B0-F2C9FCF9ACEC"),
                    NotificationType.HearingConfirmationStaffMember, MessageType.Email,
                    "case name,case number,staff member,day month year,time,username", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("D2CA16E9-58CD-4A07-8E7C-B440D42FBFA9"),
                    NotificationType.HearingConfirmationStaffMemberMultiDay, MessageType.Email,
                    "case name,case number,staff member,Start Day Month Year,time,number of days,username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("82AD1652-1E23-425F-A06B-668125C98634"), NotificationType.StaffMemberDemoOrTest,
                    MessageType.Email, "test type,date,time,case number,staff member", DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("8AD8FEC0-C692-41FC-9B8E-36F0CE3FF84C"), NotificationType.NewHearingReminderLIP,
                    MessageType.Email, "case name, case number, name, day month year, time, username", DateTime.UtcNow,
                    DateTime.UtcNow)
            },
            {
                new Template(new Guid("1E7BFC5C-12E7-45FF-93FE-972820161D52"),
                    NotificationType.NewHearingReminderRepresentative, MessageType.Email,
                    "case name, case number, client name, solicitor name, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
            {
                new Template(new Guid("954E98DD-354A-4580-9855-C23FE16BA312"), NotificationType.NewHearingReminderJOH,
                    MessageType.Email, "case name, case number, judicial office holder, day month year, time, username",
                    DateTime.UtcNow, DateTime.UtcNow)
            },
        };

        public IList<Template> Get(string environment)
        {
            switch (environment)
            {
                case "Dev":
                    return _sourceTemplatesDev;
                case "Preview":
                    return _sourceTemplatesPreview;
                case "AAT":
                    return _sourceTemplatesAAT;
                case "Sandbox":
                    return _sourceTemplatesSandbox;
                case "Test1":
                    return _sourceTemplatesTest1;
                case "Demo":
                    return _sourceTemplatesDemo;
                case "PreProd":
                    return _sourceTemplatesPreProd;
                case "Prod":
                    return _sourceTemplatesProd;
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
                    throw new ArgumentException($"Environment variable {environment} is not set - unable to find the list of templates");
            }
        }
    }
}
