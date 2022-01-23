using System;
using System.Collections.Generic;
using ContactService.Application.Enum;
using ContactService.Application.Exception;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Xunit;

namespace ContactService.UnitTest.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class ApiResponseExceptionFilter_Tests
    {
        private readonly ExceptionContext _exceptionContext;

        public ApiResponseExceptionFilter_Tests()
        {
            ActionContext actionContext = new()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new(),
                ActionDescriptor = new()
            };
            _exceptionContext = new(actionContext, new List<IFilterMetadata>());
        }

        [Fact]
        public void OnException_ReturnHandleException()
        {
            ApiResponseExceptionFilter apiResponseExceptionFilter = new();
            System.SystemException exception = new("An Error");
            HandledException handledException = new(exception, MessageType.Error,
                StatusCodes.Status400BadRequest, "an error");

            _exceptionContext.Exception = handledException;
            apiResponseExceptionFilter.OnException(_exceptionContext);
            _exceptionContext.ExceptionHandled.Should().BeTrue();
            _exceptionContext.Result.Should().BeOfType<ObjectResult>();
        }

        [Fact]
        public void OnException_ReturnHandleExceptionWarning()
        {
            ApiResponseExceptionFilter apiResponseExceptionFilter = new();
            HandledException handledException = new(MessageType.Warning, StatusCodes.Status400BadRequest, "an error");
            _exceptionContext.Exception = handledException;
            apiResponseExceptionFilter.OnException(_exceptionContext);
            _exceptionContext.ExceptionHandled.Should().BeTrue();
            _exceptionContext.Result.Should().BeOfType<ObjectResult>();
        }

        [Fact]
        public void OnException_ReturnGenericException()
        {
            ApiResponseExceptionFilter apiResponseExceptionFilter = new();
            Exception exception = new("An Error");

            _exceptionContext.Exception = exception;
            apiResponseExceptionFilter.OnException(_exceptionContext);
            _exceptionContext.ExceptionHandled.Should().BeTrue();
            _exceptionContext.Result.Should().BeOfType<ObjectResult>();
        }
    }
}
