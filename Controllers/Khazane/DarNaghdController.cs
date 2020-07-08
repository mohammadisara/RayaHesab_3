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
using Newtonsoft.Json;
using RayaHesab.Models;

namespace RayaHesab.Controllers.Khazane
{
    public class DarNaghdController : Controller
    {
        private readonly malisContext _context;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public DarNaghdController(malisContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> RoleManager, SignInManager<IdentityUser> signInManager)
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



        public async Task<IActionResult> List()
        {
            ViewBag.Type = 1;
            ViewBag.tittle = "دریافت نقد از حساب";
            try
            {
                    var malisContext = _context.Pardartb.Where(p => p.Typec == 3).Where(p => p.Pidbes == null)
                   .Include(p => p.MoinbedNavigation)
                   .Include(p => p.MoinbedNavigation.K)
                   .Include(p => p.MoinbesNavigation.K)
                   .Include(p => p.MoinbesNavigation)
                   .Include(p => p.PidbedNavigation)
                   .Include(p => p.PidbesNavigation).Include(p => p.TafbedNavigation)
                   .Include(p => p.TafbesNavigation)
                   .Include(p => p.IdanbarNavigation)
                   .Include(p => p.SanbedNavigation);
                ViewData["listk"] = await malisContext.ToListAsync();
                return View();
            }
            catch (Exception err)
            {
                TempData["err"] = "سیستم دچار مشکل شده است!" + err.Message;
                return View();
            }

        }

        public async Task<IActionResult> Listperson()
        {
            ViewBag.tittle = "دریافت نقد از شخص";
            try
            {
                var malisContext = _context.Pardartb.Where(p => p.Typec == 3).Where(p => p.Pidbes != null)
               .Include(p => p.MoinbedNavigation)
               .Include(p => p.MoinbedNavigation.K)
               .Include(p => p.MoinbesNavigation.K)
               .Include(p => p.MoinbesNavigation)
               .Include(p => p.PidbedNavigation)
               .Include(p => p.PidbesNavigation).Include(p => p.TafbedNavigation)
               .Include(p => p.TafbesNavigation)
               .Include(p => p.IdanbarNavigation)
               .Include(p => p.SanbedNavigation);
                ViewData["listk"] = await malisContext.ToListAsync();
                return View();
            }
            catch (Exception err)
            {
                TempData["err"] = "سیستم دچار مشکل شده است!" + err.Message;
                return View();
            }

        }
        public IActionResult Indexperson()
        {
            return View();
        }
        // GET: Pardartbs
        public IActionResult Index()
        {
            return View();
        }



        // GET: Pardartbs/Details/5
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

            var sanad = await _context.Sanadtb
                 .Include(p => p.Moin)
                 .Include(p => p.Moin.K)
                 .Include(p => p.Taf)
                 .Include(p => p.P)
                 .Include(p => p.IdanbarNavigation)
                 .Include(p => p.Pardar)
                 .Where(p => p.Pardarid == id).ToListAsync();
            if (sanad == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["sanad"] = sanad;
                return PartialView();
            }
        }

        // GET: Pardartbs/Create
        public IActionResult Create(int type)
        {
            var San = _context.Banktb.ToList();
            ViewData["San"] = new SelectList(San, "Mid", "Title");
            //var qw = _context.Usertb.Where(x => x.Mid == int.Parse(HttpContext.Session.GetString("PersonId"))).FirstOrDefault();
            //if (qw == null)
            //    return NotFound();
            var listanbar = _context.Anbartb.ToList();
            //if (qw.Idanbar != null)
            //    ViewData["listanbar"] = new SelectList(listanbar, "Mid", "Namec", qw.Idanbar);
            //else
                ViewData["listanbar"] = new SelectList(listanbar, "Mid", "Namec");
            ViewData["type"] = type;
            ViewBag.datec = MyClass.getpersiondate(DateTime.Now);
            return View();
        }

        // POST: Pardartbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Pardartb pardartb)
        {
            
            try
            {
                if (pardartb.Idanbar == 0)
                    pardartb.Idanbar = null;
                if (ModelState.IsValid)
                {
                    //pardartb.Datec = PersianDateTime.Today.ToShortDateString();

                    pardartb.Userid = await AuthenticateUser();
                    _context.Add(pardartb);
                    await _context.SaveChangesAsync();
                    return Ok(new { msg = "", err = "" });
                }
                else
                {
                    return Ok(new { msg = "ورود اطلاعات صحیح نمی باشد", err = "" });

                }

            }
            catch (Exception errmsg)
            {
                return Ok(new { msg = "خطا در ایجاد مجدد تلاش نمایید", err = errmsg.Message + (errmsg.InnerException != null ? " . " + errmsg.InnerException.Message : "" ) });
            }

        }

        // GET: Pardartbs/Edit/5
        public IActionResult Edit(int? id , int type )
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var pardartb =  _context.Pardartb.Where(x=>x.Mid == id).Include(x=>x.MoinbesNavigation)
                    .Include(x => x.MoinbesNavigation.K).Include(x=>x.TafbesNavigation).Include(x=>x.PidbesNavigation).FirstOrDefault();
                if (pardartb == null)
                {
                    return NotFound();
                }
                var San = _context.Banktb.ToList();
                ViewData["San"] = new SelectList(San, "Mid", "Title", pardartb.Sanbed);
                var lisanbar = _context.Anbartb.ToList();
                ViewData["listanbar"] = new SelectList(lisanbar, "Mid", "Namec" , pardartb.Idanbar);
                //ViewData["codehesab"] = pardartb.MoinbesNavigation.K.Codekol + " " + pardartb.MoinbesNavigation.Codemoin + (pardartb.TafbesNavigation != null ? " " + pardartb.TafbesNavigation.Codetaf : "");
                if (type == 1)
                {
                    ViewData["namehesab"] = pardartb.MoinbesNavigation.K.Namekol + " " + pardartb.MoinbesNavigation.Namemoin + (pardartb.TafbesNavigation != null ? " " + pardartb.TafbesNavigation.Nametaf : "");
                }
                else
                {
                    ViewData["namehesab"] = pardartb.PidbesNavigation.Namekamel ;

                }
                ViewData["type"] = type;
                return View(pardartb);
            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Pardartb pardartb)
        {
            if (pardartb.Idanbar == 0)
                pardartb.Idanbar = null;

                try
                {
                    _context.Update(pardartb);
                    await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
                }
                catch (Exception errmsg)
                {
                    return Ok(new { msg = "خطا در ویرایش مجدد تلاش نمایید", err = errmsg.Message + (errmsg.InnerException != null ? " . " + errmsg.InnerException.Message : "") });
                }

        }

        // POST: Pardartbs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var pardartb = await _context.Pardartb.FindAsync(id);
                _context.Pardartb.Remove(pardartb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception errmsg)
            {
                return Ok(new { msg = "خطا در حذف مجدد تلاش نمایید", err = errmsg.Message + (errmsg.InnerException != null ? " . " + errmsg.InnerException.Message : "") });
            }
        }

    }
}
