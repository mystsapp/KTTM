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
        IEnumerable<ViewSupplier> GetAll_View_CodeName_Tax();
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
        public IEnumerable<ViewSupplier> GetAll_View_CodeName_Tax()
        {
            var listViewModels = (from s in _context.ViewSuppliers
                                  select new ViewSupplier()
                                  {
                                      Code = s.Code,
                                      TaxCode = s.TaxCode,
                                      Name = s.Name,
                                      TaxSign = s.TaxSign,
                                      TaxForm = s.TaxForm,
                                      
                                  }).ToList();
            return listViewModels;
        }
    }
}
