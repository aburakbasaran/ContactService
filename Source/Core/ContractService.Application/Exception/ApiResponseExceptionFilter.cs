using ContactService.Application.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactService.Application.Exception
{
    public class ApiResponseExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ApiResponse apiResponse = new();

            if (context.Exception is HandledException handledException)
            {
                apiResponse.AddMessage(handledException.MessageType, handledException.HttpStatusCode, handledException.Message);
            }
            else
            {
                apiResponse.AddError(StatusCodes.Status500InternalServerError, context.Exception.Message);
            }

            context.ExceptionHandled = true;
            context.Result = new ObjectResult(apiResponse)
            {
                StatusCode = apiResponse.HttpStatusCode
            };
        }
    }
}
