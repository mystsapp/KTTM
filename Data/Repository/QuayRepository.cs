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
    }
}
