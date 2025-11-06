using System;
using System.Collections.Generic;

namespace BTL_LTW.Data;

public partial class BinhLuan
{
    public int MaBl { get; set; }

    public int? MaBv { get; set; }

    public int? MaTk { get; set; }

    public string? NoiDung { get; set; }

    public DateTime? NgayBinhLuan { get; set; }

    public virtual BaiViet? MaBvNavigation { get; set; }

    public virtual TaiKhoan? MaTkNavigation { get; set; }
}
