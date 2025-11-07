using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.Controllers
{
    public class DetailController : Controller
    {
        private readonly MaleFashionContext db;

        public DetailController(MaleFashionContext context) => db = context;
        public IActionResult Index(int? MaSP)
        {
            var SanPham = db.SanPhams.AsQueryable();
            if (MaSP.HasValue)
            {
                SanPham = SanPham.Where(p => p.MaSp == MaSP.Value);
            }
            Random random = new Random();
            var result = SanPham.Select(sp => new ProductVM
            {
                MaSP = sp.MaSp ?? 0,
                TenSP = sp.TenSp ?? "",
                AnhChinhSP = sp.AnhChinh ?? "",
                ChatLuongSP = (int)random.Next(1, 6),
                GiaSP = (double)(sp.Gia ?? 0),
                MaDanhMuc = sp.MaDanhMuc ?? 0
            }).FirstOrDefault();
            return View(result);
        }
    }
}
