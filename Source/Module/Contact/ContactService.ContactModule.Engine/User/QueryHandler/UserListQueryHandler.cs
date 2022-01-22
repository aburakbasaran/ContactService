using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContactService.Application.Model;
using ContactService.Application.Queries;
using ContactService.ContactModule.Data.Data;
using ContactService.ContactModule.Messages.User.Command;
using ContactService.ContactModule.Messages.User.Dto;
using ContactService.SourceGenerator.ApiGenerator;

namespace ContactService.ContactModule.Engine.User.CommandHandler
{
    [Generator(ActionName = "GetAll", ControllerName = "User", HttpMethod = HttpMethod.Get, NameSpace = "ContactService.API.Controllers")]
    public class UserListQueryHandler : IQueryHandler<GetUsersQuery, ApiResponse<List<UserListDto>>>
    {
        private readonly IContactDbContext _dbContext;
        public UserListQueryHandler(IContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<List<UserListDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            ApiResponse<List<UserListDto>> result = new();

            var users = _dbContext.Users.ToList().Select(x =>
                          new UserListDto
                          {
                              Firm = x.Firm,
                              Name = x.Name,
                              SurName = x.SurName
                          }).ToList();

            result.Data = users;

            return result;
        }


    }
}