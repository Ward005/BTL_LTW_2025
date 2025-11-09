using BTL_LTW.Areas.Admin.Models;
using BTL_LTW.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTW.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginAdminController : Controller
    {
        private readonly MaleFashionContext _context;

        public static bool IsLoggedIn = false;

        public LoginAdminController(MaleFashionContext context)
        {
            _context = context;
        }

        // GET: /Admin/LoginAdmin/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Admin/LoginAdmin/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginAdminModel model)
        {
            if (string.IsNullOrWhiteSpace(model.LoginName) || string.IsNullOrWhiteSpace(model.Password))
            {
                ViewBag.Message = "Vui lòng nhập đầy đủ thông tin!";
                return View(model);
            }

            // Kiểm tra tài khoản admin trong database
            var admin = await _context.TaiKhoans
                .FirstOrDefaultAsync(t =>
                    (t.Email == model.LoginName || t.HoTen == model.LoginName) &&
                    t.MatKhau == model.Password &&
                    t.VaiTro == "Admin");

            if (admin == null)
            {
                ViewBag.Message = "Sai tài khoản hoặc mật khẩu!";
                return View(model);
            }
            IsLoggedIn = true;


            // ✅ Lưu session thông tin admin
            HttpContext.Session.SetString("AdminName", admin.HoTen ?? "");
            HttpContext.Session.SetString("AdminEmail", admin.Email ?? "");

            IsLoggedIn = true;


            // ✅ Nếu đúng → chuyển sang Home (Admin)
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear(); // Xóa toàn bộ session đăng nhập
            return RedirectToAction("Login", "LoginAdmin"); // Quay về trang đăng nhập
        }

    }
}
