using Microsoft.AspNetCore.Mvc;
using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using X.PagedList.Extensions;

namespace BTL_LTW.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly MaleFashionContext db;

        public ShoppingCartController (MaleFashionContext context) => db = context!;
        public IActionResult Index()
        {
            // Lấy dữ liệu từ bảng SanPhams trong database
            var data = db.SanPhams.Select(sp => new ShoppingCartVM
            {
                TenSP = sp.TenSp ?? "",
                AnhChinhSP = sp.AnhChinh ?? "",
                MoTa = sp.MoTa ?? "",
                GiaSP = Convert.ToDouble(sp.Gia ?? 0)
            }).ToList();

            return View(data); // ✅ Truyền dữ liệu vào View
        }


    }
}
