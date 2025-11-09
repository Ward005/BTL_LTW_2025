using System;
using System.Collections.Generic;

namespace BTL_LTW.Data;

public partial class TaiKhoan
{
    public int MaTk { get; set; }

    public string? HoTen { get; set; }

    public string? Email { get; set; }

    public string? MatKhau { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public DateTime? NgayTao { get; set; }

    public string? VaiTro { get; set; }

    public virtual ICollection<BaiViet> BaiViets { get; set; } = new List<BaiViet>();

    public virtual ICollection<BinhLuan> BinhLuans { get; set; } = new List<BinhLuan>();

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();
}
