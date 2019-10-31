using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeKeeper.DAL
{
    public class Repository<Entity> : IRepository<Entity> where Entity : class
    {
        protected TimeKeeperContext _context;
        protected DbSet<Entity> _dbSet;

        public Repository(TimeKeeperContext context)
        {
            _context = context;
            _dbSet = _context.Set<Entity>();
        }

        public virtual IQueryable<Entity> Get() => _dbSet;
        public virtual IList<Entity> Get(Func<Entity, bool> where) => Get().Where(where).ToList();
        public virtual Entity Get(int id) => _dbSet.Find(id);
        public virtual void Insert(Entity entity) => _dbSet.Add(entity);
        public virtual void Update(Entity entity, int id)
        {
            Entity old = Get(id);
            if (old != null)
                _context.Entry(old).CurrentValues.SetValues(entity);

            else
                throw new ArgumentNullException();
        }

        public void Delete(Entity entity) => _dbSet.Remove(entity);
        public void Delete(int id)
        {
            Entity entity = Get(id);
            if (entity != null)
                Delete(entity);

            else
                throw new ArgumentNullException();
        }
    }
}
