using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ContactService.Application.Constant;
using FluentValidation.Results;


namespace ContactService.Application.Exception
{
    [Serializable]
    public class ValidationException : System.Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException() : base(MessageConstants.ValidationExceptionMessage)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
