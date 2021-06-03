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
    }
    public class ApplicationUserQLTaiKhoanRepository : IApplicationUserQLTaiKhoanRepository
    {
        private readonly qltaikhoanContext _qltaikhoanContext;

        public ApplicationUserQLTaiKhoanRepository(qltaikhoanContext qltaikhoanContext)
        {
            _qltaikhoanContext = qltaikhoanContext;
        }
        public async Task<ApplicationUser> GetByIdTwoKeyAsync(string username, string mct)
        {
            return await _qltaikhoanContext.ApplicationUsers.FindAsync(username, mct);
        }
    }
}
