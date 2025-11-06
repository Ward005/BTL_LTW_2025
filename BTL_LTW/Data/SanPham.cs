using System;
using System.Collections.Generic;

namespace BTL_LTW.Data;

public partial class SanPham
{
    public int MaSp { get; set; }

    public string? TenSp { get; set; }

    public decimal? Gia { get; set; }

    public string? MoTa { get; set; }

    public string? AnhChinh { get; set; }

    public int? MaDanhMuc { get; set; }

    public int? SoLuongTon { get; set; }

    public bool? TrangThai { get; set; }

    public virtual ICollection<AnhSanPham> AnhSanPhams { get; set; } = new List<AnhSanPham>();

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual ICollection<DealOfWeek> DealOfWeeks { get; set; } = new List<DealOfWeek>();

    public virtual ICollection<KhuyenMai> KhuyenMais { get; set; } = new List<KhuyenMai>();

    public virtual DanhMuc? MaDanhMucNavigation { get; set; }
}
