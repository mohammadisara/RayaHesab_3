using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RayaHesab.Models;

namespace RayaHesab.Controllers
{
    public class GorohpersontbsController : Controller
    {
        private readonly malisContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public GorohpersontbsController(malisContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> RoleManager, SignInManager<IdentityUser> signInManager)
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


        // GET: Gorohpersontbs
        public async Task<IActionResult> List()
        {
            try
            {
                var malisContext = _context.Gorohpersontb.Include(g => g.Moin).Include(k => k.Moin.K);
                ViewData["list"] = await malisContext.ToListAsync();
                return View();
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "");
                return View();
            }
        }
        public IActionResult Index()
        {
                return View();
        }

        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Gorohpersontb Gorohpersontb)
        {
            try
            {
                _context.Add(Gorohpersontb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ذخیره ", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }
        }

        [HttpPost]
        public string getrow(string mid)
        {
            //var model = _context.Gorohpersontb.Where(p => p.Mid == Convert.ToInt32(mid)).Include(m => m.Moin).Include(t => t.Taf).FirstOrDefault();
            var model = _context.Gorohpersontb.Where(p => p.Mid == Convert.ToInt32(mid)).
                Include(m => m.Moin).Include(k => k.Moin.K).FirstOrDefault();
            empgp em = new empgp();
            em.Mid = model.Mid;
            em.Title = model.Title;
            em.Moinid = model.Moinid;
            //em.Pid = model.Pid;
            em.MoinCode = model.Moin.Codemoin;
            em.kid = model.Moin.K.Mid;
            em.codekol = model.Moin.K.Codekol;
            em.namekol = model.Moin.K.Namekol;
            //em.Tafcode = model.Taf.Codetaf;
            //em.Tafid = model.Tafid;
            em.Moin = _context.Mointb.Where(p => p.Mid == model.Moinid).Select(p => p.Namemoin).FirstOrDefault();
            //em.Taf= _context.Submointb.Where(p => p.Mid == model.Tafid).Select(p => p.Nametaf).FirstOrDefault();
            //var MYPID = _context.Persontb.Select(x => new { pid = x.Mid, pname = x.Namec + " " + x.Family });
            //em.P= MYPID.Where(p => p.pid == model.Pid).Select(p => p.pname).FirstOrDefault();


            return JsonConvert.SerializeObject(em);
        }

        private class empgp
        {
            public int Mid { get; set; }
            public int? Moinid { get; set; }
            public string Moin { get; set; }
            public string MoinCode { get; set; }
            public int? kid { get; set; }
            public string codekol { get; set; }
            public string namekol { get; set; }

            //public int? Pid { get; set; }
            public string P { get; set; }
            public string Title { get; set; }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var anbartb = await _context.Gorohpersontb.Where(x => x.Mid == id).Include(x => x.Moin.K).Include(x => x.Moin).FirstOrDefaultAsync();
                if (anbartb == null)
                {
                    return NotFound();
                }
                ViewData["namec"] = anbartb.Moin.K.Namekol + " - " + anbartb.Moin.Namemoin;
                ViewData["codec"] = anbartb.Moin.K.Codekol + " - " + anbartb.Moin.Codemoin;
                return PartialView(anbartb);
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "");
                return PartialView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Gorohpersontb Gorohpersontb)
        {

            try
            {
                _context.Update(Gorohpersontb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ذخیره", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }
        }

        // GET: Gorohpersontbs/Delete/5
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
                if (id == null)
                {
                    return NotFound();
                }

                var Gorohpersontb = await _context.Gorohpersontb
                    .Include(g => g.Moin)
                    //.Include(g => g.Taf)
                    .FirstOrDefaultAsync(m => m.Mid == id);
                if (Gorohpersontb == null)
                {
                    return NotFound();
                }
                return PartialView(Gorohpersontb);
            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Gorohpersontbs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var anbartb = await _context.Gorohpersontb.FindAsync(id);
                _context.Gorohpersontb.Remove(anbartb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در حذف", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }
        }

        private bool GorohpersontbExists(int id)
        {
            return _context.Gorohpersontb.Any(e => e.Mid == id);
        }
    }
}
