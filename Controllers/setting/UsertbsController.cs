using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RayaHesab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace RayaHesab.Controllers
{
    public class UsertbsController : Controller
    {
        private readonly malisContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UsertbsController(malisContext context, UserManager<IdentityUser> UserManager)
        {
            _context = context;
            _userManager = UserManager;
        }

        // GET: Usertbs
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var malisContext = _context.AspNetUsers.Include(u => u.IdanbarNavigation);
            ViewData["list"] = await malisContext.ToListAsync();
            return View();
        }

        // GET: Usertbs/Details/5
        private class emplistbank
        {
            public int Mid { get; set; }
            public string Title { get; set; }
        }
        // GET: Usertbs/Create
        public IActionResult Create()
        {
            ViewData["Idanbar"] = new SelectList(_context.Anbartb, "Mid", "Namec");
            return View();
        }

        // POST: Usertbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Usertb usertb)
        {

            try
            {
                _context.Add(usertb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ذخیره ", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }

        }

        // GET: Usertbs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var usertb = await _context.AspNetUsers.FindAsync(id);
            if (usertb == null)
            {
                return NotFound();
            }
            ViewData["Idanbar"] = new SelectList(_context.Anbartb, "Mid", "Namec", usertb.Idanbar);
            return View(usertb);
        }

        // POST: Usertbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(AspNetUsers usertb)
        {
            try
            {
                var q = _context.AspNetUsers.Where(x => x.Id == usertb.Id).FirstOrDefault();
                q.Firstname = usertb.Firstname;
                q.Lastname = usertb.Lastname;
                q.PhoneNumber = usertb.PhoneNumber;
                q.Idanbar = usertb.Idanbar;
                q.Isactive = usertb.Isactive;
                _context.Update(q);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ذخیره", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }
        }

        // GET: Usertbs/Delete/5
        [HttpPost]
        public IActionResult Deletemenu(int? menuid, string uid)
        {
            try
            {
                if (menuid == null)
                {
                    return Ok(new { msg = "ورودی اشتباه می باشد", state = 0 });
                }
                var p = _context.Pertb.Where(x => x.Menuid == menuid && x.Userid == uid).FirstOrDefault();
                _context.Pertb.Remove(p);
                _context.SaveChanges();
                return Ok(new { msg = "حذف انجام شد", state = 1 });

            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا : " + err.Message, state = 0 });
            }
        }

        // POST: Usertbs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                IdentityUser user = new IdentityUser();
                user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    return NotFound();
                var anbartb = await _userManager.DeleteAsync(user);
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در حذف", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }
        }


        public async Task<IActionResult> Ganbar(string id)
        {
            ViewData["uid"] = id;
            var u = _context.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
            ViewData["nameu"] = string.Format("{0}  {1}", u.Firstname, u.Lastname);
            var q = _context.Anbartb.FromSql("select * from anbartb where mid not in (select idanbar from pertb where userid = {0} and idanbar is not  null)", id);
            ViewData["list"] = await q.ToListAsync();
            return View();
        }




        [HttpPost]
        public IActionResult savem(int menuid, string uid)
        {
            try
            {
                Pertb p = new Pertb();
                p.Menuid = menuid;
                p.Userid = uid;
                _context.Add(p);
                _context.SaveChanges();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ذخیره", err = err.Message });
            }
        }

        [HttpPost]
        public string saveg(int mid, string uid)
        {
            try
            {
                Pertb p = new Pertb();
                p.Gkalaid = mid;
                p.Userid = uid;
                _context.Add(p);
                _context.SaveChanges();
                return "1";
            }
            catch (Exception e)
            {
                return e.InnerException.Message;
            }
        }

        [HttpPost]
        public string savegperson(int mid, string uid)
        {
            try
            {
                Pertb p = new Pertb();
                p.Gperson = mid;
                p.Userid = uid;
                _context.Add(p);
                _context.SaveChanges();
                return "1";
            }
            catch (Exception e)
            {
                return e.InnerException.Message;
            }
        }

        [HttpPost]
        public string saveganbar(int mid, string uid)
        {
            try
            {
                Pertb p = new Pertb();
                p.Idanbar = mid;
                p.Userid = uid;
                _context.Add(p);
                _context.SaveChanges();
                return "1";
            }
            catch (Exception e)
            {
                return e.InnerException.Message;
            }
        }

        [HttpPost]
        public string delg(int mid)
        {
            try
            {
                var q = _context.Pertb.Where(x => x.Mid == mid).FirstOrDefault();
                _context.Pertb.Remove(q);
                _context.SaveChanges();
                return "1";
            }
            catch (Exception e)
            {
                return e.InnerException.Message;
            }
        }

        private class guser
        {
            public int Mid { get; set; }
            public string Title { get; set; }
        }
        [HttpPost]
        public string getg(string uid)
        {
            try
            {
                var q = _context.Pertb.Where(x => x.Userid == uid && x.Gkalaid != null).Include(x => x.Gkala).ToList();
                List<guser> _li = new List<guser>();
                foreach (var item in q)
                {
                    _li.Add(new guser
                    {
                        Mid = item.Mid,
                        Title = item.Gkala.Namep
                    });
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(_li);
            }
            catch
            {
                return "0";
            }
        }

        [HttpPost]
        public string getganbar(string uid)
        {
            try
            {
                var q = _context.Pertb.Where(x => x.Userid == uid && x.Idanbar != null).Include(x => x.IdanbarNavigation).ToList();
                List<guser> _li = new List<guser>();
                foreach (var item in q)
                {
                    _li.Add(new guser
                    {
                        Mid = item.Mid,
                        Title = item.IdanbarNavigation.Namec
                    });
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(_li);
            }
            catch
            {
                return "0";
            }
        }

        [HttpPost]
        public string getgperson(string uid)
        {
            try
            {
                var q = _context.Pertb.Where(x => x.Userid == uid && x.Gperson != null).Include(x => x.GpersonNavigation).ToList();
                List<guser> _li = new List<guser>();
                foreach (var item in q)
                {
                    _li.Add(new guser
                    {
                        Mid = item.Mid,
                        Title = item.GpersonNavigation.Title
                    });
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(_li);
            }
            catch
            {
                return "0";
            }
        }

        private class empuser
        {
            public string title { get; set; }
        }
        [HttpPost]
        public string getanbar(string uid)
        {
            var q = _context.Pertb.Where(x => x.Userid == uid && x.Idanbar > 0)
                .Include(x => x.IdanbarNavigation).ToList();
            string s = "";
            foreach (var item in q)
            {
                s = (string.IsNullOrEmpty(s) ? "" : s + " - ") + item.IdanbarNavigation.Namec;
            }
            return s;
        }
        public IActionResult UserMenu(string id)
        {
            ViewData["uid"] = id;
            var mastermenu = _context.Mastermenutb.Where(x => x.Act != 0).OrderBy(x => x.Mastersort).ToList();
            ViewData["mastermenu"] = mastermenu;
            return View();
        }
        public IActionResult shortcutmenu(int id)
        {
            ViewData["uid"] = id;
            var mastermenu = _context.Mastermenutb.Where(x => x.Act != 0).OrderBy(x => x.Mastersort).ToList();
            ViewData["mastermenu"] = mastermenu;
            return View();
        }
        public async Task<IActionResult> getsubmenu(int mid, string uid)
        {
            ViewData["uid"] = uid;
            var q = await (from w in _context.Menutb
                     where w.Menuid == mid && !(from e in _context.Pertb where e.Menuid != null && e.Userid == uid select e.Menuid).Contains(w.Mid) select w ).ToListAsync();
            //var p = await _context.Menutb.FromSql("select * from menutb where menuid = @p1 and mid not in (select m.menuid from pertb m where userid = @p2 and m.menuid is not null)", mid, uid).ToListAsync();
            //ViewBag.listu = p;
            return View(q);
        }
        public async Task<IActionResult> getsubmenucut(int mid, int uid)
        {
            var p = await _context.Menutb.FromSql("select * from menutb where menuid = {0} and mid not in (select m.menuid from shortcuttb m where userid = {1} and m.menuid is not null)", mid, uid).ToListAsync();
            return Ok(new { res = p });
        }
        public async Task<IActionResult> delsandogh(int mid)
        {
            try
            {
                var q =await _context.Pertb.FindAsync(mid);
                _context.Pertb.Remove(q);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "" , err = "" });
            }
            catch (Exception e)
            {
                return Ok(new { msg = "خطا در حذف", err = e.Message });
            }
        }
        public async Task<IActionResult> selsandogh(int mid , string uid)
        {
            try
            {
                Pertb p = new Pertb();
                p.Bank = mid;
                p.Userid = uid;
                await _context.Pertb.AddAsync(p);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "" , err = "" });
            }
            catch (Exception e)
            {
                return Ok(new { msg = "خطا در حذف", err = e.Message });
            }
        }
        public async Task<IActionResult> sandogh(string uid)
        {
            var q1 = await _context.Pertb.Where(x => x.Userid == uid && x.Bank != null)
                .Include(x=>x.BankNavigation).Select(x => x.Bank).ToListAsync();
            var p = await _context.Banktb.Where(x=> !q1.Contains(x.Mid)).ToListAsync();
            ViewBag.list = p;
            var q = await _context.Pertb.Where(x => x.Userid == uid && x.Bank != null)
                .Include(x => x.BankNavigation).ToListAsync();
            ViewBag.listq = q;
            ViewBag.uid = uid;
            return View();
        }
        public IActionResult AllMenu(string uid)
        {
            try
            {
                var p =  _context.Pertb.Where(x => x.Userid == uid && x.Menuid != null)
                    .Include(x => x.Menu).ToList();
                return PartialView(p);

            }
            catch (Exception e)
            {
                string s = e.Message;
                return PartialView();
            }
        }
        [HttpPost]
        public IActionResult Deletemenucut(int? mid)
        {
            try
            {

                var p = _context.Shortcuttb.Where(x => x.Mid == mid).FirstOrDefault();
                _context.Shortcuttb.Remove(p);
                _context.SaveChanges();
                return Ok(new { msg = "حذف انجام شد", state = 1 });

            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا : " + err.Message, state = 0 });
            }
        }


    }
}
