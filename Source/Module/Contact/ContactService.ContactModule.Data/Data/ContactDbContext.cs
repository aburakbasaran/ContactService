using ContactService.ContactModule.Data.Data.Entities;
using ContactService.Infrastructure.Provider.EfDbProvider.EntityFrameworkCore;
using ContactService.Infrastructure.Provider.EfDbProvider.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ContactService.ContactModule.Data.Data
{
    public class ContactDbContext : BaseDbContext, IContactDbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> dbContextOptions)
          : base(dbContextOptions)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserContactEntity> UserContacts { get; set; }

        // dotnet ef dbcontext info --project Source/Module/Contact/ContactService.ContactModule.Data
        // dotnet ef migrations list --project Source/Module/Contact/ContactService.ContactModule.Data
        // dotnet ef migrations add InitialCreate --project Source/Module/Contact/ContactService.ContactModule.Data --output-dir Data/Migrations
        // dotnet ef database update --project Source/Module/Contact/ContactService.ContactModule.Data
        public class ContactDbContextFactory : IDesignTimeDbContextFactory<ContactDbContext>
        {
            public ContactDbContext CreateDbContext(string[] args)
            {
                return DbContextBuilderHelper.GetDbContext<ContactDbContext>();
            }
        }
    }
}