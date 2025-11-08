using BTL_LTW.Areas.Admin.Models;
using BTL_LTW.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BTL_LTW.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : BaseAdminController
    {
        private readonly MaleFashionContext _db;

        public ArticleController(MaleFashionContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetList(jDataTable model)
        {
            var query = _db.BaiViets.AsQueryable();
            int totalRecords = query.Count();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(model.search.value))
            {
                query = query.Where(x => x.TieuDe.Contains(model.search.value));
            }

            // Sắp xếp
            if (model.order.Count > 0)
            {
                string sortColumn = model.columns[model.order[0].column].name;
                string sortDir = model.order[0].dir;
                if (!string.IsNullOrEmpty(sortColumn))
                    query = query.OrderBy($"{sortColumn} {sortDir}");
            }

            var data = await query.Skip(model.start).Take(model.length)
                .Select(x => new
                {
                    x.MaBv,
                    x.TieuDe,
                    x.NoiDung,
                    x.AnhBia,
                    NgayDang = x.NgayDang.HasValue ? x.NgayDang.Value.ToString("dd/MM/yyyy") : "",
                    x.MaTk
                }).ToListAsync();

            var jsonData = new
            {
                draw = model.draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data
            };

            return Ok(jsonData);
        }

        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _db.BaiViets.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ArticleViewModel model)
        {
            BaiViet item;

            if (model.MaBV == null)
            {
                item = new BaiViet
                {
                    TieuDe = model.TieuDe,
                    NoiDung = model.NoiDung,
                    AnhBia = model.AnhBia,
                    NgayDang = DateTime.Now,
                    MaTk = model.MaTK
                };
                _db.BaiViets.Add(item);
            }
            else
            {
                item = await _db.BaiViets.FindAsync(model.MaBV);
                if (item == null) return NotFound();

                item.TieuDe = model.TieuDe;
                item.NoiDung = model.NoiDung;
                item.AnhBia = model.AnhBia;
                item.MaTk = model.MaTK;
            }

            await _db.SaveChangesAsync();
            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.BaiViets.FindAsync(id);
            if (item == null) return Ok(false);

            _db.BaiViets.Remove(item);
            await _db.SaveChangesAsync();
            return Ok(true);
        }
    }
}
