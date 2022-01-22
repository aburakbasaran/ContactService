using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContactService.Application.Commmand;
using ContactService.Application.Enum;
using ContactService.Application.Model;
using ContactService.ContactModule.Data.Data;
using ContactService.ContactModule.Messages.User.Command;
using ContactService.SourceGenerator.ApiGenerator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ContactService.ContactModule.Engine.User.CommandHandler
{
    [Generator(ActionName = "Delete", ControllerName = "UserContact", HttpMethod = HttpMethod.Delete)]
    public class DeleteUserContactCommandHandler : ICommandHandler<DeleteUserContactCommand, ApiResponse<bool>>
    {
        private readonly IContactDbContext _dbContext;
        public DeleteUserContactCommandHandler(IContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteUserContactCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> result = new();
            result.Data = false;

            var userContact = await _dbContext.UserContacts.Where(x => x.Type == (byte)request.ContactType.GetHashCode()).ToListAsync(cancellationToken);

            if (userContact.Count == 0)
            {
                result.Messages = new();
                result.Messages.Add(new MessageItem { Message = "user contact not found", Type = MessageType.Error });
                result.HttpStatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            _dbContext.UserContacts.RemoveRange(userContact);

            var contextResult = await _dbContext.SaveChangesAsync(cancellationToken);

            if (contextResult > 0)
            {
                result.Data = true;
            }

            return result;
        }
    }
}