using System.ComponentModel.DataAnnotations;

namespace BTL_LTW.ViewModels
{
    public class RegisterVM
    {

        [Key]
        [Display(Name = "Tên  đăng nhập")]
        [Required(ErrorMessage = "*")]
        public int MaTk { get; set; }
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string HoTen { get; set; }
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng")]
        [RegularExpression(
        @"^[a-zA-Z0-9._%+-]+@gmail\.com$",
        ErrorMessage = "Email phải có định dạng hợp lệ và kết thúc bằng @gmail.com")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Mật khẩu")]
        [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*._-]).{8,}$",
        ErrorMessage = "Mật khẩu phải có ít nhất 1 chữ hoa, 1 chữ thường, 1 số, 1 ký tự đặc biệt và tối thiểu 8 ký tự")]
        public string MatKhau { get; set; }
        [MaxLength(24, ErrorMessage = "Tối đa 24 ký tự")]
        [RegularExpression(@"0[123456789]\d{8}", ErrorMessage = "Chưa đúng định dạng của di động việt nam")]
        public string SoDienThoai { get; set; }
        [MaxLength(60, ErrorMessage = "Tối đa 60 ký tự")]
        public string DiaChi { get; set; }

    }
}
