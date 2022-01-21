using System.Threading;
using System.Threading.Tasks;
using ContactService.ContactModule.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactService.ContactModule.Data.Data
{
    public interface IContactDbContext
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<UserContactEntity> UserContacts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}