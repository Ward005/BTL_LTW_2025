using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Drawing.Printing;
using X.PagedList.Extensions;

namespace BTL_LTW.Controllers
{
    public class ShopController : Controller
    {
        private readonly MaleFashionContext db;

        public ShopController(MaleFashionContext context) => db = context;
        public IActionResult Index(int? category, double? price, string query = "", int currentPage = 1)
        {
            var SanPham = db.SanPhams.AsQueryable();
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

        //public IActionResult FilterPrice(double? price)
        //{
        //    var SanPham = db.SanPhams.AsQueryable();
            
        //    Random random = new Random();
        //    var result = SanPham.Select(sp => new ProductVM
        //    {
        //        MaSP = sp.MaSp,
        //        TenSP = sp.TenSp ?? "",
        //        AnhChinhSP = sp.AnhChinh ?? "",
        //        ChatLuongSP = (int)random.Next(1, 6),
        //        GiaSP = (double)(sp.Gia ?? 0),
        //        MaDanhMuc = sp.MaDanhMuc ?? 0
        //    });
        //    return View("Index", result);
        //}

        //public IActionResult Search(string? query)
        //{
        //    var SanPham = db.SanPhams.AsQueryable();
        //    if (query != null)
        //    {
        //        SanPham = SanPham.Where(p => p.TenSp != null && p.TenSp.Contains(query));
        //    }
        //    Random random = new Random();
        //    var result = SanPham.Select(sp => new ProductVM
        //    {
        //        MaSP = sp.MaSp,
        //        TenSP = sp.TenSp ?? "",
        //        AnhChinhSP = sp.AnhChinh ?? "",
        //        ChatLuongSP = (int)random.Next(1, 6),
        //        GiaSP = (double)(sp.Gia ?? 0),
        //        MaDanhMuc = sp.MaDanhMuc ?? 0
        //    });
        //    return View("Index", result);
        //}
    }
}
