using System.ComponentModel.DataAnnotations;

namespace BTL_LTW.ViewModels
{
    public class LoginVM
    {

        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }
    }
}
