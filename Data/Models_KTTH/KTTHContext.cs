using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Models_KTTH
{
    public partial class KTTHContext : DbContext
    {
        public KTTHContext()
        {
        }

        public KTTHContext(DbContextOptions<KTTHContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Lockkvct> Lockkvcts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=118.68.170.128;database=KTTH;Trusted_Connection=true;User Id=vanhong;Password=Hong@2019;Integrated security=false;MultipleActiveResultSets=true");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Lockkvct>(entity =>
            {
                entity.ToTable("lockkvct");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Lockdata).HasColumnName("lockdata");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Ngaycapnhat)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaycapnhat");

                entity.Property(e => e.Thangnam)
                    .HasColumnType("datetime")
                    .HasColumnName("thangnam");

                entity.Property(e => e.Userkhoa)
                    .HasMaxLength(10)
                    .HasColumnName("userkhoa")
                    .IsFixedLength(true);
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
