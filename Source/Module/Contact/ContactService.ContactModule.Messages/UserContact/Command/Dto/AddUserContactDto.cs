using ContactService.ContactModule.Messages.Enum;

namespace ContactService.ContactModule.Messages.UserContact.Dto
{
    public class AddUserContactDto
    {
        public string UserName { get; set; }
        public ContactTypeEnum ContactType { get; set; }
        public string Value { get; set; }
    }
}
