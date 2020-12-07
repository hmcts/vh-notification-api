using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace NotificationApi.UnitTests.Domain
{
    public class MessageTypeEnumTests
    {
        [Test]
        public void should_have_same_values_in_public_contract()
        {
            // Arrange
            var availableValues = Enum.GetValues(typeof(NotificationApi.Domain.Enums.MessageType)).Cast<NotificationApi.Domain.Enums.MessageType>();
            var contractValues = Enum.GetValues(typeof(Contract.MessageType)).Cast<Contract.MessageType>();

            // Act

            // Assert

            // Assert int are same
            availableValues.All(v => contractValues.Contains((Contract.MessageType)v)).Should().BeTrue();
            contractValues.All(v => availableValues.Contains((NotificationApi.Domain.Enums.MessageType)v)).Should().BeTrue();

            // Assert string value matches
            availableValues.Select(x => x.ToString()).All(v => contractValues.Select(x => x.ToString()).Contains(v)).Should().BeTrue();
            contractValues.Select(x => x.ToString()).All(v => availableValues.Select(x => x.ToString()).Contains(v)).Should().BeTrue();
        }
    }
}
