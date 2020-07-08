using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RayaHesab.Models;

namespace RayaHesab.Controllers.Base
{
    public class LooupsubtbsController : Controller
    {
        private readonly malisContext _context;

        public LooupsubtbsController(malisContext context)
        {
            _context = context;
        }

        // GET: Looupsubtbs
        public async Task<IActionResult> Index()
        {
            ViewBag.list = await _context.Lookuptb.ToListAsync();
            return View();
        }

        // GET: Looupsubtbs/Details/5
        public IActionResult Details(int? gid)
        {
            if (gid == null)
            {
                return NotFound();
            }

            var looupsubtb = _context.Looupsubtb.Where(m => m.Grp == gid)
                .ToList();
            return Ok(new { res = looupsubtb });
        }

        // GET: Looupsubtbs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Looupsubtbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Grp, string Title)
        {
            try
            {
                Looupsubtb lb = new Looupsubtb();
                lb.Grp = Grp;
                lb.Title = Title;
                _context.Add(lb);
                await _context.SaveChangesAsync();
                return Ok(new { res = "" });

            }
            catch (Exception err)
            {
                return Ok(new { res = err.Message });
            }
        }

        // GET: Looupsubtbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var looupsubtb = await _context.Looupsubtb.FindAsync(id);
            if (looupsubtb == null)
            {
                return NotFound();
            }
            return Ok(new { res = looupsubtb });
        }

        // POST: Looupsubtbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string title)
        {
            try
            {
                var lb = _context.Looupsubtb.Where(x => x.Mid == id).FirstOrDefault();
                if (lb == null)
                    return Ok(new { res = "ردیف یافت نشد" });
                lb.Title = title;
                _context.Update(lb);
                await _context.SaveChangesAsync();
                return Ok(new { res = "" });
            }
            catch (Exception err)
            {
                return Ok(new { res = err.Message });
            }
        }

        // GET: Looupsubtbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var looupsubtb = await _context.Looupsubtb
                .FirstOrDefaultAsync(m => m.Mid == id);
            if (looupsubtb == null)
            {
                return NotFound();
            }

            return View(looupsubtb);
        }

        // POST: Looupsubtbs/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var looupsubtb = await _context.Looupsubtb.FindAsync(id);
                _context.Looupsubtb.Remove(looupsubtb);
                await _context.SaveChangesAsync();
                return Ok(new { res = "" });

            }
            catch (Exception err)
            {
                return Ok(new { res = err.Message });
            }
        }

        private bool LooupsubtbExists(int id)
        {
            return _context.Looupsubtb.Any(e => e.Mid == id);
        }
    }
}
