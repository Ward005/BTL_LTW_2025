using System;
using System.Collections.Generic;

namespace BTL_LTW.Data;

public partial class DealOfWeek
{
    public int MaDeal { get; set; }

    public int? MaSp { get; set; }

    public string? TieuDe { get; set; }

    public decimal? TyLeGiam { get; set; }

    public DateTime? NgayKetThuc { get; set; }

    public virtual SanPham? MaSpNavigation { get; set; }
}
