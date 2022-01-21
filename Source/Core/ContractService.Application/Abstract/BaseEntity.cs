using System.Threading;
using System.Threading.Tasks;
using ContactService.Application.Exception;
using ContactService.Application.Interface;

namespace ContactService.Application.Abstract
{
    public abstract class BaseEntity
    {
        public static async Task CheckRule(IBusinessRule rule, CancellationToken cancellationToken)
        {
            if (await rule.IsBroken(cancellationToken))
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
