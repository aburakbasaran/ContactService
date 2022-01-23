using ContactService.Application.Commmand;
using ContactService.Application.Enum;
using ContactService.Application.Model;
using ContactService.ContactModule.Data.Data;
using ContactService.ContactModule.Data.Data.Entities;
using ContactService.ContactModule.Messages.UserContact;
using ContactService.SourceGenerator.ApiGenerator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactService.ContactModule.Engine.User.CommandHandler
{
    [Generator(ActionName = "Add", ControllerName = "UserContact", HttpMethod = HttpMethod.Post, NameSpace = "ContactService.API.Controllers")]
    public class AddUserContactCommandHandler : ICommandHandler<AddUserContactCommand, ApiResponse<bool>>
    {
        private readonly IContactDbContext _dbContext;
        public AddUserContactCommandHandler(IContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<bool>> Handle(AddUserContactCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> result = new();
            result.Data = false;

            var userName = request.AddUserContactDto.UserName.ToLower();

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == userName, cancellationToken);

            if (user == null)
            {
                result.Messages = new();
                result.Messages.Add(new MessageItem { Message = "user not found", Type = MessageType.Error });
                result.HttpStatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            UserContactEntity userContactEntity = new()
            {
                Id = Guid.NewGuid(),
                Type = (byte)request.AddUserContactDto.ContactType.GetHashCode(),
                Value = request.AddUserContactDto.Value,
                UserId = user.Id
            };

            _dbContext.UserContacts.Add(userContactEntity);

            var contextResult = await _dbContext.SaveChangesAsync(cancellationToken);

            if (contextResult > 0)
            {
                result.Data = true;
            }

            return result;
        }
    }
}