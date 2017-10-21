using System.Collections.Generic;

namespace DAL.Repository
{
    interface IRepository<T>
    {
        void Insert(T element);

        void Delete(object id);

        void Update(T element);

        IEnumerable<T> GetAll();

        T GetById(object id);
    }
}
