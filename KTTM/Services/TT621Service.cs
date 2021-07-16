using Data.Models_KTTM;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Services
{
    public interface ITT621Service
    {
        Task<IEnumerable<TT621>> GetTT621s_By_TamUng(long tamUngId);
        Task CreateAsync(TT621 tT621);
        Task UpdateAsync(TT621 tT621);
    }
    public class TT621Service : ITT621Service
    {
        private readonly IUnitOfWork _unitOfWork;

        public TT621Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(TT621 tT621)
        {
            _unitOfWork.tT621Repository.Create(tT621);
            await _unitOfWork.Complete();
        }

        public async Task<IEnumerable<TT621>> GetTT621s_By_TamUng(long tamUngId)
        {
            return await _unitOfWork.tT621Repository.FindIncludeOneAsync(x => x.TamUng, y => y.Id == tamUngId);
        }

        public async Task UpdateAsync(TT621 tT621)
        {
            _unitOfWork.tT621Repository.Update(tT621);
            await _unitOfWork.Complete();
        }
    }
}
