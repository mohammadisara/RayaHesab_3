using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RayaHesab.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RayaHesab.Controllers.Khazane
{
    public class sandoghtbController : Controller
    {
        private readonly malisContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public sandoghtbController(malisContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> RoleManager, SignInManager<IdentityUser> signInManager)
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

        // GET: Banktbs
        public IActionResult Index()
        {
                return View();

        }
        public async Task<IActionResult> List()
        {

            try
            {
                var malisContext = _context.Banktb.Where(p => p.Typec == 3).Include(k => k.Moin.K).Include(b => b.Moin)
                    .Include(b => b.Taf);
                ViewData["list"] = await malisContext.ToListAsync();
                return View();
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "");
                return View();
            }

        }

        [HttpPost]
        public string getrow(string mid)
        {
            var model = _context.Banktb.Where(p => p.Mid == Convert.ToInt32(mid)).Include(m => m.Moin).Include(m => m.Moin.K).Include(t => t.Taf).
                FirstOrDefault();
            try
            {
                empgp em = new empgp();
                em.Mid = model.Mid;
                em.Title = model.Title;
                em.Moinid = model.Moinid;
                //em.Pid = model.Pid;
                em.MoinCode = model.Moin.Codemoin;
                if (model.Taf != null)
                {
                    em.Tafcode = model.Taf.Codetaf;
                }
                if (model.Moin.Kid != null)
                {
                    em.Kolcode = model.Moin.K.Codekol.ToString();
                }

                em.Tafid = model.Tafid;
                var moinV = _context.Mointb.Where(p => p.Mid == model.Moinid).Join(_context.Koltb, p => p.Kid, k => k.Mid, (p, k) => new { Mymoin = p, Mykol = k }).FirstOrDefault();
                em.Moin = moinV.Mymoin.Namemoin + " - " + moinV.Mykol.Namekol;
                em.Taf = _context.Submointb.Where(p => p.Mid == model.Tafid).Select(p => p.Nametaf).FirstOrDefault();
                //var MYPID = _context.Persontb.Select(x => new { pid = x.Mid, pname = x.Namec + " " + x.Family });
                //em.P= MYPID.Where(p => p.pid == model.Pid).Select(p => p.pname).FirstOrDefault();
                return JsonConvert.SerializeObject(em);
            }
            catch
            {
                empgp em = new empgp();
                em.Mid = model.Mid;
                em.Title = model.Title;
                return JsonConvert.SerializeObject(em);
            }
        }

        private class empgp
        {
            public int Mid { get; set; }
            public int? Moinid { get; set; }
            public string Moin { get; set; }
            public string MoinCode { get; set; }
            public int? Tafid { get; set; }
            public string Tafcode { get; set; }
            public string Kolcode { get; set; }
            public string Taf { get; set; }
            //public int? Pid { get; set; }
            //public string P { get; set; }
            public string Title { get; set; }
        }

        // GET: Banktbs/Details/5
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

            var banktb = await _context.Banktb
                .Include(b => b.Moin)
                .Include(b => b.Taf)
                .FirstOrDefaultAsync(m => m.Mid == id);
            if (banktb == null)
            {
                return NotFound();
            }

            return PartialView(banktb);
        }

        // GET: Banktbs/Create
        public IActionResult Create()
        {
            ViewData["Moinid"] = new SelectList(_context.Mointb, "Mid", "Namemoin");
            ViewData["Tafid"] = new SelectList(_context.Submointb, "Mid", "Nametaf");
            return PartialView();
        }

        // POST: Banktbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Banktb banktb)
        {

                try
                {
                    _context.Add(banktb);
                    await _context.SaveChangesAsync();
                    return Ok(new { msg = "", err = "" });
                }
                catch (Exception err)
                {
                    return Ok(new { msg = "خطا در ذخیره ", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
                }
        }

        // GET: Banktbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var banktb = await _context.Banktb.Where(x => x.Mid == id)
                    .Include(x=>x.Moin).Include(x=>x.Taf).Include(x=>x.Moin.K).FirstOrDefaultAsync();
                if (banktb == null)
                {
                    return NotFound();
                }
                ViewData["codec"] = (banktb.Moin != null ? banktb.Moin.K.Codekol + " - " + banktb.Moin.Codemoin
                    + (banktb.Taf != null ? " - " + banktb.Taf.Codetaf : "") : "");
                ViewData["namec"] = (banktb.Moin != null ? banktb.Moin.K.Namekol + " - " + banktb.Moin.Namemoin
                    + (banktb.Taf != null ? " - " + banktb.Taf.Nametaf : "") : "");
                return PartialView(banktb);

            }
            catch (Exception err)
            {
                ViewData["err"] = "سیستم دچار مشکل شده است!" + err.Message;
                return PartialView();
            }
        }

        // POST: Banktbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Banktb banktb)
        {

                try
                {
                    _context.Update(banktb);
                    await _context.SaveChangesAsync();
                    return Ok(new { msg = "", err = "" });
                }
                catch (Exception err)
                {
                    return Ok(new { msg = "خطا در ذخیره", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
                }
        }

        // GET: Banktbs/Delete/5
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

                var banktb = await _context.Banktb
                    .Include(b => b.Moin)
                    .Include(b => b.Taf)
                    .FirstOrDefaultAsync(m => m.Mid == id);
                if (banktb == null)
                {
                    return NotFound();
                }
                return PartialView(banktb);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", "sandoghtb");
            }


        }

        // POST: Banktbs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var anbartb = await _context.Banktb.FindAsync(id);
                _context.Banktb.Remove(anbartb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در حذف", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }
        }

        private bool BanktbExists(int id)
        {
            return _context.Banktb.Any(e => e.Mid == id);
        }
    }
}
