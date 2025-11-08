using BTL_LTW.Areas.Admin.Models;
using BTL_LTW.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BTL_LTW.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MemberController : Controller
    {
        private readonly MaleFashionContext _dbContext;

        public MemberController(MaleFashionContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> getList(jDataTable model)
        {
            var items = (from i in _dbContext.TaiKhoans select i);
            int recordsTotal = 0;
            if (!string.IsNullOrEmpty(model.columns[model.order[0].column].name) && !string.IsNullOrEmpty(model.order[0].dir))
            {
                items = items.OrderBy(model.columns[model.order[0].column].name + ' ' + model.order[0].dir);
            }
            if (!string.IsNullOrEmpty(model.search.value))
            {
                items = items.Where(i => i.HoTen.Contains(model.search.value));
            }
            recordsTotal = items.Count();
            var data = await items.Select(i => new
            {
                maTK = i.MaTk,
                hoTen = i.HoTen,
                email = i.Email,
                matKhau = i.MatKhau,
                soDienThoai = i.SoDienThoai,
                diaChi = i.DiaChi,
                ngayTao = i.NgayTao,
                vaiTro = i.VaiTro
            }).Skip(model.start).Take(model.length).ToListAsync();
            var jsonData = new
            {
                draw = model.draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = data
            };
            return Ok(jsonData);
        }
        [HttpGet]
        public async Task<IActionResult> getItem(int? id)
        {
            if (_dbContext.TaiKhoans == null)
            {
                return NotFound();
            }
            var item = await _dbContext.TaiKhoans.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

    }
}
