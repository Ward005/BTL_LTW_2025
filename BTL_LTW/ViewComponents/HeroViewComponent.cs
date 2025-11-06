using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.ViewComponents
{
    public class HeroViewComponent : ViewComponent
    {
        private readonly MaleFashionContext db;

        public HeroViewComponent(MaleFashionContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Banners.Select(ban => new HeroVM
            {
                MaHero = ban.MaBanner,
                MoTaHero = ban.MoTa ?? string.Empty,
                AnhHero = ban.Anh ?? string.Empty,
                LienKetHero = ban.LienKet ?? string.Empty,
                TrangThaiHero = ban.TrangThai ?? true
            });

            return View(data);
        }
    }
}
