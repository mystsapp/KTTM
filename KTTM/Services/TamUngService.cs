using Data.Models_KTTM;
using Data.Repository;
using Data.Utilities;
using KTTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTTM.Services
{
    public interface ITamUngService
    {
        string GetSoCT(string param);
        Task<TamUng> GetByIdAsync(long id);
        Task<IEnumerable<TamUng>> Find_TamUngs_By_MaKh_Include(string maKh);
        Task<IEnumerable<TamUng>> Find_TamUngs_By_PhieuChi_Include(string phieuChi);
        Task CreateAsync(TamUng tamUng);
        Task UpdateAsync(TamUng tamUng);
        Task<IEnumerable<TamUng>> Find_TamUngs_By_MaKh_Include_KhongTC(string maKh);
        IEnumerable<TamUng> FindTamUngs_IncludeTwice_By_Phong(string boPhan);
    }
    public class TamUngService : ITamUngService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TamUngService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(TamUng tamUng)
        {
            _unitOfWork.tamUngRepository.Create(tamUng);
            await _unitOfWork.Complete();
        }

        public IEnumerable<TamUng> FindTamUngs_IncludeTwice_By_Phong(string boPhan)
        {
            var tamUngs = _unitOfWork.tamUngRepository.FindTamUngs_IncludeTwice_By_Phong(boPhan);
            return tamUngs.Where(x => x.ConLaiNT > 0);
        }

        public IEnumerable<TamUngModel> TamUngModels_ByTamUng(IEnumerable<TamUng> tamUngs)
        {
            List<TamUngModel> tamUngModels = new List<TamUngModel>();
            foreach(var item in tamUngs)
            {
                
            }
        }

        public async Task<IEnumerable<TamUng>> Find_TamUngs_By_MaKh_Include(string maKh)
        {
            var tamUngs = await _unitOfWork.tamUngRepository.FindIncludeOneAsync(x => x.KVCTPCT, y => y.MaKhNo == maKh && y.ConLai > 0);
            return tamUngs;
        }

        public async Task<IEnumerable<TamUng>> Find_TamUngs_By_MaKh_Include_KhongTC(string maKh)
        {
            var tamUngs = await Find_TamUngs_By_MaKh_Include(maKh);
            var tamUngs1 = tamUngs.ToList();

            foreach (var item in tamUngs1.Reverse<TamUng>())
            {
                var tT621s = await _unitOfWork.tT621Repository.FindAsync(x => x.TamUngId == item.Id);
                if (tT621s.Count() > 1) // tT621s == 0 or 1 
                    tamUngs1.Remove(item);

                if (tT621s.Count() == 1 && tT621s.FirstOrDefault().SoTienNT != item.SoTienNT) // tT621s.sotiennt  khac' tamung.sotiennt
                    tamUngs1.Remove(item);
            }

            return tamUngs1;
        }

        public async Task<IEnumerable<TamUng>> Find_TamUngs_By_PhieuChi_Include(string phieuChi)
        {
            return await _unitOfWork.tamUngRepository.FindIncludeOneAsync(x => x.KVCTPCT, y => y.PhieuChi == phieuChi);
        }

        public async Task<TamUng> GetByIdAsync(long id)
        {
            return await _unitOfWork.tamUngRepository.GetByLongIdAsync(id);
        }

        public string GetSoCT(string param)
        {
            //DateTime dateTime;
            //dateTime = DateTime.Now;
            //dateTime = TourVM.Tour.NgayKyHopDong.Value;

            var currentYear = DateTime.Now.Year; // ngay hien tai
            var subfix = param + currentYear.ToString(); // QT2021? ?QC2021? ?NT2021? ?NC2021?
            //var tamUng = _unitOfWork.tamUngRepository.GetAllAsNoTracking().OrderByDescending(x => x.SoCT).ToList().FirstOrDefault();
            var tamUng = _unitOfWork.kVPCTRepository.Find(x => x.SoCT.Contains(param)) // chi lay nhung soCT cung param: UN, UV
                                                    .OrderByDescending(x => x.SoCT)
                                                    .FirstOrDefault();
            if (tamUng == null || string.IsNullOrEmpty(tamUng.SoCT))
            {
                return GetNextId.NextID("", "") + subfix; // 0001
            }
            else
            {
                var oldYear = tamUng.SoCT.Substring(6, 4);
                // cung nam
                if (oldYear == currentYear.ToString())
                {
                    var oldSoCT = tamUng.SoCT.Substring(0, 4);
                    return GetNextId.NextID(oldSoCT, "") + subfix;
                }
                else
                {
                    // sang nam khac' chay lai tu dau
                    return GetNextId.NextID("", "") + subfix; // 0001
                }
            }
        }

        public async Task UpdateAsync(TamUng tamUng)
        {
            _unitOfWork.tamUngRepository.Update(tamUng);
            await _unitOfWork.Complete();
        }
    }
}
