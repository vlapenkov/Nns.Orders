using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Nns.Orders.Domain.Entities;
using Nns.Orders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Infrastructure
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly OrderDbContext _dataContext;

        public EfRepository(OrderDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Add(T entity)
        {
            _dataContext.Set<T>().Add(entity);           
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dataContext.Set<T>().AnyAsync(predicate);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            return _dataContext.Set<T>().AsNoTracking().Where(predicate);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await _dataContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }


        public async Task<T> GetByIdAsync(long id)
        {
            return await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<T> FirstOrDefaultAsync(IQueryable<T> query)
        {
            return await query.FirstOrDefaultAsync();

        }

        public async Task<int> SaveChangesAsync()
        {
          var result =  await _dataContext.SaveChangesAsync();

            return result;
        }
    }
    }
