using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL_LTW.ViewModels
{
    public class CheckoutVM
    {
        [Required]
        public string HoTen { get; set; } = "";

        [Required]
        public string DiaChi { get; set; } = "";

        [Required, Phone]
        public string SoDienThoai { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        public string? GhiChu { get; set; }

        public decimal TongTien { get; set; }

        public List<CartItemVM> Items { get; set; } = new();

        // ✅ Thêm thuộc tính phương thức thanh toán
        [Required]
        public string PaymentMethod { get; set; } = "COD";
    }
}
