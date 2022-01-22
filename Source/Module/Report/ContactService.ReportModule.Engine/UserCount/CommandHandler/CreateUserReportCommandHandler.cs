using System;
using System.Threading;
using System.Threading.Tasks;
using ContactService.Application.Commmand;
using ContactService.Application.Model;
using ContactService.ContactModule.Messages.User.Command;
using ContactService.Infrastructure.Provider.Bus.RabbitQueue;
using ContactService.ReportModule.Data.Data;
using ContactService.SourceGenerator.ApiGenerator;

namespace ContactService.ContactModule.Engine.User.CommandHandler
{
    [Generator(ActionName = "Create", ControllerName = "CreateUserCountReport", HttpMethod = HttpMethod.Post, NameSpace = "ContactService.Report.Api.Controllers")]
    public class CreateUserReportCommandHandler : ICommandHandler<CreateUserCountReportCommand, ApiResponse<bool>>
    {
        private readonly IContactReportDbContext _dbContext;
        private readonly IBus _busControl;
        public CreateUserReportCommandHandler(IContactReportDbContext dbContext, IBus busControl)
        {
            _dbContext = dbContext;
            _busControl = busControl;
        }

        public async Task<ApiResponse<bool>> Handle(CreateUserCountReportCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> result = new();
            result.Data = false;

            var id = Guid.NewGuid();

            _dbContext.Reports.Add(new()
            {
                Id = id,
                CreatedDateTime = DateTime.UtcNow,
                IsCompleted = false
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
            await _busControl.SendAsync(Queue.Processing, id);

            result.Data = true;

            return result;
        }
    }
}