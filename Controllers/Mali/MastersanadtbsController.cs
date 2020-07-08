using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MD.PersianDateTime.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RayaHesab.Models;

namespace RayaHesab.Controllers
{
    public class MastersanadtbsController : Controller
    {
        private readonly malisContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public MastersanadtbsController(malisContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> RoleManager, SignInManager<IdentityUser> signInManager)
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



        // GET: Mastersanadtbs
        public async Task<IActionResult> Index()
        {
            ViewData["PageT"] = "حسابداری";
            try
            {
                var malisContext = await _context.Query<vmastersanad>().OrderByDescending(x => x.datec).ToListAsync();
                ViewData["list"] = malisContext;
                return View();
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "");
                return View();
            }
        }

        public class empsanad
        {
            public int Nosanad { get; set; }
            public string Datec { get; set; }
            public string Title { get; set; }
            public int Typec { get; set; }
            public int BedS { get; set; }
            public int BesS { get; set; }
        }

        // GET: Mastersanadtbs/Create
        public IActionResult Create()
        {
            try
            {
                int mdata = _context.Mastersanadtb.Max(x => x.Nosanad);
                ViewData["maxid"] = mdata + 1;
            }
            catch
            {

                ViewData["maxid"] = 1;
            }

            return PartialView();

        }

        // POST: Mastersanadtbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Mastersanadtb Gorohpersontb)
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

        // GET: Mastersanadtbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var mastersanadtb = await _context.Mastersanadtb.FindAsync(id);
                if (mastersanadtb == null)
                {
                    return NotFound();
                }
                return PartialView(mastersanadtb);

            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "");
                return PartialView();
            }
        }

        // POST: Mastersanadtbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Mastersanadtb mastersanadtb)
        {

            try
            {
                _context.Update(mastersanadtb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ذخیره", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }
        }

        // GET: Mastersanadtbs/Delete/5
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

                var mastersanadtb = await _context.Mastersanadtb
                    .FirstOrDefaultAsync(m => m.Nosanad == id);
                if (mastersanadtb == null)
                {
                    return NotFound();
                }
                return PartialView(mastersanadtb);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Mastersanadtbs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var mastersanadtb = await _context.Mastersanadtb.FindAsync(id);
                _context.Mastersanadtb.Remove(mastersanadtb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در حذف", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }
        }

        private bool MastersanadtbExists(int id)
        {
            return _context.Mastersanadtb.Any(e => e.Nosanad == id);
        }
    }
}
