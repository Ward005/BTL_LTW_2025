using System;
using System.Collections.Generic;

namespace BTL_LTW.Data;

public partial class KhuyenMai
{
    public int MaKm { get; set; }

    public int? MaSp { get; set; }

    public int? MaDanhMuc { get; set; }

    public string? MaCode { get; set; }

    public decimal? TyLeGiam { get; set; }

    public DateTime? NgayBatDau { get; set; }

    public DateTime? NgayKetThuc { get; set; }

    public virtual DanhMuc? MaDanhMucNavigation { get; set; }

    public virtual SanPham? MaSpNavigation { get; set; }
}
