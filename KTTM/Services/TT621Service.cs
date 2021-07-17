using Data.Models_KTTM;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Services
{
    public interface ITT621Service
    {
        Task<IEnumerable<TT621>> GetTT621s_By_TamUng(long tamUngId);
        Task CreateAsync(TT621 tT621);
        Task UpdateAsync(TT621 tT621);
        TT621 GetDummyTT621_By_KVCTPCT(long tamUngId);
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

        public TT621 GetDummyTT621_By_KVCTPCT(long tamUngId)
        {
            var kVCTPCT = _unitOfWork.kVCTPCTRepository.GetById(tamUngId);
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
    }
}
