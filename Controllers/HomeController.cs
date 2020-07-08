using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RayaHesab.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RayaHesab.Controllers
{
    public class HomeController : Controller
    {
        private readonly malisContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public HomeController(malisContext context, IHostingEnvironment hostingEnvironment, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> RoleManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
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


public async Task<IActionResult> signout()
        {
            try
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                await _signInManager.SignOutAsync();

            }
            catch (Exception e)
            {

                string ww = e.Message;
            }
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
        public IActionResult Index(string err = "")
        {
            Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("fa")),
                                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                            );
            return RedirectToPage("/Account/Login", new { area = "Identity" });

        }



        [HttpPost]
        public IActionResult Login(Usertb user)
        {
            try
            {
                var account = _context.Usertb.Where(p => p.Namec == user.Namec && p.Pass == user.Pass).FirstOrDefault();
                if (account != null)
                {
                    return RedirectToAction("Index", "User");
                }
                ViewBag.err = "نام کاربری و کلمه عبور اشتباه می باشد";
                return View();
                //return RedirectToAction(nameof(Login), new { _err = "نام کاربری و کلمه عبود اشتباه می باشد" });
            }
            catch (Exception err)
            {
                ViewBag.err = err.Message;
                return View();
                //return RedirectToAction(nameof(Login), new { _err = err.Message });
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }
        //[HttpGet("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 100;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
