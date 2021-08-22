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
        IEnumerable<TamUng> FindTamUngs_IncludeTwice_By_Phong(string boPhan, string maCn);
        IEnumerable<TamUng> FindTamUngs_IncludeTwice_By_Phong(string maCn);
    }
    public class TamUngRepository : Repository_KTTM<TamUng>, ITamUngRepository
    {
        public TamUngRepository(KTTMDbContext context) : base(context)
        {
        }

        public IEnumerable<TamUng> FindTamUngs_IncludeTwice_By_Phong(string boPhan, string maCn)
        {
            var tamUngs = _context.TamUngs.Where(x => x.Phong == boPhan && x.ConLaiNT > 0).Include(x => x.KVCTPTC).ThenInclude(x => x.KVPTC).ToList();
            tamUngs = tamUngs.Where(x => x.MaCn == maCn).ToList();
            return tamUngs;
        }
        
        public IEnumerable<TamUng> FindTamUngs_IncludeTwice_By_Phong(string maCn)
        {
            return _context.TamUngs.Where(x => x.ConLaiNT > 0 && x.MaCn == maCn).Include(x => x.KVCTPTC).ThenInclude(x => x.KVPTC);
        }
        
    }
}
