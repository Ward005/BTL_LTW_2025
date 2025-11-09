using BTL_LTW.Areas.Admin.Models;
using BTL_LTW.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTW.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : BaseAdminController
    {
        private readonly MaleFashionContext _db;

        public OrderController(MaleFashionContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Đơn đặt hàng";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetList(jDataTable model)
        {
            var items = _db.DonHangs
                .Include(o => o.MaTkNavigation)
                .Include(o => o.ChiTietDonHangs)
                    .ThenInclude(ct => ct.MaSpNavigation)
                .Select(o => new OrderViewModel
                {
                    MaDH = o.MaDh,
                    TenKhachHang = o.MaTkNavigation.HoTen,
                    NgayDat = o.NgayDat,
                    TongTien = o.TongTien,
                    TrangThai = o.TrangThai,
                    DiaChiGiao = o.DiaChiGiao,
                    SanPhams = o.ChiTietDonHangs.Select(ct => new OrderItemVM
                    {
                        MaSP = ct.MaSp,
                        TenSP = ct.MaSpNavigation.TenSp,
                        SoLuong = ct.SoLuong,
                        Gia = ct.Gia
                    }).ToList()
                });

            if (!string.IsNullOrEmpty(model.search.value))
                items = items.Where(i => i.TenKhachHang.Contains(model.search.value));

            int recordsTotal = await items.CountAsync();

            var data = await items
                .OrderByDescending(i => i.NgayDat)
                .Skip(model.start)
                .Take(model.length)
                .ToListAsync();

            return Ok(new
            {
                draw = model.draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetDetail(int id)
        {
            var order = await _db.DonHangs
                .Include(o => o.MaTkNavigation)
                .Include(o => o.ChiTietDonHangs)
                    .ThenInclude(ct => ct.MaSpNavigation)
                .Where(o => o.MaDh == id)
                .Select(o => new OrderViewModel
                {
                    MaDH = o.MaDh,
                    TenKhachHang = o.MaTkNavigation.HoTen,
                    NgayDat = o.NgayDat,
                    TongTien = o.TongTien,
                    DiaChiGiao = o.DiaChiGiao,
                    TrangThai = o.TrangThai,
                    SanPhams = o.ChiTietDonHangs.Select(ct => new OrderItemVM
                    {
                        MaSP = ct.MaSp,
                        TenSP = ct.MaSpNavigation.TenSp,
                        SoLuong = ct.SoLuong,
                        Gia = ct.Gia
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // ✅ Cập nhật trạng thái đơn hàng + giảm tồn kho nếu hoàn tất
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string trangThai)
        {
            var order = await _db.DonHangs
                .Include(o => o.ChiTietDonHangs)
                    .ThenInclude(ct => ct.MaSpNavigation)
                .FirstOrDefaultAsync(o => o.MaDh == id);

            if (order == null)
                return NotFound();

            // Nếu trạng thái cũ khác "Hoàn tất" và trạng thái mới là "Hoàn tất"
            bool isNewlyCompleted = order.TrangThai != "Hoàn tất" && trangThai == "Hoàn tất";

            order.TrangThai = trangThai;
            await _db.SaveChangesAsync();

            // ✅ Nếu chuyển sang "Hoàn tất" → trừ tồn kho
            if (isNewlyCompleted)
            {
                foreach (var ct in order.ChiTietDonHangs)
                {
                    var sp = ct.MaSpNavigation;
                    if (sp != null && ct.SoLuong.HasValue)
                    {
                        sp.SoLuongTon = (sp.SoLuongTon ?? 0) - ct.SoLuong.Value;
                        if (sp.SoLuongTon < 0) sp.SoLuongTon = 0; // tránh âm
                    }
                }
                await _db.SaveChangesAsync();
            }

            return Ok(new { success = true, message = "Cập nhật trạng thái thành công!" });
        }
    }
}
