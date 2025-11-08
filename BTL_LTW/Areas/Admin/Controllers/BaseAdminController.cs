using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaseAdminController : Controller
    {
        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            // ✅ Nếu chưa đăng nhập -> quay lại trang login
            if (!LoginAdminController.IsLoggedIn)
            {
                context.Result = new RedirectToActionResult("Login", "LoginAdmin", new { area = "Admin" });
            }

            base.OnActionExecuting(context);
        }
    }
}
