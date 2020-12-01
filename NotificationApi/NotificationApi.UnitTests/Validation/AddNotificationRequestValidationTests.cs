using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.Contract.Requests;
using NotificationApi.Validations;
using NUnit.Framework;

namespace NotificationApi.UnitTests.Validation
{
    public class AddNotificationRequestValidationTests
    {
        private AddNotificationRequestValidation _validator;
        private AddNotificationRequest _request;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _validator = new AddNotificationRequestValidation();
        }

        [SetUp]
        public void SetUp()
        {
            _request = InitRequest();
        }

        [Test]
        public async Task Should_Validate_Successfully_With_A_Correct_Model()
        {
            var result = await _validator.ValidateAsync(_request);
            result.IsValid.Should().BeTrue();
        }

        [Test]
        public async Task Should_Fail_When_Email_Is_Missing_When_MessageType_Is_Email()
        {
            _request.ContactEmail = null;
            _request.MessageType = Contract.MessageType.Email;
            var result = await _validator.ValidateAsync(_request);
            result.IsValid.Should().BeFalse();
            result.Errors.Any(x => x.ErrorMessage == AddNotificationRequestValidation.MissingEmailMessage).Should()
                .BeTrue();
        }
        
        [Test]
        public async Task Should_Fail_When_HearingId_Is_Missing()
        {
            _request.HearingId = Guid.Empty;
            var result = await _validator.ValidateAsync(_request);
            result.IsValid.Should().BeFalse();
            result.Errors.Any(x => x.ErrorMessage == AddNotificationRequestValidation.MissingHearingIdMessage).Should()
                .BeTrue();
        }
        
        [Test]
        public async Task Should_Fail_When_MessageType_Is_Invalid()
        {
            _request.MessageType = (Contract.MessageType)4;
            var result = await _validator.ValidateAsync(_request);
            result.IsValid.Should().BeFalse();
            result.Errors.Any(x => x.ErrorMessage == AddNotificationRequestValidation.InvalidMessageTypeMessage).Should()
                .BeTrue();
        }
        
        [Test]
        public async Task Should_Fail_When_NotificationType_Is_Invalid()
        {
            _request.NotificationType = (Contract.NotificationType)4;
            var result = await _validator.ValidateAsync(_request);
            result.IsValid.Should().BeFalse();
            result.Errors.Any(x => x.ErrorMessage == AddNotificationRequestValidation.InvalidNotificationTypeMessage).Should()
                .BeTrue();
        }

        [Test]
        public async Task Should_Fail_When_Parameters_Are_Missing()
        {
            _request.Parameters = null;
            var result = await _validator.ValidateAsync(_request);
            result.IsValid.Should().BeFalse();
            result.Errors.Any(x => x.ErrorMessage == AddNotificationRequestValidation.MissingParametersMessage).Should()
                .BeTrue();
        }
        
        [Test]
        public async Task Should_Fail_When_ParticipantId_Is_Missing()
        {
            _request.ParticipantId = Guid.Empty;
            var result = await _validator.ValidateAsync(_request);
            result.IsValid.Should().BeFalse();
            result.Errors.Any(x => x.ErrorMessage == AddNotificationRequestValidation.MissingParticipantIdMessage).Should()
                .BeTrue();
        }
        
        [Test]
        public async Task Should_Fail_When_Phone_Number_Is_Missing_When_MessageType_Is_SMS()
        {
            _request.PhoneNumber = null;
            _request.MessageType = Contract.MessageType.SMS;
            var result = await _validator.ValidateAsync(_request);
            result.IsValid.Should().BeFalse();
            result.Errors.Any(x => x.ErrorMessage == AddNotificationRequestValidation.MissingPhoneNumberMessage).Should()
                .BeTrue();
        }
        
        private AddNotificationRequest InitRequest()
        {
            var parameters = new Dictionary<string, string> {{"test", "test1"}};

            return new AddNotificationRequest
            {
                ContactEmail = "email@email.com",
                HearingId = Guid.NewGuid(),
                MessageType = Contract.MessageType.Email,
                NotificationType = Contract.NotificationType.CreateIndividual,
                Parameters = parameters,
                ParticipantId = Guid.NewGuid(),
                PhoneNumber = "1234567890"
            };
        }
    }
}
