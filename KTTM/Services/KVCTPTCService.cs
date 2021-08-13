﻿using Data.Models_Cashier;
using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.Models_QLTour;
using Data.Repository;
using Data.ViewModels;
using KTTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTTM.Services
{
    public interface IKVCTPTCService
    {
        Task<IEnumerable<KVCTPTC>> List_KVCTPCT_By_SoCT(string soCT);
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
        IEnumerable<KVCTPTC> GetKVCTPCTs(string baoCaoSo, string soCT, string username, string maCN, string loaiPhieu, string tk); // noptien => two keys  
        Task CreateRange(IEnumerable<KVCTPTC> kVCTPCTs);
        IEnumerable<DmTk> GetAll_DmTk_Cashier(); IEnumerable<DmTk> GetAll_DmTk_TienMat();
        IEnumerable<KVCTPTC> GetAll();
        Task<KVCTPTC> GetById(long id);
        IEnumerable<Data.Models_HDVATOB.Supplier> GetAll_KhachHangs_HDVATOB();
        IEnumerable<Data.Models_HDVATOB.Supplier> GetSuppliersByCode(string code, string maCn);
        IEnumerable<Data.Models_HDVATOB.Supplier> GetSuppliersByCodeName(string code, string maCn);
        IEnumerable<Dgiai> GetAll_DienGiai();
        KVCTPTC GetBySoCTAsNoTracking(long id);
        Task UpdateAsync(KVCTPTC kVCTPCT);
        Task UpdateAsync_NopTien(Noptien noptien);
        Task DeleteAsync(KVCTPTC kVCTPCT);
        IEnumerable<ListViewModel> LoaiHDGocs();
        string AutoSgtcode(string param);
        Task<KVCTPTC> FindByIdInclude(long kVCTPCTId_PhieuTC);
        Task<IEnumerable<KVCTPTC>> FinByDate(string searchFromDate, string searchToDate);
        List<KVCTPCT_Model_GroupBy_SoCT> KVCTPCT_Model_GroupBy_SoCTs(IEnumerable<KVCTPTC> kVCTPCTs);
    }
    public class KVCTPTCService : IKVCTPTCService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KVCTPTCService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<KVCTPTC>> List_KVCTPCT_By_SoCT(string soCT)
        {
            return await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, x => x.KVPTCId == soCT);
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
            var dgiais = _unitOfWork.dGiaiRepository.GetAll();
            tkNo ??= "";
            tkCo ??= "";
            var dgiais1 = dgiais.Where(x => x.Tkno.Trim() == tkNo.Trim() && x.Tkco.Trim() == tkCo.Trim());
            //dgiais1 ??= null;
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

        public IEnumerable<KVCTPTC> GetKVCTPCTs(string baoCaoSo, string soCT, string username, string maCN, string loaiPhieu, string tk) // noptien => two keys
        {

            var ntbills = _unitOfWork.ntbillRepository.Find(x => x.Soct == baoCaoSo && x.Chinhanh == maCN);

            string nguoiTao = username;
            DateTime ngayTao = DateTime.Now;

            // ghi log
            string logFile = "-User kéo từ cashier: " + username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            List<KVCTPTC> kVCTPCTs = new List<KVCTPTC>();

            if (ntbills != null)
            {

                foreach (var item in ntbills)
                {
                    string boPhan = item.Bophan;
                    string maKh = item.Coquan;
                    var viewSupplier = GetAll_KhachHangs_HDVATOB().Where(x => x.Code == maKh).FirstOrDefault();
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

                    string dienGiaiP = loaiPhieu == "T" ? "THU BILL " + item.Bill : "CHI BILL " + item.Bill; // ??
                    var loaiHDGoc = "VAT";// item.Loaihd; // ??
                    var soCTGoc = item.Bill; // ??
                    var ngayBill = item.Ngaybill; // ??

                    foreach (var item1 in ctbills)
                    {
                        KVCTPTC kVCTPCT = new KVCTPTC();

                        // THONG TIN VE TAI CHINH
                        kVCTPCT.KVPTCId = soCT;
                        kVCTPCT.DienGiaiP = dienGiaiP;
                        kVCTPCT.SoTienNT = item1.Sotiennt;
                        kVCTPCT.LoaiTien = item1.Loaitien;
                        kVCTPCT.TyGia = item1.Tygia;
                        kVCTPCT.SoTien = item1.Sotien;

                        // THONG TIN VE CONG NO DOAN
                        if (loaiPhieu == "T")
                        {
                            var dienGiai = Get_DienGiai_By_TkNo_TkCo("1111000000", tk).FirstOrDefault(); // chac chan tien mat : 1111000000
                            kVCTPCT.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                            kVCTPCT.TKNo = "1111000000";
                            kVCTPCT.TKCo = tk;
                            kVCTPCT.MaKhCo = maKh;
                            kVCTPCT.CoQuay = boPhan;
                        }
                        else
                        {
                            var dienGiai = Get_DienGiai_By_TkNo_TkCo(tk, "1111000000").FirstOrDefault();
                            kVCTPCT.DienGiai = dienGiai == null ? "" : dienGiai.DienGiai;
                            kVCTPCT.TKNo = tk;
                            kVCTPCT.TKCo = "1111000000";
                            kVCTPCT.MaKhNo = maKh;
                            kVCTPCT.NoQuay = boPhan;
                        }

                        kVCTPCT.BoPhan = boPhan;
                        kVCTPCT.Sgtcode = item1.Sgtcode;
                        kVCTPCT.CardNumber = item1.Cardnumber;
                        kVCTPCT.SalesSlip = item1.Saleslip;

                        // THONG TIN VE THUE
                        kVCTPCT.LoaiHDGoc = loaiHDGoc;
                        kVCTPCT.SoCTGoc = soCTGoc;
                        kVCTPCT.NgayCTGoc = ngayBill;

                        kVCTPCT.KyHieu = kyHieu;
                        kVCTPCT.MauSoHD = mauSo;
                        kVCTPCT.MsThue = msThue;
                        kVCTPCT.MaKh = maKh;
                        kVCTPCT.TenKH = tenKh;
                        kVCTPCT.DiaChi = diaChi;

                        kVCTPCT.NguoiTao = nguoiTao;
                        kVCTPCT.NgayTao = ngayTao;
                        kVCTPCT.LogFile = logFile;

                        kVCTPCTs.Add(kVCTPCT);
                    }

                }

            }
            return kVCTPCTs;
        }

        public async Task CreateRange(IEnumerable<KVCTPTC> kVCTPCTs)
        {
            await _unitOfWork.kVCTPCTRepository.CreateRange(kVCTPCTs);
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

        public IEnumerable<DmTk> GetAll_DmTk_TienMat()
        {
            var dmTks = GetAll_DmTk().Where(x => x.Tkhoan.StartsWith("111"));
            List<DmTk> dmTks1 = new List<DmTk>();
            foreach (var item in dmTks)
            {
                dmTks1.Add(new DmTk() { Tkhoan = item.Tkhoan });
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

        public IEnumerable<DmTk> GetAll_DmTk_TaiKhoan()
        {
            var dmTks = GetAll_DmTk();
            List<DmTk> dmTks1 = new List<DmTk>();
            foreach (var item in dmTks)
            {
                dmTks1.Add(new DmTk() { Tkhoan = item.Tkhoan });
            }
            return dmTks1;
        }

        public async Task UpdateAsync_NopTien(Noptien noptien)
        {
            await _unitOfWork.nopTienRepository.UpdateAsync(noptien);

        }

        public IEnumerable<Data.Models_HDVATOB.Supplier> GetSuppliersByCode(string code, string maCn)
        {
            code ??= "";
            var suppliers = _unitOfWork.supplier_Hdvatob_Repository.Find(x => x.Code.Trim().ToLower().Contains(code.Trim().ToLower()) && x.Chinhanh == maCn).ToList();
            
            return suppliers;
        }

        public IEnumerable<Data.Models_HDVATOB.Supplier> GetSuppliersByCodeName(string code, string maCn)
        {
            code ??= "";
            var suppliers = _unitOfWork.supplier_Hdvatob_Repository.Find(x => x.Code.Trim().ToLower().Contains(code.Trim().ToLower()) ||
                                                                              (!string.IsNullOrEmpty(x.Name) && x.Name.Trim().ToLower().Contains(code.Trim().ToLower()))).ToList();
            suppliers = suppliers.Where(x => x.Chinhanh == maCn).ToList();
            return suppliers;
        }

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
            var kVCTPCTs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, y => y.Id == kVCTPCTId_PhieuTC);
            return kVCTPCTs.FirstOrDefault();
        }

        public async Task<IEnumerable<KVCTPTC>> FinByDate(string searchFromDate, string searchToDate)
        {

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

                    var kVCTPCTs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, y => y.KVPTC.NgayCT >= fromDate &&
                                       y.KVPTC.NgayCT < toDate.AddDays(1));
                    list = kVCTPCTs.Where(x => x.LoaiTien == "VND").ToList();

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
                        var kVCTPCTs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, y => y.KVPTC.NgayCT >= fromDate);
                        list = kVCTPCTs.Where(x => x.LoaiTien == "VND").ToList();
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
                        var kVCTPCTs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, y => y.KVPTC.NgayCT < toDate.AddDays(1));
                        list = kVCTPCTs.Where(x => x.LoaiTien == "VND").ToList();
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

        public List<KVCTPCT_Model_GroupBy_SoCT> KVCTPCT_Model_GroupBy_SoCTs(IEnumerable<KVCTPTC> kVCTPCTs)
        {

            var result1 = (from p in kVCTPCTs
                           group p by p.KVPTCId into g
                           select new KVCTPCT_Model_GroupBy_SoCT()
                           {
                               SoCT = g.Key,
                               KVCTPCTs = g.ToList()
                           }).ToList();
            foreach (var item in result1)
            {
                item.TongCong = item.KVCTPCTs.Sum(x => x.SoTien.Value);
                
            }
            decimal congPhatSinh_Thu = 0, congPhatSinh_Chi = 0;
            foreach (var item in result1)
            {
                //item.CongPhatSinh_Thu += item.KVCTPCTs.Where(x => x.KVPCT.MFieu == "T").Sum(x => x.SoTien);
                if(item.SoCT.Contains("T"))
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

        public IEnumerable<KVCTPTC> GetAll()
        {
            return _unitOfWork.kVCTPCTRepository.GetAll();
        }
    }
}