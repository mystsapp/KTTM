using Data.Models_KTTM;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Services
{
    public interface IKVPTCService
    {
        IEnumerable<KVPCT> GetAll();
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
    }
}
