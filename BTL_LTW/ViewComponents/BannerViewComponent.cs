using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.ViewComponents
{
    public class BannerViewComponent : ViewComponent
    {
        private readonly MaleFashionContext db;

        public BannerViewComponent(MaleFashionContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.DanhMucs
                .Where(p => p.TenDanhMuc == "Áo" || p.TenDanhMuc == "Phụ kiện" || p.TenDanhMuc == "Giày")
                .Select(p => new BannerVM
                {
                    MaBanner = p.MaDanhMuc,
                    TenBanner = p.TenDanhMuc ?? string.Empty,
                    AnhBanner = p.SanPhams.Select(sp => sp.AnhChinh).FirstOrDefault() ?? string.Empty
                });
            return View(data);
        }
    }
}
