using Data.Models_DanhMucKT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface INgoaiTe_DanhMucKT_Repository
    {
        IEnumerable<NgoaiTe> GetAll();

        Task<NgoaiTe> GetByIdAsync(string id);

        NgoaiTe GetById(string id);

        IEnumerable<NgoaiTe> Find(Func<NgoaiTe, bool> predicate);
    }

    public class NgoaiTe_DanhMucKT_Repository : INgoaiTe_DanhMucKT_Repository
    {
        private readonly DanhMucKTContext _danhMucKTContext;

        public NgoaiTe_DanhMucKT_Repository(DanhMucKTContext danhMucKTContext)
        {
            _danhMucKTContext = danhMucKTContext;
        }

        public IEnumerable<NgoaiTe> Find(Func<NgoaiTe, bool> predicate)
        {
            return _danhMucKTContext.NgoaiTes.Where(predicate);
        }

        public IEnumerable<NgoaiTe> GetAll()
        {
            return _danhMucKTContext.NgoaiTes;
        }

        public NgoaiTe GetById(string id)
        {
            return _danhMucKTContext.NgoaiTes.Find(id);
        }

        public async Task<NgoaiTe> GetByIdAsync(string id)
        {
            return await _danhMucKTContext.NgoaiTes.FindAsync(id);
        }
    }
}