using System;
using System.Collections.Generic;

namespace BTL_LTW.Data;

public partial class AnhSanPham
{
    public int MaAnh { get; set; }

    public int? MaSp { get; set; }

    public string? DuongDan { get; set; }

    public virtual SanPhams? MaSpNavigation { get; set; }
}
