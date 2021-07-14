using Data.Models_KTTM;
using Data.Repository;
using Data.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public interface ITamUngService
    {
        string GetSoCT(string param);
        Task<TamUng> GetByIdAsync(long id);
        Task CreateAsync(TamUng tamUng);
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
            var tamUng = _unitOfWork.tamUngRepository.GetAllAsNoTracking().OrderByDescending(x => x.SoCT).ToList().FirstOrDefault();
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

    }
}
