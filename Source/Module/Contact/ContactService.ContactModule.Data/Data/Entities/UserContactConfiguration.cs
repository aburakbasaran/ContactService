using ContactService.Application.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactService.ContactModule.Data.Data.Entities
{
    public sealed class UserContactConfiguration : BaseEntityTypeConfiguration<UserContactEntity>
    {
        public override void Configure(EntityTypeBuilder<UserContactEntity> builder)
        {
            builder.Property(t => t.Id)
              .ValueGeneratedOnAdd();

            builder.HasKey(t => t.Id);

            builder.HasOne(c => c.User)
                .WithMany(c => c.UserContacts)
                .HasForeignKey(c => c.UserId);

            base.Configure(builder);
        }
    }
}