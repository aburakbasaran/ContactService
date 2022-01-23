using FluentValidation;

namespace ContactService.UnitTest.Behaviour
{
    public class ValidatorMockType : AbstractValidator<ValidationRequest>
    {
        public ValidatorMockType()
        {
            RuleFor(c => c.Id).GreaterThan(3);
        }
    }
}