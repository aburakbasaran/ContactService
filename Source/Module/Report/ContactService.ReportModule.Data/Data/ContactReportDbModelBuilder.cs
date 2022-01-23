using ContactService.ContactModule.Data.Data.Entities;
using ContactService.Infrastructure.Provider.EfDbProvider.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactService.ReportModule.Data.Data
{
    public sealed class ContactReportDbModelBuilder : IModelBuilder<ContactReportDbContext>
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new ReportDetailConfiguration());
        }
    }
}