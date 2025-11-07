using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly MaleFashionContext db;

        public ProductViewComponent(MaleFashionContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            Random random = new Random();
            var data = db.SanPhams
                .Select(sp => new ProductVM
                    {
                        MaSP = sp.MaSp ?? 0,
                        TenSP = sp.TenSp ?? "",
                        AnhChinhSP = sp.AnhChinh ?? "",
                        ChatLuongSP = (int)random.Next(1, 6),
                        GiaSP = (double)(sp.Gia ?? 0),
                        MaDanhMuc = sp.MaDanhMuc ?? 1
                    })
                .Take(8);

            return View(data);
        }
    }
}
