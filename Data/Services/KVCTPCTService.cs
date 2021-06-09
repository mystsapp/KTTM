using Data.Models_KTTM;
using Data.Models_QLTour;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public interface IKVCTPCTService
    {
        Task<IEnumerable<KVCTPCT>> List_KVCTPCT_By_SoCT(string soCT);
        IEnumerable<Ngoaite> GetAll_NgoaiTes();
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
    }
}
