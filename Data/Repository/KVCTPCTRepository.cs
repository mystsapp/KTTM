using Data.Interfaces;
using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IKVCTPCTRepository : IRepository<KVCTPCT>
    {

    }
    public class KVCTPCTRepository : Repository_KTTM<KVCTPCT>, IKVCTPCTRepository
    {
        public KVCTPCTRepository(KTTMDbContext context) : base(context)
        {
        }
    }
}
