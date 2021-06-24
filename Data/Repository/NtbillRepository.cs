using Data.Models_Cashier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface INtbillRepository
    {
        IEnumerable<Ntbill> GetAll();
        Task<Ntbill> GetById(decimal id);
        IEnumerable<Ntbill> Find(Func<Ntbill, bool> predicate);
    }
    public class NtbillRepository : INtbillRepository
    {
        private readonly qlcashierContext _context;

        public NtbillRepository(qlcashierContext context)
        {
            _context = context;
        }
        public IEnumerable<Ntbill> Find(Func<Ntbill, bool> predicate)
        {
            return _context.Ntbills.Where(predicate);
        }

        public IEnumerable<Ntbill> GetAll()
        {
            return _context.Ntbills;
        }

        public async Task<Ntbill> GetById(decimal id)
        {
            return await _context.Ntbills.FindAsync(id);
        }
    }
}
