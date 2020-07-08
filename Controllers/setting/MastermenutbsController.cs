using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RayaHesab.Models;
using Microsoft.AspNetCore.Http;
namespace RayaHesab.Controllers.setting
{
    public class MastermenutbsController : Controller
    {
        private readonly malisContext _context;

        public MastermenutbsController(malisContext context)
        {
            _context = context;
        }

        // GET: Mastermenutbs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mastermenutb.ToListAsync());
        }

        // GET: Mastermenutbs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var mastermenutb = await _context.Mastermenutb
            //    .FirstOrDefaultAsync(m => m.Mid == id);
            //if (mastermenutb == null)
            //{
            //    return NotFound();
            //}

//            return View(mastermenutb);
            return View();
        }

        // GET: Mastermenutbs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mastermenutbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mid,Title")] Mastermenutb mastermenutb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mastermenutb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mastermenutb);
        }

        // GET: Mastermenutbs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mastermenutb = await _context.Mastermenutb.FindAsync(id);
            if (mastermenutb == null)
            {
                return NotFound();
            }
            return View(mastermenutb);
        }

        // POST: Mastermenutbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

    }
}
