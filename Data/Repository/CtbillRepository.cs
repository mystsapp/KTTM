using Data.Models_Cashier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ICtbillRepository
    {
        IEnumerable<Ctbill> GetAll();
        Task<Ctbill> GetById(decimal id);
        IEnumerable<Ctbill> Find(Func<Ctbill, bool> predicate);
    }
    public class CtbillRepository : ICtbillRepository
    {
        private readonly qlcashierContext _context;

        public CtbillRepository(qlcashierContext context)
        {
            _context = context;
        }
        public IEnumerable<Ctbill> Find(Func<Ctbill, bool> predicate)
        {
            return _context.Ctbills.Where(predicate);
        }

        public IEnumerable<Ctbill> GetAll()
        {
            return _context.Ctbills;
        }

        public async Task<Ctbill> GetById(decimal id)
        {
            return await _context.Ctbills.FindAsync(id);
        }
    }
}
