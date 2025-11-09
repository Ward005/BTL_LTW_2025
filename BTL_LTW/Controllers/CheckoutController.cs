using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BTL_LTW.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly MaleFashionContext _db;
        public CheckoutController(MaleFashionContext db) => _db = db;

        // ===== HIỂN THỊ TRANG CHECKOUT =====
        [HttpGet]
        public IActionResult Index()
        {
            var session = HttpContext.Session.GetString("UserLogin");
            if (session == null)
                return RedirectToAction("DangNhap", "KhachHang");

            var user = JsonSerializer.Deserialize<UserSessionVM>(session)!;
            var gioHang = _db.GioHangs
                .Include(g => g.ChiTietGioHangs)
                .ThenInclude(c => c.MaSpNavigation)
                .FirstOrDefault(g => g.MaTk == user.MaTK);

            if (gioHang == null || !gioHang.ChiTietGioHangs.Any())
                return RedirectToAction("Index", "ShoppingCart");

            var items = gioHang.ChiTietGioHangs.Select(c => new CartItemVM
            {
                MaSP = c.MaSp,
                TenSP = c.MaSpNavigation.TenSp ?? "",
                Gia = c.MaSpNavigation.Gia ?? 0,
                SoLuong = c.SoLuong ?? 0,
                AnhChinh = c.MaSpNavigation.AnhChinh ?? "/img/no-image.png"
            }).ToList();

            var vm = new CheckoutVM
            {
                Items = items,
                TongTien = items.Sum(i => i.ThanhTien)
            };

            return View(vm);
        }

        // ===== XỬ LÝ ĐẶT HÀNG =====
        [HttpPost]
        public IActionResult Index(CheckoutVM model)
        {
            var session = HttpContext.Session.GetString("UserLogin");
            if (session == null)
                return RedirectToAction("DangNhap", "KhachHang");

            var user = JsonSerializer.Deserialize<UserSessionVM>(session)!;

            var gioHang = _db.GioHangs
                .Include(g => g.ChiTietGioHangs)
                .ThenInclude(c => c.MaSpNavigation)
                .FirstOrDefault(g => g.MaTk == user.MaTK);

            if (gioHang == null || !gioHang.ChiTietGioHangs.Any())
                return RedirectToAction("Index", "ShoppingCart");

            // ===== TẠO ĐƠN HÀNG =====
            var donHang = new DonHang
            {
                MaTk = user.MaTK,
                NgayDat = DateTime.Now,
                DiaChiGiao = model.DiaChi,
                TrangThai = "Chờ xác nhận",
                TongTien = gioHang.ChiTietGioHangs.Sum(c => (c.MaSpNavigation.Gia ?? 0) * (c.SoLuong ?? 1))
            };
            _db.DonHangs.Add(donHang);
            _db.SaveChanges();

            // ===== TẠO CHI TIẾT ĐƠN HÀNG =====
            foreach (var item in gioHang.ChiTietGioHangs)
            {
                _db.ChiTietDonHangs.Add(new ChiTietDonHang
                {
                    MaDh = donHang.MaDh,
                    MaSp = item.MaSp,
                    SoLuong = item.SoLuong,
                    Gia = item.MaSpNavigation.Gia
                });
            }

            // ✅ ===== TẠO THANH TOÁN =====
            var thanhToan = new ThanhToan
            {
                MaDh = donHang.MaDh,
                PhuongThuc = model.PaymentMethod ?? "COD",
                TrangThai = model.PaymentMethod == "COD" ? "Chờ thanh toán" : "Đã thanh toán",
                NgayThanhToan = model.PaymentMethod == "COD" ? null : DateTime.Now
            };
            _db.ThanhToans.Add(thanhToan);

            // ===== XOÁ GIỎ HÀNG SAU KHI ĐẶT =====
            _db.ChiTietGioHangs.RemoveRange(gioHang.ChiTietGioHangs);
            _db.SaveChanges();

            return View("ThanhCong", donHang);

        }

        // ===== TRANG XÁC NHẬN =====
        public IActionResult ThanhCong()
        {
            return View();
        }
    }
}
