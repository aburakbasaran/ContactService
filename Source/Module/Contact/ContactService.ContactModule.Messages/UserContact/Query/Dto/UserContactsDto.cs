using System.Collections.Generic;

namespace ContactService.ContactModule.Messages.User.Dto
{
    public class UserContactsDto
    {
        public string Name { get; set; }

        public List<UserContactDto> UserContacts { get; set; }
    }
}
