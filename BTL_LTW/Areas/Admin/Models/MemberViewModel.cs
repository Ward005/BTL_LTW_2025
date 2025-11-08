using System.ComponentModel.DataAnnotations;

namespace BTL_LTW.Areas.Admin.Models
{
    public class MemberViewModel
    {
        public int? MaTK { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string MatKhau { get; set; }

        public string? SoDienThoai { get; set; }

        public string? DiaChi { get; set; }

        public string? VaiTro { get; set; }

        public DateTime? NgayTao { get; set; }
    }
}
