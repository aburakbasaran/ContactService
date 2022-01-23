using ContactService.Application.Abstract;
using ContactService.ReportModule.Data.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactService.ContactModule.Data.Data.Entities
{
    public sealed class ReportConfiguration : BaseEntityTypeConfiguration<ReportEntity>
    {
        public override void Configure(EntityTypeBuilder<ReportEntity> builder)
        {
            builder.HasKey(t => t.Id);

            base.Configure(builder);
        }
    }
}