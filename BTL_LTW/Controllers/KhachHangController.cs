using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace BTL_LTW.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly MaleFashionContext db;
        public KhachHangController(MaleFashionContext context)
        {
            db = context;
        }
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
                //kiem tra email
                var email1 = db.TaiKhoans.FirstOrDefault(x => x.Email == model.Email);
                if (email1 != null)
                {
                    ViewBag.Message = "Tài khoản đã tồn tại";
                    return View(model);
                }

                // nếu chưa có tài khaorn thì thêm vào database
                var user = new TaiKhoan
                {
                    HoTen = model.HoTen,
                    Email = model.Email,
                    MatKhau = model.MatKhau,
                    SoDienThoai = model.SoDienThoai,
                    DiaChi = model.DiaChi,
                    VaiTro = "KhachHang"
                };
                db.TaiKhoans.Add(user);
                db.SaveChanges();
                //xoa du lieu form va thong bao thanh cong
                ModelState.Clear();
                //var KhachHang = model;
                ViewBag.Message = "Đăng ký thành công";
                return View(new RegisterVM());



            }
            return View(model);
        }

        //

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
                //kiem tra email
                var user1 = db.TaiKhoans.FirstOrDefault(x => x.Email == model.Email && x.MatKhau == model.MatKhau);
                if (user1 != null)
                {
                    // ViewBag.Message = "Tài khoản đã tồn tại";
                    //return View(model);

                    HttpContext.Session.SetString("UserEmail", user1.Email);
                    HttpContext.Session.SetString("UserName", user1.HoTen ?? "");
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Message = "Email hoặc mật khẩu không đúng định dạng";
                // nếu chưa có tài khaorn thì thêm vào database


            }
            return View(model);
        }
    }
}
