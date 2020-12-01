using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace NotificationApi.UnitTests.Domain
{
    public class NotificationTypeEnumTests
    {
        [Test]
        public void should_have_same_values_in_public_contract()
        {
            // Arrange
            var availableValues = Enum.GetValues(typeof(NotificationApi.Domain.Enums.NotificationType)).Cast<NotificationApi.Domain.Enums.NotificationType>();
            var contractValues = Enum.GetValues(typeof(Contract.NotificationType)).Cast<Contract.NotificationType>();

            // Act

            // Assert

            // Assert int are same
            availableValues.All(v => contractValues.Contains((Contract.NotificationType)v)).Should().BeTrue();
            contractValues.All(v => availableValues.Contains((NotificationApi.Domain.Enums.NotificationType)v)).Should().BeTrue();

            // Assert string value matches
            availableValues.Select(x => x.ToString()).All(v => contractValues.Select(x => x.ToString()).Contains(v)).Should().BeTrue();
            contractValues.Select(x => x.ToString()).All(v => availableValues.Select(x => x.ToString()).Contains(v)).Should().BeTrue();
        }
    }
}
