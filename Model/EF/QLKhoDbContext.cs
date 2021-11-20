using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.EF
{
    public partial class QLKhoDbContext : DbContext
    {
        public QLKhoDbContext()
            : base("name=QLKhoDbContext")
        {
        }

        public virtual DbSet<ChiTietHDBan> ChiTietHDBans { get; set; }
        public virtual DbSet<ChiTietHDNhap> ChiTietHDNhaps { get; set; }
        public virtual DbSet<ChiTietKhaoSat> ChiTietKhaoSats { get; set; }
        public virtual DbSet<CongTy> CongTies { get; set; }
        public virtual DbSet<Congty_HDNo> Congty_HDNo { get; set; }
        public virtual DbSet<HDBan> HDBans { get; set; }
        public virtual DbSet<HDNhap> HDNhaps { get; set; }
        public virtual DbSet<HDNoCongTy> HDNoCongTies { get; set; }
        public virtual DbSet<HDNoKhachHang1> HDNoKhachHang1 { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<KhachHang_HDNO> KhachHang_HDNO { get; set; }
        public virtual DbSet<KhaoSatKho> KhaoSatKhoes { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<TrangTrai> TrangTrais { get; set; }
        public virtual DbSet<TraNo> TraNoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CongTy>()
                .Property(e => e.STK)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CongTy>()
                .Property(e => e.SoDienThoai)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CongTy>()
                .HasMany(e => e.Congty_HDNo)
                .WithRequired(e => e.CongTy)
                .HasForeignKey(e => e.MaCongTy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CongTy>()
                .HasMany(e => e.HDNhaps)
                .WithOptional(e => e.CongTy)
                .HasForeignKey(e => e.MaCongTy);

            modelBuilder.Entity<CongTy>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.CongTy)
                .HasForeignKey(e => e.MaCongTy);

            modelBuilder.Entity<HDBan>()
                .HasMany(e => e.ChiTietHDBans)
                .WithRequired(e => e.HDBan)
                .HasForeignKey(e => e.MaHDBan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HDBan>()
                .HasMany(e => e.HDNoKhachHang1)
                .WithRequired(e => e.HDBan)
                .HasForeignKey(e => e.MaHDBan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HDNhap>()
                .HasMany(e => e.ChiTietHDNhaps)
                .WithRequired(e => e.HDNhap)
                .HasForeignKey(e => e.MaHDNhap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HDNhap>()
                .HasMany(e => e.HDNoCongTies)
                .WithRequired(e => e.HDNhap)
                .HasForeignKey(e => e.MaHDNhap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HDNoCongTy>()
                .HasMany(e => e.Congty_HDNo)
                .WithRequired(e => e.HDNoCongTy)
                .HasForeignKey(e => e.MaHDNoCongTy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HDNoKhachHang1>()
                .HasMany(e => e.KhachHang_HDNO)
                .WithRequired(e => e.HDNoKhachHang1)
                .HasForeignKey(e => e.MaHDNO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.STK)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.HDBans)
                .WithOptional(e => e.KhachHang)
                .HasForeignKey(e => e.MaKhachHang);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.KhachHang_HDNO)
                .WithRequired(e => e.KhachHang)
                .HasForeignKey(e => e.MaKhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhaoSatKho>()
                .HasMany(e => e.ChiTietKhaoSats)
                .WithRequired(e => e.KhaoSatKho)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietHDBans)
                .WithRequired(e => e.SanPham)
                .HasForeignKey(e => e.MaSanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietHDNhaps)
                .WithRequired(e => e.SanPham)
                .HasForeignKey(e => e.MaSanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietKhaoSats)
                .WithRequired(e => e.SanPham)
                .HasForeignKey(e => e.MaSanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrangTrai>()
                .Property(e => e.STK)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TrangTrai>()
                .Property(e => e.SoDienThoai)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TrangTrai>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<TrangTrai>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);
        }
    }
}
