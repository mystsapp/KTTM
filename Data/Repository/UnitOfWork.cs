using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.Models_QLTaiKhoan;
using Data.Models_QLTour;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {

        // qltaikhoan
        IUserQLTaiKhoanRepository userQLTaiKhoanRepository { get; }
        IApplicationUserQLTaiKhoanRepository applicationUserQLTaiKhoanRepository { get; }
        IApplicationQLTaiKhoanRepository applicationQLTaiKhoanRepository { get; }

        // qltour
        INgoaiTeRepository ngoaiTeRepository { get; }
        IPhongBanRepository phongBanRepository { get; }

        // KTTM
        IKVPCTRepository kVPCTRepository { get; }
        IKVCTPCTRepository kVCTPCTRepository { get; }

        // DanhMucKT
        IDmTkRepository dmTkRepository { get; }
        ITkCongNoRepository tkCongNoRepository { get; }
        IDmHttcRepository dmHttcRepository { get; }
        IDGiaiRepository dGiaiRepository { get; }
        IQuayRepository quayRepository { get; }
        ISupplier_DanhMucKT_Repository supplier_DanhMucKT_Repository { get; }
        IMatHangRepository matHangRepository { get; }
        IPhongBan_DanhMucKT_Repository phongBan_DanhMucKT_Repository { get; }
        Task<int> Complete();

    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly qltaikhoanContext _qltaikhoanContext;
        private readonly KTTMDbContext _kTTMDbContext;
        private readonly qltourContext _qltourContext;
        private readonly DanhMucKTContext _danhMucKTContext;

        public UnitOfWork(qltaikhoanContext qltaikhoanContext, KTTMDbContext kTTMDbContext, qltourContext qltourContext, DanhMucKTContext danhMucKTContext)
        {
            _qltaikhoanContext = qltaikhoanContext;
            _kTTMDbContext = kTTMDbContext;
            _qltourContext = qltourContext;
            _danhMucKTContext = danhMucKTContext;

            // qltaikhoan
            userQLTaiKhoanRepository = new UserQLTaiKhoanRepository(_qltaikhoanContext);
            applicationUserQLTaiKhoanRepository = new ApplicationUserQLTaiKhoanRepository(_qltaikhoanContext);
            applicationQLTaiKhoanRepository = new ApplicationQLTaiKhoanRepository(_qltaikhoanContext);

            // qltour
            ngoaiTeRepository = new NgoaiTeRepository(_qltourContext);
            phongBanRepository = new PhongBanRepository(_qltourContext);

            // kttm
            kVPCTRepository = new KVPCTRepository(_kTTMDbContext);
            kVCTPCTRepository = new KVCTPCTRepository(_kTTMDbContext);

            // DanhMucKT
            dmTkRepository = new DmTkRepository(_danhMucKTContext);
            tkCongNoRepository = new TkCongNoRepository(_danhMucKTContext);
            dmHttcRepository = new DmHttcRepository(_danhMucKTContext);
            dGiaiRepository = new DGiaiRepository(_danhMucKTContext);
            quayRepository = new QuayRepository(_danhMucKTContext);
            supplier_DanhMucKT_Repository = new Supplier_DanhMucKT_Repository(_danhMucKTContext);
            matHangRepository = new MatHangRepository(_danhMucKTContext);
            phongBan_DanhMucKT_Repository = new PhongBan_DanhMucKT_Repository(_danhMucKTContext);
        }

        public IUserQLTaiKhoanRepository userQLTaiKhoanRepository { get; }

        public IApplicationUserQLTaiKhoanRepository applicationUserQLTaiKhoanRepository { get; }

        public IApplicationQLTaiKhoanRepository applicationQLTaiKhoanRepository { get; }

        public IKVPCTRepository kVPCTRepository { get; }

        public IKVCTPCTRepository kVCTPCTRepository { get; }

        public INgoaiTeRepository ngoaiTeRepository { get; }

        public IPhongBanRepository phongBanRepository { get; }

        public ITkCongNoRepository tkCongNoRepository { get; }

        public IDmHttcRepository dmHttcRepository { get; }

        public IDmTkRepository dmTkRepository { get; }

        public IDGiaiRepository dGiaiRepository { get; }

        public IQuayRepository quayRepository { get; }

        public ISupplier_DanhMucKT_Repository supplier_DanhMucKT_Repository { get; }

        public IMatHangRepository matHangRepository { get; }

        public IPhongBan_DanhMucKT_Repository phongBan_DanhMucKT_Repository { get; }

        public async Task<int> Complete()
        {
            await _qltaikhoanContext.SaveChangesAsync();
            await _kTTMDbContext.SaveChangesAsync();

            return 1;
        }

        public void Dispose()
        {
            _qltaikhoanContext.Dispose();
            _kTTMDbContext.Dispose();
            _qltourContext.Dispose();
            _danhMucKTContext.Dispose();
            GC.Collect();
        }
    }
}
