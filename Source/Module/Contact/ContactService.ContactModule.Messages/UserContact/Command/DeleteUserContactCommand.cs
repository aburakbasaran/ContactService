using ContactService.Application.Commmand;
using ContactService.Application.Model;
using ContactService.ContactModule.Messages.Enum;

namespace ContactService.ContactModule.Messages.User.Command
{
    public class DeleteUserContactCommand : BaseCommand<ApiResponse<bool>>
    {
        public ContactTypeEnum ContactType { get; set; }
    }
}
