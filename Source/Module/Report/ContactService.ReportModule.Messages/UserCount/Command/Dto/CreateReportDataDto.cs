using Microsoft.AspNetCore.Mvc;
using System;

namespace ContactService.ReportModule.Messages.UserCount.Command.Dto
{
    public class CreateReportDataDto
    {
        [FromQuery]
        public Guid ReportId { get; set; }
    }
}
