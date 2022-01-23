using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContactService.Application.Model;
using ContactService.Application.Queries;
using ContactService.ContactModule.Data.Data;
using ContactService.ContactModule.Messages.Enum;
using ContactService.ContactModule.Messages.User.Command;
using ContactService.ContactModule.Messages.UserContact.Query.Dto;
using ContactService.SourceGenerator.ApiGenerator;

namespace ContactService.ContactModule.Engine.UserContact.QueryHandler
{
    [Generator(ActionName = "Get", ControllerName = "UserContactLocationsReport", HttpMethod = HttpMethod.Get, NameSpace = "ContactService.API.Controllers")]
    public class UsersLocationReportQueryHandler : IQueryHandler<GetUsersLocationQuery, ApiResponse<List<UserLocationReportDto>>>
    {
        private readonly IContactDbContext _dbContext;
        public UsersLocationReportQueryHandler(IContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<List<UserLocationReportDto>>> Handle(GetUsersLocationQuery request, CancellationToken cancellationToken)
        {
            ApiResponse<List<UserLocationReportDto>> result = new();

            var query = _dbContext.UserContacts.Where(x => x.Type == ContactTypeEnum.Location.GetHashCode()).ToList()
                    .GroupBy(p => p.Value)
                    .Select(g => new UserLocationReportDto { Location = g.Key, UserCount = g.Count() });

            result.Data = query.ToList();

            return result;
        }


    }
}