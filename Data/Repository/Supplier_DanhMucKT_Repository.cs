using Data.Models_DanhMucKT;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ISupplier_DanhMucKT_Repository
    {
        IEnumerable<Supplier> GetAll();
        IEnumerable<ViewSupplier> GetAll_ViewSupplier();
        IEnumerable<ViewSupplierCode> GetAll_ViewCode();
        IEnumerable<ListViewModel> GetAll_View_CodeName();
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
        public IEnumerable<ViewSupplierCode> GetAll_ViewCode()
        {
            return _context.ViewSupplierCodes;

        }

        public IEnumerable<ViewSupplier> GetAll_ViewSupplier()
        {
            return _context.ViewSuppliers;
        }
        public IEnumerable<ListViewModel> GetAll_View_CodeName()
        {
            var listViewModels = (from s in _context.ViewSuppliers
                                 select new ListViewModel() { Code = s.Code, Name = s.Name } ).ToList();
            return listViewModels;
        }
    }
}
