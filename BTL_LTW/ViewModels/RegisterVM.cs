using System.ComponentModel.DataAnnotations;

namespace BTL_LTW.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "*")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        public string MatKhau { get; set; }

        public string? SoDienThoai { get; set; }
        public string? DiaChi { get; set; }

    }
}
