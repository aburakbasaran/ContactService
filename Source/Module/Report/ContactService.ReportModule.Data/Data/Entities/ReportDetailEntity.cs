using ContactService.Application.Abstract;
using System;

namespace ContactService.ReportModule.Data.Data.Entities
{
    public class ReportDetailEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ReportId { get; set; }
        public string ReportJson { get; set; }
        public virtual ReportEntity Report { get; set; }

    }
}