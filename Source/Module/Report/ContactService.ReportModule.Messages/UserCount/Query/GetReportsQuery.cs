using ContactService.Application.Model;
using ContactService.Application.Queries;
using ContactService.ContactModule.Messages.User.Dto;

namespace ContactService.ReportModule.Messages.UserCount.Query
{
    public class GetReportsQuery : BaseQuery<ApiResponse<ReportDetailDto>>
    {
    }
}
