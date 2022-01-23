using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure.Provider.EfDbProvider.EntityFrameworkCore
{
    public interface IModelBuilder<TContext> where TContext : BaseDbContext
    {
        void Build(ModelBuilder modelBuilder);
    }
}
