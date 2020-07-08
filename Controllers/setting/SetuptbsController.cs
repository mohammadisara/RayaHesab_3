using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RayaHesab.Models;

namespace RayaHesab.Controllers
{
    public class SetuptbsController : Controller
    {
        private readonly malisContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SetuptbsController(malisContext context , IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Setuptbs
        //[HttpPost]
        //public async Task<IActionResult> savepic(string pic)
        //{

        //}
        public async Task<IActionResult> Index(List<IFormFile> files = null , int mid = 0)
        {
            if (files.Count != 0)
            {
                long size = files.Sum(f => f.Length);
                // full path to file in temp location
                string webRootPath = _hostingEnvironment.WebRootPath;
                var path = webRootPath + "/images/";
                //var path =  "/imgkala/";
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(formFile.FileName);
                        var q = _context.Setuptb.Where(x => x.Mid == mid).FirstOrDefault();
                        using (var stream = new FileStream(path + mid.ToString() + fileName, FileMode.Create))
                        {

                            formFile.CopyTo(stream);
                            q.Addlogo = mid.ToString() + fileName;
                            _context.Update(q);
                            _context.SaveChanges();
                        }
                    }
                }

                // process uploaded files
                // Don't rely on or trust the FileName property without validation.

                return Ok();
            }

            ViewBag.MID = 4;
            var qr = await (from s in _context.Setuptb
                     join a in _context.Anbartb on s.Idanbar equals a.Mid
                     select new { s, a }).ToListAsync();
            List<empsetup> emp = new List<empsetup>();
            foreach (var item in qr)
            {
                emp.Add(new empsetup()
                {
                    Addlogo = item.s.Addlogo,
                    Bfforosh = item.s.Bfforosh,
                    Fforosh = item.s.Fforosh,
                    Fkharid = item.s.Fkharid,
                    Fsal = item.s.Fsal,
                    Idanbar = item.s.Idanbar,
                    Mid = item.s.Mid,
                    Namec = item.a.Namec,
                    Pfforosh = item.s.Pfforosh,
                    Sarbarg = item.s.Sarbarg,
                    Sfforosh = item.s.Sfforosh,
                    Sfkharid = item.s.Sfkharid,
                    Tsal = item.s.Tsal
                });
            }
            ViewData["list"] = emp;
            return View();
        }
        public  class empsetup
        {
            public int Mid { get; set; }
            public string Namec { get; set; }
            public string Sarbarg { get; set; }
            public string Fsal { get; set; }
            public string Tsal { get; set; }
            public string Fforosh { get; set; }
            public string Fkharid { get; set; }
            public string Sfforosh { get; set; }
            public string Sfkharid { get; set; }
            public string Pfforosh { get; set; }
            public string Bfforosh { get; set; }
            public int? Idanbar { get; set; }
            public string Addlogo { get; set; }
        }


        // GET: Setuptbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setuptb = await _context.Setuptb
                .FirstOrDefaultAsync(m => m.Mid == id);
            if (setuptb == null)
            {
                return NotFound();
            }

            return View(setuptb);
        }

        // GET: Setuptbs/Create
        public IActionResult Create()
        {
            ViewBag.MID = 4;
            return View();
        }

        // POST: Setuptbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mid,Sarbarg,Fsal,Tsal,Fforosh,Fkharid,Sfforosh,Sfkharid,Pfforosh,Bfforosh")] Setuptb setuptb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(setuptb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(setuptb);
        }

        // GET: Setuptbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.MID = 4;
            if (id == null)
            {
                return NotFound();
            }

            var setuptb = await _context.Setuptb.FindAsync(id);
            if (setuptb == null)
            {
                return NotFound();
            }
            return View(setuptb);
        }

        // POST: Setuptbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Setuptb setuptb)
        {
            if (id != setuptb.Mid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setuptb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetuptbExists(setuptb.Mid))
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
            return View(setuptb);
        }

        // GET: Setuptbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setuptb = await _context.Setuptb
                .FirstOrDefaultAsync(m => m.Mid == id);
            if (setuptb == null)
            {
                return NotFound();
            }

            return View(setuptb);
        }

        // POST: Setuptbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var setuptb = await _context.Setuptb.FindAsync(id);
            _context.Setuptb.Remove(setuptb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SetuptbExists(int id)
        {
            return _context.Setuptb.Any(e => e.Mid == id);
        }
    }
}
