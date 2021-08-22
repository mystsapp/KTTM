using Data.Interfaces;
using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IKVPCTRepository : IRepository<KVPTC>
    {
        Task<KVPTC> GetByGuidIdAsync(Guid id);
    }
    public class KVPCTRepository : Repository_KTTM<KVPTC>, IKVPCTRepository
    {
        public KVPCTRepository(KTTMDbContext context) : base(context)
        {
        }

        public async Task<KVPTC> GetByGuidIdAsync(Guid id)
        {
            return await _context.KVPTCs.FindAsync(id);
        }
    }

}
