using System;
using System.Collections.Generic;

namespace BTL_LTW.Models;

public class SanPham
{
    public int? MaSp { get; set; }

    public string? TenSp { get; set; }

    public decimal? Gia { get; set; }

    public string? MoTa { get; set; }

    public string? AnhChinh { get; set; }

    public int? MaDanhMuc { get; set; }

    public int? SoLuongTon { get; set; }

    public bool? TrangThai { get; set; }
}
