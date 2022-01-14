using Data.Dtos;
using Data.Models_HDVATOB;
using Data.Models_KTTM;
using Data.Repository;
using Data.Utilities;
using KTTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Services
{
    public interface ITT621Service
    {
        Task<IEnumerable<TT621>> GetTT621s_By_TamUng(long tamUngId);

        Task DeleteRangeAsync(IEnumerable<TT621> tT621s);

        Task CreateAsync(TT621 tT621);

        Task UpdateAsync(TT621 tT621);

        Task DeleteAsync(TT621 tT621);

        TT621 GetDummyTT621_By_KVCTPCT(long tamUngId);

        IEnumerable<TT621> FindByTamUngId(long tamUngId);

        string GetSoCT(string param, string maCn);

        decimal GetSoTienNT_TrongTT621_TheoTamUng(long tamUngId);

        Task<TT621> FindById_Include(long id);

        TT621 GetByIdAsNoTracking(long tt621Id);

        decimal Get_SoTienNT_CanKetChuyen(long tamUngId, decimal soTienNT_Tren_TT621Create, string loaiPhieu);

        Task<TT621Dto> ConvertTT621ToTT621Dto(TT621 tT621);

        IEnumerable<VSupplierTaiKhoan> GetSuppliersByCode(string code, string maCn);

        IEnumerable<TT621> GetAll();

        IEnumerable<TT621> FindTT621s_IncludeTwice_By_Date(string searchFromDate, string searchToDate, string maCn, string maKhCo, string username);

        IEnumerable<TT621> FindTT621s_IncludeTwice(long tamUngId);

        Task CreateRangeAsync(List<TT621> tT621s);

        Task<List<TT621>> GetByPhieuTC(string soCT, string maCn);

        IEnumerable<TT621GroupBy_PhieuTC> GroupBy_PhieuTC(IEnumerable<TT621> tT621s);
    }

    public class TT621Service : ITT621Service
    {
        private readonly IUnitOfWork _unitOfWork;

        public TT621Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(TT621 tT621)
        {
            _unitOfWork.tT621Repository.Create(tT621);
            await _unitOfWork.Complete();
        }

        public IEnumerable<TT621> FindByTamUngId(long tamUngId)
        {
            return _unitOfWork.tT621Repository.GetAllAsNoTracking().Where(y => y.TamUngId == tamUngId);
        }

        public TT621 GetDummyTT621_By_KVCTPCT(long kVCTPCTId)
        {
            var kVCTPCT = _unitOfWork.kVCTPCTRepository.GetById(kVCTPCTId);
            TT621 tT621 = new TT621
            {
                DienGiaiP = kVCTPCT.DienGiaiP,
                HTTC = kVCTPCT.HTTC,
                SoTienNT = kVCTPCT.SoTienNT.Value,
                LoaiTien = kVCTPCT.LoaiTien,
                TyGia = kVCTPCT.TyGia.Value,
                SoTien = kVCTPCT.SoTien.Value,
                TKNo = kVCTPCT.TKNo,
                TKCo = kVCTPCT.TKCo,
                DienGiai = kVCTPCT.DienGiai,
                MaKhNo = kVCTPCT.MaKhNo,
                MaKhCo = kVCTPCT.MaKhCo,
                NoQuay = kVCTPCT.NoQuay,
                CoQuay = kVCTPCT.CoQuay,
                BoPhan = kVCTPCT.BoPhan,
                Sgtcode = kVCTPCT.Sgtcode,
                SoXe = kVCTPCT.SoXe,
                LoaiHDGoc = kVCTPCT.LoaiHDGoc,
                SoCTGoc = kVCTPCT.SoCTGoc,
                NgayCTGoc = kVCTPCT.NgayCTGoc,
                KyHieu = kVCTPCT.KyHieu,
                MauSoHD = kVCTPCT.MauSoHD,
                MsThue = kVCTPCT.MsThue,
                VAT = kVCTPCT.VAT.Value,
                DSKhongVAT = kVCTPCT.DSKhongVAT,
                DieuChinh = kVCTPCT.DieuChinh,
                TenKH = kVCTPCT.TenKH,
                DiaChi = kVCTPCT.DiaChi,
                MatHang = kVCTPCT.MatHang,
                HoaDonDT = kVCTPCT.HoaDonDT
            };

            return tT621;
        }

        public async Task<IEnumerable<TT621>> GetTT621s_By_TamUng(long tamUngId)
        {
            return await _unitOfWork.tT621Repository.FindIncludeOneAsync(x => x.TamUng, y => y.TamUngId == tamUngId);
        }

        public async Task DeleteRangeAsync(IEnumerable<TT621> tT621s)
        {
            await _unitOfWork.tT621Repository.DeleteRangeAsync(tT621s);
        }

        public async Task<TT621Dto> ConvertTT621ToTT621Dto(TT621 tT621)
        {
            var kVPCTId = _unitOfWork.kVCTPCTRepository.GetById(tT621.TamUngId).KVPTCId;
            var kVPCT = await _unitOfWork.kVPCTRepository.GetByGuidIdAsync(kVPCTId);
            TT621Dto tT621Dto = new TT621Dto()
            {
                BoPhan = tT621.BoPhan,
                CoQuay = tT621.CoQuay,
                DiaChi = tT621.DiaChi,
                DienGiai = tT621.DienGiai,
                DienGiaiP = tT621.DienGiaiP,
                DieuChinh = tT621.DieuChinh,
                DSKhongVAT = tT621.DSKhongVAT,
                GhiSo = tT621.GhiSo,
                HoaDonDT = tT621.HoaDonDT,
                HTTC = tT621.HTTC,
                Id = tT621.Id,
                KyHieu = tT621.KyHieu,
                KyHieuHD = tT621.KyHieuHD,
                LapPhieu = tT621.LapPhieu,
                LoaiHDGoc = tT621.LoaiHDGoc,
                LoaiPhieu = kVPCT.MFieu,
                LoaiTien = tT621.LoaiTien,
                LogFile = tT621.LogFile,
                MaKhCo = tT621.MaKhCo,
                MaKhNo = tT621.MaKhNo,
                MatHang = tT621.MatHang,
                MauSoHD = tT621.MauSoHD,
                MsThue = tT621.MsThue,
                NgayCT = tT621.NgayCT,
                NgayCTGoc = tT621.NgayCTGoc,
                NgaySua = tT621.NgaySua,
                NgayTao = tT621.NgayTao,
                NguoiSua = tT621.NguoiSua,
                NguoiTao = tT621.NguoiTao,
                NoQuay = tT621.NoQuay,
                PhieuTC = tT621.PhieuTC,
                PhieuTU = tT621.PhieuTU,
                Sgtcode = tT621.Sgtcode,
                SoCT = tT621.SoCT,
                SoCTGoc = tT621.SoCTGoc,
                SoTien = tT621.SoTien,
                SoTienNT = tT621.SoTienNT,
                SoXe = tT621.SoXe,
                TamUngId = tT621.TamUngId,
                TenKH = tT621.TenKH,
                TKCo = tT621.TKCo,
                TKNo = tT621.TKNo,
                TyGia = tT621.TyGia,
                VAT = tT621.VAT
            };

            return tT621Dto;
        }

        public async Task UpdateAsync(TT621 tT621)
        {
            _unitOfWork.tT621Repository.Update(tT621);
            await _unitOfWork.Complete();
        }

        public string GetSoCT(string param, string maCn)
        {
            //DateTime dateTime;
            //dateTime = DateTime.Now;
            //dateTime = TourVM.Tour.NgayKyHopDong.Value;

            var currentYear = DateTime.Now.Year; // ngay hien tai
            var subfix = param + currentYear.ToString(); // TN2015?  TV2015?
            //var tT621 = _unitOfWork.tT621Repository.GetAllAsNoTracking().OrderByDescending(x => x.SoCT).ToList().FirstOrDefault();
            var tT621s = _unitOfWork.tT621Repository.Find(x => x.SoCT.Contains(subfix)).ToList(); // chi lay nhung soCT cung param: TV, TN

            var tT621 = new TT621();
            if (tT621s.Count() > 0)
            {
                tT621s = tT621s.Where(x => x.MaCn == maCn).ToList();
                tT621 = tT621s.OrderByDescending(x => x.SoCT).FirstOrDefault();
            }
            if (tT621 == null || string.IsNullOrEmpty(tT621.SoCT))
            {
                return GetNextId.NextID("", "") + subfix; // 0001
            }
            else
            {
                var oldYear = tT621.SoCT.Substring(6, 4);
                // cung nam
                if (oldYear == currentYear.ToString())
                {
                    var oldSoCT = tT621.SoCT.Substring(0, 4);
                    return GetNextId.NextID(oldSoCT, "") + subfix;
                }
                else
                {
                    // sang nam khac' chay lai tu dau
                    return GetNextId.NextID("", "") + subfix; // 0001
                }
            }
        }

        public decimal GetSoTienNT_TrongTT621_TheoTamUng(long tamUngId)
        {
            var tT621s = FindByTamUngId(tamUngId);
            decimal soTienNT_TrongTT621_TheoTamUng = 0;
            if (tT621s.Count() > 0)
            {
                soTienNT_TrongTT621_TheoTamUng = tT621s.Sum(x => x.SoTienNT.Value);
            }

            return soTienNT_TrongTT621_TheoTamUng;
        }

        public async Task<TT621> FindById_Include(long id)
        {
            var tT621s = await _unitOfWork.tT621Repository.FindIncludeOneAsync(x => x.TamUng, y => y.Id == id);
            return tT621s.FirstOrDefault();
        }

        public TT621 GetByIdAsNoTracking(long tt621Id)
        {
            return _unitOfWork.tT621Repository.GetByIdAsNoTracking(x => x.Id == tt621Id);
        }

        public decimal Get_SoTienNT_CanKetChuyen(long tamUngId, decimal soTienNT_Tren_TT621Create, string loaiPhieu)
        {
            var tamUng = _unitOfWork.tamUngRepository.GetByIdAsNoTracking(x => x.Id == tamUngId);
            decimal soTienNTTrongTT621_TheoTamUng = GetSoTienNT_TrongTT621_TheoTamUng(tamUngId);
            decimal soTienNT_CanKetChuyen;
            if (loaiPhieu == "C") // phieu C
            {
                soTienNT_CanKetChuyen = tamUng.SoTienNT.Value - soTienNTTrongTT621_TheoTamUng + soTienNT_Tren_TT621Create;
            }
            else // phieu T
            {
                soTienNT_CanKetChuyen = tamUng.SoTienNT.Value - soTienNTTrongTT621_TheoTamUng - soTienNT_Tren_TT621Create;
            }

            return soTienNT_CanKetChuyen;
        }

        public async Task DeleteAsync(TT621 tT621)
        {
            _unitOfWork.tT621Repository.Delete(tT621);
            await _unitOfWork.Complete();
        }

        public IEnumerable<VSupplierTaiKhoan> GetSuppliersByCode(string code, string maCn)
        {
            code ??= "";
            // supplier co 2 key
            //var suppliers = _unitOfWork.supplier_Hdvatob_Repository.Find(x => x.Code.Trim().ToLower() == code.Trim().ToLower() && x.Chinhanh == maCn).ToList();
            var suppliers = _unitOfWork.supplier_Hdvatob_Repository.GetSuppliersByCodeName(code);
            suppliers = suppliers.Where(x => x.Chinhanh == maCn).ToList();
            return suppliers;
        }

        public IEnumerable<TT621> GetAll()
        {
            return _unitOfWork.tT621Repository.GetAll();
        }

        public IEnumerable<TT621> FindTT621s_IncludeTwice_By_Date(string searchFromDate, string searchToDate, string maCn, string maKhCo,
            string username)
        {
            List<TT621> list = new List<TT621>();
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

                    list = _unitOfWork.tT621Repository.FindTT621s_IncludeTwice_By_Date(fromDate, toDate).ToList();

                    if (username.ToLower() != "hongvt") // account !=
                    {
                        list = list.Where(x => x.MaCn == maCn).ToList();
                    }

                    if (!string.IsNullOrEmpty(maKhCo))
                    {
                        list = list.Where(x => x.MaKhCo.Trim().ToUpper() == maKhCo.Trim().ToUpper()
                        || x.MaKhNo.Trim().ToUpper() == maKhCo.Trim().ToUpper()).ToList();
                    }
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
                        list = _unitOfWork.tT621Repository.FindTT621s_IncludeTwice_By_Date(fromDate, "").ToList();
                        list = list.Where(x => x.MaCn == maCn).ToList();
                        if (!string.IsNullOrEmpty(maKhCo))
                        {
                            list = list.Where(x => x.MaKhCo.Trim().ToUpper() == maKhCo.Trim().ToUpper()
                            || x.MaKhNo.Trim().ToUpper() == maKhCo.Trim().ToUpper()).ToList();
                        }
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
                        //list = list.Where(x => x.NgayCT < toDate.AddDays(1)).ToList();
                        list = _unitOfWork.tT621Repository.FindTT621s_IncludeTwice_By_Date("", toDate).ToList();
                        list = list.Where(x => x.MaCn == maCn).ToList();
                        if (!string.IsNullOrEmpty(maKhCo))
                        {
                            list = list.Where(x => x.MaKhCo.Trim().ToUpper() == maKhCo.Trim().ToUpper()
                            || x.MaKhNo.Trim().ToUpper() == maKhCo.Trim().ToUpper()).ToList();
                        }
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

        public IEnumerable<TT621> FindTT621s_IncludeTwice(long tamUngId)
        {
            return _unitOfWork.tT621Repository.FindTT621s_IncludeTwice(tamUngId);
        }

        public async Task CreateRangeAsync(List<TT621> tT621s)
        {
            await _unitOfWork.tT621Repository.CreateRange(tT621s);
            await _unitOfWork.Complete();
        }

        public async Task<List<TT621>> GetByPhieuTC(string soCT, string maCn)
        {
            var tT621s = await _unitOfWork.tT621Repository.FindIncludeOneAsync(x => x.TamUng,
                y => y.PhieuTC == soCT && y.MaCn == maCn);
            return tT621s.ToList();
        }

        public IEnumerable<TT621GroupBy_PhieuTC> GroupBy_PhieuTC(IEnumerable<TT621> tT621s)
        {
            var result = (from t in tT621s
                          group t by t.PhieuTC into g
                          select new TT621GroupBy_PhieuTC
                          {
                              PhieuTC = g.Key,
                              TT621s = g.ToList()
                          }).ToList();

            return result;
        }
    }
}