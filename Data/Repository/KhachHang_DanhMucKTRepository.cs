using Data.Models_DanhMucKT;
using Data.Models_HDVATOB;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IKhachHang_DanhMucKTRepository
    {
        IEnumerable<KhachHang> GetAll();

        Task<KhachHang> GetKhachHangById(string id); // id = code

        IEnumerable<KhachHang> Find(Func<KhachHang, bool> predicate);

        IEnumerable<KhachHang> GetKhachHangsByCodeName(string code);
    }

    public class KhachHang_DanhMucKTRepository : IKhachHang_DanhMucKTRepository
    {
        private readonly DanhMucKTContext _context;

        public KhachHang_DanhMucKTRepository(DanhMucKTContext context)
        {
            _context = context;
        }

        public IEnumerable<KhachHang> Find(Func<KhachHang, bool> predicate)
        {
            return _context.KhachHangs.Where(predicate);
        }

        public IEnumerable<KhachHang> GetAll()
        {
            return _context.KhachHangs;
        }

        public async Task<KhachHang> GetKhachHangById(string id)
        {
            return await _context.KhachHangs.FindAsync(id);
        }

        public IEnumerable<KhachHang> GetKhachHangsByCodeName(string code)
        {
            return _context.KhachHangs.Where(x => x.Code.Trim().ToLower().Contains(code.Trim().ToLower()) ||
                                             (!string.IsNullOrEmpty(x.TenThuongMai) && x.TenThuongMai.Trim().ToLower().Contains(code.Trim().ToLower())) ||
                                             (!string.IsNullOrEmpty(x.TenGiaoDich) && x.TenGiaoDich.Trim().ToLower().Contains(code.Trim().ToLower())));
        }

        //public async Task<KhachHang> GetKhachHangById(string id)
        //{
        //    return await _context.KhachHangs.FindAsync(id);
        //}
    }
}