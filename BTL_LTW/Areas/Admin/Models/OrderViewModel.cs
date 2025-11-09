namespace BTL_LTW.Areas.Admin.Models
{
    public class OrderViewModel
    {
        public int? MaDH { get; set; }
        public int? MaTK { get; set; }
        public string? TenKhachHang { get; set; }
        public DateTime? NgayDat { get; set; }
        public decimal? TongTien { get; set; }
        public string? TrangThai { get; set; }
        public string? DiaChiGiao { get; set; }

        public List<OrderItemVM>? SanPhams { get; set; }
    }

    public class OrderItemVM
    {
        public int MaSP { get; set; }
        public string? TenSP { get; set; }
        public int? SoLuong { get; set; }
        public decimal? Gia { get; set; }
    }
}
