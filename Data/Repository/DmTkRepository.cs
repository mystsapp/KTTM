using Data.Models_DanhMucKT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IDmTkRepository
    {
        IEnumerable<DmTk> GetAll();
        IEnumerable<DmTk> GetAllAsNoTracking();
        IEnumerable<ViewDmTk> GetAll_View();
        void Update(DmTk dmTk);
        void Create(DmTk entity);
        Task UpdateRangeAsync(List<DmTk> dmTks);
    }
    public class DmTkRepository : IDmTkRepository
    {
        private readonly DanhMucKTContext _context;

        public DmTkRepository(DanhMucKTContext context)
        {
            _context = context;
        }

        public void Create(DmTk entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }
        public IEnumerable<DmTk> GetAll()
        {
            return _context.DmTks;
            
        }

        public IEnumerable<DmTk> GetAllAsNoTracking()
        {
            return _context.DmTks.AsNoTracking();
        }

        public IEnumerable<ViewDmTk> GetAll_View()
        {
            return _context.ViewDmTks;
        }

        public void Update(DmTk dmTk)
        {
            _context.Entry(dmTk).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateRangeAsync(List<DmTk> dmTks)
        {
            _context.DmTks.UpdateRange(dmTks);
            await _context.SaveChangesAsync();
        }
    }
}
