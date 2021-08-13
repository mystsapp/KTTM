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
    }
    public class KVCTPCTRepository : Repository_KTTM<KVCTPTC>, IKVCTPCTRepository
    {
        public KVCTPCTRepository(KTTMDbContext context) : base(context)
        {
        }

    }
}
