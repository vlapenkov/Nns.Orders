using Nns.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Interfaces
{
    public interface IRepository<T>
       where T : BaseEntity
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate=null);

        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(long id);

        Task Add(T entity);

        Task<int> SaveChangesAsync();

    }

}
