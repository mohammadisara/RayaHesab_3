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
    public class SubmointbsController : Controller
    {
        private readonly malisContext _context;

        public SubmointbsController(malisContext context)
        {
            _context = context;
        }

        // GET: Submointbs
        public async Task<IActionResult> Index(int moinid)
        {

            try
            {
                ViewData["taf"] = await _context.RlateMstb.Where(x => x.Moinid == moinid).Include(p => p.Moin.K).Include(p => p.Moin).Include(p => p.Taf).ToListAsync();
                return View();
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message;
                return View();
            }
        }

        // GET: Submointbs/Create
        public IActionResult Create(int id)
        {
            var tmp = _context.Mointb.Include(k => k.K)
                .Select(x => new
                {
                    Mid = x.Mid,
                    Namemoin = x.K.Namekol + " " + x.Namemoin
                }).ToList();
            ViewData["codeparent"] = new SelectList(tmp, "Mid", "Namemoin", id);
            ViewData["mid"] = id;
            string maxcode = "0001";
            try
            {
                var _li = _context.Submointb.Max(x => int.Parse(x.Codetaf)).ToString();
                int i = int.Parse(_li);
                i = i + 1;
                maxcode = "0000" + i.ToString();
                maxcode = maxcode.Substring(maxcode.Length - 4);
            }
            catch
            {
            }
            ViewData["maxcode"] = maxcode;
            return PartialView();
        }

        // POST: Submointbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(RlateMstb submointb)
        {
            try
            {
                if (submointb.Moinid == 0)
                {
                    return Ok(new { msg = "حساب معین", err = "لطفا حساب معین را انتخاب نمایید" });
                }
                Submointb submoin = new Submointb();
                submoin.Codetaf = submointb.Taf.Codetaf;
                submoin.Nametaf = submointb.Taf.Nametaf;
                _context.Add(submoin);
                await _context.SaveChangesAsync();
                int id = submoin.Mid;
                submointb.Tafid = id;
                _context.Add(submointb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ذخیره", err = err.Message });
            }
        }

        // GET: Submointbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var submointb = await _context.RlateMstb.Include(p => p.Moin).Include(p => p.Taf).Where(p => p.Mid == id).FirstOrDefaultAsync();
                if (submointb == null)
                {
                    return NotFound();
                }
                int tafid = submointb.Tafid;
                int moinid = submointb.Moinid;

                var tmp = _context.Mointb.ToList();
                ViewData["codeparent"] = new SelectList(tmp, "Mid", "Namemoin", submointb.Moinid);
                return PartialView(submointb);
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message;
                return PartialView();
            }
        }

        // POST: Submointbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, RlateMstb submointb)
        {
            if (id != submointb.Mid)
            {
                return NotFound();
            }
            try
            {
                Submointb submoin = new Submointb();
                submoin.Mid = submointb.Tafid;
                submoin.Codetaf = submointb.Taf.Codetaf;
                submoin.Nametaf = submointb.Taf.Nametaf;
                _context.Update(submoin);
                _context.Update(submointb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ویرایش", err = err.Message });
            }
        }

        // GET: Submointbs/Delete/5
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
                var submointb = await _context.RlateMstb.Include(p => p.Taf).Where(p => p.Mid == id)
                    .FirstOrDefaultAsync();
                if (submointb == null)
                {
                    return NotFound();
                }

                return PartialView(submointb);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", "Coding");
            }
        }

        // POST: Submointbs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var submointb = await _context.RlateMstb.Include(p => p.Moin).Include(p => p.Taf).Where(p => p.Mid == id)
                   .FirstOrDefaultAsync();
                var submoin = await _context.Submointb.Where(p => p.Mid == submointb.Tafid).FirstOrDefaultAsync();
                _context.RlateMstb.Remove(submointb);
                _context.Submointb.Remove(submoin);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در حذف", err = err.Message });
            }
        }

        private bool SubmointbExists(int id)
        {
            return _context.Submointb.Any(e => e.Mid == id);
        }
    }
}
