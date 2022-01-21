using System;
using System.Runtime.Serialization;
using ContactService.Application.Interface;

namespace ContactService.Application.Exception
{
    [Serializable]
    public class BusinessRuleValidationException : System.Exception
    {
        public IBusinessRule BrokenRule { get; }

        public string Details { get; }

        public BusinessRuleValidationException(IBusinessRule brokenRule) : base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
            Details = brokenRule.Message;
        }

        protected BusinessRuleValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string ToString()
        {
            return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
        }
    }
}
