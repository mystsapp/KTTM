using Data.Models_KTTM;
using Data.Repository;
using Data.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Services
{
    public interface ITT621Service
    {
        Task<IEnumerable<TT621>> GetTT621s_By_TamUng(long kVCTPCTId);
        Task CreateAsync(TT621 tT621);
        Task UpdateAsync(TT621 tT621);
        TT621 GetDummyTT621_By_KVCTPCT(long tamUngId);
        Task<IEnumerable<TT621>> FindByTamUngId(long tamUngId);
        string GetSoCT(string param);
        Task<decimal> GetSoTienNT_TrongTT621_TheoTamUngAsync(long tamUngId);
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

        public async Task<IEnumerable<TT621>> FindByTamUngId(long tamUngId)
        {
            return await _unitOfWork.tT621Repository.FindIncludeOneAsync(x => x.TamUng, y => y.TamUngId == tamUngId);
        }

        public TT621 GetDummyTT621_By_KVCTPCT(long kVCTPCTId)
        {
            var kVCTPCT = _unitOfWork.kVCTPCTRepository.GetById(kVCTPCTId);
            TT621 tT621 = new TT621
            {
                DienGiaiP = kVCTPCT.DienGiaiP,
                HTTC = kVCTPCT.HTTC,
                SoTienNT = kVCTPCT.SoTienNT,
                LoaiTien = kVCTPCT.LoaiTien,
                TyGia = kVCTPCT.TyGia,
                SoTien = kVCTPCT.SoTien,
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
                VAT = kVCTPCT.VAT,
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

        public async Task UpdateAsync(TT621 tT621)
        {
            _unitOfWork.tT621Repository.Update(tT621);
            await _unitOfWork.Complete();
        }


        public string GetSoCT(string param)
        {
            //DateTime dateTime;
            //dateTime = DateTime.Now;
            //dateTime = TourVM.Tour.NgayKyHopDong.Value;

            var currentYear = DateTime.Now.Year; // ngay hien tai
            var subfix = param + currentYear.ToString(); // TN2015?  TV2015? 
            var tT621 = _unitOfWork.tT621Repository.GetAllAsNoTracking().OrderByDescending(x => x.SoCT).ToList().FirstOrDefault();
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

        public async Task<decimal> GetSoTienNT_TrongTT621_TheoTamUngAsync(long tamUngId)
        {
            var tT621s = await FindByTamUngId(tamUngId);
            decimal soTienTrongTT621_TheoTamUng = 0;
            if (tT621s.Count() > 0)
            {
                soTienTrongTT621_TheoTamUng = tT621s.Sum(x => x.SoTien);
            }

            return soTienTrongTT621_TheoTamUng;
        }

    }
}
