using Data.Models_DanhMucKT;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IMatHangRepository
    {
        IEnumerable<MatHang> GetAll();
        IEnumerable<ViewMatHang> GetAll_View();
    }
    public class MatHangRepository : IMatHangRepository
    {
        private readonly DanhMucKTContext _context;

        public MatHangRepository(DanhMucKTContext context)
        {
            _context = context;
        }
        public IEnumerable<MatHang> GetAll()
        {
            return _context.MatHangs;
            
        }

        public IEnumerable<ViewMatHang> GetAll_View()
        {
            return _context.ViewMatHangs;
        }
    }
}
