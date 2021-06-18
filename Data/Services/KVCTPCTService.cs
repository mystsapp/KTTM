using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.Models_QLTour;
using Data.Repository;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public interface IKVCTPCTService
    {
        Task<IEnumerable<KVCTPCT>> List_KVCTPCT_By_SoCT(string soCT);
        IEnumerable<Ngoaite> GetAll_NgoaiTes();
        IEnumerable<DmHttc> GetAll_DmHttc();
        DmTk Get_DmTk_By_TaiKhoan(string tk);
        Task Create(KVCTPCT kVCTPCT);
        IEnumerable<ListViewModel> GetAll_TkCongNo_With_TenTK();
        IEnumerable<ListViewModel> GetAll_TaiKhoan_Except_TkConngNo();
    }
    public class KVCTPCTService : IKVCTPCTService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KVCTPCTService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<KVCTPCT>> List_KVCTPCT_By_SoCT(string soCT)
        {
            return await _unitOfWork.kVCTPCTRepository.FindIncludeOneAsync(x => x.KVPCT, x => x.KVPCTId == soCT);
        }

        public IEnumerable<Ngoaite> GetAll_NgoaiTes()
        {
            return _unitOfWork.ngoaiTeRepository.GetAll();
        }

        public async Task Create(KVCTPCT kVCTPCT)
        {
            _unitOfWork.kVCTPCTRepository.Create(kVCTPCT);
            await _unitOfWork.Complete();
        }

        public IEnumerable<DmHttc> GetAll_DmHttc()
        {
            return _unitOfWork.dmHttcRepository.GetAll();
        }
        public DmTk Get_DmTk_By_TaiKhoan(string tk)
        {
            var dmTks = _unitOfWork.dmTkRepository.GetAll();
            return dmTks.Where(x => x.Tkhoan == tk).FirstOrDefault();
        }

        public IEnumerable<ListViewModel> GetAll_TkCongNo_With_TenTK()
        {

            var dmTks = GetAll_Tk_By_TkCongNo();

           var  listTkCongNoWithTenTK = Convert_DmTk_To_ListViewModel(dmTks);

            return listTkCongNoWithTenTK;

        }
        public IEnumerable<DmTk> GetAll_Tk_By_TkCongNo()
        {
            var dmTks = _unitOfWork.dmTkRepository.GetAll();
            var tkCongNos = _unitOfWork.tkCongNoRepository.GetAll();
            var dmTks1 = dmTks.Where(item1 => tkCongNos.Any(item2 => item1.Tkhoan == item2.Tkhoan));// lấy những tkcongno có trong dmtk => tentk
            return dmTks1;
        }

        public IEnumerable<ListViewModel> Convert_DmTk_To_ListViewModel(IEnumerable<DmTk> dmTks)
        {

            List<ListViewModel> listTkCongNoWithTenTK = new List<ListViewModel>();
            foreach (var item in dmTks)
            {
                listTkCongNoWithTenTK.Add(new ListViewModel() { StringId = item.Tkhoan, Name = item.Tkhoan + " - " + item.TenTk });
            }

            return listTkCongNoWithTenTK;
        }
        public IEnumerable<ListViewModel> GetAll_TaiKhoan_Except_TkConngNo()
        {
            var dmTks = _unitOfWork.dmTkRepository.GetAll();
            var dmTks1 = GetAll_Tk_By_TkCongNo();
            // peopleList2.Except(peopleList1) c1
            // var result = peopleList2.Where(p => !peopleList1.Any(p2 => p2.ID == p.ID)); c2
            // var result = peopleList2.Where(p => peopleList1.All(p2 => p2.ID != p.ID)); c3
            var dmTks2 = dmTks1.Except(dmTks); // 
           var listViewModels = Convert_DmTk_To_ListViewModel(dmTks2);

            return listViewModels;
        }
    }
}
