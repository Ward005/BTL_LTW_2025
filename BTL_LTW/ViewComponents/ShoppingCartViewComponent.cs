using BTL_LTW.Data;
using BTL_LTW.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.ViewComponents
{
    public class ShoppingCartViewComponent :ViewComponent
    {
        private readonly MaleFashionContext db;
        public ShoppingCartViewComponent (MaleFashionContext context) =>  db = context;
        
        public IViewComponentResult Invoke()
        {
            var data = db.SanPhams.Select( sp => new ShoppingCartVM
            {
                TenSP = sp.TenSp,
                AnhChinhSP = sp.AnhChinh,
                MoTa = sp.MoTa,
                GiaSP = (double)sp.Gia
            }).ToList();
            return View(data);
        }

    }
}
