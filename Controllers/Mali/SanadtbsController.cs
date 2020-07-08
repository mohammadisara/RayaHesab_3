using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MD.PersianDateTime.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RayaHesab.Models;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RayaHesab.Controllers
{
    public class SanadtbsController : Controller
    {
        private readonly malisContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public SanadtbsController(malisContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> RoleManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = RoleManager;
        }
        private async Task<string> AuthenticateUser()
        {
            try
            {
                string s = "";
                if (!string.IsNullOrEmpty(Request.Cookies["token"]))
                    s = Request.Cookies["token"];
                else
                    return "";
                var p = await _context.AspNetUserTokens.Where(x => x.Value == s).FirstOrDefaultAsync();
                if (p == null)
                    return "";
                var user = await _userManager.FindByIdAsync(p.UserId);
                if (user == null)
                    return "";
                var claims = new List<Claim>{
                    new Claim (JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.NameId,user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString())
                };
                var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                User.AddIdentity(claimsIdentity);
                var qwe = User.Identities.Where(x => x.IsAuthenticated == true && x.Name == user.UserName).FirstOrDefault();
                if (qwe == null)
                    return "";
                return p.UserId;
            }
            catch (Exception e)
            {
                string qvvv = e.Message;
                return "";
            }
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string appid = AuthenticateUser().Result;
            if (string.IsNullOrEmpty(appid))
            {
                RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            try
            {
                if (!string.IsNullOrEmpty(Request.Cookies["fullname"]))
                    ViewData["fullname"] = Request.Cookies["fullname"];
                if (!string.IsNullOrEmpty(Request.Cookies["username"]))
                    ViewData["username"] = Request.Cookies["username"];
                //if (!string.IsNullOrEmpty(Request.Cookies["logo"]))
                //    ViewData["logo"] = Request.Cookies["logo"];
                if (!string.IsNullOrEmpty(Request.Cookies["idanbar"]))
                    ViewData["idanbar"] = int.Parse(Request.Cookies["idanbar"]);
            }
            catch
            {
            }
        }


        // GET: Sanadtbs
        public async Task<IActionResult> list(int? nosanad)
        {
            var malisContext = _context.Sanadtb.Where(p => p.Nosanad == nosanad).Include(s => s.Moin).
                Include(s => s.NosanadNavigation).Include(s => s.P).Include(s => s.Pardar).
                Include(s => s.Taf).Include(s => s.User).Include(s => s.Moin.K)
                .Include(x => x.IdanbarNavigation);
            ViewData["list"] = await malisContext.ToListAsync();
            return View();
        }
        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                ViewBag.nosanad = id;
                ViewData["PageT"] = "حسابداری";
                ViewData["listanbar"] = new SelectList(_context.Anbartb.FromSql("select * from anbartb where mid in "
                    + " (select isnull(p.idanbar , 0)  from pertb p where p.userid = {0} )"
                    , int.Parse(HttpContext.Session.GetString("PersonId"))).ToList(), "Mid", "Namec");
                var query = await _context.Mastersanadtb.Where(p => p.Nosanad == id).ToListAsync();
                foreach (var item in query)
                {
                    ViewBag.datec = item.Datec.ToString();
                    ViewBag.typec = item.Typec.ToString();
                    if (item.Title == null)
                        ViewBag.titlesanad = "";
                    else
                        ViewBag.titlesanad = item.Title.ToString();
                }
                return View();
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "");
                return View();
            }
        }
        public class mastersanad
        {
            public string title { get; set; }
            public string datec { get; set; }
        }
        // GET: Sanadtbs/Details/5
        public async Task<IActionResult> Details(int? id)
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

            var sanadtb = await _context.Sanadtb
                .Include(s => s.Moin)
                .Include(s => s.NosanadNavigation)
                .Include(s => s.P)
                .Include(s => s.Pardar)
                .Include(s => s.Taf)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Mid == id);
            if (sanadtb == null)
            {
                return NotFound();
            }

            return PartialView(sanadtb);
        }

        // GET: Sanadtbs/Create
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
            ViewData["Moinid"] = new SelectList(_context.Mointb, "Mid", "Namemoin");
            ViewData["Nosanad"] = new SelectList(_context.Mastersanadtb, "Nosanad", "Title");
            var MYPID = _context.Persontb.Select(x => new { pid = x.Mid, pname = x.Namec + " " + x.Family });
            ViewData["Pid"] = new SelectList(MYPID, "pid", "pname");
            ViewData["Pardarid"] = new SelectList(_context.Pardartb, "Mid", "Datec");
            ViewData["Tafid"] = new SelectList(_context.Submointb, "Mid", "Nametaf");
            ViewData["Userid"] = HttpContext.Session.GetString("PersonId");

            return PartialView();
        }

        // POST: Sanadtbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sanadtb sanadtb)
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
                    sanadtb.Datemiladi = DateTime.Now;
                    _context.Add(sanadtb);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { id = sanadtb.Nosanad });
                }
                ViewData["Moinid"] = new SelectList(_context.Mointb, "Mid", "Namemoin", sanadtb.Moinid);
                ViewData["Nosanad"] = new SelectList(_context.Mastersanadtb, "Nosanad", "Title", sanadtb.Nosanad);
                var MYPID = _context.Persontb.Select(x => new { pid = x.Mid, pname = x.Namec + " " + x.Family });
                ViewData["Pid"] = new SelectList(MYPID, "pid", "pname", sanadtb.Pid);
                ViewData["Pardarid"] = new SelectList(_context.Pardartb, "Mid", "Datec", sanadtb.Pardarid);
                ViewData["Tafid"] = new SelectList(_context.Submointb, "Mid", "Nametaf", sanadtb.Tafid);
                ViewData["Userid"] = HttpContext.Session.GetString("PersonId");
                return View(sanadtb);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                //return RedirectToAction("/Sanadtbs/Index/" + HttpContext.Session.GetString("PersonId"));
                return RedirectToAction("Index", new { id = sanadtb.Nosanad });
            }
        }

        // GET: Sanadtbs/Edit/5
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

                var sanadtb = await _context.Sanadtb.FindAsync(id);
                if (sanadtb == null)
                {
                    return NotFound();
                }
                ViewData["Moinid"] = new SelectList(_context.Mointb, "Mid", "Namemoin", sanadtb.Moinid);
                ViewData["Nosanad"] = new SelectList(_context.Mastersanadtb, "Nosanad", "Title", sanadtb.Nosanad);
                var MYPID = _context.Persontb.Select(x => new { pid = x.Mid, pname = x.Namec + " " + x.Family });
                ViewData["Pid"] = new SelectList(MYPID, "pid", "pname", sanadtb.Pid);
                ViewData["Pardarid"] = new SelectList(_context.Pardartb, "Mid", "Datec", sanadtb.Pardarid);
                ViewData["Tafid"] = new SelectList(_context.Submointb, "Mid", "Nametaf", sanadtb.Tafid);
                ViewData["Userid"] = HttpContext.Session.GetString("PersonId");
                return PartialView(sanadtb);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", "Mastersanadtbs");
            }
        }

        // POST: Sanadtbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sanadtb sanadtb)
        {
            if (TempData["errormsg1"] != null)
            {
                ViewBag.errormsg = TempData["errormsg1"].ToString();
            }
            else
            {
                ViewBag.errormsg = "";
            }
            if (id != sanadtb.Mid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanadtb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanadtbExists(sanadtb.Mid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = sanadtb.Nosanad });
            }
            ViewData["Moinid"] = new SelectList(_context.Mointb, "Mid", "Namemoin", sanadtb.Moinid);
            ViewData["Nosanad"] = new SelectList(_context.Mastersanadtb, "Nosanad", "Title", sanadtb.Nosanad);
            var MYPID = _context.Persontb.Select(x => new { pid = x.Mid, pname = x.Namec + " " + x.Family });
            ViewData["Pid"] = new SelectList(MYPID, "pid", "pname", sanadtb.Pid);
            ViewData["Pardarid"] = new SelectList(_context.Pardartb, "Mid", "Datec", sanadtb.Pardarid);
            ViewData["Tafid"] = new SelectList(_context.Submointb, "Mid", "Nametaf", sanadtb.Tafid);
            ViewData["Userid"] = HttpContext.Session.GetString("PersonId");
            return PartialView(sanadtb);
        }

        // GET: Sanadtbs/Delete/5
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

                var sanadtb = await _context.Sanadtb
                    .Include(s => s.Moin)
                    .Include(s => s.NosanadNavigation)
                    .Include(s => s.P)
                    .Include(s => s.Pardar)
                    .Include(s => s.Taf)
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(m => m.Mid == id);
                if (sanadtb == null)
                {
                    return NotFound();
                }
                return PartialView(sanadtb);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", "Mastersanadtbs");
            }
        }

        // POST: Sanadtbs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            
            try
            {

                var sanadtb = await _context.Sanadtb.FindAsync(id);
                if (sanadtb.Pardarid == null && sanadtb.Factid == null && sanadtb.Factpid == null && sanadtb.Sbimarid == null)
                {
                    _context.Sanadtb.Remove(sanadtb);
                    await _context.SaveChangesAsync();
                    return Ok(new { msg = "", err = "" });
                }
                else
                {
                    return Ok(new { msg = "شما قادر به حذف اسناد اتومات نمی باشد", err = "" });
                }
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در حذف", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }
        }

        private bool SanadtbExists(int id)
        {
            return _context.Sanadtb.Any(e => e.Mid == id);
        }

        [HttpPost]
        public async Task<IActionResult> savesubsanad(int typesave, int nosanad, int moinid, int? tafid, int? pid, string note, decimal? bed, decimal? bes, string nofish, int idanbar, string namemoin, string nameperson, int mid)
        {
            var qper = _context.Gorohpersontb.Where(x => x.Moinid == moinid).FirstOrDefault();
            if (qper != null && (pid == null || pid == 0))
            {
                return Ok(new { msg = "خطا", err = "این حساب متصل به اشخاص و مشتریان است لطفا مشتری و یا شخص رو انتخاب نمایید" });
            }
            if (string.IsNullOrEmpty(namemoin))
                return Ok(new { msg = "خطا", err = "حساب را انتخاب کنید" });
            if (string.IsNullOrEmpty(nameperson))
                pid = null;
            if (typesave == 0)
            {
                Sanadtb sanad = new Sanadtb();
                try
                {
                    sanad.Nosanad = nosanad;
                    sanad.Moinid = moinid;
                    sanad.Tafid = tafid;
                    sanad.Pid = pid;
                    sanad.Note = note;
                    sanad.Nofish = nofish;
                    sanad.Bed = bed;
                    sanad.Bes = bes;
                    sanad.Idanbar = idanbar;
                    sanad.Userid = await AuthenticateUser();
                    _context.Add(sanad);
                    _context.SaveChanges();
                }
                catch (Exception err)
                {
                    return Ok(new { msg = "خطا در ذخیره", err = err.Message });
                }
            }
            else if (typesave == 1)
            {
                var q = await _context.Sanadtb.Where(x => x.Mid == mid).FirstOrDefaultAsync();
                if (q != null)
                {
                    q.Moinid = moinid;
                    q.Tafid = tafid;
                    q.Pid = pid;
                    q.Note = note;
                    q.Nofish = nofish;
                    q.Bed = bed;
                    q.Bes = bes;
                    q.Idanbar = idanbar;
                    q.Userid = await AuthenticateUser();
                    _context.Sanadtb.Update(q);
                    _context.SaveChanges();
                }
                else
                {
                    return Ok(new { msg = "خطا در ذخیره", err = "هیچ داده ای برای ذخیره موجود نمی باشد" });
                }
            }
            return Ok(new { msg = "", err = "" });
        }

        [HttpPost]
        public string getrow(string mid)
        {

            var sanad = _context.Sanadtb.Where(x => x.Mid == int.Parse(mid)).Include(x => x.Moin)
                .Include(x => x.Moin.K).Include(x => x.Taf).Include(x => x.P).FirstOrDefault();
            empsanad emp = new empsanad();
            if (!string.IsNullOrEmpty(sanad.Bed.ToString()))
                emp.bed = MainController.Totext(sanad.Bed.ToString());
            if (!string.IsNullOrEmpty(sanad.Bes.ToString()))
                emp.bes = MainController.Totext(sanad.Bes.ToString());
            emp.mid = sanad.Mid.ToString(sanad.Mid.ToString());
            if (sanad.Moin != null)
            {
                emp.moinid = sanad.Moinid.ToString();
                emp.codemoin = sanad.Moin.Codemoin.ToString();
                emp.namemoin = sanad.Moin.Namemoin.ToString();
                emp.codekol = sanad.Moin.K.Codekol.ToString();
                emp.namekol = sanad.Moin.K.Namekol.ToString();
            }
            if (!string.IsNullOrEmpty(sanad.Note))
                emp.note = sanad.Note.ToString();
            if (sanad.P != null)
            {
                emp.pid = sanad.Pid.ToString();
                emp.namekamel = sanad.P.Namekamel.ToString();
            }
            if (sanad.Taf != null)
            {
                emp.tafid = sanad.Tafid.ToString();
                emp.codetaf = sanad.Taf.Codetaf.ToString();
                emp.nametaf = sanad.Taf.Nametaf.ToString();
            }
            emp.idanbar = sanad.Idanbar;
            emp.fish = sanad.Nofish;
            return JsonConvert.SerializeObject(emp);
        }
        private class empsanad
        {
            public string mid { get; set; }
            public string moinid { get; set; }
            public string tafid { get; set; }
            public string pid { get; set; }
            public string bed { get; set; }
            public string bes { get; set; }
            public string note { get; set; }
            public int? idanbar { get; set; }
            public string fish { get; set; }
            public string codekol { get; set; }
            public string codemoin { get; set; }
            public string codetaf { get; set; }


            public string namekol { get; set; }
            public string namemoin { get; set; }
            public string nametaf { get; set; }
            public string namekamel { get; set; }
        }


    }
}
