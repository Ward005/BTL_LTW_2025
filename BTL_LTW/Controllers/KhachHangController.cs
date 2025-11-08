using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BTL_LTW.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly MaleFashionContext _db;

        public KhachHangController(MaleFashionContext context)
        {
            _db = context;
        }

        // ========== ĐĂNG KÝ ==========
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                // 🔹 Kiểm tra email đã tồn tại chưa
                var existing = _db.TaiKhoans.FirstOrDefault(x => x.Email == model.Email);
                if (existing != null)
                {
                    ViewBag.Message = "Tài khoản đã tồn tại!";
                    return View(model);
                }

                // 🔹 Nếu chưa có thì thêm mới
                var newUser = new TaiKhoan
                {
                    HoTen = model.HoTen,
                    Email = model.Email,
                    MatKhau = model.MatKhau,
                    SoDienThoai = model.SoDienThoai,
                    DiaChi = model.DiaChi,
                    VaiTro = "KhachHang",
                    NgayTao = DateTime.Now
                };

                _db.TaiKhoans.Add(newUser);
                _db.SaveChanges();

                // 🔹 Sau khi đăng ký thành công -> chuyển sang đăng nhập
                TempData["Message"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("DangNhap");
            }

            return View(model);
        }

        // ========== ĐĂNG NHẬP ==========
        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangNhap(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                // 🔹 Tìm user hợp lệ
                var user = _db.TaiKhoans.FirstOrDefault(x => x.Email == model.Email && x.MatKhau == model.MatKhau);
                if (user != null)
                {
                    // 🔹 Lưu toàn bộ thông tin vào Session (JSON)
                    var userSession = new UserSessionVM
                    {
                        MaTK = user.MaTk,
                        HoTen = user.HoTen,
                        Email = user.Email,
                        VaiTro = user.VaiTro
                    };

                    HttpContext.Session.SetString("UserLogin", JsonSerializer.Serialize(userSession));

                    TempData["Message"] = $"Chào mừng {user.HoTen} quay lại!";
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Message = "Email hoặc mật khẩu không đúng!";
            }

            return View(model);
        }

        // ========== ĐĂNG XUẤT ==========
        public IActionResult DangXuat()
        {
            HttpContext.Session.Remove("UserLogin");
            return RedirectToAction("DangNhap");
        }
    }
}
