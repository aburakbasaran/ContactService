using ContactService.Application.Abstract;
using ContactService.ReportModule.Data.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactService.ContactModule.Data.Data.Entities
{
    public sealed class ReportDetailConfiguration : BaseEntityTypeConfiguration<ReportDetailEntity>
    {
        public override void Configure(EntityTypeBuilder<ReportDetailEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(c => c.Report).WithOne(x => x.ReportDetail).HasForeignKey<ReportDetailEntity>(b => b.ReportId);

            base.Configure(builder);
        }
    }
}