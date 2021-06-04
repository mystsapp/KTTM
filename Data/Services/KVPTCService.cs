using Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Services
{
    public interface IKVPTCService
    {

    }
    public class KVPTCService : IKVPTCService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KVPTCService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
