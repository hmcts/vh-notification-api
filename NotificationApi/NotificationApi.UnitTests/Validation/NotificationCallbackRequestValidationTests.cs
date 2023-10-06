using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.Contract.Requests;
using NotificationApi.Validations;
using NUnit.Framework;

namespace NotificationApi.UnitTests.Validation
{
    public class NotificationCallbackRequestValidationTests
    {
        private NotificationCallbackRequestValidation _validator;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _validator = new NotificationCallbackRequestValidation();
        }
        
        [Test]
        public async Task should_validate_successfully_with_a_correct_model()
        {
            var request = InitRequest();
            var result = await _validator.ValidateAsync(request);
            result.IsValid.Should().BeTrue();
        }

        [Test] 
        public async Task should_fail_validation_when_id_is_missing()
        {
            var request = InitRequest();
            request.Id = null;
            var result = await _validator.ValidateAsync(request);
            result.IsValid.Should().BeFalse();
            result.Errors.Exists(x => x.ErrorMessage == NotificationCallbackRequestValidation.MissingIdMessage)
                .Should().BeTrue();
        }
        
        [Test] 
        public async Task should_fail_validation_when_reference_is_missing()
        {
            var request = InitRequest();
            request.Reference = null;
            var result = await _validator.ValidateAsync(request);
            result.IsValid.Should().BeFalse();
            result.Errors.Exists(x => x.ErrorMessage == NotificationCallbackRequestValidation.MissingReferenceMessage)
                .Should().BeTrue();
        }
        
        [Test] 
        public async Task should_fail_validation_when_reference_is_not_a_guid()
        {
            var request = InitRequest();
            request.Reference = "unknown";
            var result = await _validator.ValidateAsync(request);
            result.IsValid.Should().BeFalse();
            result.Errors.Exists(x => x.ErrorMessage == NotificationCallbackRequestValidation.InvalidReferenceMessage)
                .Should().BeTrue();
        }
        
        [Test] 
        public async Task should_fail_validation_when_status_is_missing()
        {
            var request = InitRequest();
            request.Status = null;
            var result = await _validator.ValidateAsync(request);
            result.IsValid.Should().BeFalse();
            result.Errors.Exists(x => x.ErrorMessage == NotificationCallbackRequestValidation.MissingStatusMessage)
                .Should().BeTrue();
        }
        
        [Test] 
        public async Task should_fail_validation_when_status_is_not_recognised()
        {
            var request = InitRequest();
            request.Status = "unknown";
            var result = await _validator.ValidateAsync(request);
            result.IsValid.Should().BeFalse();
            result.Errors.Exists(x => x.ErrorMessage == NotificationCallbackRequestValidation.InvalidStatusMessage)
                .Should().BeTrue();
        }

        private NotificationCallbackRequest InitRequest()
        {
            return new NotificationCallbackRequest
            {
                Id = Guid.NewGuid().ToString(),
                Reference = Guid.NewGuid().ToString(),
                Status = "delivered"
            };
        }
    }
}
