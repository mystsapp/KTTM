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
        Task<int> Complete();

    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly qltaikhoanContext _qltaikhoanContext;
        private readonly KTTMDbContext _kTTMDbContext;
        private readonly qltourContext _qltourContext;

        public UnitOfWork(qltaikhoanContext qltaikhoanContext, KTTMDbContext kTTMDbContext, qltourContext qltourContext)
        {
            _qltaikhoanContext = qltaikhoanContext;
            _kTTMDbContext = kTTMDbContext;
            _qltourContext = qltourContext;

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
        }

        public IUserQLTaiKhoanRepository userQLTaiKhoanRepository { get; }

        public IApplicationUserQLTaiKhoanRepository applicationUserQLTaiKhoanRepository { get; }

        public IApplicationQLTaiKhoanRepository applicationQLTaiKhoanRepository { get; }

        public IKVPCTRepository kVPCTRepository { get; }

        public IKVCTPCTRepository kVCTPCTRepository { get; }

        public INgoaiTeRepository ngoaiTeRepository { get; }

        public IPhongBanRepository phongBanRepository { get; }

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
            GC.Collect();
        }
    }
}
