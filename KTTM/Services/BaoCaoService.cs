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

        IEnumerable<NgoaiTe> GetAllNgoaiTe();
    }

    public class BaoCaoService : IBaoCaoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaoCaoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NgoaiTe> GetAllNgoaiTe()
        {
            return _unitOfWork.ngoaiTe_DanhMucKT_Repository.GetAll();
        }

        public PhongBan GetPhongBanById(int id)
        {
            return _unitOfWork.phongBan_DanhMucKT_Repository.GetById(id);
        }
    }
}