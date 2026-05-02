using KursPortal.DataAccess.Abstract;
using KursPortal.DataAccess.Context;
using KursPortal.Entity.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DataAccess.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task<int> CountAsync()
        {
            return await Table.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await Table.CountAsync(expression);
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await Table.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression)
        {
            return await Table.Where(expression).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }
    }
}
