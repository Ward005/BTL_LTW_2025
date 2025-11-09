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

        // 📄 Hiển thị trang danh sách
        public IActionResult Index()
        {
            return View();
        }

        // 📊 Lấy danh sách thành viên (cho DataTables)
        [HttpPost]
        public async Task<IActionResult> GetList(jDataTable model)
        {
            var query = _dbContext.TaiKhoans.AsQueryable();

            // 🔍 Tìm kiếm
            if (!string.IsNullOrEmpty(model.search.value))
            {
                query = query.Where(t =>
                    t.HoTen.Contains(model.search.value) ||
                    t.Email.Contains(model.search.value) ||
                    t.VaiTro.Contains(model.search.value));
            }

            int totalRecords = await query.CountAsync();

            // ⚙️ Sắp xếp
            if (!string.IsNullOrEmpty(model.columns[model.order[0].column].name) &&
                !string.IsNullOrEmpty(model.order[0].dir))
            {
                query = query.OrderBy(model.columns[model.order[0].column].name + " " + model.order[0].dir);
            }

            // 📋 Lấy dữ liệu phân trang
            var data = await query
                .Skip(model.start)
                .Take(model.length)
                .Select(t => new
                {
                    t.MaTk,
                    t.HoTen,
                    t.Email,
                    t.MatKhau,
                    t.SoDienThoai,
                    t.DiaChi,
                    t.NgayTao,
                    t.VaiTro
                })
                .ToListAsync();

            return Ok(new
            {
                draw = model.draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data
            });
        }

        // 📦 Lấy thông tin 1 thành viên
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _dbContext.TaiKhoans
                .Where(t => t.MaTk == id)
                .Select(t => new
                {
                    t.MaTk,
                    t.HoTen,
                    t.Email,
                    t.MatKhau,
                    t.SoDienThoai,
                    t.DiaChi,
                    t.NgayTao,
                    t.VaiTro
                })
                .FirstOrDefaultAsync();

            return Ok(item);
        }

        // 💾 Lưu (Thêm hoặc Sửa)
        [HttpPost]
        public async Task<IActionResult> Save(TaiKhoan model)
        {
            if (model.MaTk == 0)
            {
                // ➕ Thêm mới
                model.NgayTao = DateTime.Now;
                _dbContext.TaiKhoans.Add(model);
            }
            else
            {
                // ✏️ Cập nhật
                var existing = await _dbContext.TaiKhoans.FindAsync(model.MaTk);
                if (existing == null) return NotFound();

                existing.HoTen = model.HoTen;
                existing.Email = model.Email;
                existing.MatKhau = model.MatKhau;
                existing.SoDienThoai = model.SoDienThoai;
                existing.DiaChi = model.DiaChi;
                existing.VaiTro = model.VaiTro;
            }

            await _dbContext.SaveChangesAsync();
            return Ok(new { success = true });
        }

        // ❌ Xóa thành viên
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _dbContext.TaiKhoans.FindAsync(id);
            if (item == null) return NotFound();

            _dbContext.TaiKhoans.Remove(item);
            await _dbContext.SaveChangesAsync();

            return Ok(true);
        }
    }
}
