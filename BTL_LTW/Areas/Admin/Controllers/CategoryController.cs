using BTL_LTW.Areas.Admin.Models;
using BTL_LTW.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BTL_LTW.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : BaseAdminController
    {
        private readonly MaleFashionContext _dbContext;

        public CategoryController(MaleFashionContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetList(jDataTable model)
        {
            var items = from d in _dbContext.DanhMucs select d;

            // Tìm kiếm
            if (!string.IsNullOrEmpty(model.search.value))
            {
                items = items.Where(d => d.TenDanhMuc.Contains(model.search.value));
            }

            // Sắp xếp
            if (model.order != null && model.order.Count > 0)
            {
                var colName = model.columns[model.order[0].column].name;
                var dir = model.order[0].dir;
                if (!string.IsNullOrEmpty(colName) && !string.IsNullOrEmpty(dir))
                {
                    items = items.OrderBy(colName + " " + dir);
                }
            }

            int recordsTotal = await items.CountAsync();

            var data = await items.Select(d => new
            {
                d.MaDanhMuc,
                d.TenDanhMuc,
                d.MoTa
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
        public async Task<IActionResult> GetItem(int? id)
        {
            if (id == null) return NotFound();

            var item = await _dbContext.DanhMucs.FindAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryViewModel model)
        {
            DanhMuc item;
            if (model.MaDanhMuc == null)
            {
                item = new DanhMuc();
                _dbContext.DanhMucs.Add(item);
            }
            else
            {
                item = await _dbContext.DanhMucs.FindAsync(model.MaDanhMuc);
                if (item == null) return NotFound();
            }

            item.TenDanhMuc = model.TenDanhMuc;
            item.MoTa = model.MoTa;

            await _dbContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _dbContext.DanhMucs.FindAsync(id);
            if (item == null) return Ok(false);

            _dbContext.Entry(item).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Ok(true);
        }
    }
}
