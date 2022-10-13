using Data.Models_Cashier;
using Data.Models_DanhMucKT;
using Data.Models_HDVATOB;
using Data.Models_KTTH;
using Data.Models_KTTM;
using Data.Models_QLTaiKhoan;
using Data.Models_QLTour;
using Data.Models_QLXe;
using Data.Repository.KTTH;
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
        ISupplier_QLTaiKhoan_Repository supplier_QLTaiKhoan_Repository { get; }
        IChiNhanhRepository chiNhanhRepository { get; }

        // qltour
        INgoaiTeRepository ngoaiTeRepository { get; }

        IPhongBanRepository phongBanRepository { get; }

        // KTTM
        IKVPCTRepository kVPCTRepository { get; }

        IKVCTPCTRepository kVCTPCTRepository { get; }
        ITamUngRepository tamUngRepository { get; }
        ITT621Repository tT621Repository { get; }
        ITonQuyRepository tonQuyRepository { get; }
        IKVCLTGRepository kVCLTGRepository { get; }
        IErrorLogRepository errorLogRepository { get; }

        // DanhMucKT
        IDmTkRepository dmTkRepository { get; }

        ITkCongNoRepository tkCongNoRepository { get; }
        IDmHttcRepository dmHttcRepository { get; }
        IDGiaiRepository dGiaiRepository { get; }
        IQuayRepository quayRepository { get; }
        ISupplier_DanhMucKT_Repository supplier_DanhMucKT_Repository { get; }
        IMatHangRepository matHangRepository { get; }
        IPhongBan_DanhMucKT_Repository phongBan_DanhMucKT_Repository { get; }
        INgoaiTe_DanhMucKT_Repository ngoaiTe_DanhMucKT_Repository { get; }
        IKhachHang_DanhMucKTRepository khachHang_DanhMucKTRepository { get; }

        // Cashier
        INopTienRepository nopTienRepository { get; }

        INtbillRepository ntbillRepository { get; }
        ICtbillRepository ctbillRepository { get; }

        // hdvatob
        ISupplier_hdvatob_Repository supplier_Hdvatob_Repository { get; }

        // xe
        IXeRepository xeRepository { get; }

        // ktth
        ILockKvctRepository lockKvctRepository { get; }

        Task<int> Complete();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly qltaikhoanContext _qltaikhoanContext;
        private readonly KTTMDbContext _kTTMDbContext;
        private readonly qltourContext _qltourContext;
        private readonly DanhMucKTContext _danhMucKTContext;
        private readonly qlcashierContext _qlcashierContext;
        private readonly hdvatobContext _hdvatobContext;
        private readonly quanlyxeContext _quanlyxeContext;
        private readonly KTTHContext _kTTHContext;

        public UnitOfWork(qltaikhoanContext qltaikhoanContext, KTTMDbContext kTTMDbContext,
            qltourContext qltourContext, DanhMucKTContext danhMucKTContext,
            qlcashierContext qlcashierContext, hdvatobContext hdvatobContext, quanlyxeContext quanlyxeContext,
            KTTHContext kTTHContext)
        {
            _qltaikhoanContext = qltaikhoanContext;
            _kTTMDbContext = kTTMDbContext;
            _qltourContext = qltourContext;
            _danhMucKTContext = danhMucKTContext;
            _qlcashierContext = qlcashierContext;
            _hdvatobContext = hdvatobContext;
            _quanlyxeContext = quanlyxeContext;
            _kTTHContext = kTTHContext;

            // qltaikhoan
            userQLTaiKhoanRepository = new UserQLTaiKhoanRepository(_qltaikhoanContext);
            applicationUserQLTaiKhoanRepository = new ApplicationUserQLTaiKhoanRepository(_qltaikhoanContext);
            applicationQLTaiKhoanRepository = new ApplicationQLTaiKhoanRepository(_qltaikhoanContext);
            supplier_QLTaiKhoan_Repository = new Supplier_QLTaiKhoan_Repository(_qltaikhoanContext);
            chiNhanhRepository = new ChiNhanhRepository(_qltaikhoanContext);

            // qltour
            ngoaiTeRepository = new NgoaiTeRepository(_qltourContext);
            phongBanRepository = new PhongBanRepository(_qltourContext);

            // kttm
            kVPCTRepository = new KVPCTRepository(_kTTMDbContext);
            kVCTPCTRepository = new KVCTPCTRepository(_kTTMDbContext);
            tamUngRepository = new TamUngRepository(_kTTMDbContext);
            tT621Repository = new TT621Repository(_kTTMDbContext);
            kVCLTGRepository = new KVCLTGRepository(_kTTMDbContext);
            tonQuyRepository = new TonQuyRepository(_kTTMDbContext);

            // DanhMucKT
            dmTkRepository = new DmTkRepository(_danhMucKTContext);
            tkCongNoRepository = new TkCongNoRepository(_danhMucKTContext);
            dmHttcRepository = new DmHttcRepository(_danhMucKTContext);
            dGiaiRepository = new DGiaiRepository(_danhMucKTContext);
            quayRepository = new QuayRepository(_danhMucKTContext);
            supplier_DanhMucKT_Repository = new Supplier_DanhMucKT_Repository(_danhMucKTContext);
            matHangRepository = new MatHangRepository(_danhMucKTContext);
            phongBan_DanhMucKT_Repository = new PhongBan_DanhMucKT_Repository(_danhMucKTContext);
            ngoaiTe_DanhMucKT_Repository = new NgoaiTe_DanhMucKT_Repository(_danhMucKTContext);
            khachHang_DanhMucKTRepository = new KhachHang_DanhMucKTRepository(_danhMucKTContext);
            errorLogRepository = new ErrorLogRepository(_kTTMDbContext);

            // Cashier
            nopTienRepository = new NopTienRepository(_qlcashierContext);
            ntbillRepository = new NtbillRepository(_qlcashierContext);
            ctbillRepository = new CtbillRepository(_qlcashierContext);

            // hdvatob
            supplier_Hdvatob_Repository = new Supplier_hdvatob_Repository(_hdvatobContext);

            // xe
            xeRepository = new XeRepository(_quanlyxeContext);

            // ktth
            lockKvctRepository = new LockKvctRepository(_kTTHContext);
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

        public INopTienRepository nopTienRepository { get; }

        public INtbillRepository ntbillRepository { get; }

        public ICtbillRepository ctbillRepository { get; }

        public ISupplier_QLTaiKhoan_Repository supplier_QLTaiKhoan_Repository { get; }

        public ISupplier_hdvatob_Repository supplier_Hdvatob_Repository { get; }

        public ITamUngRepository tamUngRepository { get; }

        public ITT621Repository tT621Repository { get; }

        public ITonQuyRepository tonQuyRepository { get; }

        public INgoaiTe_DanhMucKT_Repository ngoaiTe_DanhMucKT_Repository { get; }

        public IXeRepository xeRepository { get; }

        public IKhachHang_DanhMucKTRepository khachHang_DanhMucKTRepository { get; }

        public IErrorLogRepository errorLogRepository { get; }

        public IChiNhanhRepository chiNhanhRepository { get; }

        public IKVCLTGRepository kVCLTGRepository { get; }

        public ILockKvctRepository lockKvctRepository { get; }

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
            _qlcashierContext.Dispose();
            GC.Collect();
        }
    }
}