using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RayaHesab.Models;
using Microsoft.AspNetCore.Http;
namespace RayaHesab.Controllers.Base
{
    public class OtherhesabtbsController : Controller
    {
        private readonly malisContext _context;

        public OtherhesabtbsController(malisContext context)
        {
            _context = context;
        }

        // GET: Otherhesabtbs
        public async Task<IActionResult> Index()
        {


            var malisContext = _context.Otherhesabtb.Include(o => o.IdmoinNavigation).Include(o => o.IdtafNavigation).Include(o => o.P);
            return View(await malisContext.ToListAsync());
        }

        // GET: Otherhesabtbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherhesabtb = await _context.Otherhesabtb
                .Include(o => o.IdmoinNavigation)
                .Include(o => o.IdtafNavigation)
                .Include(o => o.P)
                .FirstOrDefaultAsync(m => m.Mid == id);
            if (otherhesabtb == null)
            {
                return NotFound();
            }

            return View(otherhesabtb);
        }

        // GET: Otherhesabtbs/Create
        public IActionResult Create()
        {
            ViewData["Idmoin"] = new SelectList(_context.Mointb, "Mid", "Namemoin");
            ViewData["Idtaf"] = new SelectList(_context.Submointb, "Mid", "Nametaf");
            ViewData["Pid"] = new SelectList(_context.Persontb, "Mid", "Namekamel");
            return View();
        }

        // POST: Otherhesabtbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mid,Namec,Idmoin,Idtaf,Pid")] Otherhesabtb otherhesabtb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(otherhesabtb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idmoin"] = new SelectList(_context.Mointb, "Mid", "Nametaf", otherhesabtb.Idmoin);
            ViewData["Idtaf"] = new SelectList(_context.Submointb, "Mid", "Mid", otherhesabtb.Idtaf);
            ViewData["Pid"] = new SelectList(_context.Persontb, "Mid", "Namekamel", otherhesabtb.Pid);
            return View(otherhesabtb);
        }

        // GET: Otherhesabtbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherhesabtb = await _context.Otherhesabtb.FindAsync(id);
            if (otherhesabtb == null)
            {
                return NotFound();
            }
            ViewData["Idmoin"] = new SelectList(_context.Mointb, "Mid", "Nametaf", otherhesabtb.Idmoin);
            ViewData["Idtaf"] = new SelectList(_context.Submointb, "Mid", "Mid", otherhesabtb.Idtaf);
            ViewData["Pid"] = new SelectList(_context.Persontb, "Mid", "Namekamel", otherhesabtb.Pid);

            return View(otherhesabtb);
        }

        // POST: Otherhesabtbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mid,Namec,Idmoin,Idtaf,Pid")] Otherhesabtb otherhesabtb)
        {
            if (id != otherhesabtb.Mid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(otherhesabtb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OtherhesabtbExists(otherhesabtb.Mid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idmoin"] = new SelectList(_context.Mointb, "Mid", "Nametaf", otherhesabtb.Idmoin);
            ViewData["Idtaf"] = new SelectList(_context.Submointb, "Mid", "Mid", otherhesabtb.Idtaf);
            ViewData["Pid"] = new SelectList(_context.Persontb, "Mid", "Namekamel", otherhesabtb.Pid);
            return View(otherhesabtb);
        }

        // GET: Otherhesabtbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherhesabtb = await _context.Otherhesabtb
                .Include(o => o.IdmoinNavigation)
                .Include(o => o.IdtafNavigation)
                .Include(o => o.P)
                .FirstOrDefaultAsync(m => m.Mid == id);
            if (otherhesabtb == null)
            {
                return NotFound();
            }

            return View(otherhesabtb);
        }

        // POST: Otherhesabtbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var otherhesabtb = await _context.Otherhesabtb.FindAsync(id);
            _context.Otherhesabtb.Remove(otherhesabtb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OtherhesabtbExists(int id)
        {
            return _context.Otherhesabtb.Any(e => e.Mid == id);
        }
    }
}
