using System.Collections.Generic;
using DAL.Repository;

namespace BLL.Services.ServiceBase
{
    public abstract class MicroServiceBase<T> : IMicroServiceBase<T> 
        where T : class 
    {
        protected GenericRepository<T> Repository;

        protected MicroServiceBase(GenericRepository<T> repository)
        {
            Repository = repository;
        }

        public IEnumerable<T> GetAll()
        {
            return Repository.GetAll();
        }

        public T GetById(object id)
        {
            return Repository.GetById(id);
        }

        public void Insert(T entity)
        {
            Repository.Insert(entity);
        }

        public void Update(T entity)
        {
            Repository.Update(entity);
        }

        public void Delete(object id)
        {
            Repository.Delete(id);
        }
    }
}
