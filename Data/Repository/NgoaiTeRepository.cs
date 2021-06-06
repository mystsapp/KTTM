using Data.Models_QLTour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface INgoaiTeRepository
    {
        IEnumerable<Ngoaite> GetAll();

        Task<Ngoaite> GetByIdAsync(int id);
        Ngoaite GetById(int id);

        IEnumerable<Ngoaite> Find(Func<Ngoaite, bool> predicate);
    }
    public class NgoaiTeRepository : INgoaiTeRepository
    {
        private readonly qltourContext _qltourContext;

        public NgoaiTeRepository(qltourContext qltourContext)
        {
            _qltourContext = qltourContext;
        }

        public IEnumerable<Ngoaite> Find(Func<Ngoaite, bool> predicate)
        {
            return _qltourContext.Ngoaites.Where(predicate);
        }

        public IEnumerable<Ngoaite> GetAll()
        {
            return _qltourContext.Ngoaites;
        }

        public Ngoaite GetById(int id)
        {
            return _qltourContext.Ngoaites.Find(id);
        }

        public async Task<Ngoaite> GetByIdAsync(int id)
        {
            return await _qltourContext.Ngoaites.FindAsync(id);
        }
    }
}
