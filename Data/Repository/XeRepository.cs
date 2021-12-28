using Data.Models_Cashier;
using Data.Models_QLXe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IXeRepository
    {
        IEnumerable<Thuchi> GetAll();

        Task<Thuchi> GetById(int id);

        Task CreateAsync(Thuchi thuchi);

        Task UpdateAsync(Thuchi thuchi);

        IEnumerable<Thuchi> Find(Func<Thuchi, bool> predicate);
    }

    public class XeRepository : IXeRepository
    {
        private readonly quanlyxeContext _context;

        public XeRepository(quanlyxeContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Thuchi Thuchi)
        {
            await _context.Thuchis.AddAsync(Thuchi);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Thuchi> Find(Func<Thuchi, bool> predicate)
        {
            return _context.Thuchis.Where(predicate);
        }

        public IEnumerable<Thuchi> GetAll()
        {
            return _context.Thuchis;
        }

        public async Task<Thuchi> GetById(int id)
        {
            return await _context.Thuchis.FindAsync(id);
        }

        public async Task UpdateAsync(Thuchi Thuchi)
        {
            _context.Thuchis.Update(Thuchi);
            await _context.SaveChangesAsync();
        }
    }
}