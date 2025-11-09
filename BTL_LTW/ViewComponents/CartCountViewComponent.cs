using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BTL_LTW.ViewComponents
{
    public class CartCountViewComponent : ViewComponent
    {
        private readonly MaleFashionContext _db;

        public CartCountViewComponent(MaleFashionContext context)
        {
            _db = context;
        }

        public IViewComponentResult Invoke()
        {
            int count = 0;

            var session = HttpContext.Session.GetString("UserLogin");
            if (session != null)
            {
                var user = JsonSerializer.Deserialize<UserSessionVM>(session);
                if (user != null)
                {
                    count = (from g in _db.GioHangs
                             join c in _db.ChiTietGioHangs on g.MaGioHang equals c.MaGioHang
                             where g.MaTk == user.MaTK
                             select (c.SoLuong ?? 0)).DefaultIfEmpty(0).Sum();
                }
            }

            return View(count);
        }
    }
}
