using System.Collections.Generic;

namespace BLL.Services.ServiceBase
{
    /// <summary>
    /// Interface for CRUD operation
    /// </summary>
    /// <typeparam name="T">Type of entity that service work with</typeparam>
    public interface IMicroServiceBase<T>
    {
        IEnumerable<T> GetAll();

        T GetById(object id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(object id);
    }
}
