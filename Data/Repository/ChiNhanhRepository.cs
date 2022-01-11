using Data.Models_Cashier;
using Data.Models_QLTaiKhoan;
using Data.Models_QLXe;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IChiNhanhRepository
    {
        IEnumerable<Dmchinhanh> GetAll();

        Task<Dmchinhanh> GetById(int id);

        Task<Dmchinhanh> GetByMaCn(string id);

        IEnumerable<Dmchinhanh> Find(Func<Dmchinhanh, bool> predicate);
    }

    public class ChiNhanhRepository : IChiNhanhRepository
    {
        private readonly qltaikhoanContext _context;

        public ChiNhanhRepository(qltaikhoanContext context)
        {
            _context = context;
        }

        public IEnumerable<Dmchinhanh> Find(Func<Dmchinhanh, bool> predicate)
        {
            return _context.Dmchinhanhs.Where(predicate);
        }

        public IEnumerable<Dmchinhanh> GetAll()
        {
            return _context.Dmchinhanhs;
        }

        public async Task<Dmchinhanh> GetById(int id)
        {
            return await _context.Dmchinhanhs.FindAsync(id);
        }

        public async Task<Dmchinhanh> GetByMaCn(string maCn)
        {
            return await _context.Dmchinhanhs.Where(x => x.Macn == maCn).FirstOrDefaultAsync();
        }
    }
}