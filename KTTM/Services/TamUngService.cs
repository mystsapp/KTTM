using Data.Models_DanhMucKT;
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
        IEnumerable<TamUng> GetAll();

        string GetSoCT(string param, string maCn);

        Task<TamUng> GetByIdAsync(long id);

        Task<IEnumerable<TamUng>> Find_TamUngs_By_MaKh_Include(string maKh, string maCn, string tk);

        Task<IEnumerable<TamUng>> Find_TamUngs_By_PhieuChi_Include(string phieuChi, string maCn);

        Task CreateAsync(TamUng tamUng);

        Task CreateRangeAsync(IEnumerable<TamUng> tamUngs);

        Task UpdateAsync(TamUng tamUng);

        Task<IEnumerable<TamUng>> Find_TamUngs_By_MaKh_Include_KhongTC(string maKh, string maCn);

        IEnumerable<TamUng> FindTamUngs_IncludeTwice_By_Phong(string boPhan, string maCn);

        IEnumerable<TamUngModel_GroupBy_Name> TamUngModels_GroupBy_Name(IEnumerable<TamUng> tamUngs, string maCn);

        Task<IEnumerable<TamUngModel_GroupBy_Name_Phong>> TamUngModels_GroupBy_Name_TwoKey_Phong(IEnumerable<TamUng> tamUngs, string maCn);

        Task<IEnumerable<TamUng>> Find_TamUngs_By_PhieuTT(string soCT, string maCn);

        List<TamUng> FindByMaCn(string maCn);
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

        public IEnumerable<TamUng> FindTamUngs_IncludeTwice_By_Phong(string boPhan, string maCn)
        {
            List<TamUng> tamUngs = new List<TamUng>();
            if (string.IsNullOrEmpty(boPhan))
            {
                tamUngs = _unitOfWork.tamUngRepository.FindTamUngs_IncludeTwice_By_Phong(maCn).ToList(); // getall with conlaiNT > 0
            }
            else
            {
                tamUngs = _unitOfWork.tamUngRepository.FindTamUngs_IncludeTwice_By_Phong(boPhan, maCn).ToList(); // by phong with conlaiNT > 0
            }

            return tamUngs;
        }

        public IEnumerable<TamUngModel_GroupBy_Name> TamUngModels_GroupBy_Name(IEnumerable<TamUng> tamUngs, string maCn)
        {
            List<TamUngModel> tamUngModels = new List<TamUngModel>();
            foreach (var item in tamUngs)
            {
                //var supplier = _unitOfWork.supplier_Hdvatob_Repository.Find(x => x.Code == item.MaKhNo && x.Chinhanh == maCn).FirstOrDefault();
                var supplier = _unitOfWork.khachHang_DanhMucKTRepository.GetKhachHangsByCodeName(item.MaKhNo).FirstOrDefault();
                if (supplier == null)
                {
                    return new List<TamUngModel_GroupBy_Name>() { new TamUngModel_GroupBy_Name() { Status = false, MaKh = item.MaKhNo } };
                }

                //var supplier = _unitOfWork.supplier_Hdvatob_Repository.Find(x => x.Code == item.MaKhNo && x.Chinhanh == maCn).FirstOrDefault();
                tamUngModels.Add(new TamUngModel()
                {
                    NgayCT = item.NgayCT,
                    SoCT = item.KVCTPTC.SoCT,// item.SoCT,
                    DienGiai = item.DienGiai,
                    SoTienNT = item.SoTienNT,
                    LT = item.LoaiTien,
                    TyGia = item.TyGia,
                    VND = item.SoTien,
                    Name = item.MaKhNo + " " + supplier.TenGiaoDich,//.TenThuongMai,//item.KVCTPTC.TenKH, --> thao
                    Name_Phong = item.Phong,
                    Id = item.Id
                });
            }

            var result1 = (from p in tamUngModels
                           group p by p.Name into g
                           select new TamUngModel_GroupBy_Name()
                           {
                               Name = g.Key,
                               TamUngModels = g.ToList()
                           }).ToList();
            foreach (var item in result1)
            {
                item.TongCong = item.TamUngModels.Where(x => x.LT == "VND").Sum(x => x.VND.Value);

                //IEnumerable<NgoaiTe> ngoaiTes = GetAllNgoaiTe().Where(x => x.MaNt != "VND");
                //foreach (var ngoaiTe in ngoaiTes.OrderByDescending(x => x.MaNt))
                //{
                //}
            }
            return result1;
        }

        private IEnumerable<NgoaiTe> GetAllNgoaiTe()
        {
            return _unitOfWork.ngoaiTe_DanhMucKT_Repository.GetAll();
        }

        public async Task<IEnumerable<TamUngModel_GroupBy_Name_Phong>> TamUngModels_GroupBy_Name_TwoKey_Phong(IEnumerable<TamUng> tamUngs, string maCn)
        {
            tamUngs = tamUngs.Where(x => x.ConLaiNT > 0 && x.MaCn == maCn);
            List<TamUngModel> tamUngModels = new List<TamUngModel>();
            foreach (var item in tamUngs)
            {
                //var supplier = _unitOfWork.supplier_Hdvatob_Repository.Find(x => x.Code == item.MaKhNo && x.Chinhanh == maCn).FirstOrDefault();
                var supplier = _unitOfWork.khachHang_DanhMucKTRepository.GetKhachHangsByCodeName(item.MaKhNo).FirstOrDefault();
                if (supplier == null)
                {
                    return new List<TamUngModel_GroupBy_Name_Phong>() { new TamUngModel_GroupBy_Name_Phong() { Status = false, MaKh = item.MaKhNo } };
                }

                //var supplier = _unitOfWork.supplier_Hdvatob_Repository.Find(x => x.Code == item.MaKhNo && x.Chinhanh == maCn).FirstOrDefault();

                try
                {
                    tamUngModels.Add(new TamUngModel()
                    {
                        NgayCT = item.NgayCT,
                        SoCT = item.KVCTPTC.SoCT,// item.SoCT,
                        DienGiai = item.DienGiai,
                        SoTienNT = item.SoTienNT,
                        LT = item.LoaiTien,
                        TyGia = item.TyGia,
                        VND = item.SoTien,
                        Name = item.MaKhNo + " " + supplier.TenGiaoDich,//supplier.TenThuongMai ?? "",
                        //Name = item.MaKhNo + " " + item.KVCTPTC.KVPTC.HoTen,
                        Name_Phong = item.Phong,
                        Id = item.Id
                    });
                }
                catch (Exception ex)
                {
                    ErrorLog errorLog = new ErrorLog()
                    {
                        Message = ex.Message,
                        InnerMessage = ex.InnerException.ToString(),
                        MaCn = maCn,
                        LogFile = "TamUngService " + "TamUngModels_GroupBy_Name_TwoKey_Phong " + DateTime.Now,
                        NgayTao = DateTime.Now
                    };
                    await _unitOfWork.errorLogRepository.CreateAsync(errorLog);
                }
            }

            var result1 = (from p in tamUngModels
                           group p by new
                           {
                               p.Name,
                               p.Name_Phong
                           } into g
                           select new TamUngModel_GroupBy_Name()
                           {
                               Name = g.Key.Name,
                               Name_Phong = g.Key.Name_Phong,
                               TamUngModels = g.ToList()
                           }).ToList();
            foreach (var item in result1)
            {
                item.TongCong = item.TamUngModels.Sum(x => x.VND.Value);
            }

            // group theo phong
            var result2 = (from r in result1
                           group r by r.Name_Phong into gr
                           select new TamUngModel_GroupBy_Name_Phong()
                           {
                               Name_Phong = gr.Key,
                               TamUngModel_GroupBy_Names = gr.ToList()
                           }).ToList();
            return result2;
        }

        public async Task<IEnumerable<TamUng>> Find_TamUngs_By_MaKh_Include(string maKh, string maCn, string tk)
        {
            var tamUngs = await _unitOfWork.tamUngRepository.FindIncludeOneAsync(x => x.KVCTPTC,
                y => y.MaKhNo == maKh && y.MaCn == maCn);
            if (tk != "")
            {
                tamUngs = tamUngs.Where(x => x.TKNo == tk); // tkNo: 1411 VND, 1412 NgoaiTe
            }

            // else : lấy hết TU VND và Ngoại Tệ --> dành cho KhongTC

            tamUngs = tamUngs.Where(x => x.ConLaiNT > 0);

            return tamUngs;
        }

        public async Task<IEnumerable<TamUng>> Find_TamUngs_By_MaKh_Include_KhongTC(string maKh, string maCn)
        {
            var tamUngs = await Find_TamUngs_By_MaKh_Include(maKh, maCn, ""); // tkNo: 1411 VND, 1412 NgoaiTe
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

        public async Task<IEnumerable<TamUng>> Find_TamUngs_By_PhieuChi_Include(string phieuChi, string maCn)
        {
            return await _unitOfWork.tamUngRepository.FindIncludeOneAsync(x => x.KVCTPTC, y => y.PhieuChi == phieuChi && y.MaCn == maCn);
        }

        public async Task<TamUng> GetByIdAsync(long id)
        {
            return await _unitOfWork.tamUngRepository.GetByLongIdAsync(id);
        }

        public string GetSoCT(string param, string maCn)
        {
            //DateTime dateTime;
            //dateTime = DateTime.Now;
            //dateTime = TourVM.Tour.NgayKyHopDong.Value;

            var currentYear = DateTime.Now.Year; // ngay hien tai
            var subfix = param + currentYear.ToString(); // QT2021? ?QC2021? ?NT2021? ?NC2021?
            //var tamUng = _unitOfWork.tamUngRepository.GetAllAsNoTracking().OrderByDescending(x => x.SoCT).ToList().FirstOrDefault();
            var tamUngs = _unitOfWork.tamUngRepository.Find(x => x.SoCT.Trim().Contains(subfix)).ToList();// // chi lay nhung soCT cung param: UN, UV

            var tamUng = new TamUng();
            if (tamUngs.Count() > 0)
            {
                tamUngs = tamUngs.Where(x => x.MaCn == maCn).ToList();
                tamUng = tamUngs.OrderByDescending(x => x.SoCT).FirstOrDefault();
            }

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

        public async Task CreateRangeAsync(IEnumerable<TamUng> tamUngs)
        {
            await _unitOfWork.tamUngRepository.CreateRange(tamUngs);
            await _unitOfWork.Complete();
        }

        public IEnumerable<TamUng> GetAll()
        {
            return _unitOfWork.tamUngRepository.GetAll();
        }

        public async Task<IEnumerable<TamUng>> Find_TamUngs_By_PhieuTT(string soCT, string maCn)
        {
            return await _unitOfWork.tamUngRepository.FindAsync(x => x.PhieuTT == soCT && x.MaCn == maCn);
        }

        public List<TamUng> FindByMaCn(string maCn)
        {
            return _unitOfWork.tamUngRepository.Find(x => x.MaCn == maCn).ToList();
        }
    }
}