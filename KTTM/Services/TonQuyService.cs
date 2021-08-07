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
    }
    public class TonQuyService : ITonQuyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TonQuyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                if (!string.IsNullOrEmpty(searchFromDate)) // NgayCT
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
                if (!string.IsNullOrEmpty(searchToDate)) // NgayCT
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
    }
}
