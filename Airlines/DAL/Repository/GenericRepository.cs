using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Db;

namespace DAL.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AirlineDbContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AirlineDbContext db)
        {
            _db = db;
            _dbSet = db.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity newEntity)
        {
            _dbSet.Add(newEntity);
            _db.SaveChanges();
        }

        public virtual void Update(TEntity updatedEntity)
        {
            _db.Entry(updatedEntity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_db.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
            _db.SaveChanges();
        }
    }
}
