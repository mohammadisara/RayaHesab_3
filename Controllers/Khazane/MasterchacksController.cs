using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RayaHesab.Models;

namespace RayaHesab.Controllers.Khazane
{
    public class MasterchacksController : Controller
    {
        private readonly malisContext _context;

        public MasterchacksController(malisContext context)
        {
            _context = context;
        }

        // GET: Masterchacks
        public async Task<IActionResult> Index(int id=0)
        {
            ViewData["PageT"] = "اطلاعات پایه";
            if (TempData["errormsg1"] != null)
            {
                ViewBag.errormsg = TempData["errormsg1"].ToString();
            }
            else
            {
                ViewBag.errormsg = "";
            }
            ViewBag.MID = 5;
            try
            {
                HttpContext.Session.SetInt32("midb",id);
                var malisContext = _context.Masterchack.Where(p => p.Bankid == id).Include(m => m.Bank);
                return View(await malisContext.ToListAsync());
            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction(nameof(Index));
            }
        }        

        // GET: Masterchacks/Create
        public IActionResult Create()
        {
            if (TempData["errormsg1"] != null)
            {
                ViewBag.errormsg = TempData["errormsg1"].ToString();
            }
            else
            {
                ViewBag.errormsg = "";
            }
            //ViewData["Bankid"] = new SelectList(_context.Banktb, "Mid", "Title");
            ViewData["Bankid"] = HttpContext.Session.GetInt32("midb");

            return PartialView();
        }

        // POST: Masterchacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Masterchack masterchack)
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
                if (ModelState.IsValid)
                {
                    _context.Add(masterchack);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { id = masterchack.Bankid });
                }
                ViewData["Bankid"] = new SelectList(_context.Banktb, "Mid", "Title", masterchack.Bankid);
                return View(masterchack);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", new { id = masterchack.Bankid });
            }            
        }

        // GET: Masterchacks/Edit/5
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

                var masterchack = await _context.Masterchack.FindAsync(id);
                if (masterchack == null)
                {
                    return NotFound();
                }
                ViewData["Bankid"] = new SelectList(_context.Banktb, "Mid", "Title", masterchack.Bankid);
                return PartialView(masterchack);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", "Masterchacks");
            }

        }

        // POST: Masterchacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Masterchack masterchack)
        {
            if (TempData["errormsg1"] != null)
            {
                ViewBag.errormsg = TempData["errormsg1"].ToString();
            }
            else
            {
                ViewBag.errormsg = "";
            }
            if (id != masterchack.Mid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(masterchack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterchackExists(masterchack.Mid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = masterchack.Bankid });
            }
            ViewData["Bankid"] = new SelectList(_context.Banktb, "Mid", "Title", masterchack.Bankid);
            return View(masterchack);
        }

        // GET: Masterchacks/Delete/5
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

                var masterchack = await _context.Masterchack
                    .Include(m => m.Bank)
                    .FirstOrDefaultAsync(m => m.Mid == id);
                if (masterchack == null)
                {
                    return NotFound();
                }

                return PartialView(masterchack);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", "Masterchacks");
            }

        }

        // POST: Masterchacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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
                var masterchack = await _context.Masterchack.FindAsync(id);
                _context.Masterchack.Remove(masterchack);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = masterchack.Bankid });
            }
            catch
            {
                TempData["errormsg1"] = "امکان حذف وجود ندارد!";
                return RedirectToAction("Index", "Mastersanadtbs");
            }
        }

        private bool MasterchackExists(int id)
        {
            return _context.Masterchack.Any(e => e.Mid == id);
        }
    }
}
