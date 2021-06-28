using Data.Models_QLTaiKhoan;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ISupplier_QLTaiKhoan_Repository
    {
        IEnumerable<Supplier> GetAll();
        IEnumerable<Supplier> Find(Func<Supplier, bool> predicate);

    }
    public class Supplier_QLTaiKhoan_Repository : ISupplier_QLTaiKhoan_Repository
    {
        private readonly qltaikhoanContext _context;

        public Supplier_QLTaiKhoan_Repository(qltaikhoanContext context)
        {
            _context = context;
        }

        public IEnumerable<Supplier> Find(Func<Supplier, bool> predicate)
        {
            return _context.Suppliers.Where(predicate);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers;

        }
        
    }
}
