using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Drawing.Printing;
using X.PagedList.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTW.Controllers
{
    public class ShopController : Controller
    {
        private readonly MaleFashionContext _db;

        public ShopController(MaleFashionContext context) => _db = context;
        public IActionResult Index(int? category, double? price, string query = "", int currentPage = 1)
        {
            var SanPham = _db.SanPhams.AsQueryable();
            if (category.HasValue)
            {
                SanPham = SanPham.Where(p => p.MaDanhMuc == category.Value);
            }
            if (price.HasValue)
            {
                decimal giaMin = (decimal)(price.Value * 50);
                decimal giaMax = (decimal)((price.Value + 1) * 50);
                if (price.Value < 5) SanPham = SanPham.Where(p => p.Gia >= giaMin && p.Gia <= giaMax);
                else SanPham = SanPham.Where(p => p.Gia >= giaMin);
            }
            if (query != null)
            {
                SanPham = SanPham.Where(p => p.TenSp != null && p.TenSp.Contains(query));
            }
            Random random = new Random();
            var result = SanPham.Select(sp => new ProductVM
            {
                MaSP = sp.MaSp,
                TenSP = sp.TenSp ?? "",
                AnhChinhSP = sp.AnhChinh ?? "",
                ChatLuongSP = (int)random.Next(1, 6),
                GiaSP = (double)(sp.Gia ?? 0),
                MaDanhMuc = sp.MaDanhMuc ?? 0
            });
            int totalRecords = result.Count();
            int pageSize = 6;
            int totalPages = (int)(Math.Ceiling((double)(totalRecords) / pageSize));

            result = result.OrderBy(sp => sp.MaSP)
                           .Skip((currentPage - 1) * pageSize)
                           .Take(pageSize);

            ViewBag.CurrentPage = currentPage;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.Category = category;
            ViewBag.Query = query;
            ViewBag.Price = price;
            ViewBag.ProductSize = result.Count();

            return View(result);
        }
        public IActionResult Details(int id)
        {
            var sp = _db.SanPhams
                        .Include(x => x.AnhSanPhams)
                        .Include(x => x.MaDanhMucNavigation)
                        .FirstOrDefault(x => x.MaSp == id);

            if (sp == null)
                return NotFound();

            // Tạo ViewModel chứa ảnh chính và phụ
            var vm = new ProductDetailVM
            {
                MaSP = sp.MaSp,
                TenSP = sp.TenSp ?? "",
                GiaSP = (double)(sp.Gia ?? 0),
                MoTaSP = sp.MoTa ?? "",
                AnhChinhSP = sp.AnhChinh ?? "/img/default.jpg",
                DanhMuc = sp.MaDanhMucNavigation?.TenDanhMuc ?? "",
                SoLuongTon = sp.SoLuongTon ?? 0
            };

            return View(vm);
        }

      
    }
}
