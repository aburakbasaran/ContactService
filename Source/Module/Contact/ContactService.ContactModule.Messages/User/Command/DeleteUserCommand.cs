using ContactService.Application.Commmand;
using ContactService.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.ContactModule.Messages.User.Command
{
    public class DeleteUserCommand : BaseCommand<ApiResponse<bool>>
    {
        public string Name { get; set; }
    }
}
