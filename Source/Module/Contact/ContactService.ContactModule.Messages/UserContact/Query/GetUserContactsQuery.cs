using ContactService.Application.Model;
using ContactService.Application.Queries;
using ContactService.ContactModule.Messages.User.Dto;

namespace ContactService.ContactModule.Messages.User.Command
{
    public class GetUserContactsQuery : BaseQuery<ApiResponse<UserContactsDto>>
    {
    }
}
