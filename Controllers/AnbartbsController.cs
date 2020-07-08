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

namespace RayaHesab.Controllers.KharidForosh
{
    public class AnbartbsController : Controller
    {
        private readonly malisContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AnbartbsController(malisContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> RoleManager, SignInManager<IdentityUser> signInManager)
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

        // GET: Anbartbs
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            try
            {
                ViewData["list"] = await _context.Anbartb.Include(b => b.Moin).Include(k => k.Moin.K).Include(b => b.Taf).ToListAsync();
                return View();
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "");
                return View();
            }
        }


        // GET: Anbartbs/Create
        public IActionResult Create()
        {

            return PartialView();
        }

        // POST: Anbartbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Anbartb anbartb)
        {
            try
            {
                _context.Add(anbartb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ذخیره ", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }

        }
        // GET: Anbartbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var anbartb = await _context.Anbartb.Where(x=>x.Mid == id).Include(x=>x.Moin.K).Include(x=>x.Moin).Include(x=>x.Taf).FirstOrDefaultAsync();
                if (anbartb == null)
                {
                    return NotFound();
                }
                ViewData["namec"] = anbartb.Moin.K.Namekol + " - " + anbartb.Moin.Namemoin + (anbartb.Taf != null ? " - "+ anbartb.Taf.Nametaf : "");
                ViewData["codec"] = anbartb.Moin.K.Codekol + " - " + anbartb.Moin.Codemoin + (anbartb.Taf != null ? " - "+anbartb.Taf.Codetaf : "");
                return PartialView(anbartb);
            }
            catch
            {
                return PartialView();
            }
        }

        // POST: Anbartbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Anbartb anbartb)
        {


            try
            {
                _context.Update(anbartb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ذخیره", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }
        }

        // GET: Anbartbs/Delete/5
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
            if (HttpContext.Session.GetString("PersonId") != null)
            {
                ViewData["Admin"] = HttpContext.Session.GetString("PersonName");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var anbartb = await _context.Anbartb
                    .FirstOrDefaultAsync(m => m.Mid == id);
                if (anbartb == null)
                {
                    return NotFound();
                }
                return PartialView(anbartb);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", "Anbartbs");
            }
        }

        // POST: Anbartbs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var anbartb = await _context.Anbartb.FindAsync(id);
                _context.Anbartb.Remove(anbartb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در حذف", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }
        }

        private bool AnbartbExists(int id)
        {
            return _context.Anbartb.Any(e => e.Mid == id);
        }
    }
}
