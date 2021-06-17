using Data.Models_DanhMucKT;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IDmHttcRepository
    {
        IEnumerable<DmHttc> GetAll();
    }
    public class DmHttcRepository : IDmHttcRepository
    {
        private readonly DanhMucKTContext _context;

        public DmHttcRepository(DanhMucKTContext context)
        {
            _context = context;
        }
        public IEnumerable<DmHttc> GetAll()
        {
            return _context.TkCongNos;
            
        }
    }
}
