using Data.Interfaces;
using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IKVCLTGRepository : IRepository<KVCLTG>
    {
        Task<KVCLTG> CreateReturnAsync(KVCLTG entity);
    }

    public class KVCLTGRepository : Repository_KTTM<KVCLTG>, IKVCLTGRepository
    {
        public KVCLTGRepository(KTTMDbContext context) : base(context)
        {
        }

        public async Task<KVCLTG> CreateReturnAsync(KVCLTG entity)
        {
            var entityEntry = await _context.KVCLTGs.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }
    }
}