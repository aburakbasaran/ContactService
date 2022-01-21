using ContactService.Application.Commmand;
using ContactService.Application.Model;
using ContactService.ContactModule.Messages.User.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.ContactModule.Messages.User.Command
{
    public class CreateUserCommand : BaseCommand<ApiResponse<bool>>
    {
        [FromBody]
        public CreateUserDto CreateUserDto { get; set; }
    }
}
