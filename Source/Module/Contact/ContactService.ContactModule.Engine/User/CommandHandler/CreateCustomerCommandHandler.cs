using System.Threading;
using System.Threading.Tasks;
using ContactService.Application.Commmand;
using ContactService.Application.Model;
using ContactService.ContactModule.Messages.User.Command;
using ContactService.SourceGenerator.ApiGenerator;

namespace ContactService.ContactModule.Engine.User.CommandHandler
{
    [Generator(ActionName = "Create", ControllerName = "User", HttpMethod = HttpMethod.Post)]
    public class CreateCustomerCommandHandler : ICommandHandler<AddUserCommand, ApiResponse<bool>>
    {
        public async  Task<ApiResponse<bool>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> result = new();

            result.Data = false;
            return result;
        }
    }
}