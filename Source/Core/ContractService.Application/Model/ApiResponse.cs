using System.Collections.Generic;
using System.Text.Json.Serialization;
using ContactService.Application.Enum;
using Microsoft.AspNetCore.Http;

namespace ContactService.Application.Model
{
    public class ApiResponse
    {
        [JsonIgnore]
        public int HttpStatusCode { get; set; } = StatusCodes.Status200OK;

        public List<MessageItem> Messages { get; set; }

        public void AddMessage(MessageType type, int httpStatusCode, string message)
        {
            HttpStatusCode = httpStatusCode;
            Messages ??= new();
            Messages.Add(new()
            {
                Type = type,
                Message = message
            });
        }

        public void AddWarning(int httpStatusCode, string message)
        {
            HttpStatusCode = httpStatusCode;
            Messages ??= new();
            Messages.Add(new()
            {
                Type = MessageType.Warning,
                Message = message
            });
        }

        public void AddError(int httpStatusCode, string message)
        {
            HttpStatusCode = httpStatusCode;
            Messages ??= new();
            Messages.Add(new()
            {
                Type = MessageType.Error,
                Message = message
            });
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public ApiResponse()
        {
        }

        public ApiResponse(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
