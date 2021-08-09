using Data.Models_KTTM;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Services
{
    public interface ITonQuyService
    {
        IEnumerable<TonQuy> FindTonQuy_By_Date(string searchFromDate, string searchToDate);
        Task CreateAsync(TonQuy tonQuy);
        bool Find_Equal_By_Date(DateTime dateTime);
        Task<IEnumerable<KVCTPCT>> CheckTonDauStatus(DateTime fromDate);
    }
    public class TonQuyService : ITonQuyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TonQuyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<KVCTPCT> CheckTonDauStatus(DateTime fromDate)
        {
            var kVCTPCTs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPCT, y => y.KVPCT.NgayCT < fromDate.AddDays(1));
            kVCTPCTs = kVCTPCTs.Where(x => x.LoaiTien == "VND").ToList();
            var tonQuies = await _unitOfWork.tonQuyRepository.FindAsync(y => y.NgayCT < fromDate.AddDays(1));
            List<KVCTPCT> kVCTPCTs1 = new List<KVCTPCT>(); // nhung kvctpct chua ton tai trong tonquy => chua tinh tonquy
            foreach (var item in kVCTPCTs.Reverse<KVCTPCT>())
            {
                var boolK = tonQuies.ToList().Exists(x => x.NgayCT == item.KVPCT.NgayCT);
                if (!boolK) // nhung kvctpct chua ton tai trong tonquy => chua tinh tonquy
                {
                    kVCTPCTs1.Add(item);
                }

            }

        }

        public async Task CreateAsync(TonQuy tonQuy)
        {
            _unitOfWork.tonQuyRepository.Create(tonQuy);
            await _unitOfWork.Complete();
        }

        public IEnumerable<TonQuy> FindTonQuy_By_Date(string searchFromDate, string searchToDate)
        {

            List<TonQuy> list = new List<TonQuy>();
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

                    list = _unitOfWork.tonQuyRepository.Find(x => x.NgayCT >= fromDate &&
                                       x.NgayCT < toDate.AddDays(1)).ToList();
                    
                }
                catch (Exception)
                {

                    return null;
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(searchFromDate)) // tungay
                {
                    try
                    {
                        fromDate = DateTime.Parse(searchFromDate);
                        list = _unitOfWork.tonQuyRepository.Find(x => x.NgayCT >= fromDate).ToList();
                        
                    }
                    catch (Exception)
                    {
                        return null;
                    }

                }
                if (!string.IsNullOrEmpty(searchToDate)) // denngay
                {
                    try
                    {
                        toDate = DateTime.Parse(searchToDate);
                        list = _unitOfWork.tonQuyRepository.Find(x => x.NgayCT < toDate.AddDays(1)).ToList();
                        
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

        public bool Find_Equal_By_Date(DateTime dateTime)
        {
            var tonQuies = _unitOfWork.tonQuyRepository.Find(x => x.NgayCT.Value.ToShortDateString() == dateTime.ToShortDateString()).ToList();
            if(tonQuies.Count == 0)
            {
                return false;
            }
            return true;
        }

    }
}
