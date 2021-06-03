using Data.Models_QLTaiKhoan;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IApplicationUserQLTaiKhoanRepository
    {
        Task<ApplicationUser> GetByIdTwoKeyAsync(string username, string mct);
        IEnumerable<ApplicationUser> GetAll();
    }
    public class ApplicationUserQLTaiKhoanRepository : IApplicationUserQLTaiKhoanRepository
    {
        private readonly qltaikhoanContext _qltaikhoanContext;

        public ApplicationUserQLTaiKhoanRepository(qltaikhoanContext qltaikhoanContext)
        {
            _qltaikhoanContext = qltaikhoanContext;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _qltaikhoanContext.ApplicationUsers;
        }

        public async Task<ApplicationUser> GetByIdTwoKeyAsync(string username, string mct)
        {
            return await _qltaikhoanContext.ApplicationUsers.FindAsync(username, mct);
        }
    }
}
