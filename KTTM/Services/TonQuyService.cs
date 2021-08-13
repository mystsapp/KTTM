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
        Task UpdateAsync(TonQuy tonQuy);
        List<TonQuy> Find_Equal_By_Date(DateTime dateTime);
        Task<string> CheckTonDauStatus(DateTime fromDate);
        Task<string> CheckTonDauStatus_NT(DateTime fromDate, string loaiTien);
        TonQuy GetById(long id);
    }
    public class TonQuyService : ITonQuyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TonQuyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> CheckTonDauStatus_NT(DateTime fromDate, string loaiTien)
        {
            //var date = DateTime.Now.AddDays(1);
            // ds tonquy truoc tuNgay
            List<TonQuy> tonQuies1 = _unitOfWork.tonQuyRepository.Find(x => x.NgayCT <= fromDate 
                                                                         && x.LoaiTien == loaiTien).ToList();
            
            // tonquy sau cung nhat
            TonQuy tonQuy = tonQuies1.OrderByDescending(x => x.NgayCT).FirstOrDefault();
            
            // lay tat ca chi tiet truóc tuNgay(fromDate)
            var kVCTPCTs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, y => y.KVPTC.NgayCT < fromDate.AddDays(1));
            kVCTPCTs = kVCTPCTs.Where(x => x.LoaiTien == loaiTien).ToList();
            string stringDate = "";

            // tonQuy.NgayCT (sau cung nhat) < nhung chi tiet < tuNggay (fromdate)
            for (DateTime i = tonQuy.NgayCT.Value.AddDays(1); i < fromDate; i = i.AddDays(1)) // chay tu ngay tonquy den fromday
            {
                var boolK = kVCTPCTs.ToList().Exists(x => x.KVPTC.NgayCT.Value.ToShortDateString() == i.ToShortDateString());
                if (boolK)
                {
                    stringDate += i.ToString("dd/MM/yyyy") + "-" ;
                }
            }

            return stringDate;

        }
        
        public async Task<string> CheckTonDauStatus(DateTime fromDate)
        {
            //var date = DateTime.Now.AddDays(1);
            // ds tonquy truoc tuNgay
            List<TonQuy> tonQuies1 = _unitOfWork.tonQuyRepository.Find(x => x.NgayCT <= fromDate 
                                                                         && x.LoaiTien == "VND").ToList();
            
            // tonquy sau cung nhat
            TonQuy tonQuy = tonQuies1.OrderByDescending(x => x.NgayCT).FirstOrDefault();
            
            // lay tat ca chi tiet truóc tuNgay(fromDate)
            var kVCTPCTs = await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPTC, y => y.KVPTC.NgayCT < fromDate.AddDays(1));
            kVCTPCTs = kVCTPCTs.Where(x => x.LoaiTien == "VND").ToList();
            string stringDate = "";

            // tonQuy.NgayCT (sau cung nhat) < nhung chi tiet < tuNggay (fromdate)
            for (DateTime i = tonQuy.NgayCT.Value.AddDays(1); i < fromDate; i = i.AddDays(1)) // chay tu ngay tonquy den fromday
            {
                var boolK = kVCTPCTs.ToList().Exists(x => x.KVPTC.NgayCT.Value.ToShortDateString() == i.ToShortDateString());
                if (boolK)
                {
                    stringDate += i.ToString("dd/MM/yyyy") + "-" ;
                }
            }

            return stringDate;

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

        public List<TonQuy> Find_Equal_By_Date(DateTime dateTime)
        {
            var tonQuies = _unitOfWork.tonQuyRepository.Find(x => x.NgayCT.Value.ToShortDateString() == dateTime.ToShortDateString()).ToList();
            if(tonQuies.Count == 0)
            {
                return tonQuies;
            }
            return tonQuies;
        }

        public TonQuy GetById(long id)
        {
            return _unitOfWork.tonQuyRepository.GetById(id);

        }

        public async Task UpdateAsync(TonQuy tonQuy)
        {
            _unitOfWork.tonQuyRepository.Update(tonQuy);
            await _unitOfWork.Complete();
        }
    }
}
