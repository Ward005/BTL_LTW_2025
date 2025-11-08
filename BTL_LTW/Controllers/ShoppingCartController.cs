using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BTL_LTW.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly MaleFashionContext _db;
        public ShoppingCartController(MaleFashionContext context) => _db = context;

        // ====== THÊM SẢN PHẨM VÀO GIỎ ======
        public IActionResult AddToCart(int id)
        {
            var session = HttpContext.Session.GetString("UserLogin");
            if (session == null)
                return RedirectToAction("DangNhap", "KhachHang");

            var user = JsonSerializer.Deserialize<UserSessionVM>(session)!;

            var gioHang = _db.GioHangs.FirstOrDefault(g => g.MaTk == user.MaTK);
            if (gioHang == null)
            {
                gioHang = new GioHang { MaTk = user.MaTK, NgayCapNhat = DateTime.Now };
                _db.GioHangs.Add(gioHang);
                _db.SaveChanges();
            }

            var sp = _db.SanPhams.FirstOrDefault(x => x.MaSp == id && x.TrangThai == true);
            if (sp == null) return NotFound();

            var ct = _db.ChiTietGioHangs
                .FirstOrDefault(c => c.MaGioHang == gioHang.MaGioHang && c.MaSp == id);

            if (ct == null)
                _db.ChiTietGioHangs.Add(new ChiTietGioHang
                {
                    MaGioHang = gioHang.MaGioHang,
                    MaSp = id,
                    SoLuong = 1
                });
            else
            {
                ct.SoLuong = (ct.SoLuong ?? 0) + 1;
                _db.ChiTietGioHangs.Update(ct);
            }

            gioHang.NgayCapNhat = DateTime.Now;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // ====== HIỂN THỊ GIỎ HÀNG ======
        public IActionResult Index()
        {
            var session = HttpContext.Session.GetString("UserLogin");
            if (session == null)
                return RedirectToAction("DangNhap", "KhachHang");

            var user = JsonSerializer.Deserialize<UserSessionVM>(session)!;

            var gioHang = _db.GioHangs.FirstOrDefault(g => g.MaTk == user.MaTK);
            if (gioHang == null)
                return View(new ShoppingCartVM());

            var items = _db.ChiTietGioHangs
                .Include(c => c.MaSpNavigation)
                .Where(c => c.MaGioHang == gioHang.MaGioHang)
                .Select(c => new CartItemVM
                {
                    MaSP = c.MaSp,
                    TenSP = c.MaSpNavigation.TenSp ?? "Không rõ",
                    AnhChinh = c.MaSpNavigation.AnhChinh ?? "/img/no-image.png",
                    Gia = c.MaSpNavigation.Gia ?? 0,
                    SoLuong = c.SoLuong ?? 1
                })
                .ToList();

            var vm = new ShoppingCartVM { Items = items };
            return View(vm);
        }

        // ====== XOÁ SẢN PHẨM ======
        public IActionResult Remove(int id)
        {
            var session = HttpContext.Session.GetString("UserLogin");
            if (session == null)
                return RedirectToAction("DangNhap", "KhachHang");

            var user = JsonSerializer.Deserialize<UserSessionVM>(session)!;
            var gioHang = _db.GioHangs.FirstOrDefault(g => g.MaTk == user.MaTK);
            if (gioHang == null) return RedirectToAction("Index");

            var item = _db.ChiTietGioHangs.FirstOrDefault(c => c.MaGioHang == gioHang.MaGioHang && c.MaSp == id);
            if (item != null)
            {
                _db.ChiTietGioHangs.Remove(item);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // ====== CẬP NHẬT SỐ LƯỢNG (AJAX) ======
        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var session = HttpContext.Session.GetString("UserLogin");
            if (session == null)
                return Json(new { success = false });

            var user = JsonSerializer.Deserialize<UserSessionVM>(session)!;
            var gioHang = _db.GioHangs.FirstOrDefault(g => g.MaTk == user.MaTK);
            if (gioHang == null)
                return Json(new { success = false });

            var item = _db.ChiTietGioHangs.FirstOrDefault(c => c.MaGioHang == gioHang.MaGioHang && c.MaSp == id);
            if (item == null)
                return Json(new { success = false });

            item.SoLuong = quantity;
            if (item.SoLuong <= 0)
                _db.ChiTietGioHangs.Remove(item);

            _db.SaveChanges();

            var total = _db.ChiTietGioHangs
                .Include(c => c.MaSpNavigation)
                .Where(c => c.MaGioHang == gioHang.MaGioHang)
                .Sum(c => (c.MaSpNavigation.Gia ?? 0) * (c.SoLuong ?? 1));

            return Json(new { success = true, total });
        }

        // ====== LẤY TỔNG TIỀN CHO HEADER ======
        [HttpGet]
        public IActionResult GetCartTotal()
        {
            var session = HttpContext.Session.GetString("UserLogin");
            if (session == null) return Json(new { total = 0, count = 0 });

            var user = JsonSerializer.Deserialize<UserSessionVM>(session)!;
            var gioHang = _db.GioHangs.FirstOrDefault(g => g.MaTk == user.MaTK);
            if (gioHang == null) return Json(new { total = 0, count = 0 });

            var items = _db.ChiTietGioHangs
                .Include(c => c.MaSpNavigation)
                .Where(c => c.MaGioHang == gioHang.MaGioHang)
                .ToList();

            var total = items.Sum(c => (c.MaSpNavigation.Gia ?? 0) * (c.SoLuong ?? 1));
            var count = items.Sum(c => c.SoLuong ?? 0);

            return Json(new { total, count });
        }

    }
}
