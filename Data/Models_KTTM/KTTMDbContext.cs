using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models_KTTM
{
    public class KTTMDbContext : DbContext
    {
        public KTTMDbContext(DbContextOptions<KTTMDbContext> options) : base(options)
        {
        }

        public DbSet<KVPTC> KVPTCs { get; set; }
        public DbSet<KVCTPTC> KVCTPTCs { get; set; }
        public DbSet<TamUng> TamUngs { get; set; }
        public DbSet<TT621> TT621s { get; set; }
        public DbSet<TonQuy> TonQuies { get; set; }

        public DbSet<KVCLTG> KVCLTGs { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }
    }
}