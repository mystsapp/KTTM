using Data.Interfaces;
using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface ITonQuyRepository : IRepository<TonQuy>
    {

    }
    public class TonQuyRepository : Repository_KTTM<TonQuy>, ITonQuyRepository
    {
        public TonQuyRepository(KTTMDbContext context) : base(context)
        {
        }
    }
}
