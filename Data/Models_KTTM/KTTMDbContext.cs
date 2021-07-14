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

        public DbSet<KVPCT> KVPCTs { get; set; }
        public DbSet<KVCTPCT> KVCTPCTs { get; set; }
        public DbSet<TamUng> TamUngs { get; set; }
        //public DbSet<TonQuy> TonQuies { get; set; }
        //public DbSet<KVCLTG> KVCLTGs { get; set; }
    }
}
