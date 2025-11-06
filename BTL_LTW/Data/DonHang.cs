using System;
using System.Collections.Generic;

namespace BTL_LTW.Data;

public partial class DonHang
{
    public int MaDh { get; set; }

    public int? MaTk { get; set; }

    public DateTime? NgayDat { get; set; }

    public decimal? TongTien { get; set; }

    public string? TrangThai { get; set; }

    public string? DiaChiGiao { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual TaiKhoan? MaTkNavigation { get; set; }

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
