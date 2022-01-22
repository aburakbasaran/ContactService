using System;
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
    [Generator(ActionName = "Delete", ControllerName = "User", HttpMethod = HttpMethod.Delete)]
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, ApiResponse<bool>>
    {
        private readonly IContactDbContext _dbContext;
        public DeleteUserCommandHandler(IContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> result = new();
            result.Data = false;

            request.Name = request.Name.ToLower();

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

            if (user == null)
            {
                result.Messages = new();
                result.Messages.Add(new MessageItem { Message = "user not found", Type = MessageType.Error });
                result.HttpStatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            _dbContext.Users.Remove(user);

            var contextResult = await _dbContext.SaveChangesAsync(cancellationToken);

            if (contextResult > 0)
            {
                result.Data = true;
            }

            return result;
        }
    }
}