using Data.Models_DanhMucKT;
using Data.Models_HDVATOB;
using Data.Models_KTTM;
using Data.Models_QLTour;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace KTTM.Models
{
    public class KVCTPCTViewModel
    {
        public KVPTC KVPTC { get; set; }
        public KVCTPTC KVCTPTC { get; set; }
        public IEnumerable<KVCTPTC> KVCTPTCs { get; set; }
        public IPagedList<KVCTPTC> ListThuHo { get; set; }
        public IEnumerable<NgoaiTe> Ngoaites { get; set; }
        public IEnumerable<ViewDmHttc> DmHttcs { get; set; }
        public IEnumerable<DmTk> DmTks_TkNo { get; set; }
        public IEnumerable<DmTk> DmTks_TkCo { get; set; }
        public IEnumerable<DmTk> DmTks_Cashier { get; set; }
        public IEnumerable<ViewQuay> Quays { get; set; }

        public IPagedList<KhachHang> KhachHangs_HDVATOB { get; set; }
        //public IEnumerable<VSupplierTaiKhoan> KhachHangs_HDVATOB { get; set; }
        //public IEnumerable<Data.Models_HDVATOB.Supplier> KhachHangs_HDVATOB { get; set; }

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

        public string ThueGTGT { get; set; }
    }
}