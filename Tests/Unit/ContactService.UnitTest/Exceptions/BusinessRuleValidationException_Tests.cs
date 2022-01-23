using System;
using ContactService.Application.Exception;
using ContactService.Application.Interface;
using FluentAssertions;
using Moq;
using Xunit;

namespace ContactService.UnitTest.Exceptions
{
    public class BusinessRuleValidationException_Tests
    {
        [Fact]
        public void ToString_NullBussinessRule_ThrowException()
        {
            Action exceptionAction = () =>
            {
                BusinessRuleValidationException businessRuleValidationException = new(null);
            };
            exceptionAction.Should().Throw<Exception>();
        }

        [Fact]
        public void ToString_BussinessRuleWithMessage_ReturnMessageString()
        {
            Mock<IBusinessRule> bussinessRule = new();
            string bussinessRuleMessage = "message_message";
            bussinessRule.SetupGet(c => c.Message).Returns(bussinessRuleMessage);

            BusinessRuleValidationException exception = new(bussinessRule.Object);
            exception.Should().NotBeNull();
            exception.Details.Should().NotBeNullOrEmpty();
            exception.Details.Should().BeEquivalentTo(bussinessRuleMessage);
            string toStringResult = exception.ToString();

            toStringResult.Should().NotBeNullOrEmpty();
            toStringResult.Should().Contain(bussinessRuleMessage);
        }
    }
}
