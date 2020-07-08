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
    public class PersontbsController : Controller
    {
        private readonly malisContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public PersontbsController(malisContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> RoleManager, SignInManager<IdentityUser> signInManager)
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


        // GET: Persontbs
        private class mlist
        {
            public string namec { get; set; }
            public string mobile { get; set; }
            public int? jensiat { get; set; }
        }
        public async Task<IActionResult> StatePerson()
        {
            var p = await _context.Persontb.Select(x => new { x.Namekamel  , x.Mid }).ToListAsync();
            ViewData["listperson"] = new SelectList(p, "Mid", "Namekamel");
            ViewData["listanbar"] = new SelectList(_context.Anbartb.FromSql("select * from anbartb where mid in  (select isnull(p.idanbar , 0)  from pertb p where p.userid = {0} )", int.Parse(HttpContext.Session.GetString("PersonId"))).ToList(), "Mid", "Namec");
            return View();
        }
        public async Task<IActionResult> liststate(int pid, int anbar = 0)
        {
            try
            {
                var q = await _context.Query<factpersonrepview>().Where(x => x.idperson == pid || x.personkharid == pid).ToListAsync();
                if (anbar > 0)
                {
                    q = q.Where(x => x.idanbar == anbar).ToList();
                }
                ViewData["listdkh"] = q.Where(x => x.typec == 4).ToList();
                ViewData["listkh"] = q.Where(x => x.typec == 0).ToList();
                ViewData["listpfactf"] = q.Where(x => x.typec == 2).ToList();
                ViewData["listsefareshf"] = q.Where(x => x.typec == 5).ToList();
                ViewData["listtahvilforosh"] = q.Where(x => x.typec == 3).ToList();
                ViewData["listforosh"] = q.Where(x => x.typec == 1).ToList();
                return View();

            }
            catch (Exception _err)
            {
                ViewData["err"] = _err.Message + " ** " +(_err.InnerException != null ? _err.InnerException.Message : "");
                return View();
            }
        }


        public IActionResult Factallperson()
        {
            try
            {
                var mindate = _context.Mastersanadtb.Min(x => x.Datec) ?? "";
                var maxdate = _context.Mastersanadtb.Max(x => x.Datec) ?? "";
                ViewData["mindate"] = mindate;
                ViewData["maxdate"] = maxdate;
            }
            catch 
            {
            }
            return View();
        }
        public IActionResult Listfactallperson(string mindate , string maxdate)
        {
            try
            {
                var q = _context.sp_soodperson.FromSql("exec dbo.sp_soodperson {0},{1}",  mindate, maxdate).ToList();
                ViewData["list"] = q;
                return View();
            }
            catch (Exception _err)
            {
                ViewData["err"] = _err.Message + " ** " + (_err.InnerException != null ? _err.InnerException.Message : "");
                return View();
            }
        }


        public IActionResult Index()
        {
                return View();
        }
        public async Task<IActionResult> List()
        {
            try
            {
                var malisContext = _context.Persontb.Include(x => x.G);
                ViewData["list"] = await malisContext.ToListAsync();
                return View();
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "");
                return View();
            }
        }

        // GET: Persontbs/Create
        public IActionResult Create()
        {

            try
            {
                ViewData["gid"] = new SelectList(_context.Gorohpersontb, "Mid", "Title");
                return PartialView();

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Persontbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Persontb persontb)
        {
            try
            {
                if (persontb.Gid == -1)
                {
                    return Ok(new { msg = "گروه را انتخاب کنید", err = "" });
                }
                _context.Add(persontb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ذخیره ", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }

        }

        // GET: Persontbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var persontb = await _context.Persontb.FindAsync(id);
                if (persontb == null)
                {
                    return NotFound();
                }
                ViewData["gid"] = new SelectList(_context.Gorohpersontb, "Mid", "Title", persontb.Gid);

                return PartialView(persontb);
            }
            catch
            {
                TempData["err"] = "سیستم دچار مشکل شده است!";
                return PartialView();

            }

        }

        // POST: Persontbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Persontb persontb)
        {
            if (id != persontb.Mid)
            {
                return NotFound();
            }
            try
            {
                if (persontb.Gid == -1)
                {
                    return Ok(new { msg = "گروه را انتخاب کنید", err = "" });
                }
                _context.Update(persontb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ذخیره", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }

        }

        // GET: Persontbs/Delete/5
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

                var persontb = await _context.Persontb
                    .FirstOrDefaultAsync(m => m.Mid == id);
                if (persontb == null)
                {
                    return NotFound();
                }

                return PartialView(persontb);
            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Persontbs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var anbartb = await _context.Persontb.FindAsync(id);
                _context.Persontb.Remove(anbartb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در حذف", err = err.Message + (err.InnerException != null ? " ** " + err.InnerException.Message : "") });
            }
        }
        [HttpPost]
        public string selsearch(string t)
        {
            var s = _context.Gorohpersontb.ToList();
            return JsonConvert.SerializeObject(s);
        }
        private bool PersontbExists(int id)
        {
            return _context.Persontb.Any(e => e.Mid == id);
        }
        [HttpPost]
        public string Deleteajax(int mid)
        {
            try
            {
                var m = _context.Persontb.Where(x => x.Mid == mid).FirstOrDefault();
                if (m == null)
                    return "ردیف موجود نمی باشد";
                _context.Remove(m);
                _context.SaveChanges();
                return "1";
            }
            catch (Exception e)
            {
                return e.InnerException.Message;
            }
        }
    }
}
