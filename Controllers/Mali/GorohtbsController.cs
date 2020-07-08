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
    public class GorohtbsController : Controller
    {
        private readonly malisContext _context;

        public GorohtbsController(malisContext context)
        {
            _context = context;
        }

        // GET: Gorohtbs
        public async Task<IActionResult> Index()
        {
            ViewData["Group"] = await _context.Gorohtb.ToListAsync();
            return View();
        }

        // GET: Gorohtbs/Create
        public IActionResult Create()
        {
            int? mdata = _context.Gorohtb.Max(x => x.Mid);
            ViewData["maxid"] = 1;
            if (mdata != null)
            {
                ViewData["maxid"] = mdata + 1;
            }
            return PartialView();
        }

        // POST: Gorohtbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Gorohtb gorohtb)
        {
            try
            {
                _context.Add(gorohtb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ایجاد", err = err.Message });
            }
        }

        // GET: Gorohtbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (TempData["errormsg1"] != null)
            {
                ViewBag.errormsg = TempData["errormsg1"].ToString();
            }
            else
            {
                ViewBag.errormsg = "";
            }
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var gorohtb = await _context.Gorohtb.FindAsync(id);
                if (gorohtb == null)
                {
                    return NotFound();
                }
                return PartialView(gorohtb);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", "Coding");
            }
        }

        // POST: Gorohtbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Gorohtb gorohtb)
        {
            if (id != gorohtb.Mid)
            {
                return NotFound();
            }

            try
            {
                _context.Update(gorohtb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ویرایش ", err = err.Message });
            }
        }

        // GET: Gorohtbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (TempData["errormsg1"] != null)
            {
                ViewBag.errormsg = TempData["errormsg1"].ToString();
            }
            else
            {
                ViewBag.errormsg = "";
            }
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var gorohtb = await _context.Gorohtb
                    .FirstOrDefaultAsync(m => m.Mid == id);
                if (gorohtb == null)
                {
                    return NotFound();
                }

                return PartialView(gorohtb);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", "Coding");
            }
        }

        // POST: Gorohtbs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var gorohtb = await _context.Gorohtb.FindAsync(id);
                _context.Gorohtb.Remove(gorohtb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });

            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در حذف", err = err.Message });
            }
        }

        private bool GorohtbExists(int id)
        {
            return _context.Gorohtb.Any(e => e.Mid == id);
        }
    }
}
