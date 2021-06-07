using Data.Dtos;
using Data.Models_KTTM;
using Data.Models_QLTour;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace Data.Services
{
    public interface IKVPTCService
    {
        IEnumerable<KVPCT> GetAll();
        IEnumerable<Ngoaite> GetAllNgoaiTe();
        IEnumerable<Phongban> GetAllPhongBan();
        IPagedList<KVPTCDto> ListKVPTC(string searchString, string searchFromDate, string searchToDate, int? page);
    }
    public class KVPTCService : IKVPTCService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KVPTCService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<KVPCT> GetAll()
        {
            return _unitOfWork.kVPCTRepository.GetAll();
        }

        public IEnumerable<Ngoaite> GetAllNgoaiTe()
        {
            return _unitOfWork.ngoaiTeRepository.GetAll();
        }

        public IEnumerable<Phongban> GetAllPhongBan()
        {
            return _unitOfWork.phongBanRepository.GetAll();
        }

        public IPagedList<KVPTCDto> ListKVPTC(string searchString,  string searchFromDate, string searchToDate, int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            var list = new List<KVPTCDto>();
            var kVPCTs = GetAll();
            
            if (kVPCTs == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                kVPCTs = kVPCTs.Where(x => x.SoCT.ToLower().Contains(searchString.Trim().ToLower()) ||
                                       (!string.IsNullOrEmpty(x.MFieu.ToLower()) && x.MFieu.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.NgoaiTe.ToLower()) && x.NgoaiTe.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.HoTen.ToLower()) && x.HoTen.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.DonVi.ToLower()) && x.DonVi.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.Phong.ToLower()) && x.Phong.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.LapPhieu.ToLower()) && x.LapPhieu.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.MayTinh.ToLower()) && x.MayTinh.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.Locker.ToLower()) && x.Locker.ToLower().Contains(searchString.ToLower()))).ToList();
            }

            foreach (var item in kVPCTs)
            {
                var kVPTCDto = new KVPTCDto();

                kVPTCDto.SoCT = item.SoCT;
                kVPTCDto.NgayCT = item.NgayCT.Value;
                kVPTCDto.MFieu = item.MFieu;
                kVPTCDto.NgoaiTe = item.NgoaiTe;
                kVPTCDto.HoTen = item.HoTen;
                kVPTCDto.DonVi = item.DonVi;
                kVPTCDto.Phong = item.Phong;
                kVPTCDto.LapPhieu = item.LapPhieu;
                kVPTCDto.Create = item.Create;
                kVPTCDto.MayTinh = item.MayTinh;
                kVPTCDto.Lock = item.Lock;
                kVPTCDto.Locker = item.Locker;
                
                list.Add(kVPTCDto);
            }

            list = list.OrderByDescending(x => x.Create).ToList();
            var count = list.Count();

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
                    
                    list = list.Where(x => x.NgayCT >= fromDate &&
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
                        list = list.Where(x => x.NgayCT >= fromDate).ToList();
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
                        list = list.Where(x => x.NgayCT < toDate.AddDays(1)).ToList();

                    }
                    catch (Exception)
                    {
                        return null;
                    }

                }
            }
            // search date

            //// List<string> listRoleChiNhanh --> chi lay nhung tour thuộc phanKhuCN cua minh
            //if (listRoleChiNhanh.Count > 0)
            //{
            //    list = list.Where(item1 => listRoleChiNhanh.Any(item2 => item1.MaCNTao == item2)).ToList();
            //}
            //// List<string> listRoleChiNhanh --> chi lay nhung tour thuộc phanKhuCN cua minh

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
    }
}
