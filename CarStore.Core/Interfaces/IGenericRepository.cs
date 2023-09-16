using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> List();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
