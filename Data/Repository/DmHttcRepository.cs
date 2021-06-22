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
        IEnumerable<ViewDmHttc> GetAll_View();
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
            return _context.DmHttcs;
            
        }

        public IEnumerable<ViewDmHttc> GetAll_View()
        {
            return _context.ViewDmHttcs;
        }
    }
}
