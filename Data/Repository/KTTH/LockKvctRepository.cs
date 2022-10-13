using Data.Models_KTTH;
using Data.Models_QLTaiKhoan;
using Data.Models_QLTour;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.KTTH
{
    public interface ILockKvctRepository
    {
        IEnumerable<Lockkvct> GetAll();

        Task<Lockkvct> GetById(Guid id);

        Task<Lockkvct> GetByMaCn(string id);

        IEnumerable<Lockkvct> Find(Func<Lockkvct, bool> predicate);
        Task<Lockkvct> GetLockKvct(int thang, int nam, string maCn);
    }
    public class LockKvctRepository : ILockKvctRepository
    {
        private readonly KTTHContext _context;

        public LockKvctRepository(KTTHContext context)
        {
            _context = context;
        }

        public IEnumerable<Lockkvct> Find(Func<Lockkvct, bool> predicate)
        {
            return _context.Lockkvcts.Where(predicate);
        }

        public IEnumerable<Lockkvct> GetAll()
        {
            return _context.Lockkvcts;
        }

        public async Task<Lockkvct> GetById(Guid id)
        {
            return await _context.Lockkvcts.FindAsync(id);
        }

        public async Task<Lockkvct> GetByMaCn(string maCn)
        {
            return await _context.Lockkvcts.Where(x => x.Chinhanh == maCn).FirstOrDefaultAsync();
        }

        public async Task<Lockkvct> GetLockKvct(int thang, int nam, string maCn) // ngayCt: 01/01/2022 or 01/02/2022 ...
        {
            
            return await _context.Lockkvcts.FirstOrDefaultAsync(x => x.Thangnam.Value.Month == thang && 
            x.Thangnam.Value.Year == nam && x.Chinhanh == maCn);
        }
    }
}
