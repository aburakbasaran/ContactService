using ContactService.ContactModule.Data.Data.Entities;
using ContactService.Infrastructure.Provider.EfDbProvider.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactService.ContactModule.Data.Data
{
    public sealed class ContactDbModelBuilder : IModelBuilder<ContactDbContext>
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserContactConfiguration());
        }
    }
}