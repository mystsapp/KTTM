using Data.Models_DanhMucKT;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IDmTkRepository
    {
        IEnumerable<DmTk> GetAll();
        IEnumerable<ViewDmTk> GetAll_View();
    }
    public class DmTkRepository : IDmTkRepository
    {
        private readonly DanhMucKTContext _context;

        public DmTkRepository(DanhMucKTContext context)
        {
            _context = context;
        }
        public IEnumerable<DmTk> GetAll()
        {
            return _context.DmTks;
            
        }

        public IEnumerable<ViewDmTk> GetAll_View()
        {
            return _context.ViewDmTks;
        }
    }
}
