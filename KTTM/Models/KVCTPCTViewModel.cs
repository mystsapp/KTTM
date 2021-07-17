using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.Models_QLTour;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class KVCTPCTViewModel
    {
        public KVPCT KVPCT { get; set; }
        public KVCTPCT KVCTPCT { get; set; }
        public IEnumerable<KVCTPCT> KVCTPCTs { get; set; }
        public IEnumerable<Ngoaite> Ngoaites { get; set; }
        public IEnumerable<ViewDmHttc> DmHttcs { get; set; }        
        public IEnumerable<DmTk> DmTks_TkNo { get; set; }
        public IEnumerable<DmTk> DmTks_TkCo { get; set; }
        public IEnumerable<DmTk> DmTks_Cashier { get; set; }
        public IEnumerable<ViewQuay> Quays { get; set; }
        //public IEnumerable<DmTk> GetAll_TkCongNo_With_TenTK { get; set; }
        //public IEnumerable<DmTk> GetAll_TaiKhoan_Except_TkConngNo { get; set; }
        //public IEnumerable<Data.Models_DanhMucKT.ViewSupplierCode> KhachHangs { get; set; }
        public IEnumerable<Data.Models_HDVATOB.Supplier> KhachHangs_HDVATOB { get; set; }
        //public IEnumerable<Data.Models_DanhMucKT.ViewSupplierCode> KhachHang_Edits { get; set; }
        //public IEnumerable<Data.Models_DanhMucKT.Supplier> KhachHang_Edit_Nos { get; set; }
        public IEnumerable<ViewMatHang> MatHangs { get; set; }
        public IEnumerable<ViewPhongBan> PhongBans { get; set; }
        public LayDataCashierModel LayDataCashierModel { get; set; }
        public string StrUrl { get; set; }
        public int Page { get; set; }
        public string MaKhText { get; set; }
        public string TenTkNo { get; set; }
        public string TenTkCo { get; set; }
        public IEnumerable<Dgiai> Dgiais { get; set; }
        public IEnumerable<ListViewModel> LoaiHDGocs { get; set; }

    }
}
