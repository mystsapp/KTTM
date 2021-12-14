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
    public interface ISupplier_hdvatob_Repository
    {
        IEnumerable<Supplier> GetAll();

        Task<Supplier> GetSupplierById(string id);

        IEnumerable<Supplier> Find(Func<Supplier, bool> predicate);

        IEnumerable<VSupplierTaiKhoan> GetSuppliersByCodeName(string code);
    }

    public class Supplier_hdvatob_Repository : ISupplier_hdvatob_Repository
    {
        private readonly hdvatobContext _context;

        public Supplier_hdvatob_Repository(hdvatobContext context)
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

        public async Task<Supplier> GetSupplierById(string id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public IEnumerable<VSupplierTaiKhoan> GetSuppliersByCodeName(string code)
        {
            return _context.VSupplierTaiKhoans.Where(x => x.Code.Trim().ToLower().Contains(code.Trim().ToLower()) ||
                                             (!string.IsNullOrEmpty(x.Name) && x.Name.Trim().ToLower().Contains(code.Trim().ToLower())));
        }

        //public async Task<Supplier> GetSupplierById(string id)
        //{
        //    return await _context.Suppliers.FindAsync(id);
        //}
    }
}