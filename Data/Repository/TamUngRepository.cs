using Data.Interfaces;
using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface ITamUngRepository : IRepository<TamUng>
    {

    }
    public class TamUngRepository : Repository_KTTM<TamUng>, ITamUngRepository
    {
        public TamUngRepository(KTTMDbContext context) : base(context)
        {
        }

    }
}
