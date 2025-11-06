using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.ViewComponents
{
    public class MenuCategory : ViewComponent
    {
        private readonly MaleFashionContext db;

        public MenuCategory(MaleFashionContext context) => db = context;

        public IViewComponentResult Invoke ()
        {
            var data = db.DanhMucs.Select(p => new Category
            {
                MaCategory = p.MaDanhMuc,
                TenCategory = p.TenDanhMuc ?? "",
                MoTaCategory = p.MoTa ?? "",
                SLCategory = p.SanPhams.Count()
            });
            return View(data);
        }
    }
}
