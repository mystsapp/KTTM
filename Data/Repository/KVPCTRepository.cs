using Data.Interfaces;
using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IKVPCTRepository : IRepository<KVPCT>
    {

    }
    public class KVPCTRepository : Repository_KTTM<KVPCT>, IKVPCTRepository
    {
        public KVPCTRepository(KTTMDbContext context) : base(context)
        {
        }
    }

}
