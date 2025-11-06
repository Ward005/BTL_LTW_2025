using System;
using System.Collections.Generic;

namespace BTL_LTW.Data;

public partial class BaiViet
{
    public int MaBv { get; set; }

    public string? TieuDe { get; set; }

    public string? NoiDung { get; set; }

    public string? AnhBia { get; set; }

    public DateTime? NgayDang { get; set; }

    public int? MaTk { get; set; }

    public virtual ICollection<BinhLuan> BinhLuans { get; set; } = new List<BinhLuan>();

    public virtual TaiKhoan? MaTkNavigation { get; set; }
}
