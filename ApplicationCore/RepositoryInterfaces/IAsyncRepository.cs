using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        // CRUD
        // get by id
        Task<T> GetById(int Id);
        Task<IEnumerable<T>> GetAll();
        // get data by condition
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        // the count of certain thing based on certain condition
        Task<int> GetCount(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
    }
}
