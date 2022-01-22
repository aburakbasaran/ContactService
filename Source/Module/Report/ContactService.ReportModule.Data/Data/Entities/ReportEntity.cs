using System;
using ContactService.Application.Abstract;

namespace ContactService.ReportModule.Data.Data.Entities
{
    public class ReportEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsCompleted { get; set; }
        public virtual ReportDetailEntity ReportDetail { get; set; }
    }
}