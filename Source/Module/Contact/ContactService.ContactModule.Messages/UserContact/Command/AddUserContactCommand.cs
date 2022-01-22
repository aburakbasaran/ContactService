using ContactService.Application.Commmand;
using ContactService.Application.Model;
using ContactService.ContactModule.Messages.UserContact.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.ContactModule.Messages.UserContact
{
    public class AddUserContactCommand : BaseCommand<ApiResponse<bool>>
    {
        [FromBody]
        public AddUserContactDto AddUserContactDto { get; set; }
    }
}
