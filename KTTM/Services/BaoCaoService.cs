using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.Repository;
using Data.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTTM.Services
{
    public interface IBaoCaoService
    {
        PhongBan GetPhongBanById(int id);
    }
    public class BaoCaoService : IBaoCaoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaoCaoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PhongBan GetPhongBanById(int id)
        {
            return _unitOfWork.phongBan_DanhMucKT_Repository.GetById(id);
        }
    }
}
