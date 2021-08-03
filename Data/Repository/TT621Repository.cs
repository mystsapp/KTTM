using Data.Interfaces;
using Data.Models_KTTM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ITT621Repository : IRepository<TT621>
    {
        IEnumerable<TT621> FindTT621s_IncludeTwice_By_Date(DateTime fromDate, DateTime toDate);
        IEnumerable<TT621> FindTT621s_IncludeTwice_By_Date(DateTime fromDate, string nullToDate);
        IEnumerable<TT621> FindTT621s_IncludeTwice_By_Date(string nullFromDate, DateTime toDate);
    }
    public class TT621Repository : Repository_KTTM<TT621>, ITT621Repository
    {
        public TT621Repository(KTTMDbContext context) : base(context)
        {
        }

        public IEnumerable<TT621> FindTT621s_IncludeTwice_By_Date(DateTime fromDate, DateTime toDate)
        {
            return _context.TT621s.Where(x => x.NgayCT >= fromDate && x.NgayCT < toDate.AddDays(1))
                                  .Include(x => x.TamUng)
                                  .ThenInclude(x => x.KVCTPCT)
                                  .ThenInclude(x => x.KVPCT);
            
        }

        public IEnumerable<TT621> FindTT621s_IncludeTwice_By_Date(DateTime fromDate, string nullToDate)
        {
            return _context.TT621s.Where(x => x.NgayCT >= fromDate)
                                  .Include(x => x.TamUng)
                                  .ThenInclude(x => x.KVCTPCT)
                                  .ThenInclude(x => x.KVPCT);
            
        }
        
        public IEnumerable<TT621> FindTT621s_IncludeTwice_By_Date(string nullFromDate, DateTime toDate)
        {
            return _context.TT621s.Where(x => x.NgayCT < toDate.AddDays(1))
                                  .Include(x => x.TamUng)
                                  .ThenInclude(x => x.KVCTPCT)
                                  .ThenInclude(x => x.KVPCT);
            
        }

    }
}
