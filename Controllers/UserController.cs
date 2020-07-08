using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RayaHesab.Models;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RayaHesab.Controllers
{
    public class UserController : Controller
    {
        private readonly malisContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public UserController(malisContext context , UserManager<IdentityUser> userManager, RoleManager<IdentityRole> RoleManager, SignInManager<IdentityUser> signInManager)
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

        private class listcheck
        {
            int Mid { get; set; }
            string Datec { get; set; }
            decimal Mablagh { get; set; }
            decimal Nocheck { get; set; }
        }
        public IActionResult Index(int alarm = 1, int shortcut = 1, int menuid = 0)
        {
            return View();
        }
        public async Task<IActionResult> Menu()
        {
            var q = await _context.Mastermenutb.Where(x=>x.Act == null).Include(x=>x.Menutb)
                .OrderBy(x=>x.Mastersort)
                .ToListAsync();
            ViewBag.mmenu = q;
            return View();
        }
        public async Task<IActionResult> Menudash1()
        {
            var q = await _context.Mastermenutb.Where(x=>x.Act == null).Include(x=>x.Menutb)
                .OrderBy(x=>x.Mastersort)
                .ToListAsync();
            ViewBag.mmenu = q;
            return View();
        }
        
        public IActionResult Hesabdary()
        {
            ViewData["PageT"] = "حسابداری";
            ViewBag.MID = 1;
            return View();
        }
        public IActionResult Dash1()
        {
            return View();
        }

        public IActionResult Setting()
        {
            ViewBag.MID = 4;
            ViewData["PageT"] = "تنظیمات";
            return View();
        }

        public IActionResult Khazaneh()
        {
            ViewData["PageT"] = "خزانه";
            ViewBag.MID = 2;
            return View();
        }

        public IActionResult BaseInfo()
        {
            ViewData["PageT"] = "اطلاعات پایه";
            ViewBag.MID = 5;
            return View();
        }

        public IActionResult ReportMali()
        {
            ViewData["PageT"] = "گزارشات مالی";
            ViewBag.MID = 6;
            return View();
        }

        public IActionResult Tolid()
        {
            ViewData["PageT"] = " تولید";
            ViewBag.MID = 6;
            return View();
        }

        public IActionResult reportanbar()
        {
            ViewData["PageT"] = "گزارشات خرید ، فروش و انبار";
            ViewBag.MID = 7;
            return View();
        }

        public IActionResult KharidForosh()
        {
            ViewData["PageT"] = "خرید/فروش/انبار";
            ViewBag.MID = 3;
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}