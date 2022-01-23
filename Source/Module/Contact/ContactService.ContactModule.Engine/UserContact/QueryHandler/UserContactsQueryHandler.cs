using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContactService.Application.Model;
using ContactService.Application.Queries;
using ContactService.ContactModule.Data.Data;
using ContactService.ContactModule.Messages.Enum;
using ContactService.ContactModule.Messages.User.Command;
using ContactService.ContactModule.Messages.User.Dto;
using ContactService.SourceGenerator.ApiGenerator;
using Microsoft.EntityFrameworkCore;

namespace ContactService.ContactModule.Engine.UserContact.QueryHandler
{
    [Generator(ActionName = "GetAll", ControllerName = "UserContact", HttpMethod = HttpMethod.Get, NameSpace = "ContactService.API.Controllers")]
    public class UserContactsQueryHandler : IQueryHandler<GetUserContactsQuery, ApiResponse<UserContactsDto>>
    {
        private readonly IContactDbContext _dbContext;
        public UserContactsQueryHandler(IContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<UserContactsDto>> Handle(GetUserContactsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse<UserContactsDto> result = new();

            var usersContacts = await _dbContext.Users.Include(x => x.UserContacts).ToListAsync(cancellationToken);
            UserContactsDto userContactsDto = new();
            userContactsDto.UserContacts = new();

            foreach (var user in usersContacts)
            {
                userContactsDto.Name = user.Name;
                userContactsDto.UserContacts.AddRange(user.UserContacts.Select(x => new UserContactDto() { Type = ((ContactTypeEnum)x.Type).ToString(), Value = x.Value }));
            }

            result.Data = userContactsDto;

            return result;
        }


    }
}