using Data.Interfaces;
using Data.Models_KTTM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Repository
{
    public interface ITamUngRepository : IRepository<TamUng>
    {
        IEnumerable<TamUng> FindTamUngs_IncludeTwice_By_Phong(string boPhan);
        IEnumerable<TamUng> FindTamUngs_IncludeTwice_By_Phong();
    }
    public class TamUngRepository : Repository_KTTM<TamUng>, ITamUngRepository
    {
        public TamUngRepository(KTTMDbContext context) : base(context)
        {
        }

        public IEnumerable<TamUng> FindTamUngs_IncludeTwice_By_Phong(string boPhan)
        {
            return _context.TamUngs.Where(x => x.Phong == boPhan && x.ConLaiNT > 0).Include(x => x.KVCTPCT).ThenInclude(x => x.KVPCT);
        }
        
        public IEnumerable<TamUng> FindTamUngs_IncludeTwice_By_Phong()
        {
            return _context.TamUngs.Where(x => x.ConLaiNT > 0).Include(x => x.KVCTPCT).ThenInclude(x => x.KVPCT);
        }
        
    }
}
