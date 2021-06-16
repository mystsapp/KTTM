using Data.Models_DanhMucKT;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ITkCongNoRepository
    {
        IEnumerable<TkCongNo> GetAll();
    }
    public class TkCongNoRepository : ITkCongNoRepository
    {
        private readonly DanhMucKTContext _context;

        public TkCongNoRepository(DanhMucKTContext context)
        {
            _context = context;
        }
        public IEnumerable<TkCongNo> GetAll()
        {
            return _context.TkCongNos;
            
        }
    }
}
