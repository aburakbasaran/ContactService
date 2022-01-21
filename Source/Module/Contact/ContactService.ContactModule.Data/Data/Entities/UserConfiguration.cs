using ContactService.Application.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactService.ContactModule.Data.Data.Entities
{
    public sealed class UserConfiguration : BaseEntityTypeConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(t => t.Id)
              .ValueGeneratedOnAdd();

            builder.HasKey(t => t.Id);

            base.Configure(builder);
        }
    }
}