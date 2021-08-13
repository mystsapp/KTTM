﻿using Data.Dtos;
using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.Models_QLTour;
using Data.Repository;
using Data.Utilities;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace KTTM.Services
{
    public interface IKVPTCService
    {
        IEnumerable<KVPTC> GetAll();
        IEnumerable<Ngoaite> GetAllNgoaiTe();
        IEnumerable<Phongban> GetAllPhongBan();
        Task<IPagedList<KVPTCDto>> ListKVPTC(string searchString, string searchFromDate, string searchToDate, string boolSgtcode, int? page);

        IEnumerable<ListViewModel> ListLoaiPhieu();
        IEnumerable<ListViewModel> ListLoaiTien();
        string GetSoCT(string param);
        Task CreateAsync(KVPTC kVPCT);
        Task CreateRangeAsync(List<KVPTC> kVPCTs);
        Task<KVPTC> GetBySoCT(string soCT);
        KVPTC GetBySoCTAsNoTracking(string soCT);
        Task UpdateAsync(KVPTC kVPCT);

        IEnumerable<TkCongNo> GetAllTkCongNo();

    }
    public class KVPTCService : IKVPTCService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KVPTCService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<KVPTC> GetAll()
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

        public async Task<KVPTC> GetBySoCT(string soCT)
        {
            return await _unitOfWork.kVPCTRepository.GetByIdAsync(soCT);
        }

        public KVPTC GetBySoCTAsNoTracking(string soCT)
        {
            return _unitOfWork.kVPCTRepository.GetByIdAsNoTracking(x => x.SoCT == soCT);
        }

        public string GetSoCT(string param)
        {
            //DateTime dateTime;
            //dateTime = DateTime.Now;
            //dateTime = TourVM.Tour.NgayKyHopDong.Value;

            var currentYear = DateTime.Now.Year; // ngay hien tai
            var subfix = param + currentYear.ToString(); // QT2021? ?QC2021? ?NT2021? ?NC2021?
            var kVPCT = _unitOfWork.kVPCTRepository.Find(x => x.SoCT.Contains(param)) // chi lay nhung soCT cung param: QT, TC, NT, NC
                                                   .OrderByDescending(x => x.SoCT)
                                                   .FirstOrDefault();
            if (kVPCT == null || string.IsNullOrEmpty(kVPCT.SoCT))
            {
                return GetNextId.NextID("", "") + subfix; // 0001
            }
            else
            {
                var oldYear = kVPCT.SoCT.Substring(6, 4);
                
                // cung nam
                if (oldYear == currentYear.ToString())
                {

                    var oldSoCT = kVPCT.SoCT.Substring(0, 4);
                    return GetNextId.NextID(oldSoCT, "") + subfix;

                }
                else
                {
                    // sang nam khac' chay lai tu dau
                    return GetNextId.NextID("", "") + subfix; // 0001
                }
            }
        }

        public async Task<IPagedList<KVPTCDto>> ListKVPTC(string searchString, string searchFromDate, string searchToDate, string boolSgtcode, int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            var list = new List<KVPTCDto>();
            var kVPCTs = new List<KVPTC>();
            //var kVPCTs = GetAll();

            //if (kVPCTs == null)
            //{
            //    return null;
            //}

            // search for sgtcode in kvctpct
            if (!string.IsNullOrEmpty(boolSgtcode) && !string.IsNullOrEmpty(searchString))
            {

                List<KVCTPTC> kVCTPCTs = _unitOfWork.kVCTPCTRepository.Find(x => !string.IsNullOrEmpty(x.Sgtcode) && x.Sgtcode.Contains(searchString.Trim())).ToList();

                if (kVCTPCTs.Count() > 0)
                {
                    List<KVPTC> kVPCTs1 = new List<KVPTC>();
                    foreach (var item in kVCTPCTs)
                    {
                        KVPTC kVPCT = await GetBySoCT(item.KVPTCId);
                        kVPCTs1.Add(kVPCT);
                    }
                    if (kVPCTs1.Count > 0)
                    {
                        kVPCTs = kVPCTs1.Distinct().ToList();
                    }
                }

            }

            else if (string.IsNullOrEmpty(boolSgtcode) && !string.IsNullOrEmpty(searchString))
            {
                kVPCTs = _unitOfWork.kVPCTRepository.Find(x => x.SoCT.ToLower().Contains(searchString.Trim().ToLower()) ||
                                       (!string.IsNullOrEmpty(x.MFieu) && x.MFieu.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.NgoaiTe) && x.NgoaiTe.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.HoTen) && x.HoTen.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.DonVi) && x.DonVi.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.Phong) && x.Phong.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.LapPhieu) && x.LapPhieu.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.MayTinh) && x.MayTinh.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.Locker) && x.Locker.ToLower().Contains(searchString.ToLower()))).ToList();

                //kVPCTs = kVPCTs.Where(x => x.SoCT.ToLower().Contains(searchString.Trim().ToLower()) ||
                //                       (!string.IsNullOrEmpty(x.MFieu) && x.MFieu.ToLower().Contains(searchString.ToLower())) ||
                //                       (!string.IsNullOrEmpty(x.NgoaiTe) && x.NgoaiTe.ToLower().Contains(searchString.ToLower())) ||
                //                       (!string.IsNullOrEmpty(x.HoTen) && x.HoTen.ToLower().Contains(searchString.ToLower())) ||
                //                       (!string.IsNullOrEmpty(x.DonVi) && x.DonVi.ToLower().Contains(searchString.ToLower())) ||
                //                       (!string.IsNullOrEmpty(x.Phong) && x.Phong.ToLower().Contains(searchString.ToLower())) ||
                //                       (!string.IsNullOrEmpty(x.LapPhieu) && x.LapPhieu.ToLower().Contains(searchString.ToLower())) ||
                //                       (!string.IsNullOrEmpty(x.MayTinh) && x.MayTinh.ToLower().Contains(searchString.ToLower())) ||
                //                       (!string.IsNullOrEmpty(x.Locker) && x.Locker.ToLower().Contains(searchString.ToLower()))).ToList();


            }
            else
            {
                kVPCTs = GetAll().ToList();

                if (kVPCTs == null)
                {
                    return null;
                }
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

        public IEnumerable<ListViewModel> ListLoaiPhieu()
        {
            return new List<ListViewModel>()
            {
                new ListViewModel() { Name = "T" },
                new ListViewModel() { Name = "C" }
            };
        }

        public IEnumerable<ListViewModel> ListLoaiTien()
        {
            return new List<ListViewModel>()
            {
                new ListViewModel() { Name = "VN" },
                new ListViewModel() { Name = "NT" }
            };
        }

        public async Task CreateAsync(KVPTC kVPCT)
        {
            _unitOfWork.kVPCTRepository.Create(kVPCT);
            await _unitOfWork.Complete();
        }

        public async Task UpdateAsync(KVPTC kVPCT)
        {
            _unitOfWork.kVPCTRepository.Update(kVPCT);
            await _unitOfWork.Complete();
        }

        public IEnumerable<TkCongNo> GetAllTkCongNo()
        {
            return _unitOfWork.tkCongNoRepository.GetAll();
        }

        public async Task CreateRangeAsync(List<KVPTC> kVPCTs)
        {
            await _unitOfWork.kVPCTRepository.CreateRange(kVPCTs);
            await _unitOfWork.Complete();
        }
    }
}