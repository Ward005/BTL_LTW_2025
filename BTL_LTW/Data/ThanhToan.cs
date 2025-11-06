using System;
using System.Collections.Generic;

namespace BTL_LTW.Data;

public partial class ThanhToan
{
    public int MaTt { get; set; }

    public int? MaDh { get; set; }

    public string? PhuongThuc { get; set; }

    public string? TrangThai { get; set; }

    public DateTime? NgayThanhToan { get; set; }

    public virtual DonHang? MaDhNavigation { get; set; }
}
