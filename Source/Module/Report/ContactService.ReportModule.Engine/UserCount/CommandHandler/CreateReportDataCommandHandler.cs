using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ContactService.Application.Commmand;
using ContactService.Application.Enum;
using ContactService.Application.Model;
using ContactService.ContactModule.Messages.User.Command;
using ContactService.ContactModule.Messages.User.Dto;
using ContactService.Infrastructure.Common;
using ContactService.ReportModule.Data.Data;
using ContactService.ReportModule.Data.Data.Entities;
using ContactService.SourceGenerator.ApiGenerator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ContactService.ContactModule.Engine.User.CommandHandler
{
    [Generator(ActionName = "Create", ControllerName = "CreateUserCountReportData", HttpMethod = HttpMethod.Post, NameSpace = "ContactService.Report.Api.Controllers")]
    public class CreateReportDataCommandHandler : ICommandHandler<CreateReportCommand, ApiResponse<bool>>
    {
        private readonly IContactReportDbContext _dbContext;
        private readonly IApiClient _apiClient;

        public CreateReportDataCommandHandler(IContactReportDbContext dbContext, IApiClient apiClient)
        {
            _dbContext = dbContext;
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<bool>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            ApiResponse<bool> result = new();
            result.Data = false;

            var report = await _dbContext.Reports.FirstOrDefaultAsync(x => x.Id == request
            .ReportId, cancellationToken);

            if (report == null)
            {
                result.Messages = new();
                result.Messages.Add(new MessageItem { Message = "user not found", Type = MessageType.Error });
                result.HttpStatusCode = StatusCodes.Status400BadRequest;
                return result;
            }

            var reportData = await GetReportData(cancellationToken);

            _dbContext.ReportDetails.Add(new ReportDetailEntity()
            {
                Id = Guid.NewGuid(),
                ReportJson = JsonSerializer.Serialize(reportData.Data),
                ReportId = request.ReportId
            });

            report.IsCompleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);

            result.Data = true;

            return result;
        }

        private async Task<ApiResponse<List<UserLocationReportDto>>> GetReportData(CancellationToken cancellationToken)
        {
            string url = "https://localhost:5001/UserContactLocationsReport";
            return await _apiClient.Get<ApiResponse<List<UserLocationReportDto>>>(url,
                30, cancellationToken);
        }
    }
}