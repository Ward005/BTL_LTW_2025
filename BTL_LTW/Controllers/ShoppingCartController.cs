using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Text.Json;

namespace BTL_LTW.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly MaleFashionContext _db;
        private const int PageSize = 5;
        public ShoppingCartController(MaleFashionContext context) => _db = context;

        // ====== THÊM SẢN PHẨM VÀO GIỎ ======
        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var session = HttpContext.Session.GetString("UserLogin");
            if (session == null)
                return Json(new { ok = false, needLogin = true });

            var user = System.Text.Json.JsonSerializer.Deserialize<UserSessionVM>(session)!;

            // Lấy/tạo giỏ
            var gioHang = _db.GioHangs.FirstOrDefault(g => g.MaTk == user.MaTK);
            if (gioHang == null)
            {
                gioHang = new GioHang { MaTk = user.MaTK, NgayCapNhat = DateTime.Now };
                _db.GioHangs.Add(gioHang);
                _db.SaveChanges();
            }

            // Kiểm tra sản phẩm
            var sp = _db.SanPhams.FirstOrDefault(x => x.MaSp == id && x.TrangThai == true);
            if (sp == null) return Json(new { ok = false, message = "Sản phẩm không tồn tại" });

            // Thêm / tăng SL
            var ct = _db.ChiTietGioHangs.FirstOrDefault(c => c.MaGioHang == gioHang.MaGioHang && c.MaSp == id);
            if (ct == null)
                _db.ChiTietGioHangs.Add(new ChiTietGioHang { MaGioHang = gioHang.MaGioHang, MaSp = id, SoLuong = 1 });
            else
                ct.SoLuong = (ct.SoLuong ?? 0) + 1;

            gioHang.NgayCapNhat = DateTime.Now;
            _db.SaveChanges();

            // Tính lại tổng & số lượng để cập nhật header
            var items = _db.ChiTietGioHangs
                .Include(c => c.MaSpNavigation)
                .Where(c => c.MaGioHang == gioHang.MaGioHang)
                .ToList();

            var total = items.Sum(c => (c.MaSpNavigation.Gia ?? 0) * (c.SoLuong ?? 1));
            var count = items.Sum(c => c.SoLuong ?? 0);

            return Json(new { ok = true, message = "Đã thêm vào giỏ hàng!", total, count });
        }


        // ====== HIỂN THỊ GIỎ HÀNG ======
        public IActionResult Index(int page = 1)
        {
            var session = HttpContext.Session.GetString("UserLogin");
            if (session == null)
                return RedirectToAction("DangNhap", "KhachHang");

            var user = JsonSerializer.Deserialize<UserSessionVM>(session)!;

            var gioHang = _db.GioHangs.FirstOrDefault(g => g.MaTk == user.MaTK);
            if (gioHang == null)
                return View(new PagedCartVM { Items = new List<CartItemVM>(), CurrentPage = 1, TotalPages = 1 });

            var query = _db.ChiTietGioHangs
                .Include(c => c.MaSpNavigation)
                .Where(c => c.MaGioHang == gioHang.MaGioHang)
                .Select(c => new CartItemVM
                {
                    MaSP = c.MaSp,
                    TenSP = c.MaSpNavigation.TenSp ?? "Không rõ",
                    AnhChinh = c.MaSpNavigation.AnhChinh ?? "/img/no-image.png",
                    Gia = c.MaSpNavigation.Gia ?? 0,
                    SoLuong = c.SoLuong ?? 1
                });

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);

            var items = query
                .OrderBy(x => x.MaSP)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var vm = new PagedCartVM
            {
                Items = items,
                CurrentPage = page,
                TotalPages = totalPages
            };

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
            if (session == null) return RedirectToAction("DangNhap", "KhachHang");

            var user = JsonSerializer.Deserialize<UserSessionVM>(session)!;
            var gioHang = _db.GioHangs.FirstOrDefault(g => g.MaTk == user.MaTK);
            if (gioHang == null) return NotFound();

            var ct = _db.ChiTietGioHangs.FirstOrDefault(c => c.MaGioHang == gioHang.MaGioHang && c.MaSp == id);
            if (ct != null)
            {
                ct.SoLuong = quantity;
                _db.Update(ct);
                _db.SaveChanges();
            }
            return Ok();
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

        [HttpPost]
        public IActionResult AddToCartWithQty(int id, int quantity)
        {
            var session = HttpContext.Session.GetString("UserLogin");
            if (session == null)
                return Json(new { ok = false, needLogin = true });

            var user = System.Text.Json.JsonSerializer.Deserialize<UserSessionVM>(session)!;

            // Lấy/tạo giỏ hàng
            var gioHang = _db.GioHangs.FirstOrDefault(g => g.MaTk == user.MaTK);
            if (gioHang == null)
            {
                gioHang = new GioHang { MaTk = user.MaTK, NgayCapNhat = DateTime.Now };
                _db.GioHangs.Add(gioHang);
                _db.SaveChanges();
            }

            // Kiểm tra sản phẩm
            var sp = _db.SanPhams.FirstOrDefault(x => x.MaSp == id && x.TrangThai == true);
            if (sp == null)
                return Json(new { ok = false, message = "Sản phẩm không tồn tại" });

            // Không cho vượt quá số lượng tồn
            if (sp.SoLuongTon < quantity)
                return Json(new { ok = false, message = "Không đủ hàng trong kho" });

            // Thêm / cập nhật số lượng
            var ct = _db.ChiTietGioHangs.FirstOrDefault(c => c.MaGioHang == gioHang.MaGioHang && c.MaSp == id);
            if (ct == null)
                _db.ChiTietGioHangs.Add(new ChiTietGioHang { MaGioHang = gioHang.MaGioHang, MaSp = id, SoLuong = quantity });
            else
                ct.SoLuong = (ct.SoLuong ?? 0) + quantity;

            gioHang.NgayCapNhat = DateTime.Now;
            _db.SaveChanges();

            // Tính tổng
            var items = _db.ChiTietGioHangs
                .Include(c => c.MaSpNavigation)
                .Where(c => c.MaGioHang == gioHang.MaGioHang)
                .ToList();

            var total = items.Sum(c => (c.MaSpNavigation.Gia ?? 0) * (c.SoLuong ?? 1));
            var count = items.Sum(c => c.SoLuong ?? 0);

            return Json(new { ok = true, message = "Đã thêm sản phẩm vào giỏ hàng!", total, count });
        }


    }
}
