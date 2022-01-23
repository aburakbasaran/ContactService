using ContactService.ReportModule.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContactService.ReportModule.Data.Data
{
    public interface IContactReportDbContext
    {
        DbSet<ReportEntity> Reports { get; set; }
        DbSet<ReportDetailEntity> ReportDetails { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}