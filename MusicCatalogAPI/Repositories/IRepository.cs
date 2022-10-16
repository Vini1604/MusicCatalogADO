using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public interface IRepository<T> where T:class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(Guid id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
    }
}
