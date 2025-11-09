using BTL_LTW.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTW.Controllers
{
    public class BlogController : Controller
    {
        private readonly MaleFashionContext _db;
        public BlogController(MaleFashionContext db)
        {
            _db = db;
        }

        // ===== HIỂN THỊ DANH SÁCH BÀI VIẾT =====
        public IActionResult Index()
        {
            var baiViets = _db.BaiViets
                .Include(b => b.MaTkNavigation)
                .OrderByDescending(b => b.NgayDang)
                .ToList();

            return View(baiViets);
        }

        // ===== CHI TIẾT BÀI VIẾT =====
        public IActionResult Details(int id)
        {
            var baiViet = _db.BaiViets
                .Include(b => b.MaTkNavigation)
                .FirstOrDefault(b => b.MaBv == id);

            if (baiViet == null)
                return NotFound();

            return View(baiViet);
        }
    }
}
