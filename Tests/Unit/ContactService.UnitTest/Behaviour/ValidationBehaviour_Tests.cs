using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContactService.Application.Behaviors;
using ContactService.Application.Model;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace ContactService.UnitTest.Behaviour
{
    public class ValidationBehaviour_Tests
    {
        private readonly Mock<RequestHandlerDelegate<ApiResponse>> _requestHandler;

        public ValidationBehaviour_Tests()
        {
            _requestHandler = new();
        }

        [Fact]
        public async Task NoValidation_ShouldNext()
        {
            ApiResponse response = new();
            _requestHandler.Setup(c => c.Invoke()).ReturnsAsync(response).Verifiable();

            ValidationBehaviour<ValidationRequest, ApiResponse> validationBehaviour = new(Enumerable.Empty<ValidatorMockType>());
            await validationBehaviour.Handle(new(), CancellationToken.None, _requestHandler.Object);
            _requestHandler.Verify(c => c.Invoke());
        }

        [Fact]
        public async Task HasValidation_ShouldReturnApiResponse()
        {
            List<ValidatorMockType> validationRules = new() { new() };
            ValidationBehaviour<ValidationRequest, ApiResponse> validationBehaviour = new(validationRules);
            ApiResponse validationResponse = await validationBehaviour.Handle(new() { Id = 1 }, CancellationToken.None, _requestHandler.Object);
            validationResponse.Should().NotBeNull();
            validationResponse.Messages.Should().HaveCount(1);
            validationResponse.HttpStatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
