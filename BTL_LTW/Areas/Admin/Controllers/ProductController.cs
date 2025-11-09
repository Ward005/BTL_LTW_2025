using BTL_LTW.Areas.Admin.Models;
using BTL_LTW.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;

namespace BTL_LTW.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : BaseAdminController
    {
        private readonly MaleFashionContext _dbContext;

        public ProductController(MaleFashionContext context) => _dbContext = context;

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> getList(jDataTable model)
        {
            var items = (from i in _dbContext.SanPhams select i);
            int recordsTotal = 0;
            if (!string.IsNullOrEmpty(model.columns[model.order[0].column].name) && !string.IsNullOrEmpty(model.order[0].dir))
            {
                items = items.OrderBy(model.columns[model.order[0].column].name + ' ' + model.order[0].dir);
            }
            if (!string.IsNullOrEmpty(model.search.value))
            {
                items = items.Where(i => i.TenSp.Contains(model.search.value));
            }
            recordsTotal = items.Count();
            var data = await items.Select(i => new
            {
                i.MaSp,
                i.TenSp,
                i.Gia,
                i.MoTa,
                i.AnhChinh,
                i.MaDanhMuc,
                i.SoLuongTon,
                i.TrangThai
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
            if (_dbContext.SanPhams == null)
            {
                return NotFound();
            }
            var item = await _dbContext.SanPhams.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductViewModel model)
        {
            BTL_LTW.Data.SanPham item;
            if (model.MaSP == null)
            {
                item = new BTL_LTW.Data.SanPham();
                item.TenSp = model.TenSP;
                await _dbContext.SanPhams.AddAsync(item);
            }
            else
            {
                item = await _dbContext.SanPhams.FindAsync(model.MaSP);
                if (item == null)
                {
                    return NotFound();
                }
            }
            item.TenSp = model.TenSP;
            item.Gia = model.Gia;
            item.MoTa = model.MoTa;
            item.AnhChinh = model.AnhChinh;
            item.MaDanhMuc = model.MaDanhMuc;
            item.SoLuongTon = model.SoLuongTon;
            item.TrangThai = model.TrangThai;
            await _dbContext.SaveChangesAsync();
            return Ok(item);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var productItem = await _dbContext.SanPhams.Where(p => p.MaSp == id).FirstOrDefaultAsync();
            if (productItem != null)
            {
                var item = await _dbContext.SanPhams.FindAsync(id);
                _dbContext.Entry(item).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
                return Ok(true);
            }
            return Ok(false);
        }
    }
}
