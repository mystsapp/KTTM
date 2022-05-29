using Data.Models_Cashier;
using Data.Models_DanhMucKT;
using Data.Models_HDVATOB;
using Data.Models_KTTM;
using Data.Models_QLTour;
using Data.Models_QLXe;
using Data.Repository;
using Data.ViewModels;
using KTTM.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace KTTM.Services
{
    public interface IKVCTPTCService
    {
        Task<IEnumerable<KVCTPTC>> List_KVCTPCT_By_KVPTCid(Guid KVPTCid);

        Task<List<KVCTPTC>> List_KVCTPCT_By_SoCT(string soCT, string maCn);

        IEnumerable<Ngoaite> GetAll_NgoaiTes();

        IEnumerable<DmHttc> GetAll_DmHttc();

        IEnumerable<ViewDmHttc> GetAll_DmHttc_View();

        DmTk Get_DmTk_By_TaiKhoan(string tk);

        Task Create(KVCTPTC kVCTPCT);

        IEnumerable<DmTk> GetAll_TkCongNo_With_TenTK();

        IEnumerable<DmTk> GetAll_TaiKhoan_Except_TkConngNo();

        IEnumerable<DmTk> GetAll_DmTk();

        IEnumerable<DmTk> GetAll_DmTk_TaiKhoan();

        IEnumerable<ViewDmTk> GetAll_DmTk_View();

        IEnumerable<Dgiai> Get_DienGiai_By_TkNo_TkCo(string tkNo, string tkCo);

        IEnumerable<Quay> GetAll_Quay();

        IEnumerable<ViewQuay> GetAll_Quay_View();

        IEnumerable<MatHang> GetAll_MatHangs();

        IEnumerable<ViewMatHang> GetAll_MatHangs_View();

        IEnumerable<PhongBan> GetAll_PhongBans();

        IEnumerable<ViewPhongBan> GetAll_PhongBans_View();

        IEnumerable<Dgiai> Get_DienGiai_By_TkNo(string tkNo);

        IEnumerable<KVCTPTC> GetKVCTPTCs(string baoCaoSo, Guid kVPTCId, string soCT, string username, string maCN, string loaiPhieu, string tk, bool tienMat, bool tTThe); // noptien => two keys
        IEnumerable<KVCTPTC> GetKVCTPTCs_VCBChau(string baoCaoSo, Guid kVPTCId, string soCT, string username, string maCN, string loaiPhieu, string tk, bool tienMat, bool tTThe); // noptien => two keys

        Task CreateRange(IEnumerable<KVCTPTC> kVCTPTCs);

        IEnumerable<DmTk> GetAll_DmTk_Cashier(); IEnumerable<DmTk> GetAll_DmTk_TienMat();

        IEnumerable<KVCTPTC> GetAll();

        Task<KVCTPTC> GetById(long id);

        IEnumerable<Data.Models_HDVATOB.Supplier> GetAll_KhachHangs_HDVATOB();

        IEnumerable<KhachHang> GetSuppliersByCode(string code);

        //IEnumerable<Data.Models_HDVATOB.Supplier> GetSuppliersByCodeName(string code, string maCn);
        IEnumerable<VSupplierTaiKhoan> GetSuppliersByCodeName(string code, string maCn);

        IEnumerable<Dgiai> GetAll_DienGiai();

        IEnumerable<NgoaiTe> GetAll_NgoaiTes_DanhMucKT();

        KVCTPTC GetBySoCTAsNoTracking(long id);

        Task UpdateAsync(KVCTPTC kVCTPCT);

        Task UpdateAsync_NopTien(Noptien noptien);
        IEnumerable<DmTk> Get1411();
        Task DeleteAsync(KVCTPTC kVCTPCT);
        IEnumerable<DmTk> Get1412();
        Task DeleteRangeAsync(IEnumerable<KVCTPTC> kVCTPCTs);
        IEnumerable<DmTk> Get1111000000();
        IEnumerable<ListViewModel> LoaiHDGocs();

        string AutoSgtcode(string param);

        Task<KVCTPTC> FindByIdInclude(long kVCTPCTId_PhieuTC);

        Task<IEnumerable<KVCTPTC>> FinByDate(string searchFromDate, string searchToDate, string maCn);

        Task<IEnumerable<KVCTPTC>> FinBy_TonQuy_Date(string searchFromDate, string searchToDate, string maCn, string loaiTien = "");

        List<KVCTPCT_Model_GroupBy_SoCT> KVCTPTC_Model_GroupBy_SoCTs(IEnumerable<KVCTPTC> kVCTPTCs);

        //List<KVCTPCT_Model_GroupBy_LoaiTien> TonQuy_Model_GroupBy_NgayCTs(IEnumerable<TonQuy> tonQuies);
        List<KVCTPTC_NT_GroupBy_SoCTs> KVCTPTC_NT_GroupBy_SoCTs(IEnumerable<KVCTPTC> kVCTPTCs);

        Task<List<KVCTPTC>> FinBy_SoCT(string soCT, string maCn);

        Task<Data.Models_HDVATOB.Supplier> GetSupplierSingleByCode(string maKh);

        IPagedList<KhachHang> GetSuppliersByCodeName_PagedList(string code, string maCn, int? page);

        Ntbill GetNtbillBySTT(string sTT);

        Task<Noptien> GetNopTienById(string soct, string macn);

        IEnumerable<Thuchi> GetThuChiXe_By_SoPhieu(string soPhieu);

        IEnumerable<Thuchi> GetThuChiXe_By_SoCT_KTTM(string soCTKTTM);

        Task UpdateAsync_ThuChiXe(Thuchi thuchi);

        Task<IEnumerable<KVCTPTC>> GetKVCTPTCs_QLXe(string soPhieu, Guid kVPTCId, string soCT, string username, string macn);

        List<KVCTPTC> FindByMaCN(string maCn);

        Task<IPagedList<KVCTPTC>> ListThuHo(string searchString, string searchFromDate, string searchToDate, int? page, string macn);

        Task<IEnumerable<KVCTPTC>> ExportThuHo(string searchString, string searchFromDate, string searchToDate, string macn);
        Task<KVCTPTC> GetByIdIncludeKVPTC(long id);

        //Task<KhachHang> GetKhachHangById(string maKhNo);
    }

    public class KVCTPTCService : IKVCTPTCService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public KVCTPTCService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<KVCTPTC>> List_KVCTPCT_By_KVPTCid(Guid KVPTCid)
        {
            return await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, x => x.KVPTCId == KVPTCid);
        }

        public async Task<List<KVCTPTC>> List_KVCTPCT_By_SoCT(string soCT, string maCn)
        {
            var kVCTPTCs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, x => x.SoCT == soCT && x.MaCn == maCn);
            return kVCTPTCs.ToList();
        }

        public IEnumerable<Ngoaite> GetAll_NgoaiTes()
        {
            return _unitOfWork.ngoaiTeRepository.GetAll();
        }

        public async Task Create(KVCTPTC kVCTPCT)
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
            //tkNo ??= "";
            //tkCo ??= "";

            //var dgiais = _unitOfWork.dGiaiRepository.Find(x => x.Tkno == tkNo).ToList();

            //var dgiais1 = dgiais.Where(x => x.Tkno.Trim() == tkNo.Trim() && x.Tkco.Trim() == tkCo.Trim());
            //dgiais1 ??= null;
            //return dgiais1;

            // var dgiais = _unitOfWork.dGiaiRepository.GetAll().ToList();
            tkNo ??= "";
            tkCo ??= "";

            var dgiais1 = _unitOfWork.dGiaiRepository.GetAll().Where(x => x.Tkno.Trim() == tkNo.Trim() && x.Tkco.Trim() == tkCo.Trim());

            //var dgiais = _unitOfWork.dGiaiRepository.GetAll();
            //dgiais.Where(x => x.Tkno)
            dgiais1 ??= null;
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

        public IEnumerable<Dgiai> Get_DienGiai_By_TkNo(string tkNo)
        {
            var dgiais = _unitOfWork.dGiaiRepository.GetAll();
            var dgiais1 = dgiais.Where(x => x.Tkno.Trim() == tkNo.Trim());
            return dgiais1;
        }

        public IEnumerable<KVCTPTC> GetKVCTPTCs(string baoCaoSo, Guid kVPTCId,
            string soCT, string username, string maCN, string loaiPhieu,
            string tk, bool tienMat, bool tTThe) // noptien => two keys
        {
            var ntbills = _unitOfWork.ntbillRepository.Find(x => x.Soct == baoCaoSo && x.Chinhanh == maCN);

            string nguoiTao = username;
            DateTime ngayTao = DateTime.Now;

            // ghi log
            string logFile = "-User kéo từ cashier: " + username + " , báo cáo số " + baoCaoSo + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            List<KVCTPTC> kVCTPTCs = new List<KVCTPTC>();

            if (ntbills.Count() > 0)
            {
                foreach (var item in ntbills)
                {
                    string boPhan = item.Bophan;

                    string maKh = string.IsNullOrEmpty(item.Coquan) ? "50000" : item.Coquan;
                    var viewSupplier = GetSuppliersByCodeName(maKh, maCN).FirstOrDefault();//.Where(x => x.Code == maKh).FirstOrDefault();
                    //var viewSupplier = GetAll_KhachHangs_HDVATOB().Where(x => x.Code == maKh).FirstOrDefault();
                    string kyHieu = "", mauSo = "", msThue = "", tenKh = "", diaChi = "";
                    if (viewSupplier != null)
                    {
                        kyHieu = viewSupplier.Taxsign;
                        mauSo = viewSupplier.Taxform;
                        msThue = viewSupplier.Taxcode;
                        tenKh = viewSupplier.Name;
                        diaChi = viewSupplier.Address;
                    }
                    var ctbills = _unitOfWork.ctbillRepository.Find(x => x.Idntbill == item.Idntbill);
                    var ctbills_TienMat = ctbills.Where(x => string.IsNullOrEmpty(x.Cardnumber) && string.IsNullOrEmpty(x.Loaicard));
                    var ctbills_TTThe = ctbills.Except(ctbills_TienMat);

                    string dienGiaiP = loaiPhieu == "T" ? "THU BILL " + (item.Bill ?? item.Stt) : "CHI BILL " + (item.Bill ?? item.Stt); // ??
                    var loaiHDGoc = "VAT";// item.Loaihd; // ??
                    var soCTGoc = item.Bill ?? item.Stt;// item.Stt; // thao
                    var ngayBill = item.Ngaybill;

                    KVCTPTC kVCTPTC = new KVCTPTC();
                    kVCTPTC.STT = item.Stt; // ben [ntbill] cashier

                    if (tienMat) // if (maCN == "STN") // thao
                    {
                        if (ctbills_TienMat.Count() > 0)
                        {
                            // THONG TIN VE TAI CHINH
                            kVCTPTC.KVPTCId = kVPTCId;
                            kVCTPTC.SoCT = soCT;
                            kVCTPTC.MaCn = maCN;
                            kVCTPTC.DienGiaiP = dienGiaiP;
                            kVCTPTC.SoTienNT = ctbills_TienMat.Sum(x => x.Sotiennt);// item1.Sotiennt;
                            kVCTPTC.LoaiTien = ctbills_TienMat.FirstOrDefault().Loaitien;// item1.Loaitien;
                            kVCTPTC.TyGia = ctbills_TienMat.FirstOrDefault().Tygia;// item1.Tygia;
                            kVCTPTC.SoTien = ctbills_TienMat.Sum(x => x.Sotien);// item1.Sotien;
                                                                                //kVCTPTC.CardNumber = ctbills_TienMat.FirstOrDefault().Cardnumber;// item1.Cardnumber;
                                                                                //kVCTPTC.LoaiThe = ctbills_TienMat.FirstOrDefault().Loaicard;// item1.Loaicard;
                            kVCTPTC.BoPhan = boPhan;
                            // THONG TIN VE CONG NO DOAN
                            if (loaiPhieu == "T") // phieu thu
                            {
                                var dienGiai = Get_DienGiai_By_TkNo_TkCo("1111000000", tk).FirstOrDefault(); // chac chan tien mat : 1111000000
                                kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                                kVCTPTC.TKNo = "1111000000";
                                kVCTPTC.TKCo = tk;

                                kVCTPTC.CoQuay = boPhan;

                                switch (baoCaoSo.Substring(5, 3)) // STS
                                {
                                    //case "CHK":
                                    //    kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                    //    break;

                                    case "TWI":
                                        kVCTPTC.MaKhCo = "0000000006"; //maKh;
                                        kVCTPTC.CoQuay = "TF";
                                        kVCTPTC.BoPhan = "TF";
                                        break;

                                    case "TND":
                                        kVCTPTC.MaKhCo = "0310891532051"; //maKh;
                                        kVCTPTC.TKCo = "1368000000";
                                        break;

                                    case "TOB":
                                        kVCTPTC.MaKhCo = "0000000002"; //maKh;
                                        break;

                                    case "TXE":
                                        kVCTPTC.CoQuay = "XE";
                                        kVCTPTC.BoPhan = "XE";
                                        //kVCTPTC.MaKhCo = "0000000001"; //maKh;
                                        break;

                                    case "TIB":
                                        kVCTPTC.MaKhCo = "0000000005"; //maKh;
                                        kVCTPTC.TKCo = "1311120000";
                                        kVCTPTC.CoQuay = "IB";
                                        kVCTPTC.BoPhan = "IB";
                                        break;

                                    //// them
                                    //case "BHK":
                                    //    kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                    //    break;

                                    //case "PHK":
                                    //    kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                    //    break;

                                    case "VHK":
                                        kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                        break;

                                    case "BIB":
                                        kVCTPTC.MaKhCo = "0000000005"; //maKh;
                                        kVCTPTC.TKCo = "1311120000";
                                        kVCTPTC.CoQuay = "IB";
                                        kVCTPTC.BoPhan = "IB";
                                        break;

                                    case "PIB":
                                        kVCTPTC.MaKhCo = "0000000005"; //maKh;
                                        kVCTPTC.TKCo = "1311120000";
                                        kVCTPTC.CoQuay = "IB";
                                        kVCTPTC.BoPhan = "IB";
                                        break;

                                    case "PWI":
                                        kVCTPTC.MaKhCo = "0000000006"; //maKh;
                                        kVCTPTC.CoQuay = "TF";
                                        kVCTPTC.BoPhan = "TF";
                                        break;

                                    case "BWI":
                                        kVCTPTC.MaKhCo = "0000000006"; //maKh;
                                        kVCTPTC.CoQuay = "TF";
                                        kVCTPTC.BoPhan = "TF";
                                        break;

                                    case "BND":
                                    case "BDN":
                                        kVCTPTC.MaKhCo = "0310891532051"; //maKh;
                                        kVCTPTC.TKCo = "1368000000";
                                        break;

                                    case "PND":
                                        kVCTPTC.MaKhCo = "0310891532051"; //maKh;
                                        kVCTPTC.TKCo = "1368000000";
                                        break;

                                    case "BOB":
                                        kVCTPTC.MaKhCo = "0000000002"; //maKh;
                                        break;

                                    case "BDO":
                                        kVCTPTC.MaKhCo = "0000000007"; //maKh;
                                        kVCTPTC.CoQuay = "OB";
                                        kVCTPTC.BoPhan = "OB";
                                        break;

                                    case "POB":
                                        kVCTPTC.MaKhCo = "0000000002"; //maKh;
                                        break;

                                    case "BXE":
                                        kVCTPTC.MaKhCo = "0000000001"; //maKh;
                                        kVCTPTC.CoQuay = "XE";
                                        kVCTPTC.BoPhan = "XE";
                                        break;

                                    case "PXE":
                                        kVCTPTC.CoQuay = "XE";
                                        kVCTPTC.BoPhan = "XE";
                                        kVCTPTC.MaKhCo = "0000000001"; //maKh;
                                        break;

                                    case "BXK": // xuat khau: thao
                                        kVCTPTC.CoQuay = "XK";
                                        kVCTPTC.BoPhan = "XK";
                                        //switch (kVCTPTC.Sgtcode)
                                        switch (ctbills_TienMat.FirstOrDefault().Sgtcode.Substring(0, 6))
                                        {
                                            case "0COSTA":
                                                kVCTPTC.MaKhCo = "0000000015";
                                                break;

                                            case "0STARS":
                                                kVCTPTC.MaKhCo = "0000000016";
                                                break;

                                            case "0SPGLO":
                                                kVCTPTC.MaKhCo = "0000000017";
                                                break;

                                            case "0MIEJP":
                                                kVCTPTC.MaKhCo = "0000000009";
                                                break;

                                            case "0STUOS":
                                                kVCTPTC.MaKhCo = "0000000013";
                                                break;

                                        }
                                        break;

                                    default:
                                        kVCTPTC.MaKhCo = maKh; //maKh;
                                        break;
                                }

                                //kVCTPTC.MaKhCo = maKh; //maKh;
                                if (tk == "1368000000")
                                {
                                    kVCTPTC.MaKhCo = "0310891532051"; //maKh;
                                }

                                if (maCN == "STN") // thao
                                {
                                    switch (baoCaoSo.Substring(5, 3))
                                    {
                                        case "BND":
                                        case "BDN":
                                        case "TND":
                                        case "PND":
                                            kVCTPTC.TKCo = "1311110000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0000000003";
                                            kVCTPTC.BoPhan = "ND";
                                            kVCTPTC.CoQuay = "ND";
                                            break;

                                        case "HND":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000003";
                                            kVCTPTC.MaKhCo = "";
                                            kVCTPTC.BoPhan = "ND";
                                            kVCTPTC.NoQuay = "ND";
                                            break;

                                        case "BOB":
                                        case "TOB":
                                        case "POB":
                                        case "CHK":
                                        case "BHK":
                                        case "PHK":
                                        case "VHK":
                                        case "BDO":
                                        case "BXK":
                                            kVCTPTC.TKCo = "1368000000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0310891532";
                                            kVCTPTC.BoPhan = "ND";
                                            kVCTPTC.CoQuay = "";
                                            kVCTPTC.NoQuay = "";
                                            break;

                                        case "HOB":
                                        case "HHK":
                                            kVCTPTC.TKNo = "1368000000";
                                            kVCTPTC.MaKhCo = "";
                                            kVCTPTC.MaKhNo = "0310891532";
                                            kVCTPTC.BoPhan = "ND";
                                            kVCTPTC.CoQuay = "";
                                            kVCTPTC.NoQuay = "";
                                            break;
                                    }
                                }
                                if (maCN != "STS" && maCN != "STN") // Tram STD
                                {
                                    switch (baoCaoSo.Substring(5, 3))
                                    {
                                        case "BND":
                                        case "TND":
                                        case "PND":
                                            kVCTPTC.TKCo = "1311110000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0000000003";
                                            break;

                                        case "HND":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000003";
                                            kVCTPTC.MaKhCo = "";
                                            break;

                                        case "BOB":
                                        case "TOB":
                                        case "POB":
                                            kVCTPTC.TKCo = "1311110000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0000000002";
                                            break;

                                        case "CHK":
                                        case "BHK":
                                        case "PHK":
                                        case "VHK":
                                            kVCTPTC.TKCo = "1311110000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0000000004";
                                            break;

                                        case "HOB":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000002";
                                            kVCTPTC.MaKhCo = "";
                                            break;

                                        case "HHK":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000004";
                                            kVCTPTC.MaKhCo = "";
                                            break;

                                        case "BDN":
                                            kVCTPTC.TKCo = "1311110000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0000000008";
                                            break;

                                    }
                                }
                                //// anh son kt
                                //if (maKh == "0000000003") // makh
                                //{
                                //    kVCTPTC.TKCo = "1311110000";
                                //}
                            }
                            else // phieu chi
                            {
                                var dienGiai = Get_DienGiai_By_TkNo_TkCo(tk, "1111000000").FirstOrDefault();
                                kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                                kVCTPTC.TKNo = tk;
                                kVCTPTC.TKCo = "1111000000";
                                kVCTPTC.MaKhNo = maKh;
                                //// anh son kt
                                //if (maKh == "0000000003") // makh
                                //{
                                //    kVCTPTC.TKNo = "1311110000";
                                //}

                                kVCTPTC.NoQuay = boPhan;

                                switch (baoCaoSo.Substring(5, 3))
                                {
                                    //case "HHK":
                                    //    kVCTPTC.MaKhNo = "0000000004"; //maKh;
                                    //    break;

                                    case "HWI":
                                        kVCTPTC.MaKhNo = "0000000006"; //maKh;
                                        kVCTPTC.NoQuay = "TF";
                                        kVCTPTC.BoPhan = "TF";
                                        break;

                                    case "HND":
                                        kVCTPTC.MaKhNo = "0310891532051"; //maKh;
                                        kVCTPTC.TKNo = "1368000000";
                                        break;

                                    case "HOB":
                                        kVCTPTC.MaKhNo = "0000000002"; //maKh;
                                        break;

                                    case "HXE":
                                        kVCTPTC.NoQuay = "XE";
                                        kVCTPTC.BoPhan = "XE";
                                        //kVCTPTC.MaKhNo = "0000000001"; //maKh;
                                        break;

                                    case "HIB":
                                        kVCTPTC.MaKhNo = "0000000005"; //maKh;
                                        kVCTPTC.TKNo = "1311120000";
                                        kVCTPTC.NoQuay = "IB";
                                        kVCTPTC.BoPhan = "IB";
                                        break;


                                    case "HXK": // xuat khau: thao
                                        kVCTPTC.NoQuay = "XK";
                                        kVCTPTC.BoPhan = "XK";
                                        switch (kVCTPTC.Sgtcode)
                                        {
                                            case "0COSTA":
                                                kVCTPTC.MaKhNo = "0000000015";
                                                break;

                                            case "0STARS":
                                                kVCTPTC.MaKhNo = "0000000016";
                                                break;

                                            case "0SPGLO":
                                                kVCTPTC.MaKhNo = "0000000017";
                                                break;

                                            case "0MIEJP":
                                                kVCTPTC.MaKhNo = "0000000009";
                                                break;

                                            case "0STUOS":
                                                kVCTPTC.MaKhNo = "0000000013";
                                                break;

                                        }
                                        break;

                                }

                                if (maCN == "STN") // thao
                                {
                                    switch (baoCaoSo.Substring(5, 3))
                                    {
                                        case "HND":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000003";
                                            kVCTPTC.MaKhCo = "";
                                            kVCTPTC.BoPhan = "ND";
                                            kVCTPTC.NoQuay = "ND";
                                            break;

                                        case "HOB":
                                        case "HHK":
                                            kVCTPTC.TKNo = "1368000000";
                                            kVCTPTC.MaKhCo = "";
                                            kVCTPTC.MaKhNo = "0310891532";
                                            kVCTPTC.BoPhan = "ND";
                                            kVCTPTC.CoQuay = "";
                                            kVCTPTC.NoQuay = "";
                                            break;
                                    }
                                }
                                if (maCN != "STS" && maCN != "STN") // Tram STD
                                {
                                    switch (baoCaoSo.Substring(5, 3))
                                    {
                                        case "HND":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000003";
                                            kVCTPTC.MaKhCo = "";
                                            break;

                                        case "HOB":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000002";
                                            kVCTPTC.MaKhCo = "";
                                            break;

                                        case "HHK":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000004";
                                            kVCTPTC.MaKhCo = "";
                                            break;
                                    }
                                }
                            }
                            //
                            var sgtcode = ctbills_TienMat.FirstOrDefault(x => !string.IsNullOrEmpty(x.Sgtcode));
                            kVCTPTC.Sgtcode = sgtcode == null ? "" : sgtcode.Sgtcode;// item1.Sgtcode;
                                                                                     //kVCTPTC.CardNumber = item1.Cardnumber;
                            kVCTPTC.SalesSlip = ctbills_TienMat.FirstOrDefault().Saleslip;// item1.Saleslip;

                            // THONG TIN VE THUE
                            kVCTPTC.LoaiHDGoc = loaiHDGoc;
                            kVCTPTC.SoCTGoc = soCTGoc;
                            kVCTPTC.NgayCTGoc = ngayBill;

                            kVCTPTC.DSKhongVAT = 0;
                            kVCTPTC.VAT = 0;

                            kVCTPTC.KyHieu = kyHieu;
                            kVCTPTC.MauSoHD = mauSo;
                            kVCTPTC.MsThue = msThue;
                            kVCTPTC.MaKh = maKh;

                            kVCTPTC.TenKH = tenKh;
                            kVCTPTC.DiaChi = diaChi;

                            kVCTPTC.NguoiTao = nguoiTao;
                            kVCTPTC.NgayTao = ngayTao;
                            kVCTPTC.LogFile = logFile;

                            kVCTPTCs.Add(kVCTPTC);

                            #region ctbill tienmat old

                            //foreach (var item1 in ctbills_TienMat)
                            //{
                            //    //KVCTPTC kVCTPTC = new KVCTPTC();

                            //    //// THONG TIN VE TAI CHINH
                            //    //kVCTPTC.KVPTCId = kVPTCId;
                            //    //kVCTPTC.SoCT = soCT;
                            //    //kVCTPTC.MaCn = maCN;
                            //    //kVCTPTC.DienGiaiP = dienGiaiP;
                            //    //kVCTPTC.SoTienNT = item1.Sotiennt;
                            //    //kVCTPTC.LoaiTien = item1.Loaitien;
                            //    //kVCTPTC.TyGia = item1.Tygia;
                            //    //kVCTPTC.SoTien = item1.Sotien;
                            //    //kVCTPTC.CardNumber = item1.Cardnumber;
                            //    //kVCTPTC.LoaiThe = item1.Loaicard;

                            //    //// THONG TIN VE CONG NO DOAN
                            //    //if (loaiPhieu == "T") // phieu thu
                            //    //{
                            //    //    var dienGiai = Get_DienGiai_By_TkNo_TkCo("1111000000", tk).FirstOrDefault(); // chac chan tien mat : 1111000000
                            //    //    kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                            //    //    kVCTPTC.TKNo = "1111000000";
                            //    //    kVCTPTC.TKCo = tk;

                            //    //    kVCTPTC.CoQuay = boPhan;

                            //    //    switch (baoCaoSo.Substring(5, 3))
                            //    //    {
                            //    //        case "CHK":
                            //    //            kVCTPTC.MaKhCo = "KLHK"; //maKh;
                            //    //            break;

                            //    //        case "TWI":
                            //    //            kVCTPTC.MaKhCo = "KLWI"; //maKh;
                            //    //            break;

                            //    //        case "TND":
                            //    //            kVCTPTC.MaKhCo = "STNCN"; //maKh;
                            //    //            break;

                            //    //        case "TOB":
                            //    //            kVCTPTC.MaKhCo = "VEKLO"; //maKh;
                            //    //            break;

                            //    //        case "TXE":
                            //    //            kVCTPTC.MaKhCo = "TX001"; //maKh;
                            //    //            break;

                            //    //        case "TIB":
                            //    //            kVCTPTC.MaKhCo = "KLIB"; //maKh;
                            //    //            break;
                            //    //    }

                            //    //    kVCTPTC.MaKhCo = maKh; //maKh;
                            //    //    if (tk == "1368000000")
                            //    //    {
                            //    //        kVCTPTC.MaKhCo = "STNCN"; //maKh;
                            //    //    }
                            //    //}
                            //    //else // phieu chi
                            //    //{
                            //    //    var dienGiai = Get_DienGiai_By_TkNo_TkCo(tk, "1111000000").FirstOrDefault();
                            //    //    kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                            //    //    kVCTPTC.TKNo = tk;
                            //    //    kVCTPTC.TKCo = "1111000000";
                            //    //    kVCTPTC.MaKhNo = maKh;
                            //    //    kVCTPTC.NoQuay = boPhan;

                            //    //    switch (baoCaoSo.Substring(5, 3))
                            //    //    {
                            //    //        case "HHK":
                            //    //            kVCTPTC.MaKhNo = "KLHK"; //maKh;
                            //    //            break;

                            //    //        case "HWI":
                            //    //            kVCTPTC.MaKhNo = "KLWI"; //maKh;
                            //    //            break;

                            //    //        case "HND":
                            //    //            kVCTPTC.MaKhNo = "STNCN"; //maKh;
                            //    //            break;

                            //    //        case "HOB":
                            //    //            kVCTPTC.MaKhNo = "VEKLO"; //maKh;
                            //    //            break;

                            //    //        case "HXE":
                            //    //            kVCTPTC.MaKhNo = "TX001"; //maKh;
                            //    //            break;

                            //    //        case "HIB":
                            //    //            kVCTPTC.MaKhNo = "KLIB"; //maKh;
                            //    //            break;
                            //    //    }
                            //    //}

                            //    //kVCTPTC.BoPhan = boPhan;
                            //    //kVCTPTC.Sgtcode = item1.Sgtcode;
                            //    //kVCTPTC.CardNumber = item1.Cardnumber;
                            //    //kVCTPTC.SalesSlip = item1.Saleslip;

                            //    //// THONG TIN VE THUE
                            //    //kVCTPTC.LoaiHDGoc = loaiHDGoc;
                            //    //kVCTPTC.SoCTGoc = soCTGoc;
                            //    //kVCTPTC.NgayCTGoc = ngayBill;

                            //    //kVCTPTC.DSKhongVAT = 0;
                            //    //kVCTPTC.VAT = 0;

                            //    //kVCTPTC.KyHieu = kyHieu;
                            //    //kVCTPTC.MauSoHD = mauSo;
                            //    //kVCTPTC.MsThue = msThue;
                            //    //kVCTPTC.MaKh = maKh;
                            //    //kVCTPTC.TenKH = tenKh;
                            //    //kVCTPTC.DiaChi = diaChi;

                            //    //kVCTPTC.NguoiTao = nguoiTao;
                            //    //kVCTPTC.NgayTao = ngayTao;
                            //    //kVCTPTC.LogFile = logFile;

                            //    //kVCTPTCs.Add(kVCTPTC);
                            //}

                            #endregion ctbill tienmat old
                        }
                    }

                    if (tTThe)
                    {
                        if (ctbills_TTThe.Count() > 0)
                        {
                            //KVCTPTC kVCTPTC = new KVCTPTC();

                            // THONG TIN VE TAI CHINH
                            kVCTPTC.KVPTCId = kVPTCId;
                            kVCTPTC.SoCT = soCT;
                            kVCTPTC.MaCn = maCN;
                            kVCTPTC.DienGiaiP = dienGiaiP;
                            kVCTPTC.SoTienNT = ctbills_TTThe.Sum(x => x.Sotiennt);// item1.Sotiennt;
                            kVCTPTC.LoaiTien = ctbills_TTThe.FirstOrDefault().Loaitien;// item1.Loaitien;
                            kVCTPTC.TyGia = ctbills_TTThe.FirstOrDefault().Tygia;// item1.Tygia;
                            kVCTPTC.SoTien = ctbills_TTThe.Sum(x => x.Sotien);// item1.Sotien;
                            kVCTPTC.CardNumber = ctbills_TTThe.FirstOrDefault().Cardnumber;// item1.Cardnumber;
                            kVCTPTC.LoaiThe = ctbills_TTThe.FirstOrDefault().Loaicard;// item1.Loaicard;

                            // vcb(chau)
                            //_httpContextAccessor.HttpContext.Session.SetString("soTienZin", )

                            // THONG TIN VE CONG NO DOAN
                            if (loaiPhieu == "T") // phieu thu
                            {
                                var dienGiai = Get_DienGiai_By_TkNo_TkCo("1111000000", tk).FirstOrDefault(); // chac chan tien mat : 1111000000
                                kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                                kVCTPTC.TKNo = "1111000000";
                                kVCTPTC.TKCo = tk;
                                kVCTPTC.MaKhCo = maKh;

                                kVCTPTC.CoQuay = boPhan;
                                switch (baoCaoSo.Substring(5, 3))
                                {
                                    case "CHK":
                                        kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                        kVCTPTC.BoPhan = "HK";
                                        break;

                                    case "TWI":
                                        kVCTPTC.MaKhCo = "0000000006"; //maKh;
                                        kVCTPTC.CoQuay = "TF";
                                        kVCTPTC.BoPhan = "TF";
                                        break;

                                    case "TND":
                                        kVCTPTC.MaKhCo = "0310891532051"; //maKh;
                                        kVCTPTC.TKCo = "1368000000";
                                        break;

                                    case "TOB":
                                        kVCTPTC.MaKhCo = "0000000002"; //maKh;
                                        break;

                                    case "TXE":
                                        kVCTPTC.CoQuay = "XE";
                                        kVCTPTC.BoPhan = "XE";
                                        //kVCTPTC.MaKhCo = "0000000001"; //maKh;
                                        break;

                                    case "TIB":
                                        kVCTPTC.MaKhCo = "0000000005"; //maKh;
                                        kVCTPTC.TKCo = "1311120000";
                                        kVCTPTC.CoQuay = "IB";
                                        kVCTPTC.BoPhan = "IB";
                                        break;

                                    //// them
                                    case "BHK":
                                        kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                        kVCTPTC.BoPhan = "HK";
                                        break;

                                    case "PHK":
                                        kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                        kVCTPTC.BoPhan = "HK";
                                        break;

                                    case "VHK":
                                        kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                        kVCTPTC.BoPhan = "HK";
                                        break;

                                    case "BIB":
                                        kVCTPTC.MaKhCo = "0000000005"; //maKh;
                                        kVCTPTC.TKCo = "1311120000";
                                        kVCTPTC.CoQuay = "IB";
                                        kVCTPTC.BoPhan = "IB";
                                        break;

                                    case "BWI":
                                        kVCTPTC.MaKhCo = "0000000006"; //maKh;
                                        kVCTPTC.CoQuay = "TF";
                                        kVCTPTC.BoPhan = "TF";
                                        break;

                                    case "BND":
                                    case "BDN":
                                        kVCTPTC.MaKhCo = "0310891532051"; //maKh;
                                        kVCTPTC.TKCo = "1368000000";
                                        break;

                                    case "BOB":
                                        kVCTPTC.MaKhCo = "0000000002"; //maKh;
                                        break;

                                    case "BDO":
                                        kVCTPTC.MaKhCo = "0000000007"; //maKh;
                                        kVCTPTC.CoQuay = "OB";
                                        kVCTPTC.BoPhan = "OB";
                                        break;

                                    case "BXE":
                                        kVCTPTC.CoQuay = "XE";
                                        kVCTPTC.BoPhan = "XE";
                                        kVCTPTC.MaKhCo = "0000000001"; //maKh;
                                        break;

                                    case "BXK": // xuat khau: thao
                                        kVCTPTC.CoQuay = "XK";
                                        kVCTPTC.BoPhan = "XK";
                                        switch (kVCTPTC.Sgtcode)
                                        {
                                            case "0COSTA":
                                                kVCTPTC.MaKhCo = "0000000015";
                                                break;

                                            case "0STARS":
                                                kVCTPTC.MaKhCo = "0000000016";
                                                break;

                                            case "0SPGLO":
                                                kVCTPTC.MaKhCo = "0000000017";
                                                break;

                                            case "0MIEJP":
                                                kVCTPTC.MaKhCo = "0000000009";
                                                break;

                                            case "0STUOS":
                                                kVCTPTC.MaKhCo = "0000000013";
                                                break;

                                        }
                                        break;

                                    default:
                                        var soBaoCao_Hoan = baoCaoSo.Substring(5, 1);//.Contains("H")
                                        if (soBaoCao_Hoan == "H")
                                            return null;///////////////////////////////////////////////////////////
                                        break;
                                }

                                if (maCN != "STS" && maCN != "STN") // Tram STD
                                {
                                    switch (baoCaoSo.Substring(5, 3))
                                    {
                                        case "BND":
                                        case "TND":
                                        case "PND":
                                            kVCTPTC.TKCo = "1311110000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0000000003";
                                            break;

                                        case "HND":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000003";
                                            kVCTPTC.MaKhCo = "";
                                            break;

                                        case "BOB":
                                        case "TOB":
                                        case "POB":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0000000002";
                                            break;

                                        case "CHK":
                                        case "BHK":
                                        case "PHK":
                                        case "VHK":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0000000004";
                                            break;

                                        case "HOB":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000002";
                                            kVCTPTC.MaKhCo = "";
                                            break;

                                        case "HHK":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000004";
                                            kVCTPTC.MaKhCo = "";
                                            break;
                                    }
                                }
                            }
                            //else // phieu chi (ko có ca` the)
                            //{
                            //    var dienGiai = Get_DienGiai_By_TkNo_TkCo(tk, "1111000000").FirstOrDefault();
                            //    kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                            //    kVCTPTC.TKNo = tk;
                            //    kVCTPTC.TKCo = "1111000000";
                            //    // anh son kt
                            //    if (maKh == "0000000003") // makh
                            //    {
                            //        kVCTPTC.TKNo = "1331000010";
                            //    }
                            //    kVCTPTC.MaKhNo = maKh;
                            //    kVCTPTC.NoQuay = boPhan;
                            //    switch (baoCaoSo.Substring(5, 3))
                            //    {
                            //        case "HHK":
                            //            kVCTPTC.MaKhNo = "KLHK"; //maKh;
                            //            break;

                            //        case "HWI":
                            //            kVCTPTC.MaKhNo = "KLWI"; //maKh;
                            //            break;

                            //        case "HND":
                            //            kVCTPTC.MaKhNo = "STNCN"; //maKh;
                            //            break;

                            //        case "HOB":
                            //            kVCTPTC.MaKhNo = "VEKLO"; //maKh;
                            //            break;

                            //        case "HXE":
                            //            kVCTPTC.MaKhNo = "TX001"; //maKh;
                            //            break;

                            //        case "HIB":
                            //            kVCTPTC.MaKhNo = "KLIB"; //maKh;
                            //            break;
                            //    }
                            //}

                            //kVCTPTC.BoPhan = boPhan;
                            //kVCTPTC.BoPhan = boPhan;

                            //kVCTPTC.Sgtcode = ctbills_TTThe.FirstOrDefault(x => !string.IsNullOrEmpty(x.Sgtcode)).Sgtcode;// item1.Sgtcode;
                            var sgtcode = ctbills_TTThe.FirstOrDefault(x => !string.IsNullOrEmpty(x.Sgtcode));
                            kVCTPTC.Sgtcode = sgtcode == null ? "" : sgtcode.Sgtcode;// item1.Sgtcode;

                            kVCTPTC.CardNumber = ctbills_TTThe.FirstOrDefault().Cardnumber;// item1.Cardnumber;
                            kVCTPTC.SalesSlip = ctbills_TTThe.FirstOrDefault().Saleslip;// item1.Saleslip;

                            // THONG TIN VE THUE
                            kVCTPTC.LoaiHDGoc = loaiHDGoc;
                            kVCTPTC.SoCTGoc = soCTGoc;
                            kVCTPTC.NgayCTGoc = ngayBill;

                            kVCTPTC.DSKhongVAT = 0;
                            kVCTPTC.VAT = 0;

                            kVCTPTC.KyHieu = kyHieu;
                            kVCTPTC.MauSoHD = mauSo;
                            kVCTPTC.MsThue = msThue;
                            kVCTPTC.MaKh = maKh;
                            kVCTPTC.TenKH = tenKh;
                            kVCTPTC.DiaChi = diaChi;

                            kVCTPTC.NguoiTao = nguoiTao;
                            kVCTPTC.NgayTao = ngayTao;
                            kVCTPTC.LogFile = logFile;

                            kVCTPTCs.Add(kVCTPTC);

                            #region ctbill ttthe old

                            //foreach (var item1 in ctbills_TTThe)
                            //{
                            //    KVCTPTC kVCTPTC = new KVCTPTC();

                            //    // THONG TIN VE TAI CHINH
                            //    kVCTPTC.KVPTCId = kVPTCId;
                            //    kVCTPTC.SoCT = soCT;
                            //    kVCTPTC.MaCn = maCN;
                            //    kVCTPTC.DienGiaiP = dienGiaiP;
                            //    kVCTPTC.SoTienNT = item1.Sotiennt;
                            //    kVCTPTC.LoaiTien = item1.Loaitien;
                            //    kVCTPTC.TyGia = item1.Tygia;
                            //    kVCTPTC.SoTien = item1.Sotien;
                            //    kVCTPTC.CardNumber = item1.Cardnumber;
                            //    kVCTPTC.LoaiThe = item1.Loaicard;

                            //    // THONG TIN VE CONG NO DOAN
                            //    if (loaiPhieu == "T") // phieu thu
                            //    {
                            //        var dienGiai = Get_DienGiai_By_TkNo_TkCo("1111000000", tk).FirstOrDefault(); // chac chan tien mat : 1111000000
                            //        kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                            //        kVCTPTC.TKNo = "1111000000";
                            //        kVCTPTC.TKCo = tk;
                            //        kVCTPTC.MaKhCo = maKh;
                            //        if (tk == "1368000000")
                            //        {
                            //            kVCTPTC.MaKhCo = "STNCN"; //maKh;
                            //        }
                            //        kVCTPTC.CoQuay = boPhan;
                            //        switch (baoCaoSo.Substring(5, 3))
                            //        {
                            //            case "CHK":
                            //                kVCTPTC.MaKhCo = "KLHK"; //maKh;
                            //                break;

                            //            case "TWI":
                            //                kVCTPTC.MaKhCo = "KLWI"; //maKh;
                            //                break;

                            //            case "TND":
                            //                kVCTPTC.MaKhCo = "STNCN"; //maKh;
                            //                break;

                            //            case "TOB":
                            //                kVCTPTC.MaKhCo = "VEKLO"; //maKh;
                            //                break;

                            //            case "TXE":
                            //                kVCTPTC.MaKhCo = "TX001"; //maKh;
                            //                break;

                            //            case "TIB":
                            //                kVCTPTC.MaKhCo = "KLIB"; //maKh;
                            //                break;
                            //        }
                            //    }
                            //    else // phieu chi
                            //    {
                            //        var dienGiai = Get_DienGiai_By_TkNo_TkCo(tk, "1111000000").FirstOrDefault();
                            //        kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                            //        kVCTPTC.TKNo = tk;
                            //        kVCTPTC.TKCo = "1111000000";
                            //        kVCTPTC.MaKhNo = maKh;
                            //        kVCTPTC.NoQuay = boPhan;
                            //        switch (baoCaoSo.Substring(5, 3))
                            //        {
                            //            case "HHK":
                            //                kVCTPTC.MaKhNo = "KLHK"; //maKh;
                            //                break;

                            //            case "HWI":
                            //                kVCTPTC.MaKhNo = "KLWI"; //maKh;
                            //                break;

                            //            case "HND":
                            //                kVCTPTC.MaKhNo = "STNCN"; //maKh;
                            //                break;

                            //            case "HOB":
                            //                kVCTPTC.MaKhNo = "VEKLO"; //maKh;
                            //                break;

                            //            case "HXE":
                            //                kVCTPTC.MaKhNo = "TX001"; //maKh;
                            //                break;

                            //            case "HIB":
                            //                kVCTPTC.MaKhNo = "KLIB"; //maKh;
                            //                break;
                            //        }
                            //    }

                            //    kVCTPTC.BoPhan = boPhan;
                            //    kVCTPTC.Sgtcode = item1.Sgtcode;
                            //    kVCTPTC.CardNumber = item1.Cardnumber;
                            //    kVCTPTC.SalesSlip = item1.Saleslip;

                            //    // THONG TIN VE THUE
                            //    kVCTPTC.LoaiHDGoc = loaiHDGoc;
                            //    kVCTPTC.SoCTGoc = soCTGoc;
                            //    kVCTPTC.NgayCTGoc = ngayBill;

                            //    kVCTPTC.DSKhongVAT = 0;
                            //    kVCTPTC.VAT = 0;

                            //    kVCTPTC.KyHieu = kyHieu;
                            //    kVCTPTC.MauSoHD = mauSo;
                            //    kVCTPTC.MsThue = msThue;
                            //    kVCTPTC.MaKh = maKh;
                            //    kVCTPTC.TenKH = tenKh;
                            //    kVCTPTC.DiaChi = diaChi;

                            //    kVCTPTC.NguoiTao = nguoiTao;
                            //    kVCTPTC.NgayTao = ngayTao;
                            //    kVCTPTC.LogFile = logFile;

                            //    kVCTPTCs.Add(kVCTPTC);
                            //}

                            #endregion ctbill ttthe old
                        }
                    }

                    if (!tienMat && !tTThe) // bỏ tróng -> lấy theo tiền mặt
                    {
                        if (ctbills_TienMat.Count() > 0)
                        {
                            //KVCTPTC kVCTPTC = new KVCTPTC();

                            // THONG TIN VE TAI CHINH
                            kVCTPTC.KVPTCId = kVPTCId;
                            kVCTPTC.SoCT = soCT;
                            kVCTPTC.MaCn = maCN;
                            kVCTPTC.DienGiaiP = dienGiaiP;
                            kVCTPTC.SoTienNT = ctbills_TienMat.Sum(x => x.Sotiennt);// item1.Sotiennt;
                            kVCTPTC.LoaiTien = ctbills_TienMat.FirstOrDefault().Loaitien;// item1.Loaitien;
                            kVCTPTC.TyGia = ctbills_TienMat.FirstOrDefault().Tygia;// item1.Tygia;
                            kVCTPTC.SoTien = ctbills_TienMat.Sum(x => x.Sotien);// item1.Sotien;
                                                                                //kVCTPTC.CardNumber = item1.Cardnumber;
                                                                                //kVCTPTC.LoaiThe = item1.Loaicard;

                            // THONG TIN VE CONG NO DOAN
                            if (loaiPhieu == "T") // phieu thu
                            {
                                var dienGiai = Get_DienGiai_By_TkNo_TkCo("1111000000", tk).FirstOrDefault(); // chac chan tien mat : 1111000000
                                kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                                kVCTPTC.TKNo = "1111000000";
                                kVCTPTC.TKCo = tk;
                                // anh son kt
                                if (maKh == "0000000003") // makh
                                {
                                    kVCTPTC.TKCo = "1331000010";
                                }
                                kVCTPTC.MaKhCo = maKh;
                                if (tk == "1368000000")
                                {
                                    kVCTPTC.MaKhCo = "STNCN"; //maKh;
                                }
                                kVCTPTC.CoQuay = boPhan;
                                switch (baoCaoSo.Substring(5, 3)) // STS
                                {
                                    //case "CHK":
                                    //    kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                    //    break;

                                    case "TWI":
                                        kVCTPTC.MaKhCo = "0000000006"; //maKh;
                                        kVCTPTC.CoQuay = "TF";
                                        kVCTPTC.BoPhan = "TF";
                                        break;

                                    case "TND":
                                        kVCTPTC.MaKhCo = "0310891532051"; //maKh;
                                        kVCTPTC.TKCo = "1368000000";
                                        break;

                                    case "TOB":
                                        kVCTPTC.MaKhCo = "0000000002"; //maKh;
                                        break;

                                    case "TXE":
                                        kVCTPTC.CoQuay = "XE";
                                        kVCTPTC.BoPhan = "XE";
                                        //kVCTPTC.MaKhCo = "0000000001"; //maKh;
                                        break;

                                    case "TIB":
                                        kVCTPTC.MaKhCo = "0000000005"; //maKh;
                                        kVCTPTC.TKCo = "1311120000";
                                        kVCTPTC.CoQuay = "IB";
                                        kVCTPTC.BoPhan = "IB";
                                        break;

                                    //// them
                                    //case "BHK":
                                    //    kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                    //    break;

                                    //case "PHK":
                                    //    kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                    //    break;

                                    case "VHK":
                                        kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                        break;

                                    case "BIB":
                                        kVCTPTC.MaKhCo = "0000000005"; //maKh;
                                        kVCTPTC.TKCo = "1311120000";
                                        kVCTPTC.CoQuay = "IB";
                                        kVCTPTC.BoPhan = "IB";
                                        break;

                                    case "PIB":
                                        kVCTPTC.MaKhCo = "0000000005"; //maKh;
                                        kVCTPTC.TKCo = "1311120000";
                                        kVCTPTC.CoQuay = "IB";
                                        kVCTPTC.BoPhan = "IB";
                                        break;

                                    case "PWI":
                                        kVCTPTC.MaKhCo = "0000000006"; //maKh;
                                        kVCTPTC.CoQuay = "TF";
                                        kVCTPTC.BoPhan = "TF";
                                        break;

                                    case "BWI":
                                        kVCTPTC.MaKhCo = "0000000006"; //maKh;
                                        kVCTPTC.CoQuay = "TF";
                                        kVCTPTC.BoPhan = "TF";
                                        break;

                                    case "BND":
                                    case "BDN":
                                        kVCTPTC.MaKhCo = "0310891532051"; //maKh;
                                        kVCTPTC.TKCo = "1368000000";
                                        break;

                                    case "PND":
                                        kVCTPTC.MaKhCo = "0310891532051"; //maKh;
                                        kVCTPTC.TKCo = "1368000000";
                                        break;

                                    case "BOB":
                                        kVCTPTC.MaKhCo = "0000000002"; //maKh;
                                        break;

                                    case "BDO":
                                        kVCTPTC.MaKhCo = "0000000007"; //maKh;
                                        kVCTPTC.CoQuay = "OB";
                                        kVCTPTC.BoPhan = "OB";
                                        break;

                                    case "POB":
                                        kVCTPTC.MaKhCo = "0000000002"; //maKh;
                                        break;

                                    case "BXE":
                                        kVCTPTC.MaKhCo = "0000000001"; //maKh;
                                        kVCTPTC.CoQuay = "XE";
                                        kVCTPTC.BoPhan = "XE";
                                        break;

                                    case "PXE":
                                        kVCTPTC.CoQuay = "XE";
                                        kVCTPTC.BoPhan = "XE";
                                        kVCTPTC.MaKhCo = "0000000001"; //maKh;
                                        break;

                                    case "BXK": // xuat khau: thao
                                        kVCTPTC.CoQuay = "XK";
                                        kVCTPTC.BoPhan = "XK";
                                        switch (kVCTPTC.Sgtcode)
                                        {
                                            case "0COSTA":
                                                kVCTPTC.MaKhCo = "0000000015";
                                                break;

                                            case "0STARS":
                                                kVCTPTC.MaKhCo = "0000000016";
                                                break;

                                            case "0SPGLO":
                                                kVCTPTC.MaKhCo = "0000000017";
                                                break;

                                            case "0MIEJP":
                                                kVCTPTC.MaKhCo = "0000000009";
                                                break;

                                            case "0STUOS":
                                                kVCTPTC.MaKhCo = "0000000013";
                                                break;

                                        }
                                        break;

                                    default:
                                        kVCTPTC.MaKhCo = maKh; //maKh;
                                        break;
                                }

                                //kVCTPTC.MaKhCo = maKh; //maKh;
                                if (tk == "1368000000")
                                {
                                    kVCTPTC.MaKhCo = "0310891532051"; //maKh;
                                }

                                if (maCN == "STN") // thao
                                {
                                    switch (baoCaoSo.Substring(5, 3))
                                    {
                                        case "BND":
                                        case "TND":
                                        case "PND":
                                            kVCTPTC.TKCo = "1311110000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0000000003";
                                            kVCTPTC.BoPhan = "ND";
                                            kVCTPTC.CoQuay = "ND";
                                            break;

                                        case "HND":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000003";
                                            kVCTPTC.MaKhCo = "";
                                            kVCTPTC.BoPhan = "ND";
                                            kVCTPTC.NoQuay = "ND";
                                            break;

                                        case "BOB":
                                        case "TOB":
                                        case "POB":
                                        case "CHK":
                                        case "BHK":
                                        case "PHK":
                                        case "VHK":
                                            kVCTPTC.TKCo = "1368000000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0310891532";
                                            kVCTPTC.BoPhan = "ND";
                                            kVCTPTC.CoQuay = "";
                                            kVCTPTC.NoQuay = "";
                                            break;

                                        case "HOB":
                                        case "HHK":
                                            kVCTPTC.TKNo = "1368000000";
                                            kVCTPTC.MaKhCo = "";
                                            kVCTPTC.MaKhNo = "0310891532";
                                            kVCTPTC.BoPhan = "ND";
                                            kVCTPTC.CoQuay = "";
                                            kVCTPTC.NoQuay = "";
                                            break;
                                    }
                                }
                                if (maCN != "STS" && maCN != "STN") // Tram STD
                                {
                                    switch (baoCaoSo.Substring(5, 3))
                                    {
                                        case "BND":
                                        case "TND":
                                        case "PND":
                                            kVCTPTC.TKCo = "1311110000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0000000003";
                                            break;

                                        case "HND":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000003";
                                            kVCTPTC.MaKhCo = "";
                                            break;

                                        case "BOB":
                                        case "TOB":
                                        case "POB":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0000000002";
                                            break;

                                        case "CHK":
                                        case "BHK":
                                        case "PHK":
                                        case "VHK":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "";
                                            kVCTPTC.MaKhCo = "0000000004";
                                            break;

                                        case "HOB":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000002";
                                            kVCTPTC.MaKhCo = "";
                                            break;

                                        case "HHK":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000004";
                                            kVCTPTC.MaKhCo = "";
                                            break;
                                    }
                                }
                            }
                            else // phieu chi
                            {
                                var dienGiai = Get_DienGiai_By_TkNo_TkCo(tk, "1111000000").FirstOrDefault();
                                kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                                kVCTPTC.TKNo = tk;
                                kVCTPTC.TKCo = "1111000000";
                                // anh son kt
                                if (maKh == "0000000003") // makh
                                {
                                    kVCTPTC.TKNo = "1331000010";
                                }
                                kVCTPTC.MaKhNo = maKh;
                                kVCTPTC.NoQuay = boPhan;
                                switch (baoCaoSo.Substring(5, 3))
                                {
                                    //case "HHK":
                                    //    kVCTPTC.MaKhNo = "0000000004"; //maKh;
                                    //    break;

                                    case "HWI":
                                        kVCTPTC.MaKhNo = "0000000006"; //maKh;
                                        kVCTPTC.NoQuay = "TF";
                                        kVCTPTC.BoPhan = "TF";
                                        break;

                                    case "HND":
                                        kVCTPTC.MaKhNo = "0310891532051"; //maKh;
                                        kVCTPTC.TKNo = "1368000000";
                                        break;

                                    case "HOB":
                                        kVCTPTC.MaKhNo = "0000000002"; //maKh;
                                        break;

                                    case "HXE":
                                        kVCTPTC.NoQuay = "XE";
                                        kVCTPTC.BoPhan = "XE";
                                        //kVCTPTC.MaKhNo = "0000000001"; //maKh;
                                        break;

                                    case "HIB":
                                        kVCTPTC.MaKhNo = "0000000005"; //maKh;
                                        break;

                                    case "HXK": // xuat khau: thao
                                        kVCTPTC.NoQuay = "XK";
                                        kVCTPTC.BoPhan = "XK";
                                        switch (kVCTPTC.Sgtcode)
                                        {
                                            case "0COSTA":
                                                kVCTPTC.MaKhNo = "0000000015";
                                                break;

                                            case "0STARS":
                                                kVCTPTC.MaKhNo = "0000000016";
                                                break;

                                            case "0SPGLO":
                                                kVCTPTC.MaKhNo = "0000000017";
                                                break;

                                            case "0MIEJP":
                                                kVCTPTC.MaKhNo = "0000000009";
                                                break;

                                            case "0STUOS":
                                                kVCTPTC.MaKhNo = "0000000013";
                                                break;

                                        }
                                        break;

                                }

                                if (maCN == "STN") // thao
                                {
                                    switch (baoCaoSo.Substring(5, 3))
                                    {
                                        case "HND":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000003";
                                            kVCTPTC.MaKhCo = "";
                                            kVCTPTC.BoPhan = "ND";
                                            kVCTPTC.NoQuay = "ND";
                                            break;

                                        case "HOB":
                                        case "HHK":
                                            kVCTPTC.TKNo = "1368000000";
                                            kVCTPTC.MaKhCo = "";
                                            kVCTPTC.MaKhNo = "0310891532";
                                            kVCTPTC.BoPhan = "ND";
                                            kVCTPTC.CoQuay = "";
                                            kVCTPTC.NoQuay = "";
                                            break;
                                    }
                                }
                                if (maCN != "STS" && maCN != "STN") // Tram STD
                                {
                                    switch (baoCaoSo.Substring(5, 3))
                                    {
                                        case "HND":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000003";
                                            kVCTPTC.MaKhCo = "";
                                            break;

                                        case "HOB":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000002";
                                            kVCTPTC.MaKhCo = "";
                                            break;

                                        case "HHK":
                                            kVCTPTC.TKNo = "1311110000";
                                            kVCTPTC.MaKhNo = "0000000004";
                                            kVCTPTC.MaKhCo = "";
                                            break;
                                    }
                                }
                            }

                            kVCTPTC.BoPhan = boPhan;
                            //kVCTPTC.Sgtcode = ctbills_TienMat.FirstOrDefault(x => !string.IsNullOrEmpty(x.Sgtcode)).Sgtcode;// item1.Sgtcode;
                            var sgtcode = ctbills_TienMat.FirstOrDefault(x => !string.IsNullOrEmpty(x.Sgtcode));
                            kVCTPTC.Sgtcode = sgtcode == null ? "" : sgtcode.Sgtcode;// item1.Sgtcode;
                            //kVCTPTC.CardNumber = item1.Cardnumber;
                            kVCTPTC.SalesSlip = ctbills_TienMat.FirstOrDefault().Saleslip;// item1.Saleslip;

                            // THONG TIN VE THUE
                            kVCTPTC.LoaiHDGoc = loaiHDGoc;
                            kVCTPTC.SoCTGoc = soCTGoc;
                            kVCTPTC.NgayCTGoc = ngayBill;

                            kVCTPTC.DSKhongVAT = 0;
                            kVCTPTC.VAT = 0;

                            kVCTPTC.KyHieu = kyHieu;
                            kVCTPTC.MauSoHD = mauSo;
                            kVCTPTC.MsThue = msThue;
                            kVCTPTC.MaKh = maKh;
                            kVCTPTC.TenKH = tenKh;
                            kVCTPTC.DiaChi = diaChi;

                            kVCTPTC.NguoiTao = nguoiTao;
                            kVCTPTC.NgayTao = ngayTao;
                            kVCTPTC.LogFile = logFile;

                            kVCTPTCs.Add(kVCTPTC);

                            #region bỏ tróng -> lấy theo tiền mặt old

                            //foreach (var item1 in ctbills_TienMat)
                            //{
                            //    KVCTPTC kVCTPTC = new KVCTPTC();

                            //    // THONG TIN VE TAI CHINH
                            //    kVCTPTC.KVPTCId = kVPTCId;
                            //    kVCTPTC.SoCT = soCT;
                            //    kVCTPTC.MaCn = maCN;
                            //    kVCTPTC.DienGiaiP = dienGiaiP;
                            //    kVCTPTC.SoTienNT = item1.Sotiennt;
                            //    kVCTPTC.LoaiTien = item1.Loaitien;
                            //    kVCTPTC.TyGia = item1.Tygia;
                            //    kVCTPTC.SoTien = item1.Sotien;
                            //    kVCTPTC.CardNumber = item1.Cardnumber;
                            //    kVCTPTC.LoaiThe = item1.Loaicard;

                            //    // THONG TIN VE CONG NO DOAN
                            //    if (loaiPhieu == "T") // phieu thu
                            //    {
                            //        var dienGiai = Get_DienGiai_By_TkNo_TkCo("1111000000", tk).FirstOrDefault(); // chac chan tien mat : 1111000000
                            //        kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                            //        kVCTPTC.TKNo = "1111000000";
                            //        kVCTPTC.TKCo = tk;
                            //        kVCTPTC.MaKhCo = maKh;
                            //        if (tk == "1368000000")
                            //        {
                            //            kVCTPTC.MaKhCo = "STNCN"; //maKh;
                            //        }
                            //        kVCTPTC.CoQuay = boPhan;
                            //        switch (baoCaoSo.Substring(5, 3))
                            //        {
                            //            case "CHK":
                            //                kVCTPTC.MaKhCo = "KLHK"; //maKh;
                            //                break;

                            //            case "TWI":
                            //                kVCTPTC.MaKhCo = "KLWI"; //maKh;
                            //                break;

                            //            case "TND":
                            //                kVCTPTC.MaKhCo = "STNCN"; //maKh;
                            //                break;

                            //            case "TOB":
                            //                kVCTPTC.MaKhCo = "VEKLO"; //maKh;
                            //                break;

                            //            case "TXE":
                            //                kVCTPTC.MaKhCo = "TX001"; //maKh;
                            //                break;

                            //            case "TIB":
                            //                kVCTPTC.MaKhCo = "KLIB"; //maKh;
                            //                break;
                            //        }
                            //    }
                            //    else // phieu chi
                            //    {
                            //        var dienGiai = Get_DienGiai_By_TkNo_TkCo(tk, "1111000000").FirstOrDefault();
                            //        kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                            //        kVCTPTC.TKNo = tk;
                            //        kVCTPTC.TKCo = "1111000000";
                            //        kVCTPTC.MaKhNo = maKh;
                            //        kVCTPTC.NoQuay = boPhan;
                            //        switch (baoCaoSo.Substring(5, 3))
                            //        {
                            //            case "HHK":
                            //                kVCTPTC.MaKhNo = "KLHK"; //maKh;
                            //                break;

                            //            case "HWI":
                            //                kVCTPTC.MaKhNo = "KLWI"; //maKh;
                            //                break;

                            //            case "HND":
                            //                kVCTPTC.MaKhNo = "STNCN"; //maKh;
                            //                break;

                            //            case "HOB":
                            //                kVCTPTC.MaKhNo = "VEKLO"; //maKh;
                            //                break;

                            //            case "HXE":
                            //                kVCTPTC.MaKhNo = "TX001"; //maKh;
                            //                break;

                            //            case "HIB":
                            //                kVCTPTC.MaKhNo = "KLIB"; //maKh;
                            //                break;
                            //        }
                            //    }

                            //    kVCTPTC.BoPhan = boPhan;
                            //    kVCTPTC.Sgtcode = item1.Sgtcode;
                            //    kVCTPTC.CardNumber = item1.Cardnumber;
                            //    kVCTPTC.SalesSlip = item1.Saleslip;

                            //    // THONG TIN VE THUE
                            //    kVCTPTC.LoaiHDGoc = loaiHDGoc;
                            //    kVCTPTC.SoCTGoc = soCTGoc;
                            //    kVCTPTC.NgayCTGoc = ngayBill;

                            //    kVCTPTC.DSKhongVAT = 0;
                            //    kVCTPTC.VAT = 0;

                            //    kVCTPTC.KyHieu = kyHieu;
                            //    kVCTPTC.MauSoHD = mauSo;
                            //    kVCTPTC.MsThue = msThue;
                            //    kVCTPTC.MaKh = maKh;
                            //    kVCTPTC.TenKH = tenKh;
                            //    kVCTPTC.DiaChi = diaChi;

                            //    kVCTPTC.NguoiTao = nguoiTao;
                            //    kVCTPTC.NgayTao = ngayTao;
                            //    kVCTPTC.LogFile = logFile;

                            //    kVCTPTCs.Add(kVCTPTC);
                            //}

                            #endregion bỏ tróng -> lấy theo tiền mặt old
                        }
                    }
                }
            }
            return kVCTPTCs;
        }

        public IEnumerable<KVCTPTC> GetKVCTPTCs_VCBChau(string baoCaoSo, Guid kVPTCId,
            string soCT, string username, string maCN, string loaiPhieu,
            string tk, bool tienMat, bool tTThe) // noptien => two keys
        {
            var ntbills = _unitOfWork.ntbillRepository.Find(x => x.Soct == baoCaoSo && x.Chinhanh == maCN);

            string nguoiTao = username;
            DateTime ngayTao = DateTime.Now;

            // ghi log
            string logFile = "-User kéo từ cashier: " + username + " , báo cáo số " + baoCaoSo + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            List<KVCTPTC> kVCTPTCs = new List<KVCTPTC>();

            if (ntbills != null)
            {
                foreach (var item in ntbills)
                {
                    string boPhan = item.Bophan;

                    string maKh = string.IsNullOrEmpty(item.Coquan) ? "50000" : item.Coquan;
                    var viewSupplier = GetSuppliersByCodeName(maKh, maCN).FirstOrDefault();//.Where(x => x.Code == maKh).FirstOrDefault();
                    //var viewSupplier = GetAll_KhachHangs_HDVATOB().Where(x => x.Code == maKh).FirstOrDefault();
                    string kyHieu = "", mauSo = "", msThue = "", tenKh = "", diaChi = "";
                    if (viewSupplier != null)
                    {
                        kyHieu = viewSupplier.Taxsign;
                        mauSo = viewSupplier.Taxform;
                        msThue = viewSupplier.Taxcode;
                        tenKh = viewSupplier.Name;
                        diaChi = viewSupplier.Address;
                    }
                    var ctbills = _unitOfWork.ctbillRepository.Find(x => x.Idntbill == item.Idntbill);
                    var ctbills_TienMat = ctbills.Where(x => string.IsNullOrEmpty(x.Cardnumber) && string.IsNullOrEmpty(x.Loaicard));
                    var ctbills_TTThe = ctbills.Except(ctbills_TienMat);

                    string dienGiaiP = loaiPhieu == "T" ? "THU BILL " + (item.Bill ?? item.Stt) : "CHI BILL " + (item.Bill ?? item.Stt); // ??
                    var loaiHDGoc = "VAT";// item.Loaihd; // ??
                    var soCTGoc = item.Bill ?? item.Stt;// item.Stt; // thao
                    var ngayBill = item.Ngaybill;

                    if (tTThe)
                    {
                        if (ctbills_TTThe.Count() > 0)
                        {
                            //KVCTPTC kVCTPTC = new KVCTPTC();

                            // THONG TIN VE TAI CHINH
                            //kVCTPTC.KVPTCId = kVPTCId;
                            //kVCTPTC.SoCT = soCT;
                            //kVCTPTC.MaCn = maCN;
                            //kVCTPTC.DienGiaiP = dienGiaiP;
                            //kVCTPTC.SoTienNT = ctbills_TTThe.Sum(x => x.Sotiennt);// item1.Sotiennt;
                            //kVCTPTC.LoaiTien = ctbills_TTThe.FirstOrDefault().Loaitien;// item1.Loaitien;
                            //kVCTPTC.TyGia = ctbills_TTThe.FirstOrDefault().Tygia;// item1.Tygia;
                            //kVCTPTC.SoTien = ctbills_TTThe.Sum(x => x.Sotien);// item1.Sotien;
                            //kVCTPTC.CardNumber = ctbills_TTThe.FirstOrDefault().Cardnumber;// item1.Cardnumber;
                            //kVCTPTC.LoaiThe = ctbills_TTThe.FirstOrDefault().Loaicard;// item1.Loaicard;

                            foreach (var item1 in ctbills_TTThe)
                            {

                                KVCTPTC kVCTPTC = new KVCTPTC();
                                kVCTPTC.STT = item.Stt; // ben [ntbill] cashier

                                kVCTPTC.KVPTCId = kVPTCId;
                                kVCTPTC.SoCT = soCT;
                                kVCTPTC.MaCn = maCN;
                                kVCTPTC.DienGiaiP = dienGiaiP;
                                kVCTPTC.SoTienNT = item1.Sotiennt;// ctbills_TTThe.Sum(x => x.Sotiennt);// 
                                kVCTPTC.LoaiTien = item1.Loaitien;// ctbills_TTThe.FirstOrDefault().Loaitien;// 
                                kVCTPTC.TyGia = item1.Tygia;// ctbills_TTThe.FirstOrDefault().Tygia;// 
                                kVCTPTC.SoTien = item1.Sotien;// ctbills_TTThe.Sum(x => x.Sotien);// 
                                kVCTPTC.CardNumber = item1.Cardnumber;// ctbills_TTThe.FirstOrDefault().Cardnumber;// 
                                kVCTPTC.LoaiThe = item1.Loaicard;// ctbills_TTThe.FirstOrDefault().Loaicard;// 
                                kVCTPTC.Sgtcode = item1.Sgtcode;

                                // THONG TIN VE CONG NO DOAN
                                if (loaiPhieu == "T") // phieu thu
                                {
                                    var dienGiai = Get_DienGiai_By_TkNo_TkCo("1111000000", tk).FirstOrDefault(); // chac chan tien mat : 1111000000
                                    kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                                    kVCTPTC.TKNo = "1111000000";
                                    kVCTPTC.TKCo = tk;
                                    kVCTPTC.MaKhCo = maKh;

                                    kVCTPTC.CoQuay = boPhan;
                                    switch (baoCaoSo.Substring(5, 3))
                                    {
                                        //case "CHK":
                                        //    kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                        //    break;

                                        case "TWI":
                                            kVCTPTC.MaKhCo = "0000000006"; //maKh;
                                            kVCTPTC.CoQuay = "TF";
                                            kVCTPTC.BoPhan = "TF";
                                            break;

                                        case "TND":
                                            kVCTPTC.MaKhCo = "0310891532051"; //maKh;
                                            kVCTPTC.TKCo = "1368000000";
                                            break;

                                        case "TOB":
                                            kVCTPTC.MaKhCo = "0000000002"; //maKh;
                                            break;

                                        case "TXE":
                                            kVCTPTC.CoQuay = "XE";
                                            kVCTPTC.BoPhan = "XE";
                                            //kVCTPTC.MaKhCo = "0000000001"; //maKh;
                                            break;

                                        case "TIB":
                                            kVCTPTC.MaKhCo = "0000000005"; //maKh;
                                            kVCTPTC.TKCo = "1311120000";
                                            kVCTPTC.CoQuay = "IB";
                                            kVCTPTC.BoPhan = "IB";
                                            break;

                                        //// them
                                        //case "BHK":
                                        //    kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                        //    break;

                                        //case "PHK":
                                        //    kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                        //    break;

                                        //case "VHK":
                                        //    kVCTPTC.MaKhCo = "0000000004"; //maKh;
                                        //    break;

                                        case "BIB":
                                            kVCTPTC.MaKhCo = "0000000005"; //maKh;
                                            kVCTPTC.TKCo = "1311120000";
                                            kVCTPTC.CoQuay = "IB";
                                            kVCTPTC.BoPhan = "IB";
                                            break;

                                        case "BWI":
                                            kVCTPTC.MaKhCo = "0000000006"; //maKh;
                                            kVCTPTC.CoQuay = "TF";
                                            kVCTPTC.BoPhan = "TF";
                                            break;

                                        case "BND":
                                        case "BDN":
                                            kVCTPTC.MaKhCo = "0310891532051"; //maKh;
                                            kVCTPTC.TKCo = "1368000000";
                                            break;

                                        case "BOB":
                                            kVCTPTC.MaKhCo = "0000000002"; //maKh;
                                            break;

                                        case "BDO":
                                            kVCTPTC.MaKhCo = "0000000007"; //maKh;
                                            kVCTPTC.CoQuay = "OB";
                                            kVCTPTC.BoPhan = "OB";
                                            break;

                                        case "BXE":
                                            kVCTPTC.CoQuay = "XE";
                                            kVCTPTC.BoPhan = "XE";
                                            kVCTPTC.MaKhCo = "0000000001"; //maKh;
                                            break;

                                        case "BXK": // xuat khau: thao
                                            kVCTPTC.CoQuay = "XK";
                                            kVCTPTC.BoPhan = "XK";
                                            switch (kVCTPTC.Sgtcode)
                                            {
                                                case "0COSTA":
                                                    kVCTPTC.MaKhCo = "0000000015";
                                                    break;

                                                case "0STARS":
                                                    kVCTPTC.MaKhCo = "0000000016";
                                                    break;

                                                case "0SPGLO":
                                                    kVCTPTC.MaKhCo = "0000000017";
                                                    break;

                                                case "0MIEJP":
                                                    kVCTPTC.MaKhCo = "0000000009";
                                                    break;

                                                case "0STUOS":
                                                    kVCTPTC.MaKhCo = "0000000013";
                                                    break;

                                            }
                                            break;

                                        default:
                                            var soBaoCao_Hoan = baoCaoSo.Substring(5, 1);//.Contains("H")
                                            if (soBaoCao_Hoan == "H")
                                                return null;///////////////////////////////////////////////////////////
                                            break;
                                    }

                                    if (maCN != "STS" && maCN != "STN") // Tram STD
                                    {
                                        switch (baoCaoSo.Substring(5, 3))
                                        {
                                            case "BND":
                                            case "TND":
                                            case "PND":
                                                kVCTPTC.TKCo = "1311110000";
                                                kVCTPTC.MaKhNo = "";
                                                kVCTPTC.MaKhCo = "0000000003";
                                                break;

                                            case "HND":
                                                kVCTPTC.TKNo = "1311110000";
                                                kVCTPTC.MaKhNo = "0000000003";
                                                kVCTPTC.MaKhCo = "";
                                                break;

                                            case "BOB":
                                            case "TOB":
                                            case "POB":
                                                kVCTPTC.TKNo = "1311110000";
                                                kVCTPTC.MaKhNo = "";
                                                kVCTPTC.MaKhCo = "0000000002";
                                                break;

                                            case "CHK":
                                            case "BHK":
                                            case "PHK":
                                            case "VHK":
                                                kVCTPTC.TKNo = "1311110000";
                                                kVCTPTC.MaKhNo = "";
                                                kVCTPTC.MaKhCo = "0000000004";
                                                break;

                                            case "HOB":
                                                kVCTPTC.TKNo = "1311110000";
                                                kVCTPTC.MaKhNo = "0000000002";
                                                kVCTPTC.MaKhCo = "";
                                                break;

                                            case "HHK":
                                                kVCTPTC.TKNo = "1311110000";
                                                kVCTPTC.MaKhNo = "0000000004";
                                                kVCTPTC.MaKhCo = "";
                                                break;
                                        }
                                    }
                                }

                                //var sgtcode = ctbills_TTThe.FirstOrDefault(x => !string.IsNullOrEmpty(x.Sgtcode));
                                //kVCTPTC.Sgtcode = sgtcode == null ? "" : sgtcode.Sgtcode;// 

                                //kVCTPTC.CardNumber = ctbills_TTThe.FirstOrDefault().Cardnumber;// item1.Cardnumber;
                                kVCTPTC.SalesSlip = ctbills_TTThe.FirstOrDefault().Saleslip;// item1.Saleslip;

                                // THONG TIN VE THUE
                                kVCTPTC.LoaiHDGoc = loaiHDGoc;
                                kVCTPTC.SoCTGoc = soCTGoc;
                                kVCTPTC.NgayCTGoc = ngayBill;

                                kVCTPTC.DSKhongVAT = 0;
                                kVCTPTC.VAT = 0;

                                kVCTPTC.KyHieu = kyHieu;
                                kVCTPTC.MauSoHD = mauSo;
                                kVCTPTC.MsThue = msThue;
                                kVCTPTC.MaKh = maKh;
                                kVCTPTC.TenKH = tenKh;
                                kVCTPTC.DiaChi = diaChi;

                                kVCTPTC.NguoiTao = nguoiTao;
                                kVCTPTC.NgayTao = ngayTao;
                                kVCTPTC.LogFile = logFile;

                                kVCTPTCs.Add(kVCTPTC);

                            }

                        }
                    }

                }
            }
            return kVCTPTCs;
        }

        public async Task CreateRange(IEnumerable<KVCTPTC> kVCTPTCs)
        {
            await _unitOfWork.kVCTPCTRepository.CreateRange(kVCTPTCs);
            await _unitOfWork.Complete();
        }

        public IEnumerable<DmTk> GetAll_DmTk_Cashier()
        {
            var dmTks = GetAll_DmTk().Where(x => x.Tkhoan.StartsWith("1311") || x.Tkhoan.StartsWith("1368"));
            List<DmTk> dmTks1 = new List<DmTk>();
            foreach (var item in dmTks)
            {
                dmTks1.Add(new DmTk() { Tkhoan = item.Tkhoan, TenTk = item.Tkhoan + " - " + item.TenTk });
            }
            return dmTks1;
        }

        public async Task<KVCTPTC> GetById(long id)
        {
            return await _unitOfWork.kVCTPCTRepository.GetByLongIdAsync(id);
        }

        public IEnumerable<Data.Models_HDVATOB.Supplier> GetAll_KhachHangs_HDVATOB()
        {
            return _unitOfWork.supplier_Hdvatob_Repository.GetAll();
        }

        public IEnumerable<Dgiai> GetAll_DienGiai()
        {
            return _unitOfWork.dGiaiRepository.GetAll();
        }

        public KVCTPTC GetBySoCTAsNoTracking(long id)
        {
            return _unitOfWork.kVCTPCTRepository.GetByIdAsNoTracking(x => x.Id == id);
        }

        public async Task UpdateAsync(KVCTPTC kVCTPCT)
        {
            _unitOfWork.kVCTPCTRepository.Update(kVCTPCT);
            await _unitOfWork.Complete();
        }

        public IEnumerable<DmTk> GetAll_DmTk_TienMat()
        {
            var dmTks = GetAll_DmTk().Where(x => x.Tkhoan.StartsWith("111"));
            List<DmTk> dmTks1 = new List<DmTk>();
            foreach (var item in dmTks)
            {
                dmTks1.Add(new DmTk() { Tkhoan = item.Tkhoan, TenTk = item.Tkhoan + " - " + item.TenTk });
            }
            return dmTks1;
        }

        public IEnumerable<DmTk> GetAll_DmTk_TaiKhoan()
        {
            //var dmtkTienMats = GetAll_DmTk_TienMat();
            //var dmTkList = GetAll_DmTk();//

            var dmTks = GetAll_DmTk();

            List<DmTk> dmTks1 = new List<DmTk>();
            foreach (var item in dmTks)
            {
                dmTks1.Add(new DmTk() { Tkhoan = item.Tkhoan, TenTk = item.Tkhoan + " - " + item.TenTk });
            }
            return dmTks1;
        }

        public async Task UpdateAsync_NopTien(Noptien noptien)
        {
            await _unitOfWork.nopTienRepository.UpdateAsync(noptien);
        }

        public async Task UpdateAsync_ThuChiXe(Thuchi thuchi)
        {
            await _unitOfWork.xeRepository.UpdateAsync(thuchi);
        }

        public IEnumerable<KhachHang> GetSuppliersByCode(string code)
        {
            code ??= "";
            var khachHangs = _unitOfWork.khachHang_DanhMucKTRepository.Find(x => x.Code.Trim().ToLower().Contains(code.Trim().ToLower())).ToList();

            return khachHangs;
        }

        public IPagedList<KhachHang> GetSuppliersByCodeName_PagedList(string code, string maCn, int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            code ??= "";
            var suppliers = _unitOfWork.khachHang_DanhMucKTRepository.GetKhachHangsByCodeName(code);
            //var suppliers = _unitOfWork.supplier_Hdvatob_Repository.GetSuppliersByCodeName(code);
            //suppliers = suppliers.Where(x => x.Chinhanh == maCn).ToList();

            var list = suppliers.OrderByDescending(x => x.Code).ToList();
            var count = list.Count();

            // page the list
            const int pageSize = 10;
            decimal aa = (decimal)list.Count() / (decimal)pageSize;
            var bb = Math.Ceiling(aa);
            if (page > bb)
            {
                page--;
            }
            page = (page == 0) ? 1 : page;
            var listPaged = list.ToPagedList(page ?? 1, pageSize);
            //if (page > listPaged.PageCount)
            //    page--;
            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;

            return listPaged;
        }

        public IEnumerable<VSupplierTaiKhoan> GetSuppliersByCodeName(string code, string maCn)
        {
            code ??= "";
            var suppliers = _unitOfWork.supplier_Hdvatob_Repository.GetSuppliersByCodeName(code);
            suppliers = suppliers.Where(x => x.Chinhanh == maCn).ToList();
            return suppliers;
        }

        //public IEnumerable<Data.Models_HDVATOB.Supplier> GetSuppliersByCodeName(string code, string maCn)
        //{
        //    code ??= "";
        //    var suppliers = _unitOfWork.supplier_Hdvatob_Repository.Find(x => x.Code.Trim().ToLower().Contains(code.Trim().ToLower()) ||
        //                                                                      (!string.IsNullOrEmpty(x.Name) && x.Name.Trim().ToLower().Contains(code.Trim().ToLower()))).ToList();
        //    suppliers = suppliers.Where(x => x.Chinhanh == maCn).ToList();
        //    return suppliers;
        //}

        public async Task DeleteAsync(KVCTPTC kVCTPCT)
        {
            _unitOfWork.kVCTPCTRepository.Delete(kVCTPCT);
            await _unitOfWork.Complete();
        }

        public IEnumerable<ListViewModel> LoaiHDGocs()
        {
            List<ListViewModel> loaiHDGocs = new List<ListViewModel>()
            {
                new ListViewModel(){Name = ""},
                new ListViewModel(){Name = "HTT"},
                new ListViewModel(){Name = "KHD"},
                new ListViewModel(){Name = "KCT"},
                new ListViewModel(){Name = "VAT"},
            };

            return loaiHDGocs;
        }

        public string AutoSgtcode(string param)
        {
            //"033-58" sẽ ra " SGT033-2021-00058"(ĐAY LÀ CODE ĐOÀN inbound")
            //"084/58" sẽ ra " STN084-2021-00058"(ĐAY LÀ CODE ĐOÀN nội địa")
            //" 58OB"  sẽ ra "STSTOB-2021-00058" (ĐAY LÀ CODE ĐOÀN outbound")

            //khác là dấu "-" là code SGT
            //còn "/" là code "STN"

            param ??= "";
            param = param.Trim();

            string sgtcode;
            string codeNumber;
            string currentYear = DateTime.Now.Year.ToString();
            string mark = "";
            try
            {
                mark = param.Substring(3, 1); // mark : - / *
            }
            catch (Exception)
            {
                return "";
            }

            string[] stringArry = param.Split(mark);

            switch (mark)
            {
                case "-":
                    codeNumber = GetCodeNumber(stringArry[1]);
                    sgtcode = "SGT" + stringArry[0] + "-" + currentYear + "-" + codeNumber;
                    break;

                case "/":
                    codeNumber = GetCodeNumber(stringArry[1]);
                    sgtcode = "STN" + stringArry[0] + "-" + currentYear + "-" + codeNumber;
                    break;

                default:
                    string paramSub = param.Substring(param.Length - 2, 2);
                    if (paramSub.ToUpper() == "OB")
                    {
                        codeNumber = param.Substring(0, param.Length - 2); // codeNumber
                        codeNumber = GetCodeNumber(codeNumber);
                        sgtcode = "STSTOB-" + currentYear + "-" + codeNumber; // TOB
                        break;
                    }
                    else
                    {
                        sgtcode = "";
                        break;
                    }
            }

            return sgtcode;
        }

        private string GetCodeNumber(string param)
        {
            string codeNumber;
            switch (param.Length)
            {
                case 1:
                    codeNumber = "0000" + param;
                    break;

                case 2:
                    codeNumber = "000" + param;
                    break;

                case 3:
                    codeNumber = "00" + param;
                    break;

                case 4:
                    codeNumber = "0" + param;
                    break;

                default:
                    codeNumber = param;
                    break;
            }

            return codeNumber;
        }

        public async Task<KVCTPTC> FindByIdInclude(long kVCTPCTId_PhieuTC)
        {
            var kVCTPTCs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, y => y.Id == kVCTPCTId_PhieuTC);
            return kVCTPTCs.FirstOrDefault();
        }

        public async Task<IEnumerable<KVCTPTC>> FinBy_TonQuy_Date(string searchFromDate, string searchToDate, string maCn, string loaiTien = "")
        {
            // searchFromDate : ngay tonquy sau cung nhat
            List<KVCTPTC> list = new List<KVCTPTC>();
            // search date
            DateTime fromDate, toDate;

            try
            {
                fromDate = DateTime.Parse(searchFromDate); // NgayCT
                toDate = DateTime.Parse(searchToDate); // NgayCT

                if (fromDate > toDate)
                {
                    return null; //
                }

                var kVCTPTCs1 = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, y => y.KVPTC.NgayCT > fromDate &&
                                                                                                     y.KVPTC.NgayCT < toDate.AddDays(1));
                var kVCTPTCs = kVCTPTCs1.ToList();
                if (!string.IsNullOrEmpty(loaiTien)) // NT
                {
                    kVCTPTCs = kVCTPTCs.Where(x => x.LoaiTien == loaiTien && x.MaCn == maCn).ToList();
                }
                else // VND
                {
                    var kVCTPTCs_VND = kVCTPTCs.Where(x => x.LoaiTien == "VND" && x.MaCn == maCn).ToList();
                    var kVCTPTCs_ThuDoiNgoaiTe = kVCTPTCs.Where(y => y.TKNo.StartsWith("11120000") && y.TKCo == "1111000000").ToList();
                    kVCTPTCs_ThuDoiNgoaiTe = kVCTPTCs_ThuDoiNgoaiTe.Where(x => x.MaCn == maCn).ToList();

                    //kVCTPTCs = kVCTPTCs_VND.Concat(kVCTPTCs_ThuDoiNgoaiTe);
                    if (kVCTPTCs_ThuDoiNgoaiTe.Count() > 0)
                    {
                        kVCTPTCs_VND.AddRange(kVCTPTCs_ThuDoiNgoaiTe);
                    }
                    kVCTPTCs = kVCTPTCs_VND;
                }

                //list = kVCTPTCs.Where(x => x.TKNo.StartsWith("11120000") && x.TKCo == "1111000000").ToList();
                list = kVCTPTCs.OrderBy(x => x.SoCT.Substring(0, 4)).ToList();
                list = list.OrderBy(x => x.SoCT.Substring(4, 2)).ToList();
                //list = list.OrderBy(x => x.KVPTCId.Substring(6, 4)).ToList();

                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<KVCTPTC>> FinByDate(string searchFromDate, string searchToDate, string maCn)
        {
            //var abc = "0001NC2020".Substring(0, 4);
            List<KVCTPTC> list = new List<KVCTPTC>();
            // search date
            DateTime fromDate, toDate;
            if (!string.IsNullOrEmpty(searchFromDate) && !string.IsNullOrEmpty(searchToDate))
            {
                try
                {
                    fromDate = DateTime.Parse(searchFromDate); // NgayCT
                    toDate = DateTime.Parse(searchToDate); // NgayCT

                    if (fromDate > toDate)
                    {
                        return null; //
                    }

                    var kVCTPTCs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, y => y.KVPTC.NgayCT >= fromDate &&
                                                                                                         y.KVPTC.NgayCT < toDate.AddDays(1));

                    var kVCTPTCs_VND = kVCTPTCs.Where(x => x.LoaiTien == "VND" && x.MaCn == maCn);
                    var kVCTPTCs_ThuDoiNgoaiTe = kVCTPTCs.Where(y => y.TKNo.StartsWith("11120000") && y.TKCo == "1111000000");

                    kVCTPTCs = kVCTPTCs_VND.Concat(kVCTPTCs_ThuDoiNgoaiTe);
                    //list = kVCTPTCs.Where(x => x.TKNo.StartsWith("11120000") && x.TKCo == "1111000000").ToList();
                    list = kVCTPTCs.OrderBy(x => x.SoCT.Substring(0, 4)).ToList();
                    list = list.OrderBy(x => x.SoCT.Substring(4, 2)).ToList();
                    //list = list.OrderBy(x => x.KVPTCId.Substring(6, 4)).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(searchFromDate)) // NgayCT
                {
                    try
                    {
                        fromDate = DateTime.Parse(searchFromDate);
                        //list = list.Where(x => x.NgayCT >= fromDate).ToList();
                        var kVCTPTCs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, y => y.KVPTC.NgayCT >= fromDate);
                        list = kVCTPTCs.Where(x => x.LoaiTien == "VND" && x.MaCn == maCn).ToList();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                if (!string.IsNullOrEmpty(searchToDate)) // NgayCT
                {
                    try
                    {
                        toDate = DateTime.Parse(searchToDate);
                        var kVCTPTCs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, y => y.KVPTC.NgayCT < toDate.AddDays(1));
                        list = kVCTPTCs.Where(x => x.LoaiTien == "VND" && x.MaCn == maCn).ToList();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
            // search date

            return list;
        }

        public List<KVCTPCT_Model_GroupBy_SoCT> KVCTPTC_Model_GroupBy_SoCTs(IEnumerable<KVCTPTC> kVCTPTCs)
        {
            var result1 = (from p in kVCTPTCs
                           group p by p.SoCT into g
                           select new KVCTPCT_Model_GroupBy_SoCT()
                           {
                               SoCT = g.Key,
                               KVCTPTCs = g.ToList()
                           }).ToList();
            foreach (var item in result1)
            {
                item.TongCong = item.KVCTPTCs.Sum(x => x.SoTien.Value);
            }
            decimal congPhatSinh_Thu = 0, congPhatSinh_Chi = 0;
            foreach (var item in result1)
            {
                //item.CongPhatSinh_Thu += item.KVCTPTCs.Where(x => x.KVPCT.MFieu == "T").Sum(x => x.SoTien);
                if (item.SoCT.Contains("QT"))
                {
                    congPhatSinh_Thu += item.TongCong;
                }
                else
                {
                    congPhatSinh_Chi += item.TongCong;
                }
            }
            foreach (var item in result1)
            {
                item.CongPhatSinh_Thu = congPhatSinh_Thu;
                item.CongPhatSinh_Chi = congPhatSinh_Chi;
            }

            return result1;
        }

        public List<KVCTPTC_NT_GroupBy_SoCTs> KVCTPTC_NT_GroupBy_SoCTs(IEnumerable<KVCTPTC> kVCTPTCs)
        {
            var result1 = (from p in kVCTPTCs
                           group p by p.SoCT into g
                           select new KVCTPTC_NT_GroupBy_SoCTs()
                           {
                               SoCT = g.Key,
                               KVCTPTCs = g.ToList()
                           }).ToList();
            foreach (var item in result1)
            {
                if (item.SoCT.Contains("NT"))
                {
                    item.TongCong_Thu = item.KVCTPTCs.Sum(x => x.SoTien.Value);
                    item.TongCong_Thu_NT = item.KVCTPTCs.Sum(x => x.SoTienNT.Value);
                }
                else
                {
                    item.TongCong_Chi = item.KVCTPTCs.Sum(x => x.SoTien.Value);
                    item.TongCong_Chi_NT = item.KVCTPTCs.Sum(x => x.SoTienNT.Value);
                }
            }
            //decimal congPhatSinh_Thu = 0, congPhatSinh_Chi = 0, congPhatSinh_Thu_NT = 0, congPhatSinh_Chi_NT = 0;
            //foreach (var item in result1)
            //{
            //    //item.CongPhatSinh_Thu += item.KVCTPTCs.Where(x => x.KVPCT.MFieu == "T").Sum(x => x.SoTien);
            //    if (item.SoCT.Contains("NT"))
            //    {
            //        //congPhatSinh_Thu_NT += item.KVCTPTCs.Sum(x => x.SoTienNT).Value;
            //        //congPhatSinh_Thu += item.KVCTPTCs.Sum(x => x.SoTien).Value;
            //        congPhatSinh_Thu_NT += item.TongCong_Thu_NT;
            //        congPhatSinh_Thu += item.TongCong_Thu;
            //    }
            //    else
            //    {
            //        //congPhatSinh_Chi_NT += item.KVCTPTCs.Sum(x => x.SoTienNT).Value;
            //        //congPhatSinh_Chi += item.KVCTPTCs.Sum(x => x.SoTien).Value;
            //        congPhatSinh_Chi_NT += item.TongCong_Chi_NT;
            //        congPhatSinh_Chi += item.TongCong_Chi;
            //    }
            //}
            //foreach (var item in result1)
            //{
            //    item.CongPhatSinh_Thu_NT = congPhatSinh_Thu_NT;
            //    item.CongPhatSinh_Thu = congPhatSinh_Thu;
            //    item.CongPhatSinh_Chi_NT = congPhatSinh_Chi_NT;
            //    item.CongPhatSinh_Chi = congPhatSinh_Chi;
            //}

            return result1;
        }

        public List<KVCTPCT_Model_GroupBy_LoaiTien> KVCTPTC_Model_GroupBy_LoaiTiens(IEnumerable<KVCTPTC> kVCTPTCs)
        {
            var result1 = (from p in kVCTPTCs
                           group p by p.LoaiTien into g
                           select new KVCTPCT_Model_GroupBy_LoaiTien()
                           {
                               LoaiTien = g.Key,
                               KVCTPTCs = g.ToList()
                           }).ToList();

            foreach (var item in result1)
            {
                item.CongPhatSinh_Thu_NT = item.KVCTPTCs.Where(x => x.SoCT.Contains("NT")).Sum(x => x.SoTienNT).Value;
                item.CongPhatSinh_Thu = item.KVCTPTCs.Where(x => x.SoCT.Contains("NT")).Sum(x => x.SoTien).Value;

                item.CongPhatSinh_Chi_NC = item.KVCTPTCs.Where(x => x.SoCT.Contains("NC")).Sum(x => x.SoTienNT).Value;
                item.CongPhatSinh_Chi = item.KVCTPTCs.Where(x => x.SoCT.Contains("NC")).Sum(x => x.SoTien).Value;
            }

            //var result1 = (from p in kVCTPTCs
            //               group p by p.LoaiTien into g
            //               select new KVCTPCT_Model_GroupBy_LoaiTien()
            //               {
            //                   LoaiTien = g.Key,
            //                   //KVCTPTCs = g.ToList()
            //                   KVCTPTC_GroupBy_SoCTs = Get_KVCTPTC_GroupBy_SoCTs(g.ToList())
            //               }).ToList();

            //foreach (var item in result1)
            //{
            //    foreach (var item1 in item.KVCTPTC_GroupBy_SoCTs)
            //    {
            //        item1.TongCong = item1.KVCTPTCs.Sum(x => x.SoTien.Value);
            //    }
            //    decimal congPhatSinh_Thu = 0, congPhatSinh_Chi = 0;
            //    foreach (var item1 in item.KVCTPTC_GroupBy_SoCTs)
            //    {
            //        //item.CongPhatSinh_Thu += item.KVCTPTCs.Where(x => x.KVPCT.MFieu == "T").Sum(x => x.SoTien);
            //        if (item1.SoCT.Contains("NT"))
            //        {
            //            congPhatSinh_Thu += item1.TongCong;
            //        }
            //        else
            //        {
            //            congPhatSinh_Chi += item1.TongCong;
            //        }
            //    }
            //    foreach (var item1 in item.KVCTPTC_GroupBy_SoCTs)
            //    {
            //        item.CongPhatSinh_Thu = congPhatSinh_Thu;
            //        item.CongPhatSinh_Chi = congPhatSinh_Chi;
            //    }
            //}

            return result1;
        }

        private IEnumerable<KVCTPCT_Model_GroupBy_SoCT> Get_KVCTPTC_GroupBy_SoCTs(List<KVCTPTC> kVCTPTCs)
        {
            return from x in kVCTPTCs
                   group x by x.SoCT into h
                   select new KVCTPCT_Model_GroupBy_SoCT()
                   {
                       SoCT = h.Key,
                       KVCTPTCs = h.ToList()
                   };
        }

        public IEnumerable<KVCTPTC> GetAll()
        {
            return _unitOfWork.kVCTPCTRepository.GetAll();
        }

        public async Task<List<KVCTPTC>> FinBy_SoCT(string soCT, string maCn)
        {
            var kVCTPTCs = await _unitOfWork.kVCTPCTRepository.FindAsync(x => x.SoCT == soCT && x.MaCn == maCn);
            return kVCTPTCs.ToList();
        }

        public async Task<Data.Models_HDVATOB.Supplier> GetSupplierSingleByCode(string maKh)
        {
            return await _unitOfWork.supplier_Hdvatob_Repository.GetSupplierById(maKh);
        }

        public async Task DeleteRangeAsync(IEnumerable<KVCTPTC> kVCTPCTs)
        {
            await _unitOfWork.kVCTPCTRepository.DeleteRangeAsync(kVCTPCTs);
        }

        public Ntbill GetNtbillBySTT(string sTT)
        {
            return _unitOfWork.ntbillRepository.Find(x => x.Stt == sTT).FirstOrDefault();
        }

        public async Task<Noptien> GetNopTienById(string soct, string macn)
        {
            return await _unitOfWork.nopTienRepository.GetById(soct, macn);
        }

        public IEnumerable<Thuchi> GetThuChiXe_By_SoPhieu(string soPhieu)
        {
            return _unitOfWork.xeRepository.Find(x => x.SoPhieu == soPhieu);
        }

        public async Task<IEnumerable<KVCTPTC>> GetKVCTPTCs_QLXe(string soPhieu, Guid kVPTCId, string soCT,
            string username, string maCn)
        {
            var thuchis = _unitOfWork.xeRepository
                .Find(x => x.SoPhieu == soPhieu.ToUpper() && x.ChiNhanh.Trim() == maCn.Trim());
            if (thuchis.Any(x => !string.IsNullOrEmpty(x.SoCtKttm)))
            {
                return new List<KVCTPTC>();
            }
            Vandoanh vandoanh = new Vandoanh();
            try
            {
                vandoanh = await _unitOfWork.xeRepository.GetVanDoanhById(thuchis.FirstOrDefault().VanDoanhId.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("vandoanhId ko tồn tại", ex.InnerException);
            }

            //string nguoiTao = username;
            //DateTime ngayTao = DateTime.Now;

            // ghi log
            string logFile = "-User kéo từ qlxe: " + username + " , số phiếu " + soPhieu + " vào lúc: " +
                System.DateTime.Now.ToString(); // user.Username

            List<KVCTPTC> kVCTPTCs = new List<KVCTPTC>();

            if (thuchis != null)
            {
                foreach (var item in thuchis)
                {
                    // string boPhan = item.;
                    // string maKh = string.IsNullOrEmpty(item.Coquan) ? "50000" : item.Coquan;
                    // var viewSupplier = GetSuppliersByCodeName(maKh, maCN).FirstOrDefault();//.Where(x => x.Code == maKh).FirstOrDefault();
                    //var viewSupplier = GetAll_KhachHangs_HDVATOB().Where(x => x.Code == maKh).FirstOrDefault();
                    // string kyHieu = "", mauSo = "", msThue = "", tenKh = "", diaChi = "";
                    //if (viewSupplier != null)
                    //{
                    //    kyHieu = viewSupplier.Taxsign;
                    //    mauSo = viewSupplier.Taxform;
                    //    msThue = viewSupplier.Taxcode;
                    //    tenKh = viewSupplier.Name;
                    //    diaChi = viewSupplier.Address;
                    //}
                    //var ctbills = _unitOfWork.ctbillRepository.Find(x => x.Idntbill == item.Idntbill);
                    //var ctbills_TienMat = ctbills.Where(x => string.IsNullOrEmpty(x.Cardnumber) && string.IsNullOrEmpty(x.Loaicard));
                    //var ctbills_TTThe = ctbills.Except(ctbills_TienMat);

                    string dienGiaiP = "CHI NL + CTP " + (vandoanh.MaDoan ?? "") + " từ ngày " +
                        (vandoanh.NgayDon.Value.ToString("dd/MM/yyyy") ?? "") + " đến ngày " +
                        (vandoanh.DenNgay.Value.ToString("dd/MM/yyyy") ?? "");

                    var chiphiXe = await _unitOfWork.xeRepository.GetChiPhiXeById(item.KhoanMuc); // item.KhoanMuc == macp

                    var loaiHDGocVM = GetLoaiCT_By_MaCp(chiphiXe.Macp).FirstOrDefault();// item.Loaihd; // ??
                    //var soCTGoc = "N/A"; //item.Bill ?? item.Stt;// item.Stt; // thao
                    //var ngayCTGoc = DateTime.Now;

                    KVCTPTC kVCTPTC = new KVCTPTC();
                    kVCTPTC.STT = item.Stt.ToString().Trim();
                    kVCTPTC.KhoangMuc = item.KhoanMuc.Trim(); // KhoanMuc ben [thuchi] (qlxe) / kVCTPTC.KhoangMuc: chi danh cho qlxe

                    // THONG TIN VE TAI CHINH
                    kVCTPTC.KVPTCId = kVPTCId;
                    kVCTPTC.SoCT = soCT;
                    kVCTPTC.MaCn = maCn;
                    kVCTPTC.DienGiaiP = dienGiaiP;
                    kVCTPTC.SoTienNT = item.SoTien.HasValue ? (decimal)item.SoTien.Value : 0;
                    kVCTPTC.LoaiTien = "VND";
                    kVCTPTC.TyGia = 1;
                    kVCTPTC.SoTien = item.SoTien.HasValue ? (decimal)item.SoTien.Value : 0;

                    // THONG TIN VE CONG NO DOAN // phieu chi

                    //var dienGiai = Get_DienGiai_By_TkNo_TkCo(loaiHDGocVM.TKNo, "1111000000").FirstOrDefault();
                    //kVCTPTC.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                    kVCTPTC.TKNo = loaiHDGocVM.TKNo.Trim();
                    kVCTPTC.TKCo = "1111000000";
                    //kVCTPTC.MaKhNo = "";
                    //kVCTPTC.NoQuay = "";

                    var sgtcode = vandoanh.MaDoan;
                    kVCTPTC.Sgtcode = string.IsNullOrEmpty(sgtcode) ? "" : vandoanh.MaDoan.Trim();// item1.Sgtcode;
                                                                                                  //kVCTPTC.CardNumber = item1.Cardnumber;
                                                                                                  //kVCTPTC.SalesSlip = "";// item1.Saleslip;

                    //// THONG TIN VE THUE
                    //kVCTPTC.SoCTGoc = soCTGoc;
                    //kVCTPTC.NgayCTGoc = ngayBill;

                    kVCTPTC.DSKhongVAT = 0;
                    kVCTPTC.VAT = 0;

                    //kVCTPTC.KyHieu = kyHieu;
                    //kVCTPTC.MauSoHD = mauSo;
                    //kVCTPTC.MsThue = msThue;
                    //kVCTPTC.MaKh = maKh;
                    //kVCTPTC.TenKH = tenKh;
                    //kVCTPTC.DiaChi = diaChi;

                    kVCTPTC.HTTC = loaiHDGocVM.HTTC.Trim();
                    kVCTPTC.DienGiai = loaiHDGocVM.DienGiai.Trim();
                    kVCTPTC.LoaiHDGoc = loaiHDGocVM.LoaiCTU.Trim();
                    kVCTPTC.BoPhan = loaiHDGocVM.BoPhan.Trim();// "XE";
                    kVCTPTC.SoXe = vandoanh.SoXe;// item.SoXe;

                    kVCTPTC.NguoiTao = username;
                    kVCTPTC.NgayTao = DateTime.Now;
                    kVCTPTC.LogFile = logFile;

                    kVCTPTCs.Add(kVCTPTC);
                }
            }
            else return null;

            return kVCTPTCs;
        }

        private List<ListViewModel> GetLoaiCT_By_MaCp(string maCP)
        {
            List<ListViewModel> viewModels = new List<ListViewModel>();
            ListViewModel viewModel = new ListViewModel();
            switch (maCP)
            {
                case "001":
                case "002":
                case "035":
                case "058":
                case "059":
                    viewModel.HTTC = "CBU";
                    viewModel.TKNo = "6278348050";
                    var dgiais = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000");
                    viewModel.DienGiai = dgiais.FirstOrDefault().DienGiai;
                    viewModel.LoaiCTU = "VAT";
                    viewModel.BoPhan = "XE";
                    viewModels.Add(viewModel);
                    break;

                case "003":
                case "004":
                case "009":
                case "022":
                case "031":
                case "054":
                case "056":
                case "057":
                case "060":
                    viewModel.HTTC = "CBU";
                    viewModel.TKNo = "6278348160";
                    viewModel.DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348160", "1111000000")
                        .FirstOrDefault().DienGiai;
                    viewModel.LoaiCTU = "KHD";
                    viewModel.BoPhan = "XE";
                    viewModels.Add(viewModel);
                    break;

                case "005":
                    viewModel.HTTC = "CBU";
                    viewModel.TKNo = "6278348140";
                    viewModel.DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348140", "1111000000")
                        .Where(x => x.DienGiai.Contains("XE")).FirstOrDefault().DienGiai; // PHI GUI XE
                    viewModel.LoaiCTU = "KHD";
                    viewModel.BoPhan = "XE";
                    viewModels.Add(viewModel);
                    break;

                case "007":
                case "008":
                case "020":
                case "048":
                case "049":
                case "055":
                    viewModel.HTTC = "CBU";
                    viewModel.TKNo = "6278348140";
                    viewModel.DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348140", "1111000000")
                        .Where(x => x.DienGiai.Contains("PHA")).FirstOrDefault().DienGiai; // PHI CAU PHA
                    viewModel.LoaiCTU = "KHD"; // thao
                    viewModel.BoPhan = "XE";
                    viewModels.Add(viewModel);
                    break;

                default: // maCP == "" || null

                    // "001""002""035""058""059"
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "001",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348050",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "VAT",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "002",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348050",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "VAT",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "035",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348050",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "VAT",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "058",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348050",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "VAT",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "059",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348050",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "VAT",
                        BoPhan = "XE"
                    });

                    // "003""004""009""022""031""054""056""057""060"
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "003",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348160",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "004",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348160",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "009",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348160",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "022",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348160",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "031",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348160",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "054",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348160",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "056",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348160",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "057",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348160",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "060",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348160",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348050", "1111000000").FirstOrDefault().DienGiai,
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });

                    // "005"
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "005",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348140",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348140", "1111000000")
                        .Where(x => x.DienGiai.Contains("PHA")).FirstOrDefault().DienGiai, // PHA
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });

                    // "007""008""020""048""049""055"
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "007",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348140",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348140", "1111000000")
                        .Where(x => x.DienGiai.Contains("PHA")).FirstOrDefault().DienGiai, // PHA
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "008",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348140",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348140", "1111000000")
                     .Where(x => x.DienGiai.Contains("PHA")).FirstOrDefault().DienGiai, // PHA
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "020",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348140",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348140", "1111000000")
                        .Where(x => x.DienGiai.Contains("PHA")).FirstOrDefault().DienGiai, // PHA
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "048",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348140",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348140", "1111000000")
                        .Where(x => x.DienGiai.Contains("PHA")).FirstOrDefault().DienGiai, // PHA
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "049",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348140",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348140", "1111000000")
                        .Where(x => x.DienGiai.Contains("PHA")).FirstOrDefault().DienGiai, // PHA
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });
                    viewModels.Add(new ListViewModel()
                    {
                        MaCP = "055",
                        TenChiPhi = _unitOfWork.xeRepository.GetChiPhiXeById("001").GetAwaiter().GetResult().Tenchiphi,
                        HTTC = "CBU",
                        TKCo = "6278348510",
                        DienGiai = Get_DienGiai_By_TkNo_TkCo("6278348510", "1111000000").FirstOrDefault().DienGiai,
                        //.Where(x => x.DienGiai.Contains("PHA")).FirstOrDefault().DienGiai, // PHA
                        LoaiCTU = "KHD",
                        BoPhan = "XE"
                    });

                    break;
            }

            return viewModels;
        }

        public IEnumerable<Thuchi> GetThuChiXe_By_SoCT_KTTM(string soCTKTTM)
        {
            return _unitOfWork.xeRepository.Find(x => x.SoCtKttm == soCTKTTM);
        }

        public IEnumerable<NgoaiTe> GetAll_NgoaiTes_DanhMucKT()
        {
            return _unitOfWork.ngoaiTe_DanhMucKT_Repository.GetAll();
        }

        public List<KVCTPTC> FindByMaCN(string maCn)
        {
            return _unitOfWork.kVCTPCTRepository.Find(x => x.MaCn == maCn).ToList();
        }

        public async Task<IPagedList<KVCTPTC>> ListThuHo(string searchString, string searchFromDate, string searchToDate, int? page, string macn)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            List<KVCTPTC> list = new List<KVCTPTC>();
            if (macn == "STN") // sts chay thuho tu noidia : 0310891532051
            {
                var kVCTPTCs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(y => y.KVPTC, x => (x.TKCo == "1368000000" && x.TKNo == "1111000000" && x.MaKhCo == "0310891532051" && x.MaCn == "STS") ||
                (x.TKNo == "1368000000" && x.TKCo == "1111000000" && x.MaKhNo == "0310891532051" && x.MaCn == "STS"));
                list = kVCTPTCs.ToList();
            }
            if (macn == "STS") // noidia chay thuho tu sts : 0310891532
            {
                var kVCTPTCs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(y => y.KVPTC, x => (x.TKCo == "1368000000" && x.TKNo == "1111000000" && x.MaKhCo == "0310891532" && x.MaCn == "STN") ||
                (x.TKNo == "1368000000" && x.TKCo == "1111000000" && x.MaKhCo == "0310891532" && x.MaCn == "STN"));
                list = kVCTPTCs.ToList();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => !string.IsNullOrEmpty(x.Sgtcode) && x.Sgtcode.ToLower().Contains(searchString.ToLower()) ||
                !string.IsNullOrEmpty(x.SoCTGoc) && x.SoCTGoc.ToLower().Contains(searchString.ToLower())).ToList();
            }

            // search date
            DateTime fromDate, toDate;
            if (!string.IsNullOrEmpty(searchFromDate) && !string.IsNullOrEmpty(searchToDate))
            {
                try
                {
                    fromDate = DateTime.Parse(searchFromDate); // NgayCT
                    toDate = DateTime.Parse(searchToDate); // NgayCT

                    if (fromDate > toDate)
                    {
                        return null; //
                    }

                    list = list.Where(x => x.KVPTC.NgayCT >= fromDate &&
                                       x.KVPTC.NgayCT < toDate.AddDays(1)).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(searchFromDate)) // NgayCT
                {
                    try
                    {
                        fromDate = DateTime.Parse(searchFromDate);
                        list = list.Where(x => x.KVPTC.NgayCT >= fromDate).ToList();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                if (!string.IsNullOrEmpty(searchToDate)) // NgayCT
                {
                    try
                    {
                        toDate = DateTime.Parse(searchToDate);
                        list = list.Where(x => x.KVPTC.NgayCT < toDate.AddDays(1)).ToList();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
            // search date
            //return list;

            // page the list
            const int pageSize = 10;
            decimal aa = (decimal)list.Count() / (decimal)pageSize;
            var bb = Math.Ceiling(aa);
            if (page > bb)
            {
                page--;
            }
            page = (page == 0) ? 1 : page;
            var listPaged = list.ToPagedList(page ?? 1, pageSize);
            //if (page > listPaged.PageCount)
            //    page--;
            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;

            return listPaged;
        }

        public async Task<IEnumerable<KVCTPTC>> ExportThuHo(string searchString, string searchFromDate, string searchToDate, string macn)
        {
            List<KVCTPTC> list = new List<KVCTPTC>();
            if (macn == "STN") // sts chay thuho tu noidia : 0310891532051
            {
                var kVCTPTCs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(y => y.KVPTC, x => (x.TKCo == "1368000000" && x.TKNo == "1111000000" && x.MaKhCo == "0310891532051" && x.MaCn == "STS") ||
                (x.TKNo == "1368000000" && x.TKCo == "1111000000" && x.MaKhNo == "0310891532051" && x.MaCn == "STS"));
                list = kVCTPTCs.ToList();
            }
            if (macn == "STS") // noidia chay thuho tu sts : 0310891532
            {
                var kVCTPTCs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(y => y.KVPTC, x => (x.TKCo == "1368000000" && x.TKNo == "1111000000" && x.MaKhCo == "0310891532" && x.MaCn == "STN") ||
                (x.TKNo == "1368000000" && x.TKCo == "1111000000" && x.MaKhCo == "0310891532" && x.MaCn == "STN"));
                list = kVCTPTCs.ToList();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => !string.IsNullOrEmpty(x.Sgtcode) && x.Sgtcode.ToLower().Contains(searchString.ToLower()) ||
                !string.IsNullOrEmpty(x.SoCTGoc) && x.SoCTGoc.ToLower().Contains(searchString.ToLower())).ToList();
            }

            // search date
            DateTime fromDate, toDate;
            if (!string.IsNullOrEmpty(searchFromDate) && !string.IsNullOrEmpty(searchToDate))
            {
                try
                {
                    fromDate = DateTime.Parse(searchFromDate); // NgayCT
                    toDate = DateTime.Parse(searchToDate); // NgayCT

                    if (fromDate > toDate)
                    {
                        return null; //
                    }

                    list = list.Where(x => x.KVPTC.NgayCT >= fromDate &&
                                       x.KVPTC.NgayCT < toDate.AddDays(1)).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(searchFromDate)) // NgayCT
                {
                    try
                    {
                        fromDate = DateTime.Parse(searchFromDate);
                        list = list.Where(x => x.KVPTC.NgayCT >= fromDate).ToList();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                if (!string.IsNullOrEmpty(searchToDate)) // NgayCT
                {
                    try
                    {
                        toDate = DateTime.Parse(searchToDate);
                        list = list.Where(x => x.KVPTC.NgayCT < toDate.AddDays(1)).ToList();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
            // search date

            return list;
        }

        public async Task<KVCTPTC> GetByIdIncludeKVPTC(long id)
        {
            var kVCTPTCs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, y => y.Id == id);
            return kVCTPTCs.FirstOrDefault();
        }

        public IEnumerable<DmTk> Get1411()
        {
            return _unitOfWork.dmTkRepository.GetAll().Where(x => x.Id == 3524); //3524: 1411
        }

        public IEnumerable<DmTk> Get1412()
        {
            return _unitOfWork.dmTkRepository.GetAll().Where(x => x.Id == 3525); //3525: 1411
        }

        public IEnumerable<DmTk> Get1111000000()
        {
            return _unitOfWork.dmTkRepository.GetAll().Where(x => x.Id == 3429); //3429: 1111000000
        }

        //public async Task<KhachHang> GetKhachHangById(string maKhNo)
        //{
        //    return await _unitOfWork.khachHang_DanhMucKTRepository.GetKhachHangById(maKhNo);
        //}
    }
}