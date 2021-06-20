using Data.Models_DanhMucKT;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IDGiaiRepository
    {
        IEnumerable<Dgiai> GetAll();
    }
    public class DGiaiRepository : IDGiaiRepository
    {
        private readonly DanhMucKTContext _context;

        public DGiaiRepository(DanhMucKTContext context)
        {
            _context = context;
        }
        public IEnumerable<Dgiai> GetAll()
        {
            return _context.Dgiais;
            
        }
    }
}
