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
using RayaHesab.Models;

namespace RayaHesab.Controllers
{
    public class CodingController : Controller
    {
        private readonly malisContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CodingController(malisContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> RoleManager, SignInManager<IdentityUser> signInManager)
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
        // GET: Coding
        public IActionResult Index()
        {
            ViewData["PageT"] = "حسابداری";
            return View();
        }

        public async Task<IActionResult> Indextree()
        {
            ViewData["PageT"] = "حسابداری";
            try
            {
                ViewBag.Group = await _context.Gorohtb.ToListAsync();
                return View();
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message;
                return View();
            }
        }

        public IActionResult UpdateList(int id, string s)
        {
            try
            {
                if (s == "g")
                {
                    HttpContext.Session.SetString("Changed", "g");
                    HttpContext.Session.SetString("MyID", id.ToString());
                }
                if (s == "k")
                {
                    HttpContext.Session.SetString("Changed", "k");
                    HttpContext.Session.SetString("MyID", id.ToString());
                }
                if (s == "m")
                {
                    HttpContext.Session.SetString("Changed", "m");
                    HttpContext.Session.SetString("MyID", id.ToString());
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction(nameof(Index));
            }
        }
        
        private bool GorohtbExists(int id)
        {
            return _context.Gorohtb.Any(e => e.Mid == id);
        }
    }
}
