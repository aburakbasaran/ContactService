using ContactService.Application.Commmand;
using ContactService.Application.Model;

namespace ContactService.ContactModule.Messages.User.Command
{
    public class DeleteUserCommand : BaseCommand<ApiResponse<bool>>
    {
        public string Name { get; set; }
    }
}
