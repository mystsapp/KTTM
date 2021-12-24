using Data.Interfaces;
using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IKVCTPCTRepository : IRepository<KVCTPTC>
    {
        Task DeleteRangeAsync(IEnumerable<KVCTPTC> kVCTPCTs);
    }

    public class KVCTPCTRepository : Repository_KTTM<KVCTPTC>, IKVCTPCTRepository
    {
        public KVCTPCTRepository(KTTMDbContext context) : base(context)
        {
        }

        public async Task DeleteRangeAsync(IEnumerable<KVCTPTC> kVCTPCTs)
        {
            _context.KVCTPTCs.RemoveRange(kVCTPCTs);
            await _context.SaveChangesAsync();
        }
    }
}