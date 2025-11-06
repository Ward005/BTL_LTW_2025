using System;
using System.Collections.Generic;

namespace BTL_LTW.Data;

public partial class GioHang
{
    public int MaGioHang { get; set; }

    public int? MaTk { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual TaiKhoan? MaTkNavigation { get; set; }
}
