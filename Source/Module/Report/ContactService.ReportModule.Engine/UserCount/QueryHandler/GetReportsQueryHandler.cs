using ContactService.Application.Model;
using ContactService.Application.Queries;
using ContactService.ContactModule.Messages.User.Dto;
using ContactService.ReportModule.Data.Data;
using ContactService.ReportModule.Messages.UserCount.Query;
using ContactService.SourceGenerator.ApiGenerator;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContactService.ContactModule.Engine.User.CommandHandler
{
    [Generator(ActionName = "Get", ControllerName = "GetReports", HttpMethod = HttpMethod.Get, NameSpace = "ContactService.Report.Api.Controllers")]
    public class GetReportsQueryHandler : IQueryHandler<GetReportsQuery, ApiResponse<ReportDetailDto>>
    {
        private readonly IContactReportDbContext _dbContext;
        public GetReportsQueryHandler(IContactReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<ReportDetailDto>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse<ReportDetailDto> result = new();
            ReportDetailDto reportDetailDto = new();

            var query = _dbContext.ReportDetails.Select(x => x.ReportJson).ToList();

            reportDetailDto.ReportJsons = query;

            result.Data = reportDetailDto;

            return result;
        }


    }
}