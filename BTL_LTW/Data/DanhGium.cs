using System;
using System.Collections.Generic;

namespace BTL_LTW.Data;

public partial class DanhGium
{
    public int MaDg { get; set; }

    public int? MaSp { get; set; }

    public int? MaTk { get; set; }

    public int? Diem { get; set; }

    public string? NoiDung { get; set; }

    public DateTime? NgayDanhGia { get; set; }

    public virtual SanPhams? MaSpNavigation { get; set; }

    public virtual TaiKhoan? MaTkNavigation { get; set; }
}
