using EmployeeAPI.Model.Domain;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeAPI.Data
{
    public class EmployeeDBcontext : IdentityDbContext
    {

        public EmployeeDBcontext(DbContextOptions options) : base(options)
        {

        }
       public DbSet<ChamCong> ChamCong { get; set; }
        public DbSet<HopDong> HopDong { get; set; }
        public DbSet<Luong> Luong { get; set; }
        public DbSet<NghiPhep> NghiPhep { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<ThanhToan> ThanhToan { get; set; }
        public DbSet<ChucVu> ChucVu { get; set; }
        public DbSet<PhongBan> PhongBan { get; set; }
        public DbSet<ExtendIdentity> ExtendIdentity { get; set; }
        public DbSet<LunchShift> LunchShift { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasMany(x => x.ChamCong)
                .WithOne(x => x.NhanVien)
                .HasForeignKey(x => x.NhanVienId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(x => x.HopDong)
                .WithOne(x => x.NhanVien)
                .HasForeignKey(x => x.NhanVienId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(x => x.ThanhToan)
                .WithOne(x => x.NhanVien)
                .HasForeignKey(x => x.NhanVienId)
                .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(x => x.NghiPhep)
                .WithOne(x => x.NhanVien)
                .HasForeignKey(x => x.NhanVienId)
                .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(x => x.LunchShift)
                .WithOne(x => x.Nhanvien)
                .HasForeignKey(x => x.NhanvienId)
                .OnDelete(DeleteBehavior.Cascade);           
            });
            modelBuilder.Entity<ChucVu>(entity =>
            {
                entity.HasMany(x => x.NhanVien)
                .WithOne(x => x.ChucVu)
                .HasForeignKey(x => x.ChucVuId)
                .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<PhongBan>(entity =>
            {
                entity.HasMany(x => x.NhanVien)
                .WithOne(x => x.PhongBan)
                .HasForeignKey(x => x.PhongBanId)
                .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Luong>(entity =>
            {
                entity.HasMany(x => x.NhanVien)
                .WithOne(x => x.Luong)
                .HasForeignKey(x => x.LuongId)
                .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<ChamCong>(entity =>
            {
                entity.Property(s => s.Clockin).HasConversion<long>();
                entity.Property(s => s.ClockOut).HasConversion<long>();
            });

            modelBuilder.Entity<ThanhToan>(entity =>
            {
                entity.HasKey(p => new { p.NhanVienId });
                entity.HasOne(x => x.NhanVien)
                .WithMany(x => x.ThanhToan)
                .HasForeignKey(x => x.NhanVienId)
                .OnDelete(DeleteBehavior.NoAction);
                entity.Property(x => x.Id)
                .ValueGeneratedNever();
            });
            modelBuilder.Entity<LunchShift>(entity =>
            {
                entity.Property(x => x.StartTime).HasConversion<long>();
                entity.Property(x => x.EndTime).HasConversion<long>();
            });

        }
    }
}
