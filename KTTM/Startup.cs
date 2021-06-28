using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Data.Models_Cashier;
using Data.Models_DanhMucKT;
using Data.Models_HDVATOB;
using Data.Models_KTTM;
using Data.Models_QLTaiKhoan;
using Data.Models_QLTour;
using Data.Repository;
using Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace KTTM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<qltaikhoanContext>(options => options.UseSqlServer(Configuration.GetConnectionString("QLTaiKhoanConnection"))/*.EnableSensitiveDataLogging()*/);
            services.AddDbContext<qltourContext>(options => options.UseSqlServer(Configuration.GetConnectionString("QLTourConnection"))/*.EnableSensitiveDataLogging()*/);
            services.AddDbContext<KTTMDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))/*.EnableSensitiveDataLogging()*/);
            services.AddDbContext<DanhMucKTContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DanhMucKTConnection"))/*.EnableSensitiveDataLogging()*/);
            services.AddDbContext<qlcashierContext>(options => options.UseSqlServer(Configuration.GetConnectionString("QLCashierConnection"))/*.EnableSensitiveDataLogging()*/);
            services.AddDbContext<hdvatobContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HdVATObConnection"))/*.EnableSensitiveDataLogging()*/);

            // qltaikhoan
            services.AddTransient<IUserQLTaiKhoanRepository, UserQLTaiKhoanRepository>();
            services.AddTransient<IApplicationUserQLTaiKhoanRepository, ApplicationUserQLTaiKhoanRepository>();
            services.AddTransient<IApplicationQLTaiKhoanRepository, ApplicationQLTaiKhoanRepository>();
            services.AddTransient<ISupplier_QLTaiKhoan_Repository, Supplier_QLTaiKhoan_Repository>();

            // qltour
            services.AddTransient<INgoaiTeRepository, NgoaiTeRepository>();
            services.AddTransient<IPhongBanRepository, PhongBanRepository>();

            // KTTM
            services.AddTransient<IKVPCTRepository, KVPCTRepository>();
            services.AddTransient<IKVCTPCTRepository, KVCTPCTRepository>();

            // DanhMucKT
            services.AddTransient<IDmTkRepository, DmTkRepository>();
            services.AddTransient<ITkCongNoRepository, TkCongNoRepository>();
            services.AddTransient<IDmHttcRepository, DmHttcRepository>();
            services.AddTransient<IDGiaiRepository, DGiaiRepository>();
            services.AddTransient<IQuayRepository, QuayRepository>();
            services.AddTransient<ISupplier_DanhMucKT_Repository, Supplier_DanhMucKT_Repository>();
            services.AddTransient<IMatHangRepository, MatHangRepository>();
            services.AddTransient<IPhongBan_DanhMucKT_Repository, PhongBan_DanhMucKT_Repository>();

            // Cashier
            services.AddTransient<INopTienRepository, NopTienRepository>();
            services.AddTransient<INtbillRepository, NtbillRepository>();
            services.AddTransient<ICtbillRepository, CtbillRepository>();

            // hdvatob
            services.AddTransient<ISupplier_hdvatob_Repository, Supplier_hdvatob_Repository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddTransient<IKVPCTService, KVPCTService>();
            services.AddTransient<IKVCTPCTService, KVCTPCTService>();

            // FOR session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // for session
            app.UseSession();

            // culture format
            var supportedCultures = new[] { new CultureInfo("en-AU") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-AU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
