using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTW.Data;

public partial class MaleFashionContext : DbContext
{
    public MaleFashionContext()
    {
    }

    public MaleFashionContext(DbContextOptions<MaleFashionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhSanPham> AnhSanPhams { get; set; }

    public virtual DbSet<BaiViet> BaiViets { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<BinhLuan> BinhLuans { get; set; }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<DanhGium> DanhGia { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DealOfWeek> DealOfWeeks { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<InstagramImage> InstagramImages { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    public virtual DbSet<ThongTinShop> ThongTinShops { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=MaleFashion;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnhSanPham>(entity =>
        {
            entity.HasKey(e => e.MaAnh).HasName("PK__AnhSanPh__356240DF2EC9175A");

            entity.ToTable("AnhSanPham");

            entity.Property(e => e.DuongDan).HasMaxLength(255);
            entity.Property(e => e.MaSp).HasColumnName("MaSP");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.AnhSanPhams)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK__AnhSanPham__MaSP__5441852A");
        });

        modelBuilder.Entity<BaiViet>(entity =>
        {
            entity.HasKey(e => e.MaBv).HasName("PK__BaiViet__272475953BB451FA");

            entity.ToTable("BaiViet");

            entity.Property(e => e.MaBv).HasColumnName("MaBV");
            entity.Property(e => e.AnhBia).HasMaxLength(255);
            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.NgayDang)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TieuDe).HasMaxLength(200);

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.BaiViets)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK__BaiViet__MaTK__71D1E811");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.MaBanner).HasName("PK__Banner__508B4A498F07E707");

            entity.ToTable("Banner");

            entity.Property(e => e.Anh).HasMaxLength(255);
            entity.Property(e => e.LienKet).HasMaxLength(255);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TieuDe).HasMaxLength(200);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<BinhLuan>(entity =>
        {
            entity.HasKey(e => e.MaBl).HasName("PK__BinhLuan__272475AF662CC8FD");

            entity.ToTable("BinhLuan");

            entity.Property(e => e.MaBl).HasColumnName("MaBL");
            entity.Property(e => e.MaBv).HasColumnName("MaBV");
            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.NgayBinhLuan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NoiDung).HasMaxLength(500);

            entity.HasOne(d => d.MaBvNavigation).WithMany(p => p.BinhLuans)
                .HasForeignKey(d => d.MaBv)
                .HasConstraintName("FK__BinhLuan__MaBV__75A278F5");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.BinhLuans)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK__BinhLuan__MaTK__76969D2E");
        });

        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => new { e.MaDh, e.MaSp }).HasName("PK__ChiTietD__F557D6E0E6BDE75F");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDon__MaDH__6383C8BA");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDon__MaSP__6477ECF3");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => new { e.MaGioHang, e.MaSp }).HasName("PK__ChiTietG__27724D227E763B48");

            entity.ToTable("ChiTietGioHang");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.SoLuong).HasDefaultValue(1);

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaGioHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietGi__MaGio__5BE2A6F2");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietGio__MaSP__5CD6CB2B");
        });

        modelBuilder.Entity<DanhGium>(entity =>
        {
            entity.HasKey(e => e.MaDg).HasName("PK__DanhGia__27258660A506C62E");

            entity.Property(e => e.MaDg).HasColumnName("MaDG");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.NgayDanhGia)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NoiDung).HasMaxLength(500);

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK__DanhGia__MaSP__6D0D32F4");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK__DanhGia__MaTK__6E01572D");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMuc__B3750887D565D9CB");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenDanhMuc).HasMaxLength(100);
        });

        modelBuilder.Entity<DealOfWeek>(entity =>
        {
            entity.HasKey(e => e.MaDeal).HasName("PK__DealOfWe__324A50A5026681BE");

            entity.ToTable("DealOfWeek");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.TieuDe).HasMaxLength(200);
            entity.Property(e => e.TyLeGiam).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.DealOfWeeks)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK__DealOfWeek__MaSP__01142BA1");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__DonHang__27258661838A4709");

            entity.ToTable("DonHang");

            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.DiaChiGiao).HasMaxLength(255);
            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.NgayDat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK__DonHang__MaTK__60A75C0F");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang).HasName("PK__GioHang__F5001DA305157130");

            entity.ToTable("GioHang");

            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK__GioHang__MaTK__5812160E");
        });

        modelBuilder.Entity<InstagramImage>(entity =>
        {
            entity.HasKey(e => e.MaAnh).HasName("PK__Instagra__356240DF3F0C6C02");

            entity.ToTable("InstagramImage");

            entity.Property(e => e.DuongDan).HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKm).HasName("PK__KhuyenMa__2725CF154EF3B812");

            entity.ToTable("KhuyenMai");

            entity.HasIndex(e => e.MaCode, "UQ__KhuyenMa__152C7C5C597330EF").IsUnique();

            entity.Property(e => e.MaKm).HasColumnName("MaKM");
            entity.Property(e => e.MaCode).HasMaxLength(50);
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.TyLeGiam).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.KhuyenMais)
                .HasForeignKey(d => d.MaDanhMuc)
                .HasConstraintName("FK__KhuyenMai__MaDan__7B5B524B");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.KhuyenMais)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK__KhuyenMai__MaSP__7A672E12");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__2725081CC9A4263C");

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.AnhChinh).HasMaxLength(255);
            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoLuongTon).HasDefaultValue(0);
            entity.Property(e => e.TenSp)
                .HasMaxLength(200)
                .HasColumnName("TenSP");
            entity.Property(e => e.TrangThai).HasDefaultValue(true);

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDanhMuc)
                .HasConstraintName("FK__SanPham__MaDanhM__5165187F");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTk).HasName("PK__TaiKhoan__2725007075B66409");

            entity.ToTable("TaiKhoan");

            entity.HasIndex(e => e.Email, "UQ__TaiKhoan__A9D10534B3D270B3").IsUnique();

            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MatKhau).HasMaxLength(100);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.VaiTro).HasMaxLength(50);
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaTt).HasName("PK__ThanhToa__272500794680C454");

            entity.ToTable("ThanhToan");

            entity.Property(e => e.MaTt).HasColumnName("MaTT");
            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.NgayThanhToan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PhuongThuc).HasMaxLength(50);
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.MaDh)
                .HasConstraintName("FK__ThanhToan__MaDH__68487DD7");
        });

        modelBuilder.Entity<ThongTinShop>(entity =>
        {
            entity.HasKey(e => e.MaTt).HasName("PK__ThongTin__27250079FD750A46");

            entity.ToTable("ThongTinShop");

            entity.Property(e => e.MaTt).HasColumnName("MaTT");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Logo).HasMaxLength(255);
            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.SoDienThoai).HasMaxLength(50);
            entity.Property(e => e.TenShop).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
