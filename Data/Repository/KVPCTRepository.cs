using Data.Interfaces;
using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IKVPCTRepository : IRepository<KVPTC>
    {

    }
    public class KVPCTRepository : Repository_KTTM<KVPTC>, IKVPCTRepository
    {
        public KVPCTRepository(KTTMDbContext context) : base(context)
        {
        }
    }

}
