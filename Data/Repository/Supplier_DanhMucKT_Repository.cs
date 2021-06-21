using Data.Models_DanhMucKT;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ISupplier_DanhMucKT_Repository
    {
        IEnumerable<Supplier> GetAll();
        IEnumerable<ViewSupplier> GetAll_ViewSupplier();
    }
    public class Supplier_DanhMucKT_Repository : ISupplier_DanhMucKT_Repository
    {
        private readonly DanhMucKTContext _context;

        public Supplier_DanhMucKT_Repository(DanhMucKTContext context)
        {
            _context = context;
        }
        public IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers;
            
        }

        public IEnumerable<ViewSupplier> GetAll_ViewSupplier()
        {
            return _context.ViewSuppliers;
        }
    }
}
