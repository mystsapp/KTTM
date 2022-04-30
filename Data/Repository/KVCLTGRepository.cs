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

    }

    public class KVCLTGRepository : Repository_KTTM<KVCLTG>, IKVCLTGRepository
    {
        public KVCLTGRepository(KTTMDbContext context) : base(context)
        {
        }

    }
}