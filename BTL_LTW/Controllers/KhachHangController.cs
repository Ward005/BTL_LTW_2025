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

        // ========== HIỂN THỊ TRANG AUTH ==========
        [HttpGet]
        public IActionResult DangNhap()
        {
            ViewBag.Mode = "login";
            return View("Auth");
        }

        [HttpGet]
        public IActionResult DangKy()
        {
            ViewBag.Mode = "register";
            return View("Auth");
        }

        // ========== XỬ LÝ ĐĂNG NHẬP ==========
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangNhap(LoginVM model)
        {
            ViewBag.Mode = "login";

            if (!ModelState.IsValid)
                return View("Auth");

            var user = _db.TaiKhoans
                .FirstOrDefault(x => x.Email == model.Email && x.MatKhau == model.MatKhau);

            if (user == null)
            {
                ViewBag.Message = "Email hoặc mật khẩu không đúng!";
                return View("Auth");
            }

            // ✅ Lưu thông tin đăng nhập vào session
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

        // ========== XỬ LÝ ĐĂNG KÝ ==========
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangKy(RegisterVM model)
        {
            ViewBag.Mode = "register";

            // Debug: Hiển thị lỗi validation (nếu có)
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                ViewBag.ValidationErrors = errors;
                return View("Auth");
            }

            var existing = _db.TaiKhoans.FirstOrDefault(x => x.Email == model.Email);
            if (existing != null)
            {
                ViewBag.Message = "Tài khoản đã tồn tại!";
                return View("Auth");
            }

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

            TempData["Message"] = "Đăng ký thành công! Vui lòng đăng nhập.";
            return RedirectToAction("DangNhap");
        }


        // ========== ĐĂNG XUẤT ==========
        public IActionResult DangXuat()
        {
            HttpContext.Session.Remove("UserLogin");
            return RedirectToAction("DangNhap");
        }
    }
}
