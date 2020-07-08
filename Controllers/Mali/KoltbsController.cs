using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RayaHesab.Models;

namespace RayaHesab.Controllers
{
    public class KoltbsController : Controller
    {
        private readonly malisContext _context;

        public KoltbsController(malisContext context)
        {
            _context = context;
        }

        // GET: Koltbs
        public async Task<IActionResult> Index(int gid)
        {

            var malisContext = _context.Koltb.Include(k => k.G).Where(x=>x.Gid == gid);
            ViewData["kol"] = await malisContext.ToListAsync();
            return View();
        }

        // GET: Koltbs/Create
        public IActionResult Create(int id)
        {
            ViewData["Glistid"] = new SelectList(_context.Gorohtb.OrderBy(x => x.Mid), "Mid", "Namec", id);
            ViewData["gid"] = id;
            var mdata = _context.Koltb.Where(x => x.Gid == id).Max(x => x.Codekol);
            if (!string.IsNullOrEmpty(mdata))
            {
                int i = int.Parse(mdata);
                ViewData["maxid"] = i + 1;
            }
            else
                ViewData["maxid"] = id + "01";
            return PartialView();
        }

        // POST: Koltbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Koltb koltb)
        {
            try
            {
                _context.Add(koltb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ایجاد", err = err.Message });
            }
        }

        // GET: Koltbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var koltb = await _context.Koltb.FindAsync(id);
                if (koltb == null)
                {
                    return NotFound();
                }
                ViewData["Gid"] = new SelectList(_context.Gorohtb, "Mid", "Namec", koltb.Gid);
                return PartialView(koltb);
            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", "Coding");
            }
        }

        // POST: Koltbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Koltb koltb)
        {
            if (id != koltb.Mid)
            {
                return NotFound();
            }
            try
            {
                _context.Update(koltb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ویرایش", err = err.Message });
            }
        }

        // GET: Koltbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var koltb = await _context.Koltb
                    .Include(k => k.G)
                    .FirstOrDefaultAsync(m => m.Mid == id);
                if (koltb == null)
                {
                    return NotFound();
                }

                return PartialView(koltb);
            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", "Coding");
            }
        }

        // POST: Koltbs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var koltb = await _context.Koltb.FindAsync(id);
                int? gid = koltb.Gid;
                _context.Koltb.Remove(koltb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ایجاد", err = err.Message });
            }
        }

        private bool KoltbExists(int id)
        {
            return _context.Koltb.Any(e => e.Mid == id);
        }
    }
}
