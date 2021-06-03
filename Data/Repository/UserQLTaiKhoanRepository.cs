using Data.Interfaces;
using Data.Models_QLTaiKhoan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IUserQLTaiKhoanRepository : IRepository<User>
    {

    }
    public class UserQLTaiKhoanRepository : Repository_QLTaiKhoan<User>, IUserQLTaiKhoanRepository
    {
        public UserQLTaiKhoanRepository(qltaikhoanContext context) : base(context)
        {
        }
    }
}
