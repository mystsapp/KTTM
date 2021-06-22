using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.Models_QLTour;
using Data.Repository;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public interface IKVCTPCTService
    {
        Task<IEnumerable<KVCTPCT>> List_KVCTPCT_By_SoCT(string soCT);
        IEnumerable<Ngoaite> GetAll_NgoaiTes();
        IEnumerable<DmHttc> GetAll_DmHttc();
        IEnumerable<ViewDmHttc> GetAll_DmHttc_View();
        DmTk Get_DmTk_By_TaiKhoan(string tk);
        Task Create(KVCTPCT kVCTPCT);
        IEnumerable<DmTk> GetAll_TkCongNo_With_TenTK();
        IEnumerable<DmTk> GetAll_TaiKhoan_Except_TkConngNo();
        IEnumerable<DmTk> GetAll_DmTk();
        IEnumerable<ViewDmTk> GetAll_DmTk_View();
        IEnumerable<Dgiai> Get_DienGiai_By_TkNo_TkCo(string tkNo, string tkCo);
        IEnumerable<Quay> GetAll_Quay();
        IEnumerable<ViewQuay> GetAll_Quay_View();
        IEnumerable<Models_DanhMucKT.Supplier> GetAll_KhachHangs();
        IEnumerable<ViewSupplier> GetAll_KhachHangs_View();
        IEnumerable<ViewSupplierCode> GetAll_KhachHangs_ViewCode();
        IEnumerable<ListViewModel> GetAll_KhachHangs_View_CodeName();
        IEnumerable<MatHang> GetAll_MatHangs();
        IEnumerable<ViewMatHang> GetAll_MatHangs_View();
        IEnumerable<PhongBan> GetAll_PhongBans();
        IEnumerable<ViewPhongBan> GetAll_PhongBans_View();
    }
    public class KVCTPCTService : IKVCTPCTService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KVCTPCTService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<KVCTPCT>> List_KVCTPCT_By_SoCT(string soCT)
        {
            return await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPCT, x => x.KVPCTId == soCT);
        }

        public IEnumerable<Ngoaite> GetAll_NgoaiTes()
        {
            return _unitOfWork.ngoaiTeRepository.GetAll();
        }

        public async Task Create(KVCTPCT kVCTPCT)
        {
            _unitOfWork.kVCTPCTRepository.Create(kVCTPCT);
            await _unitOfWork.Complete();
        }

        public IEnumerable<DmHttc> GetAll_DmHttc()
        {
            return _unitOfWork.dmHttcRepository.GetAll();
        }

        public IEnumerable<ViewDmHttc> GetAll_DmHttc_View()
        {
            return _unitOfWork.dmHttcRepository.GetAll_View();
        }

        public DmTk Get_DmTk_By_TaiKhoan(string tk)
        {
            var dmTks = _unitOfWork.dmTkRepository.GetAll();
            return dmTks.Where(x => x.Tkhoan == tk).FirstOrDefault();
        }

        public IEnumerable<DmTk> GetAll_TkCongNo_With_TenTK()
        {

            return GetAll_Tk_By_TkCongNo();

        }
        public IEnumerable<DmTk> GetAll_Tk_By_TkCongNo()
        {
            var dmTks = _unitOfWork.dmTkRepository.GetAll();
            var tkCongNos = _unitOfWork.tkCongNoRepository.GetAll();
            var dmTks1 = dmTks.Where(item1 => tkCongNos.Any(item2 => item1.Tkhoan == item2.Tkhoan));// lấy những tkcongno có trong dmtk => tentk
            return dmTks1;
        }

        //public IEnumerable<ListViewModel> Convert_DmTk_To_ListViewModel(IEnumerable<DmTk> dmTks)
        //{

        //    List<ListViewModel> listTkCongNoWithTenTK = new List<ListViewModel>();
        //    foreach (var item in dmTks)
        //    {
        //        listTkCongNoWithTenTK.Add(new ListViewModel() { StringId = item.Tkhoan.Trim(), Name = item.Tkhoan + " - " + item.TenTk });
        //    }

        //    return listTkCongNoWithTenTK;
        //}
        public IEnumerable<DmTk> GetAll_TaiKhoan_Except_TkConngNo()
        {
            var dmTks = _unitOfWork.dmTkRepository.GetAll();
            var dmTks1 = GetAll_Tk_By_TkCongNo();
            // peopleList2.Except(peopleList1) c1 
            // var result = peopleList2.Where(p => !peopleList1.Any(p2 => p2.ID == p.ID)); c2
            // var result = peopleList2.Where(p => peopleList1.All(p2 => p2.ID != p.ID)); c3
            return dmTks.Except(dmTks1);

        }

        public IEnumerable<Dgiai> Get_DienGiai_By_TkNo_TkCo(string tkNo, string tkCo)
        {
            var dgiais = _unitOfWork.dGiaiRepository.GetAll();
            var dgiais1 = dgiais.Where(x => x.Tkno == tkNo && x.Tkco == tkCo);
            return dgiais1;
        }

        public IEnumerable<Quay> GetAll_Quay()
        {
            return _unitOfWork.quayRepository.GetAll();
        }

        public IEnumerable<ViewQuay> GetAll_Quay_View()
        {
            return _unitOfWork.quayRepository.GetAll_View();
        }
        public IEnumerable<ViewSupplier> GetAll_KhachHangs_View()
        {
            //var suppliers = _unitOfWork.supplier_DanhMucKT_Repository.GetAll();
            return _unitOfWork.supplier_DanhMucKT_Repository.GetAll_ViewSupplier();
            
        }
        
        public IEnumerable<Models_DanhMucKT.Supplier> GetAll_KhachHangs()
        {
            //var suppliers = _unitOfWork.supplier_DanhMucKT_Repository.GetAll();
            return _unitOfWork.supplier_DanhMucKT_Repository.GetAll();
            
        }
        
        public IEnumerable<ListViewModel> GetAll_KhachHangs_View_CodeName()
        {
            //var suppliers = _unitOfWork.supplier_DanhMucKT_Repository.GetAll();
            return _unitOfWork.supplier_DanhMucKT_Repository.GetAll_View_CodeName();
            
        }

        public IEnumerable<MatHang> GetAll_MatHangs()
        {
            return _unitOfWork.matHangRepository.GetAll();
        }
        public IEnumerable<ViewMatHang> GetAll_MatHangs_View()
        {
            return _unitOfWork.matHangRepository.GetAll_View();
        }

        public IEnumerable<PhongBan> GetAll_PhongBans()
        {
            return _unitOfWork.phongBan_DanhMucKT_Repository.GetAll();
        }
        public IEnumerable<ViewPhongBan> GetAll_PhongBans_View()
        {
            return _unitOfWork.phongBan_DanhMucKT_Repository.GetAll_View();
        }

        public IEnumerable<DmTk> GetAll_DmTk()
        {
            return _unitOfWork.dmTkRepository.GetAll();
        }
        public IEnumerable<ViewDmTk> GetAll_DmTk_View()
        {
            return _unitOfWork.dmTkRepository.GetAll_View();
        }

        public IEnumerable<ViewSupplierCode> GetAll_KhachHangs_ViewCode()
        {
            return _unitOfWork.supplier_DanhMucKT_Repository.GetAll_ViewCode();
        }
    }
}
