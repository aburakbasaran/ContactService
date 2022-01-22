using ContactService.Application.Model;
using ContactService.Application.Queries;
using ContactService.ContactModule.Messages.User.Dto;
using System.Collections.Generic;

namespace ContactService.ContactModule.Messages.User.Command
{
    public class GetUsersLocationQuery : BaseQuery<ApiResponse<List<UserLocationReportDto>>>
    {
    }
}
