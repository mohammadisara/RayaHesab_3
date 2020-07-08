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
    public class PardartbsController : Controller
    {
        private readonly malisContext _context;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public PardartbsController(malisContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> RoleManager, SignInManager<IdentityUser> signInManager)
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





        public IActionResult Index()
        {
            ViewData["PageT"] = "خزانه";
            return View();
        }
        public IActionResult Indexnoghre()
        {
            ViewData["PageT"] = "خزانه";
            return View();
        }

        public class empasnad
        {
            public int Mid { get; set; }

            public String codekolbed;
            public String Codemoinbed { get; set; }
            public String Codetafbed { get; set; }
            public String Pidbed { get; set; }
            public String Namekolbed { get; set; }
            public String Namemoinbed { get; set; }
            public String Nametafbed { get; set; }
            public String Namekamelbed { get; set; }
            public String Codekolbes { get; set; }
            public String Codemoinbes { get; set; }
            public String Codetafbes { get; set; }
            public String Pidbes { get; set; }
            public String Namekolbes { get; set; }
            public String Namemoinbes { get; set; }
            public String Nametafbes { get; set; }
            public String Namekamelbes { get; set; }
            public string Hesabdar { get; set; }
            public string Bankdar { get; set; }
            //public string Codetafv { get; set; }
            //public string Pidv   { get; set; }
            //public string Namekolv   { get; set; }
            //public string Namemoinv   { get; set; }
            //public string Nametafv   { get; set; }
            //public string Namekamelv   { get; set; }
            public String Datec { get; set; }
            public String Note { get; set; }
            public String Nocheckq { get; set; }
            public String Datecheck { get; set; }
            public String Noghabz { get; set; }
            public int? Typec { get; set; }
            public decimal? Mablagh { get; set; }
            public String Nameanbar { get; set; }
            public int? Statecheckq { get; set; }
        }

        public async Task<IActionResult> Indexpardarfact(int idfact)
        {
            try
            {
                var malisContext = await _context.Pardartb.Where(p => p.Typec == 0 && p.Idfact == idfact)
                    .Include(p => p.MoinbedNavigation)
                    .Include(p => p.MoinbesNavigation).Include(p => p.PidbedNavigation)
                    .Include(p => p.PidbesNavigation).Include(p => p.TafbedNavigation)
                    .Include(p => p.TafbesNavigation)
                    .Include(p => p.MoinbedNavigation.K)
                    .Include(p => p.IdanbarNavigation)
                    .Include(p => p.MoinbesNavigation.K)
                    .Include(p => p.IdanbarNavigation)
                    .Include(p => p.IdfactNavigation)
                    .Include(p => p.IdmoinvNavigation)
                    .Include(p => p.IdmoinvNavigation.K)
                    .Include(p => p.IdtafvNavigation)
                    .Include(p => p.PidvNavigation)
                    .OrderByDescending(x => x.Datec)
                    .ToListAsync();
                List<empasnad> emp = new List<empasnad>();
                foreach (var item in malisContext)
                {
                    emp.Add(new empasnad
                    {
                        Datec = item.Datec,
                        Bankdar = item.Bankdar,
                        Hesabdar = item.Hesabdar,
                        codekolbed = (item.MoinbedNavigation != null ? item.MoinbedNavigation.K.Codekol : ""),
                        Codemoinbed = (item.MoinbedNavigation != null ? item.MoinbedNavigation.Codemoin : ""),
                        Codetafbed = (item.TafbedNavigation != null ? item.TafbedNavigation.Codetaf : ""),
                        Namekolbed = (item.MoinbedNavigation != null ? item.MoinbedNavigation.K.Namekol : ""),
                        Namemoinbed = (item.MoinbedNavigation != null ? item.MoinbedNavigation.Namemoin : ""),
                        Nametafbed = (item.TafbedNavigation != null ? item.TafbedNavigation.Nametaf : ""),
                        Codekolbes = (item.MoinbesNavigation != null ? item.MoinbesNavigation.K.Codekol : ""),
                        Codemoinbes = (item.MoinbesNavigation != null ? item.MoinbesNavigation.Codemoin : ""),
                        Codetafbes = (item.TafbesNavigation != null ? item.TafbesNavigation.Codetaf : ""),
                        Namekolbes = (item.MoinbesNavigation != null ? item.MoinbesNavigation.K.Namekol : ""),
                        Namemoinbes = (item.MoinbesNavigation != null ? item.MoinbesNavigation.Namemoin : ""),
                        Nametafbes = (item.TafbesNavigation != null ? item.TafbesNavigation.Nametaf : ""),
                        Datecheck = item.Datecheck,
                        Mablagh = item.Mablagh,
                        Mid = item.Mid,
                        Nameanbar = (item.IdanbarNavigation != null ? item.IdanbarNavigation.Namec : ""),
                        Namekamelbed = (item.PidbedNavigation != null ? item.PidbedNavigation.Namekamel : ""),
                        Namekamelbes = (item.PidbesNavigation != null ? item.PidbesNavigation.Namekamel : ""),
                        Nocheckq = item.Nocheckq,
                        Noghabz = item.Noghabz,
                        Note = item.Note,
                        Statecheckq = item.Statecheckq,
                        Typec = item.Typec
                    });
                }
                ViewData["listk"] = emp;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.err = e.Message;
                return View();
            }
        }

        public async Task<IActionResult> List()
        {
            ViewData["PageT"] = "خزانه";

            try
            {
                var malisContext = await _context.Pardartb.Where(p => p.Typec == 0)
                    .Include(p => p.MoinbedNavigation)
                    .Include(p => p.MoinbesNavigation).Include(p => p.PidbedNavigation)
                    .Include(p => p.PidbesNavigation).Include(p => p.TafbedNavigation)
                    .Include(p => p.TafbesNavigation)
                    .Include(p => p.MoinbedNavigation.K)
                    .Include(p => p.IdanbarNavigation)
                    .Include(p => p.MoinbesNavigation.K)
                    .Include(p => p.IdanbarNavigation)
                    .Include(p => p.IdfactNavigation)
                    .Include(p => p.IdmoinvNavigation)
                    .Include(p => p.IdmoinvNavigation.K)
                    .Include(p => p.IdtafvNavigation)
                    .Include(p => p.PidvNavigation)
                    .OrderByDescending(x => x.Datec)
                    .ToListAsync();
                List<empasnad> emp = new List<empasnad>();
                foreach (var item in malisContext)
                {
                    emp.Add(new empasnad
                    {
                        Datec = item.Datec,
                        Bankdar = item.Bankdar,
                        Hesabdar = item.Hesabdar,
                        codekolbed = (item.MoinbedNavigation != null ? item.MoinbedNavigation.K.Codekol : ""),
                        Codemoinbed = (item.MoinbedNavigation != null ? item.MoinbedNavigation.Codemoin : ""),
                        Codetafbed = (item.TafbedNavigation != null ? item.TafbedNavigation.Codetaf : ""),
                        Namekolbed = (item.MoinbedNavigation != null ? item.MoinbedNavigation.K.Namekol : ""),
                        Namemoinbed = (item.MoinbedNavigation != null ? item.MoinbedNavigation.Namemoin : ""),
                        Nametafbed = (item.TafbedNavigation != null ? item.TafbedNavigation.Nametaf : ""),
                        Codekolbes = (item.MoinbesNavigation != null ? item.MoinbesNavigation.K.Codekol : ""),
                        Codemoinbes = (item.MoinbesNavigation != null ? item.MoinbesNavigation.Codemoin : ""),
                        Codetafbes = (item.TafbesNavigation != null ? item.TafbesNavigation.Codetaf : ""),
                        Namekolbes = (item.MoinbesNavigation != null ? item.MoinbesNavigation.K.Namekol : ""),
                        Namemoinbes = (item.MoinbesNavigation != null ? item.MoinbesNavigation.Namemoin : ""),
                        Nametafbes = (item.TafbesNavigation != null ? item.TafbesNavigation.Nametaf : ""),
                        Datecheck = item.Datecheck,
                        Mablagh = item.Mablagh,
                        Mid = item.Mid,
                        Nameanbar = (item.IdanbarNavigation != null ? item.IdanbarNavigation.Namec : ""),
                        Namekamelbed = (item.PidbedNavigation != null ? item.PidbedNavigation.Namekamel : ""),
                        Namekamelbes = (item.PidbesNavigation != null ? item.PidbesNavigation.Namekamel : ""),
                        Nocheckq = item.Nocheckq,
                        Noghabz = item.Noghabz,
                        Note = item.Note,
                        Statecheckq = item.Statecheckq,
                        Typec = item.Typec
                    });
                }
                ViewData["listk"] = emp;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.err = e.Message;
                return View();
            }
        }

        public async Task<IActionResult> Listnoghre()
        {
            ViewData["PageT"] = "خزانه";

            try
            {
                var malisContext = await _context.Pardartb.Where(p => p.Typec == 5)
                    .Include(p => p.MoinbedNavigation)
                    .Include(p => p.MoinbesNavigation).Include(p => p.PidbedNavigation)
                    .Include(p => p.PidbesNavigation).Include(p => p.TafbedNavigation)
                    .Include(p => p.TafbesNavigation)
                    .Include(p => p.MoinbedNavigation.K)
                    .Include(p => p.IdanbarNavigation)
                    .Include(p => p.MoinbesNavigation.K)
                    .Include(p => p.IdanbarNavigation)
                    .Include(p => p.IdfactNavigation)
                    .Include(p => p.IdmoinvNavigation)
                    .Include(p => p.IdmoinvNavigation.K)
                    .Include(p => p.IdtafvNavigation)
                    .Include(p => p.PidvNavigation)
                    .OrderByDescending(x => x.Datec)
                    .ToListAsync();
                List<empasnad> emp = new List<empasnad>();
                foreach (var item in malisContext)
                {
                    emp.Add(new empasnad
                    {
                        Datec = item.Datec,
                        Bankdar = item.Bankdar,
                        Hesabdar = item.Hesabdar,
                        codekolbed = (item.MoinbedNavigation != null ? item.MoinbedNavigation.K.Codekol : ""),
                        Codemoinbed = (item.MoinbedNavigation != null ? item.MoinbedNavigation.Codemoin : ""),
                        Codetafbed = (item.TafbedNavigation != null ? item.TafbedNavigation.Codetaf : ""),
                        Namekolbed = (item.MoinbedNavigation != null ? item.MoinbedNavigation.K.Namekol : ""),
                        Namemoinbed = (item.MoinbedNavigation != null ? item.MoinbedNavigation.Namemoin : ""),
                        Nametafbed = (item.TafbedNavigation != null ? item.TafbedNavigation.Nametaf : ""),
                        Codekolbes = (item.MoinbesNavigation != null ? item.MoinbesNavigation.K.Codekol : ""),
                        Codemoinbes = (item.MoinbesNavigation != null ? item.MoinbesNavigation.Codemoin : ""),
                        Codetafbes = (item.TafbesNavigation != null ? item.TafbesNavigation.Codetaf : ""),
                        Namekolbes = (item.MoinbesNavigation != null ? item.MoinbesNavigation.K.Namekol : ""),
                        Namemoinbes = (item.MoinbesNavigation != null ? item.MoinbesNavigation.Namemoin : ""),
                        Nametafbes = (item.TafbesNavigation != null ? item.TafbesNavigation.Nametaf : ""),
                        Datecheck = item.Datecheck,
                        Mablagh = item.Mablagh,
                        Mid = item.Mid,
                        Nameanbar = (item.IdanbarNavigation != null ? item.IdanbarNavigation.Namec : ""),
                        Namekamelbed = (item.PidbedNavigation != null ? item.PidbedNavigation.Namekamel : ""),
                        Namekamelbes = (item.PidbesNavigation != null ? item.PidbesNavigation.Namekamel : ""),
                        Nocheckq = item.Nocheckq,
                        Noghabz = item.Noghabz,
                        Note = item.Note,
                        Statecheckq = item.Statecheckq,
                        Typec = item.Typec
                    });
                }
                ViewData["listk"] = emp;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.err = e.Message;
                return View();
            }
        }

        // GET: Pardartbs
        public async Task<IActionResult> Index1()
        {
            ViewData["PageT"] = "خزانه";
            if (TempData["errormsg1"] != null)
            {
                ViewBag.errormsg = TempData["errormsg1"].ToString();
            }
            else
            {
                ViewBag.errormsg = "";
            }
            ViewBag.MID = 2;
            try
            {
                var malisContext = _context.Pardartb.Where(p => p.Typec == 0)
                    .Include(p => p.MoinbedNavigation)
                    .Include(p => p.MoinbesNavigation).Include(p => p.PidbedNavigation)
                    .Include(p => p.PidbesNavigation).Include(p => p.TafbedNavigation)
                    .Include(p => p.TafbesNavigation)
                    .Include(p => p.MoinbedNavigation.K)
                    .Include(p => p.IdanbarNavigation)
                    .Include(p => p.MoinbesNavigation.K)
                    .Include(p => p.IdanbarNavigation);
                var lisanbar = await _context.Anbartb.ToListAsync();
                ViewData["listanbar"] = new SelectList(lisanbar, "Mid", "Namec");
                return View(await malisContext.ToListAsync());
            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public string getrow(string mid)
        {
            var model = _context.Pardartb.Where(p => p.Mid == Convert.ToInt32(mid))
                    .Include(p => p.MoinbedNavigation)
                    .Include(p => p.MoinbesNavigation).Include(p => p.PidbedNavigation)
                    .Include(p => p.PidbesNavigation).Include(p => p.TafbedNavigation)
                    .Include(p => p.TafbesNavigation)
                    .Include(p => p.MoinbedNavigation.K)
                    .Include(p => p.IdanbarNavigation)
                    .Include(p => p.MoinbesNavigation.K)
                    .Include(p => p.IdanbarNavigation).FirstOrDefault();

            empgp em = new empgp();
            em.Mid = model.Mid;
            em.Noghabz = model.Noghabz;

            em.Moinbedid = model.Moinbed;
            em.Pbedid = model.Pidbed;
            em.Tafbedid = model.Tafbed;
            if (model.MoinbedNavigation != null)
            {
                em.MoinbedCode = model.MoinbedNavigation.K.Codekol + " - " + model.MoinbedNavigation.Codemoin;
                em.Moinbed = model.MoinbedNavigation.Namemoin + " - " + model.MoinbedNavigation.Codemoin;
                if (model.Tafbed != null)
                {
                    em.Tafbedcode = model.TafbedNavigation.Codetaf;
                    em.Tafbed = model.TafbedNavigation.Nametaf;
                }
            }
            if (model.PidbedNavigation != null)
            {
                em.Pbed = model.PidbedNavigation.Namekamel;
            }

            em.Moinbesid = model.Moinbes;
            em.Pbesid = model.Pidbes;
            em.Tafbesid = model.Tafbes;
            if (model.MoinbesNavigation != null)
            {
                em.MoinbesCode = model.MoinbesNavigation.K.Codekol + " - " + model.MoinbesNavigation.Codemoin;
                em.Moinbes = model.MoinbesNavigation.Namemoin + " - " + model.MoinbesNavigation.Codemoin;
                if (model.Tafbes != null)
                {
                    em.Tafbescode = model.TafbesNavigation.Codetaf;
                    em.Tafbes = model.TafbesNavigation.Nametaf;
                }
            }
            if (model.PidbesNavigation != null)
            {
                em.Pbes = model.PidbesNavigation.Namekamel;
            }
            em.Idanbar = model.Idanbar;


            em.Datec = model.Datec;
            em.MablaghE = model.Mablagh.ToString();
            em.NoteE = model.Note;
            em.Typec = model.Typec;
            em.UserId = model.Userid;


            return JsonConvert.SerializeObject(em);
        }

        private class empgp
        {
            public int Mid { get; set; }
            public String UserId { get; set; }
            public string NoteE { get; set; }
            public string MablaghE { get; set; }
            public string Noghabz { get; set; }
            public string Datec { get; set; }
            public int? Typec { get; set; }
            public int? Idanbar { get; set; }
            public int? Moinbesid { get; set; }
            public string Moinbes { get; set; }
            public string MoinbesCode { get; set; }
            public int? Tafbesid { get; set; }
            public string Tafbescode { get; set; }
            public string Tafbes { get; set; }
            public int? Pbesid { get; set; }
            public string Pbes { get; set; }
            public int? Moinbedid { get; set; }
            public string Moinbed { get; set; }
            public string MoinbedCode { get; set; }
            public int? Tafbedid { get; set; }
            public string Tafbedcode { get; set; }
            public string Tafbed { get; set; }
            public int? Pbedid { get; set; }
            public string Pbed { get; set; }
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
                 //.Include(p => p.IdanbarNavigation)
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


        public IActionResult Createnoghre()
        {
            //var qw = _context.Usertb.Where(x => x.Mid == int.Parse(HttpContext.Session.GetString("PersonId"))).FirstOrDefault();
            //if (qw == null)
            //    return NotFound();
            var listanbar = _context.Anbartb.ToList();
            //if (qw.Idanbar != null)
            //    ViewData["listanbar"] = new SelectList(listanbar, "Mid", "Namec", qw.Idanbar);
            //else
                ViewData["listanbar"] = new SelectList(listanbar, "Mid", "Namec");
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Createnoghre(Pardartb pardartb)
        {
            pardartb.Userid = await AuthenticateUser();
            try
            {
                if (pardartb.Idanbar == 0)
                    return Ok(new { msg = "ورود فروشگاه ", err = "فروشگاه را انتخاب نمایید" });
                //var h = _context.Defcodetb.FirstOrDefault();
                //if (h == null)
                //    return NotFound();
                _context.Add(pardartb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception errmsg)
            {
                return Ok(new { msg = errmsg.Message, err = (errmsg.InnerException != null ? errmsg.InnerException.Message : "") });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Editnoghre(int pid, int pids, int thesab, int id, Pardartb pardartb)
        {
            pardartb.Userid = await AuthenticateUser();
            if (pardartb.Idanbar == 0)
                return Ok(new { msg = "ورود فروشگاه ", err = "فروشگاه را انتخاب نمایید" });
            try
            {

                //var h = _context.Defcodetb.FirstOrDefault();
                //if (h == null)
                //    return NotFound();
                //if (thesab == 0)
                //{
                //    pardartb.Pidbed = pid;
                //    pardartb.Pidbes = pids;
                //    pardartb.Moinbed = null;
                //    pardartb.Tafbed = null;
                //    pardartb.Moinbes = null;
                //    pardartb.Tafbes = null;

                //}
                //else
                //{
                //    pardartb.Pidbes = pid;
                //    pardartb.Pidbed = pids;
                //    pardartb.Moinbes = null;
                //    pardartb.Tafbes = null;
                //    pardartb.Moinbed = null;
                //    pardartb.Tafbed = null;
                //}
                _context.Update(pardartb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception errmsg)
            {
                return Ok(new { msg = errmsg.Message, err = (errmsg.InnerException != null ? errmsg.InnerException.Message : "") });
            }

        }


        // GET: Pardartbs/Create
        public IActionResult Create()
        {
            //var qw = _context.Usertb.Where(x => x.Mid == int.Parse(HttpContext.Session.GetString("PersonId"))).FirstOrDefault();
            //if (qw == null)
            //    return NotFound();
            var listanbar = _context.Anbartb.ToList();
            //if (qw.Idanbar != null)
            //    ViewData["listanbar"] = new SelectList(listanbar, "Mid", "Namec", qw.Idanbar);
            //else
                ViewData["listanbar"] = new SelectList(listanbar, "Mid", "Namec");
            //var check = _context.Checkqtb.FromSql("select * from Checkqtb where nobarg not in (select isnull(Nocheckq , 0) from pardartb where typec = 2)").ToList();
            //ViewData["listcheck"] = new SelectList(check, "Nobarg", "Nobarg");
            ViewBag.datec = MyClass.getpersiondate(DateTime.Now);
            return PartialView();
        }

        // POST: Pardartbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Pardartb pardartb)
        {
            pardartb.Userid = await AuthenticateUser();
            try
            {
                if (pardartb.Idanbar == 0)
                    return Ok(new { msg = "ورود فروشگاه ", err = "فروشگاه را انتخاب نمایید" });
                _context.Add(pardartb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception errmsg)
            {
                return Ok(new { msg = errmsg.Message, err = (errmsg.InnerException != null ? errmsg.InnerException.Message : "") });
            }

        }

        // GET: Pardartbs/Edit/5
        public async Task<IActionResult> Editnoghre(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var pardartb = await _context.Pardartb.FindAsync(id);
                if (pardartb == null)
                {
                    return NotFound();
                }
                if (pardartb.Pidbed != null)
                {
                    ViewData["pid"] = pardartb.Pidbed;
                    ViewData["pname"] = _context.Persontb.Where(x => x.Mid == pardartb.Pidbed).FirstOrDefault().Namekamel;
                }
                if (pardartb.Pidbes != null)
                {
                    ViewData["pidbes"] = pardartb.Pidbes;
                    ViewData["pnamebes"] = _context.Persontb.Where(x => x.Mid == pardartb.Pidbes).FirstOrDefault().Namekamel;
                }
                var listanbar = _context.Anbartb.ToList();
                ViewData["listanbar"] = new SelectList(listanbar, "Mid", "Namec", pardartb.Idanbar);
                return PartialView(pardartb);
            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return PartialView();
            }

        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var pardartb = await _context.Pardartb.FindAsync(id);
                if (pardartb == null)
                {
                    return NotFound();
                }
                ViewData["Moinbed"] = _context.Mointb.Where(p => p.Mid == pardartb.Moinbed).Select(p => p.Namemoin).FirstOrDefault();
                var MYPID = _context.Persontb.Select(x => new { pid = x.Mid, pname = x.Namec + " " + x.Family });
                ViewData["Pidbed"] = MYPID.Where(p => p.pid == pardartb.Pidbed).Select(p => p.pname).FirstOrDefault();
                ViewData["Tafbed"] = _context.Submointb.Where(p => p.Mid == pardartb.Tafbed).Select(p => p.Nametaf).FirstOrDefault();
                ViewData["Moinbes"] = _context.Mointb.Where(p => p.Mid == pardartb.Moinbes).Select(p => p.Namemoin).FirstOrDefault();
                ViewData["Pidbes"] = MYPID.Where(p => p.pid == pardartb.Pidbes).Select(p => p.pname).FirstOrDefault();
                ViewData["Tafbes"] = _context.Submointb.Where(p => p.Mid == pardartb.Tafbes).Select(p => p.Nametaf).FirstOrDefault();
                var listanbar = _context.Anbartb.ToList();
                ViewData["listanbar"] = new SelectList(listanbar, "Mid", "Namec", pardartb.Idanbar);
                return PartialView(pardartb);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return PartialView();
            }

        }
        // POST: Pardartbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Pardartb pardartb)
        {
            pardartb.Userid = await AuthenticateUser();
            if (pardartb.Idanbar == 0)
                return Ok(new { msg = "ورود فروشگاه ", err = "فروشگاه را انتخاب نمایید" });
            try
            {
                _context.Update(pardartb);
                await _context.SaveChangesAsync();
                return Ok(new { msg = "", err = "" });
            }
            catch (Exception errmsg)
            {
                return Ok(new { msg = errmsg.Message, err = (errmsg.InnerException != null ? errmsg.InnerException.Message : "") });
            }

        }

        // GET: Pardartbs/Delete/5
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

                var pardartb = await _context.Pardartb
                    .Include(p => p.MoinbedNavigation)
                    .Include(p => p.MoinbesNavigation)
                    .Include(p => p.PidbedNavigation)
                    .Include(p => p.PidbesNavigation)
                    .Include(p => p.TafbedNavigation)
                    .Include(p => p.TafbesNavigation)
                    .FirstOrDefaultAsync(m => m.Mid == id);
                if (pardartb == null)
                {
                    return NotFound();
                }

                return PartialView(pardartb);

            }
            catch
            {
                TempData["errormsg1"] = "سیستم دچار مشکل شده است!";
                return RedirectToAction("Index", "sandoghtb");
            }

        }

        // POST: Pardartbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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
                var pardartb = await _context.Pardartb.FindAsync(id);
                _context.Pardartb.Remove(pardartb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["errormsg1"] = "امکان حذف وجود ندارد!";
                return RedirectToAction("Index", "sandoghtb");
            }
        }

        private bool PardartbExists(int id)
        {
            return _context.Pardartb.Any(e => e.Mid == id);
        }
    }
}
