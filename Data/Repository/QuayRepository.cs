using Data.Models_DanhMucKT;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IQuayRepository
    {
        IEnumerable<Quay> GetAll();
        IEnumerable<ViewQuay> GetAll_View();
    }
    public class QuayRepository : IQuayRepository
    {
        private readonly DanhMucKTContext _context;

        public QuayRepository(DanhMucKTContext context)
        {
            _context = context;
        }
        public IEnumerable<Quay> GetAll()
        {
            return _context.Quays;
            
        }

        public IEnumerable<ViewQuay> GetAll_View()
        {
            return _context.ViewQuays;
        }
    }
}
