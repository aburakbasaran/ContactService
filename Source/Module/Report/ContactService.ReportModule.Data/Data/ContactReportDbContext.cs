using ContactService.Infrastructure.Provider.EfDbProvider.EntityFrameworkCore;
using ContactService.Infrastructure.Provider.EfDbProvider.Helpers;
using ContactService.ReportModule.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ContactService.ReportModule.Data.Data
{
    public class ContactReportDbContext : BaseDbContext, IContactReportDbContext
    {
        public ContactReportDbContext(DbContextOptions<ContactReportDbContext> dbContextOptions)
          : base(dbContextOptions)
        {

        }

        public DbSet<ReportEntity> Reports { get; set; }
        public DbSet<ReportDetailEntity> ReportDetails { get; set; }

        // dotnet ef dbcontext info --project Source/Module/Report/ContactService.ReportModule.Data
        // dotnet ef migrations list --project Source/Module/Report/ContactService.ReportModule.Data
        // dotnet ef migrations add InitialCreate --project Source/Module/Report/ContactService.ReportModule.Data --output-dir Data/Migrations
        // dotnet ef database update --project Source/Module/Report/ContactService.ReportModule.Data
        public class ContactDbContextFactory : IDesignTimeDbContextFactory<ContactReportDbContext>
        {
            public ContactReportDbContext CreateDbContext(string[] args)
            {
                return DbContextBuilderHelper.GetDbContext<ContactReportDbContext>();
            }
        }
    }
}