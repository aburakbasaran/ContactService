using ContactService.Application.Enum;
using System;
using System.Runtime.Serialization;

namespace ContactService.Application.Exception
{
    [Serializable]
    public class HandledException : SystemException
    {
        public HandledException()
        {
        }

        public HandledException(MessageType messageType, int httpStatusCode, string message)
            : base(message)
        {
            MessageType = messageType;
            HttpStatusCode = httpStatusCode;
        }

        public HandledException(SystemException ex, MessageType messageType, int httpStatusCode, string message)
           : base(message, ex)
        {
            MessageType = messageType;
            HttpStatusCode = httpStatusCode;
        }

        protected HandledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public MessageType MessageType { get; set; }
        public int HttpStatusCode { get; set; }
    }
}