using System;
using System.Collections.Generic;
using System.Linq;
using ContactService.Application.Exception;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace ContactService.UnitTest.Exceptions
{
    public class ValidationException_Tests
    {
        [Fact]
        public void DefaultConstructor_Ok()
        {
            ValidationException validationException = new();
            validationException.Should().NotBeNull();
            validationException.Should().BeAssignableTo<Exception>();
            validationException.Errors.Should().BeEmpty();
            validationException.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ParameterConstructor_WithEmptyCollection_ReturnEmptyObject()
        {
            IEnumerable<ValidationFailure> failures = Enumerable.Empty<ValidationFailure>();
            ValidationException validationException = new(failures);
            validationException.Should().NotBeNull();
            validationException.Errors.Should().BeEmpty();
            validationException.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ParameterConstructor_WithValidationFailures_ReturnObjectHasErrors()
        {
            string nameProperty = "Name";
            string namePropertyMessage = "Name is required";
            string ageProperty = "Age";
            string agePropertyMessage = "Age must be ....";

            List<ValidationFailure> failures = new()
            {
                new(nameProperty, namePropertyMessage),
                new(ageProperty, agePropertyMessage)
            };

            ValidationException validationException = new(failures);
            validationException.Should().NotBeNull();
            validationException.Errors.Should().NotBeNull();
            validationException.Errors.Should().HaveCount(2);
            validationException.Errors.Should().ContainKey(nameProperty);
            validationException.Errors.Should().ContainKey(ageProperty);
            validationException.Errors[nameProperty].Should().NotBeEmpty();
            validationException.Errors[nameProperty].Should().BeEquivalentTo(namePropertyMessage);
            validationException.Errors[ageProperty].Should().NotBeEmpty();
            validationException.Errors[ageProperty].Should().BeEquivalentTo(agePropertyMessage);
            validationException.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ParameterConstructor_WithSameKeyValidationFailures_ReturnGroupByPropertyObject()
        {
            string nameProperty = "Name";
            string namePropertyMessage = "Name is required";
            string namePropertySecondMessage = "Name must ...";
            string ageProperty = "Age";
            string agePropertyMessage = "Age must be ....";

            List<ValidationFailure> failures = new()
            {
                new(nameProperty, namePropertyMessage),
                new(nameProperty, namePropertySecondMessage),
                new(ageProperty, agePropertyMessage)
            };

            ValidationException validationException = new(failures);
            validationException.Should().NotBeNull();
            validationException.Errors.Should().NotBeNull();
            validationException.Errors.Should().HaveCount(2);
            validationException.Errors.Should().ContainKey(nameProperty);
            validationException.Errors[nameProperty].Should().NotBeEmpty();
            validationException.Errors[nameProperty].Should().BeEquivalentTo(namePropertyMessage, namePropertySecondMessage);
            validationException.Errors[ageProperty].Should().NotBeEmpty();
            validationException.Errors[ageProperty].Should().BeEquivalentTo(agePropertyMessage);
            validationException.Errors.Should().ContainKey(ageProperty);
            validationException.Message.Should().NotBeNullOrEmpty();
        }
    }
}
