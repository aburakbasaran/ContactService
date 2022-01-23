using ContactService.Application.Commmand;
using ContactService.Application.Model;
using ContactService.ContactModule.Data.Data;
using ContactService.ContactModule.Data.Data.Entities;
using ContactService.ContactModule.Messages.User.Command;
using ContactService.SourceGenerator.ApiGenerator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactService.ContactModule.Engine.User.CommandHandler
{
    [Generator(ActionName = "Create", ControllerName = "User", HttpMethod = HttpMethod.Post, NameSpace = "ContactService.API.Controllers")]
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, ApiResponse<bool>>
    {
        private readonly IContactDbContext _dbContext;
        public CreateUserCommandHandler(IContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> result = new();
            result.Data = false;

            UserEntity user = new();

            user.Id = Guid.NewGuid();
            user.Name = request.CreateUserDto.Name.ToLower();
            user.SurName = request.CreateUserDto.SurName.ToLower();
            user.Firm = request.CreateUserDto.Firm;

            _dbContext.Users.Add(user);
            var contextResult = await _dbContext.SaveChangesAsync(cancellationToken);

            if (contextResult > 0)
            {
                result.Data = true;
            }

            return result;
        }
    }
}