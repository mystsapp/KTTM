using Data.Models_DanhMucKT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IPhongBan_DanhMucKT_Repository
    {
        IEnumerable<PhongBan> GetAll();
        IEnumerable<ViewPhongBan> GetAll_View();
    }
    public class PhongBan_DanhMucKT_Repository : IPhongBan_DanhMucKT_Repository
    {
        private readonly DanhMucKTContext _context;

        public PhongBan_DanhMucKT_Repository(DanhMucKTContext context)
        {
            _context = context;
        }
        public IEnumerable<PhongBan> GetAll()
        {
            return _context.PhongBans;
        }
        public IEnumerable<ViewPhongBan> GetAll_View()
        {
            return _context.ViewPhongBans;
        }
    }
}
