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
        => optionsBuilder.UseSqlServer("Data Source=ADMIN\\SQLEXPRESS;Initial Catalog=MaleFashion;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnhSanPham>(entity =>
        {
            entity.HasKey(e => e.MaAnh).HasName("PK__AnhSanPh__356240DF35900673");

            entity.ToTable("AnhSanPham");

            entity.Property(e => e.DuongDan).HasMaxLength(255);
            entity.Property(e => e.MaSp).HasColumnName("MaSP");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.AnhSanPhams)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK__AnhSanPham__MaSP__4222D4EF");
        });

        modelBuilder.Entity<BaiViet>(entity =>
        {
            entity.HasKey(e => e.MaBv).HasName("PK__BaiViet__2724759587D7BE83");

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
                .HasConstraintName("FK__BaiViet__MaTK__5FB337D6");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.MaBanner).HasName("PK__Banner__508B4A4929428755");

            entity.ToTable("Banner");

            entity.Property(e => e.Anh).HasMaxLength(255);
            entity.Property(e => e.LienKet).HasMaxLength(255);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TieuDe).HasMaxLength(200);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<BinhLuan>(entity =>
        {
            entity.HasKey(e => e.MaBl).HasName("PK__BinhLuan__272475AF88038876");

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
                .HasConstraintName("FK__BinhLuan__MaBV__6383C8BA");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.BinhLuans)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK__BinhLuan__MaTK__6477ECF3");
        });

        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => new { e.MaDh, e.MaSp }).HasName("PK__ChiTietD__F557D6E0CEDA5D68");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDon__MaDH__5165187F");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDon__MaSP__52593CB8");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => new { e.MaGioHang, e.MaSp }).HasName("PK__ChiTietG__27724D22FCFFBD43");

            entity.ToTable("ChiTietGioHang");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.SoLuong).HasDefaultValue(1);

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaGioHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietGi__MaGio__49C3F6B7");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietGio__MaSP__4AB81AF0");
        });

        modelBuilder.Entity<DanhGium>(entity =>
        {
            entity.HasKey(e => e.MaDg).HasName("PK__DanhGia__272586604BF98D7C");

            entity.Property(e => e.MaDg).HasColumnName("MaDG");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.NgayDanhGia)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NoiDung).HasMaxLength(500);

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK__DanhGia__MaSP__5AEE82B9");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK__DanhGia__MaTK__5BE2A6F2");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMuc__B3750887642610DE");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenDanhMuc).HasMaxLength(100);
        });

        modelBuilder.Entity<DealOfWeek>(entity =>
        {
            entity.HasKey(e => e.MaDeal).HasName("PK__DealOfWe__324A50A5FFE21560");

            entity.ToTable("DealOfWeek");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.TieuDe).HasMaxLength(200);
            entity.Property(e => e.TyLeGiam).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.DealOfWeeks)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK__DealOfWeek__MaSP__05D8E0BE");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__DonHang__27258661479DDD56");

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
                .HasConstraintName("FK__DonHang__MaTK__4E88ABD4");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang).HasName("PK__GioHang__F5001DA325183B0D");

            entity.ToTable("GioHang");

            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK__GioHang__MaTK__45F365D3");
        });

        modelBuilder.Entity<InstagramImage>(entity =>
        {
            entity.HasKey(e => e.MaAnh).HasName("PK__Instagra__356240DFD7FB4284");

            entity.ToTable("InstagramImage");

            entity.Property(e => e.DuongDan).HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKm).HasName("PK__KhuyenMa__2725CF1512AD2E4C");

            entity.ToTable("KhuyenMai");

            entity.HasIndex(e => e.MaCode, "UQ__KhuyenMa__152C7C5CA1CEA4BB").IsUnique();

            entity.Property(e => e.MaKm).HasColumnName("MaKM");
            entity.Property(e => e.MaCode).HasMaxLength(50);
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.TyLeGiam).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.KhuyenMais)
                .HasForeignKey(d => d.MaDanhMuc)
                .HasConstraintName("FK__KhuyenMai__MaDan__693CA210");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.KhuyenMais)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK__KhuyenMai__MaSP__68487DD7");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__2725081C650FAA85");

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
                .HasConstraintName("FK__SanPham__MaDanhM__3F466844");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTk).HasName("PK__TaiKhoan__27250070E5E1240D");

            entity.ToTable("TaiKhoan");

            entity.HasIndex(e => e.Email, "UQ__TaiKhoan__A9D10534DE54E903").IsUnique();

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
            entity.HasKey(e => e.MaTt).HasName("PK__ThanhToa__272500790E5201A6");

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
                .HasConstraintName("FK__ThanhToan__MaDH__5629CD9C");
        });

        modelBuilder.Entity<ThongTinShop>(entity =>
        {
            entity.HasKey(e => e.MaTt).HasName("PK__ThongTin__272500794025F2A0");

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
