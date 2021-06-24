using Data.Models_Cashier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface INopTienRepository
    {
        IEnumerable<Noptien> GetAll();
        Task<Noptien> GetById(string id, string maCN);
        Task CreateAsync(Noptien noptien);
        Task UpdateAsync(Noptien noptien);
        IEnumerable<Noptien> Find(Func<Noptien, bool> predicate);
    }
    public class NopTienRepository : INopTienRepository
    {
        private readonly qlcashierContext _context;

        public NopTienRepository(qlcashierContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Noptien noptien)
        {
            await _context.Noptiens.AddAsync(noptien);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Noptien> Find(Func<Noptien, bool> predicate)
        {
            return _context.Noptiens.Where(predicate);
        }

        public IEnumerable<Noptien> GetAll()
        {
            return _context.Noptiens;
        }

        public async Task<Noptien> GetById(string id, string maCN)
        {
            return await _context.Noptiens.FindAsync(id, maCN);
        }

        public async Task UpdateAsync(Noptien noptien)
        {
            _context.Noptiens.Update(noptien);
            await _context.SaveChangesAsync();
        }
    }
}
