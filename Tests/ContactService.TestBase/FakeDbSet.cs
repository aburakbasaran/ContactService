using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ContactService.TestBase
{
    public class FakeDbSet<TEntity> : DbSet<TEntity> where TEntity : class
    {
        private readonly List<TEntity> _data;

        public FakeDbSet()
        {
            _data = new();
        }

        public FakeDbSet(IEnumerable<TEntity> data)
        {
            _data = data.ToList();
        }

        public override void AddRange(params TEntity[] entities)
        {
            _data?.AddRange(entities.ToList());
        }

        public override void AddRange(IEnumerable<TEntity> entities)
        {
            _data.AddRange(entities);
        }
    }
}