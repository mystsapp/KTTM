using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IErrorLogRepository
    {
        IEnumerable<ErrorLog> GetAll();

        Task<ErrorLog> GetById(string id);

        Task<ErrorLog> CreateAsync(ErrorLog errorLog);

        Task UpdateAsync(ErrorLog errorLog);

        IEnumerable<ErrorLog> Find(Func<ErrorLog, bool> predicate);
    }

    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly KTTMDbContext _context;

        public ErrorLogRepository(KTTMDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorLog> CreateAsync(ErrorLog errorLog)
        {
            var entityEntry = await _context.ErrorLog.AddAsync(errorLog);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public IEnumerable<ErrorLog> Find(Func<ErrorLog, bool> predicate)
        {
            return _context.ErrorLog.Where(predicate);
        }

        public IEnumerable<ErrorLog> GetAll()
        {
            return _context.ErrorLog;
        }

        public async Task<ErrorLog> GetById(string id)
        {
            return await _context.ErrorLog.FindAsync(id);
        }

        public async Task UpdateAsync(ErrorLog errorLog)
        {
            _context.ErrorLog.Update(errorLog);
            await _context.SaveChangesAsync();
        }
    }
}