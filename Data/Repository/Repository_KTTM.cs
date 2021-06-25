using Data.Interfaces;
using Data.Models_KTTM;
using Data.Models_QLTaiKhoan;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Repository_KTTM<T> : IRepository<T> where T : class
    {
        protected readonly KTTMDbContext _context;

        public Repository_KTTM(KTTMDbContext context)
        {
            _context = context;
        }
        public int Count(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).Count();
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public async Task CreateRange(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindIncludeOneAsync(Expression<Func<T, object>> expressObj, Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Include(expressObj).Where(expression).ToListAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IEnumerable<T> GetAllAsNoTracking()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<IEnumerable<T>> GetAllIncludeAsync(Expression<Func<T, object>> predicate, Expression<Func<T, object>> predicate2)
        {
            return await _context.Set<T>().Include(predicate).Include(predicate2).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllIncludeOneAsync(Expression<Func<T, object>> expression)
        {
            return await _context.Set<T>().Include(expression).ToListAsync();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetById(decimal id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetById(long id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetById(string id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetByIdAsNoTracking(Func<T, bool> predicate)
        {
            return _context.Set<T>().AsNoTracking().SingleOrDefault(predicate);
        }

        public async Task<T> GetByIdAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByLongIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T GetSingleNoTracking(Func<T, bool> predicate)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(predicate);
        }

        public async Task Save() => await _context.SaveChangesAsync();
    
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
