namespace BTL_LTW.ViewModels
{
    public class UserSessionVM
    {
        public int MaTK { get; set; }          // ID người dùng (khoá chính bảng TaiKhoan)
        public string? HoTen { get; set; }     // Họ tên người dùng
        public string? Email { get; set; }     // Email đăng nhập
        public string? VaiTro { get; set; }    // Vai trò (Admin, KhachHang, ...)
    }
}
