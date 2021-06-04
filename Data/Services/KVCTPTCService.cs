using Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Services
{
    public interface IKVCTPTCService
    {

    }
    public class KVCTPTCService : IKVCTPTCService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KVCTPTCService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
