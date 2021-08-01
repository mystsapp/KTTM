using Data.Interfaces;
using Data.Models_KTTM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ITT621Repository : IRepository<TT621>
    {
        IEnumerable<TT621> GetTT621s_IncludeTwice();
    }
    public class TT621Repository : Repository_KTTM<TT621>, ITT621Repository
    {
        public TT621Repository(KTTMDbContext context) : base(context)
        {
        }

        public IEnumerable<TT621> GetTT621s_IncludeTwice()
        {
            return _context.TT621s.Include(x => x.TamUng).ThenInclude(x => x.KVCTPCT);
        }

    }
}
