using System.Collections.Generic;
using System.Reflection;
using ContactService.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure.Provider.EfDbProvider.EntityFrameworkCore
{
    public abstract class BaseDbContext : DbContext
    {

        protected BaseDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string assemblyRoot = AssemblyHelpers.ModulesRoot;

            IEnumerable<Assembly> assemblies = AssemblyHelpers.LoadFromSearchPatterns(assemblyRoot);

            modelBuilder.Register(this, assemblies);

            RegisterConventions(modelBuilder);
        }

        protected virtual void RegisterConventions(ModelBuilder builder) { }
    }
}

