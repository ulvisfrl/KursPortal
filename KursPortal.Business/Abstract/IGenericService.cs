using KursPortal.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Abstract
{
    public interface IGenericService<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
    }
}
