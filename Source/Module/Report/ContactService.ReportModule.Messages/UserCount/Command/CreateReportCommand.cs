using ContactService.Application.Commmand;
using ContactService.Application.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ContactService.ContactModule.Messages.User.Command
{
    public class CreateReportCommand : BaseCommand<ApiResponse<bool>>
    {
        [FromBody]
        public Guid ReportId { get; set; }
    }
}
