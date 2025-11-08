using System.Linq;

namespace BTL_LTW.ViewModels
{
    public class CartItemVM
    {
        public int MaSP { get; set; }                  // Mã sản phẩm
        public string TenSP { get; set; } = string.Empty; // Tên sản phẩm
        public string AnhChinh { get; set; } = string.Empty; // Ảnh sản phẩm
        public decimal Gia { get; set; }               // Giá
        public int SoLuong { get; set; }               // Số lượng
        public decimal ThanhTien => Gia * SoLuong;     // Thành tiền
    }

    public class ShoppingCartVM
    {
        public List<CartItemVM> Items { get; set; } = new();
        public decimal TongTien => Items.Sum(i => i.ThanhTien);
    }
}
