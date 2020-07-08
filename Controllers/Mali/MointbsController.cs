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
    public class MointbsController : Controller
    {
        private readonly malisContext _context;

        public MointbsController(malisContext context)
        {
            _context = context;
        }

        // GET: Mointbs
        public async Task<IActionResult> Index(int kid)
        {

            try
            {

                var malisContext = _context.Mointb.Where(x=>x.Kid == kid).Include(m => m.K);
                ViewData["moin"] = await malisContext.ToListAsync();
                return View();

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", "Coding");
            }
        }

        // GET: Mointbs/Create
        public IActionResult Create(int id)
        {
            try
            {
                ViewData["Klistid"] = new SelectList(_context.Koltb, "Mid", "Namekol", id);
                ViewData["kid"] = id;
                var mdata = _context.Mointb.Where(x => x.Kid == id).Max(x => x.Codemoin);
                int i = 0;
                if (!string.IsNullOrEmpty(mdata))
                    i = int.Parse(mdata);
                string MyString = ("000" + (i + 1).ToString());
                ViewData["maxid"] = MyString.Substring(MyString.Length - 3);
                return PartialView();
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message;
                return PartialView();
            }
        }

        // POST: Mointbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Mointb mointb)
        {
            try
            {
                _context.Add(mointb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ایجاد", err = err.Message });
            }
        }

        // GET: Mointbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var mointb = await _context.Mointb.FindAsync(id);
                if (mointb == null)
                {
                    return NotFound();
                }
                ViewData["Kid"] = new SelectList(_context.Koltb, "Mid", "Namekol", mointb.Kid);
                return PartialView(mointb);
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message;
                return PartialView();

            }
        }

        // POST: Mointbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Mointb mointb)
        {
            if (id != mointb.Mid)
            {
                return NotFound();
            }

            try
            {
                _context.Update(mointb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ویرایش", err = err.Message });

            }
        }

            // GET: Mointbs/Delete/5
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
                if (id == null)
                {
                    return NotFound();
                }

                try
                {
                    var mointb = await _context.Mointb
                            .Include(m => m.K)
                            .FirstOrDefaultAsync(m => m.Mid == id);
                    if (mointb == null)
                    {
                        return NotFound();
                    }

                    return PartialView(mointb);
                }
                catch
                {
                    TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                    return RedirectToAction("Index", "Coding");
                }
            }

        // POST: Mointbs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var mointb = await _context.Mointb.FindAsync(id);
                _context.Mointb.Remove(mointb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در حذف", err = err.Message });

            }
        }

        private bool MointbExists(int id)
        {
            return _context.Mointb.Any(e => e.Mid == id);
        }
    }
}
