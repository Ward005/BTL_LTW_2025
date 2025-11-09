using BTL_LTW.Areas.Admin.Models;
using BTL_LTW.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTW.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : BaseAdminController
    {
        private readonly MaleFashionContext _db;

        public ReportController(MaleFashionContext db)
        {
            _db = db;
        }

        public IActionResult IncomeByMonth()
        {
            ViewBag.Title = "Thống kê doanh thu theo tháng";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetIncomeByMonth(int year = 0)
        {
            if (year == 0)
                year = DateTime.Now.Year;

            var data = await _db.DonHangs
                .Where(d => d.NgayDat.HasValue && d.TrangThai == "Hoàn tất" && d.NgayDat.Value.Year == year)
                .GroupBy(d => new { d.NgayDat.Value.Month, d.NgayDat.Value.Year })
                .Select(g => new ReportViewModel
                {
                    Thang = g.Key.Month,
                    Nam = g.Key.Year,
                    DoanhThu = g.Sum(x => x.TongTien ?? 0)
                })
                .OrderBy(x => x.Thang)
                .ToListAsync();

            return Ok(data);
        }
    }
}
