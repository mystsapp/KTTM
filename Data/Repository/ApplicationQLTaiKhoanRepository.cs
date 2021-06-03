using Data.Models_QLTaiKhoan;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IApplicationQLTaiKhoanRepository
    {
        IEnumerable<Application> GetAll();
        Task<Application> GetByIdAsync(string id);
    }
    public class ApplicationQLTaiKhoanRepository : IApplicationQLTaiKhoanRepository
    {
        private readonly qltaikhoanContext _qltaikhoanContext;

        public ApplicationQLTaiKhoanRepository(qltaikhoanContext qltaikhoanContext)
        {
            _qltaikhoanContext = qltaikhoanContext;
        }
        public IEnumerable<Application> GetAll()
        {
            return _qltaikhoanContext.Applications;
        }

        public async Task<Application> GetByIdAsync(string id)
        {
            return await _qltaikhoanContext.Applications.FindAsync(id);
        }
    }
}
