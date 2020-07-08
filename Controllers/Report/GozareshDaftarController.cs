using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RayaHesab.Models;

namespace RayaHesab.Controllers
{
    public class GozareshDaftarController : Controller
    {
        public readonly malisContext _context;

        public GozareshDaftarController(malisContext context)
        {
            _context = context;
        }

        // GET: Daftar

        public IActionResult Indexpersonallhesab()
        {
            ViewData["fdate"] = MyClass.getpersiondate(DateTime.Now);
            ViewData["tdate"] = MyClass.getpersiondate(DateTime.Now);
            var min = _context.Mastersanadtb.Min(x => x.Datec);
            if (min != null)
                ViewData["fdate"] = min.ToString();
            var max = _context.Mastersanadtb.Max(x => x.Datec);
            if (max != null)
                ViewData["tdate"] = max.ToString();
            ViewBag.Tittle = "گزارش حساب های اشخاص";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> listhesaballperson(string fdate, string tdate)
        {
            string con = _context.Database.GetDbConnection().ConnectionString;
            SqlConnection cn = new SqlConnection(con);
            cn.Open();
            SqlCommand cmd = new SqlCommand("exec dbo.sp_hesaballperson @fdate , @tdate", cn);
            cmd.Parameters.AddWithValue("@fdate", fdate);
            cmd.Parameters.AddWithValue("@tdate", tdate);
            SqlDataReader dr = cmd.ExecuteReader();
            List<object> obj1 = new List<object>();
            var columns = new List<string>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string[] str = new string[dr.FieldCount];
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        str[i] = string.Format("{0}", dr.GetValue(i));
                    }
                    obj1.Add(str);
                }
            }
            dr.Close();
            ViewData["res"] = obj1;
            ViewData["cols"] = _context.Mointb.FromSql("select * from mointb where mid in (select moinid from Gorohpersontb)").OrderBy(x => x.Mid.ToString());

            SqlCommand cmd1 = new SqlCommand("exec dbo.sp_sumtaraz @fdate , @tdate", cn);
            cmd1.Parameters.AddWithValue("@fdate", fdate);
            cmd1.Parameters.AddWithValue("@tdate", tdate);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            List<object> obj2 = new List<object>();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    string[] str = new string[dr1.FieldCount];
                    for (int i = 0; i < dr1.FieldCount; i++)
                    {
                        str[i] = string.Format("{0}", dr1.GetValue(i));
                    }
                    obj2.Add(str);
                }
            }
            dr1.Close();
            ViewData["gsum"] = obj2;
            return View();
            //return Ok(new { res = obj , cols = cols , err = err});
        }
        public class emphesab
        {
            public string hesab { get; set; }
            public string pname { get; set; }
            public int? pid { get; set; }
            public int? gid { get; set; }
            public decimal? mandeh { get; set; }
        }


        public IActionResult Indexpersontwo()
        {
            ViewData["listanbar"] = new SelectList(_context.Anbartb.FromSql("select * from anbartb where mid in "
        + " (select isnull(p.idanbar , 0)  from pertb p where p.userid = {0} )"
        , int.Parse(HttpContext.Session.GetString("PersonId"))).ToList(), "Mid", "Namec");
            ViewData["fdate"] = MyClass.getpersiondate(DateTime.Now);
            ViewData["tdate"] = MyClass.getpersiondate(DateTime.Now);
            var min = _context.Mastersanadtb.Min(x => x.Datec);
            if (min != null)
                ViewData["fdate"] = min.ToString();
            var max = _context.Mastersanadtb.Max(x => x.Datec);
            if (max != null)
                ViewData["tdate"] = max.ToString();
            ViewBag.MID = 6;
            ViewBag.Tittle = "گزارش گردش یک شخص";
            var pid = _context.Persontb.ToList();
            var kol = _context.Mointb.ToList();
            ViewData["Kol"] = new SelectList(kol, "Mid", "Namemoin");
            ViewData["pid"] = new SelectList(pid, "Mid", "Namekamel");
            return View();
        }

        public IActionResult daftarpersontwo(string fdate, string tdate, int idkol1, int idkol2, int anbar, int print , int pid , int gid = 0, int isfact = 0)
        {
            try
            {
                List<Daftarperson> obj = new List<Daftarperson>();
                var lst = _context.Daftarpersons.FromSql("exec sp_daftarpersontwohesab {0} , {1} , {2} , {3} , {4} , {5} , {6} , {7} ", MyClass.PersianToEnglish(fdate), MyClass.PersianToEnglish(tdate), anbar, pid , gid, int.Parse(HttpContext.Session.GetString("PersonId"))
                     , idkol1 , idkol2).ToList();
                if (lst.Count == 0)
                    return Ok(new { msg = "داده ای برای نمایش وجود ندارد", state = 0 });
                foreach (var item in lst)
                {
                    obj.Add(new Daftarperson
                    {
                        Mid = item.Mid,
                        Datec = (string.IsNullOrEmpty(item.Datec) ? "" : item.Datec),
                        Note = (string.IsNullOrEmpty(item.Note) ? "" : item.Note),
                        Nosanad = (item.Nosanad == null ? 0 : item.Nosanad),
                        Factid = (item.Factid == null ? 0 : item.Factid),
                        Bed = item.Bed,
                        Bes = item.Bes,
                        Bedn = item.Bedn,
                        Besn = item.Besn,
                        Userid = item.Userid,
                        Idanbar = item.Idanbar,
                        Nameanbar = item.Nameanbar,
                        Codehesab = item.Codehesab,
                        Namehesab = item.Namehesab
                    });
                }
                ViewData["list"] = obj;
                ViewData["gid"] = gid;
                ViewData["isfact"] = isfact;
                return View();
            }
            catch (Exception err)
            {
                return Ok(new { msg = err.Message.ToString(), state = 0 });
            }

        }

        public IActionResult Printdaftarpersontwo(string fdate, string tdate, int pid, int idkol1, int idkol2, string anbar, int gid = 0, int isfact = 0 )
        {
            try
            {
                List<Daftarperson> obj = new List<Daftarperson>();
                var lst = _context.Daftarpersons.FromSql("exec sp_daftarpersontwohesab {0} , {1} , {2} , {3} , {4} , {5} , {6} , {7} ", MyClass.PersianToEnglish(fdate), MyClass.PersianToEnglish(tdate), anbar, pid, gid, int.Parse(HttpContext.Session.GetString("PersonId"))
                     , idkol1, idkol2).ToList();
                if (lst.Count == 0)
                    return Ok(new { msg = "داده ای برای نمایش وجود ندارد", state = 0 });
                foreach (var item in lst)
                {
                    obj.Add(new Daftarperson
                    {
                        Mid = item.Mid,
                        Datec = (string.IsNullOrEmpty(item.Datec) ? "" : item.Datec),
                        Note = (string.IsNullOrEmpty(item.Note) ? "" : item.Note),
                        Nosanad = (item.Nosanad == null ? 0 : item.Nosanad),
                        Factid = (item.Factid == null ? 0 : item.Factid),
                        Bed = item.Bed,
                        Bes = item.Bes,
                        Bedn = item.Bedn,
                        Besn = item.Besn,
                        Userid = item.Userid,
                        Idanbar = item.Idanbar,
                        Nameanbar = item.Nameanbar,
                        Codehesab = item.Codehesab,
                        Namehesab = item.Namehesab
                    });
                }
                ViewData["list"] = obj;
                ViewData["isfact"] = isfact;
                ViewData["fdate"] = fdate;
                ViewData["tdate"] = tdate;
                var q = _context.Persontb.Where(x => x.Mid == pid).FirstOrDefault();
                ViewData["namec"] = q.Namekamel;
                return View();
            }
            catch (Exception err)
            {
                return Ok(new { msg = err.Message.ToString(), state = 0 });
            }
        }


        public IActionResult Indexperson()
        {
            ViewData["listanbar"] = new SelectList(_context.Anbartb.ToList(), "Mid", "Namec");
            ViewData["fdate"] = MyClass.getpersiondate(DateTime.Now);
            ViewData["tdate"] = MyClass.getpersiondate(DateTime.Now);
            var min = _context.Mastersanadtb.Min(x => x.Datec);
            if (min != null)
                ViewData["fdate"] = min.ToString();
            var max = _context.Mastersanadtb.Max(x => x.Datec);
            if (max != null)
                ViewData["tdate"] = max.ToString();
            ViewBag.MID = 6;
            ViewBag.Tittle = "گزارش گردش یک شخص";
            var kol = _context.Persontb.ToList();
            ViewData["Kol"] = new SelectList(kol, "Mid", "Namekamel");
            return View();
        }



        public IActionResult daftarperson(string fdate, string tdate, int idkol, int anbar, int print
            , int gid = 0, int isfact = 0)
        {
            try
            {
                List<Daftarperson> obj = new List<Daftarperson>();
                var lst = _context.Daftarpersons.FromSql("exec sp_daftarperson {0} , {1} , {2} , {3} , {4} , {5} ", MyClass.PersianToEnglish(fdate), MyClass.PersianToEnglish(tdate), anbar, idkol, gid, int.Parse(HttpContext.Session.GetString("PersonId"))).ToList();
                if (lst.Count == 0)
                    return Ok(new { msg = "داده ای برای نمایش وجود ندارد", state = 0 });
                foreach (var item in lst)
                {
                    obj.Add(new Daftarperson
                    {
                        Mid = item.Mid,
                        Datec = (string.IsNullOrEmpty(item.Datec) ? "" : item.Datec),
                        Note = (string.IsNullOrEmpty(item.Note) ? "" : item.Note),
                        Nosanad = (item.Nosanad == null ? 0 : item.Nosanad),
                        Factid = (item.Factid == null ? 0 : item.Factid),
                        Bed = item.Bed,
                        Bes = item.Bes,
                        Userid = item.Userid,
                        Idanbar = item.Idanbar,
                        Nameanbar = item.Nameanbar,
                        Codehesab = item.Codehesab,
                        Namehesab = item.Namehesab
                    });
                }
                ViewData["list"] = obj;
                ViewData["gid"] = gid;
                ViewData["isfact"] = isfact;
                return View();
            }
            catch (Exception err)
            {
                return Ok(new { msg = err.Message.ToString(), state = 0 });
            }

        }
        public IActionResult Printdaftarperson(string fdate, string tdate, int idkol, string anbar, int gid = 0, int isfact = 0)
        {
            try
            {
                List<Daftarperson> obj = new List<Daftarperson>();
                var lst = _context.Daftarpersons.FromSql("exec sp_daftarperson {0} , {1} , {2} , {3} , {4},{5} ", MyClass.PersianToEnglish(fdate), MyClass.PersianToEnglish(tdate), anbar, idkol, gid, int.Parse(HttpContext.Session.GetString("PersonId"))).ToList();
                if (lst.Count == 0)
                    return Ok(new { msg = "داده ای برای نمایش وجود ندارد", state = 0 });
                foreach (var item in lst)
                {
                    obj.Add(new Daftarperson
                    {
                        Mid = item.Mid,
                        Datec = (string.IsNullOrEmpty(item.Datec) ? "" : item.Datec),
                        Note = (string.IsNullOrEmpty(item.Note) ? "" : item.Note),
                        Nosanad = (item.Nosanad == null ? 0 : item.Nosanad),
                        Factid = (item.Factid == null ? 0 : item.Factid),
                        Bed = item.Bed,
                        Bes = item.Bes,
                        Userid = item.Userid,
                        Idanbar = item.Idanbar,
                        Nameanbar = item.Nameanbar,
                        Codehesab = item.Codehesab,
                        Namehesab = item.Namehesab
                    });
                }
                ViewData["list"] = obj;
                ViewData["isfact"] = isfact;

                ViewData["fdate"] = fdate;
                ViewData["tdate"] = tdate;
                var q = _context.Persontb.Where(x => x.Mid == idkol).FirstOrDefault();
                ViewData["namec"] = q.Namekamel;
                return View();
            }
            catch (Exception err)
            {
                return Ok(new { msg = err.Message.ToString(), state = 0 });
            }
        }



        public IActionResult Indexkol()
        {
            ViewData["listanbar"] = new SelectList(_context.Anbartb.ToList(), "Mid", "Namec");
            ViewData["fdate"] = MyClass.getpersiondate(DateTime.Now);
            ViewData["tdate"] = MyClass.getpersiondate(DateTime.Now);
            var min = _context.Mastersanadtb.Min(x => x.Datec);
            if (min != null)
                ViewData["fdate"] = min.ToString();
            var max = _context.Mastersanadtb.Max(x => x.Datec);
            if (max != null)
                ViewData["tdate"] = max.ToString();
            ViewBag.MID = 6;
            ViewBag.Tittle = "گزارش دفتر کل";
            var kol = _context.Koltb.ToList();
            ViewData["Kol"] = new SelectList(kol, "Mid", "Namekol");
            return View();
        }

        public IActionResult daftarkol(string fdate, string tdate, int idkol, int anbar, int print)
        {
            try
            {
                List<Daftarkol> obj = new List<Daftarkol>();
                var lst = _context.Daftarkol.FromSql("exec sp_daftarkol {0} , {1} , {2} , {3} ", MyClass.PersianToEnglish(fdate), MyClass.PersianToEnglish(tdate), idkol, anbar).ToList();
                if (lst.Count == 0)
                    return Ok(new { msg = "داده ای برای نمایش وجود ندارد", state = 0 });
                foreach (var item in lst)
                {
                    obj.Add(new Daftarkol
                    {
                        Mid = item.Mid,
                        Datec = (string.IsNullOrEmpty(item.Datec) ? "" : item.Datec),
                        Note = (string.IsNullOrEmpty(item.Note) ? "" : item.Note),
                        Nosanad = (item.Nosanad == null ? 0 : item.Nosanad),
                        Factid = (item.Factid == null ? 0 : item.Factid),
                        Bed = item.Bed,
                        Bes = item.Bes,
                        Userid = item.Userid,
                        Idanbar = item.Idanbar
                    });
                }
                ViewData["list"] = obj;
                return View();
            }
            catch (Exception err)
            {
                return Ok(new { msg = err.Message.ToString(), state = 0 });
            }

        }

        public IActionResult Printdaftar(string fdate, string tdate, int idkol, int anbar)
        {

            ViewData["fdate"] = fdate;
            ViewData["tdate"] = tdate;
            var kol = _context.Koltb.Where(x => x.Mid == idkol).FirstOrDefault();
            ViewData["namec"] = kol.Namekol;
            ViewData["code"] = kol.Codekol;
            var q = _context.Daftarkol.FromSql("exec sp_daftarkol {0} , {1} , {2} , {3} ", MyClass.PersianToEnglish(fdate), MyClass.PersianToEnglish(tdate), idkol, anbar).ToList();
            return View(q);
        }


        public IActionResult Indexmoin()
        {
            ViewData["listanbar"] = new SelectList(_context.Anbartb.ToList(), "Mid", "Namec");
            ViewData["fdate"] = MyClass.getpersiondate(DateTime.Now);
            ViewData["tdate"] = MyClass.getpersiondate(DateTime.Now);
            var min = _context.Mastersanadtb.Min(x => x.Datec);
            if (min != null)
                ViewData["fdate"] = min.ToString();
            var max = _context.Mastersanadtb.Max(x => x.Datec);
            if (max != null)
                ViewData["tdate"] = max.ToString();
            ViewBag.MID = 6;
            ViewBag.Tittle = "گزارش دفتر معین";
            var moin = _context.Mointb.Include(x => x.K).ToList();
            List<object> obj = new List<object>();
            foreach (var item in moin)
            {
                obj.Add(new
                {
                    Mid = item.Mid,
                    Namekol = item.K.Namekol + " - " + item.Namemoin
                });
            }
            ViewData["Kol"] = new SelectList(obj, "Mid", "Namekol");
            return View();
        }
        public IActionResult daftarmoin(string fdate, string tdate, int idkol, int anbar)
        {
            try
            {
                List<Daftarkol> obj = new List<Daftarkol>();
                var lst = _context.Daftarkol.FromSql("exec sp_daftarmoin {0} , {1} , {2} , {3} ", MyClass.PersianToEnglish(fdate), MyClass.PersianToEnglish(tdate), idkol, anbar).ToList();
                if (lst.Count == 0)
                    return Ok(new { msg = "داده ای برای نمایش وجود ندارد", state = 0 });
                foreach (var item in lst)
                {
                    obj.Add(new Daftarkol
                    {
                        Mid = item.Mid,
                        Datec = (string.IsNullOrEmpty(item.Datec) ? "" : item.Datec),
                        Note = (string.IsNullOrEmpty(item.Note) ? "" : item.Note),
                        Nosanad = (item.Nosanad == null ? 0 : item.Nosanad),
                        Factid = (item.Factid == null ? 0 : item.Factid),
                        Bed = item.Bed,
                        Bes = item.Bes,
                        Userid = item.Userid,
                        Idanbar = item.Idanbar
                    });
                }
                ViewData["list"] = obj;
                return View();
            }
            catch (Exception err)
            {
                return Ok(new { msg = err.Message.ToString(), state = 0 });
            }

        }
        public IActionResult Printdaftarmoin(string fdate, string tdate, int idkol, int anbar)
        {
            ViewData["fdate"] = fdate;
            ViewData["tdate"] = tdate;
            var kol = _context.Mointb.Where(x => x.Mid == idkol)
                .Include(x => x.K).FirstOrDefault();
            ViewData["namec"] = kol.K.Namekol + ' ' + kol.Namemoin;
            ViewData["code"] = kol.K.Codekol + ' ' + kol.Codemoin;
            var q = _context.Daftarkol.FromSql("exec sp_daftarmoin {0} , {1} , {2} , {3} ", MyClass.PersianToEnglish(fdate), MyClass.PersianToEnglish(tdate), idkol, anbar).ToList();
            return View("Printdaftar", q);
        }

        public IActionResult Indextaf()
        {
            ViewData["listanbar"] = new SelectList(_context.Anbartb.ToList(), "Mid", "Namec");
            ViewData["fdate"] = MyClass.getpersiondate(DateTime.Now);
            ViewData["tdate"] = MyClass.getpersiondate(DateTime.Now);
            var min = _context.Mastersanadtb.Min(x => x.Datec);
            if (min != null)
                ViewData["fdate"] = min.ToString();
            var max = _context.Mastersanadtb.Max(x => x.Datec);
            if (max != null)
                ViewData["tdate"] = max.ToString();
            ViewBag.MID = 6;
            ViewBag.Tittle = "گزارش دفتر تفصیلی";
            var moin = _context.Mointb.Include(x => x.K).ToList();
            List<object> obj = new List<object>();
            foreach (var item in moin)
            {
                obj.Add(new
                {
                    Mid = item.Mid,
                    Namekol = item.K.Namekol + " - " + item.Namemoin
                });
            }
            ViewData["Kol"] = new SelectList(obj, "Mid", "Namekol");
            ViewData["taf"] = new SelectList(_context.Submointb.ToList(), "Mid", "Nametaf");
            return View();
        }
        public IActionResult daftartaf(string fdate, string tdate, int idkol, int idtaf, int anbar, int print)
        {
            try
            {
                List<Daftarkol> obj = new List<Daftarkol>();
                var lst = _context.Daftarkol.FromSql("exec sp_daftarsubmoin {0} , {1} , {2} , {3} , {4} ", MyClass.PersianToEnglish(fdate), MyClass.PersianToEnglish(tdate), anbar, idkol, idtaf).ToList();
                if (lst.Count == 0)
                    return Ok(new { msg = "داده ای برای نمایش وجود ندارد", state = 0 });
                foreach (var item in lst)
                {
                    obj.Add(new Daftarkol
                    {
                        Mid = item.Mid,
                        Datec = (string.IsNullOrEmpty(item.Datec) ? "" : item.Datec),
                        Note = (string.IsNullOrEmpty(item.Note) ? "" : item.Note),
                        Nosanad = (item.Nosanad == null ? 0 : item.Nosanad),
                        Factid = (item.Factid == null ? 0 : item.Factid),
                        Bed = item.Bed,
                        Bes = item.Bes,
                        Userid = item.Userid,
                        Idanbar = item.Idanbar
                    });
                }
                ViewData["list"] = obj;
                return View();
            }
            catch (Exception err)
            {
                return Ok(new { msg = err.Message.ToString(), state = 0 });
            }

        }

        public IActionResult Printdaftartaf(string fdate, string tdate, int idkol, int idtaf, int anbar)
        {
            ViewData["fdate"] = fdate;
            ViewData["tdate"] = tdate;
            var kol = _context.Mointb.Where(x => x.Mid == idkol)
                .Include(x => x.K).FirstOrDefault();
            var taf = _context.Submointb.Where(x => x.Mid == idtaf)
                .FirstOrDefault();
            ViewData["namec"] = kol.K.Namekol + ' ' + kol.Namemoin + ' ' + taf.Nametaf;
            ViewData["code"] = kol.K.Codekol + ' ' + kol.Codemoin + ' ' + taf.Codetaf;
            var lst = _context.Daftarkol.FromSql("exec sp_daftarsubmoin {0} , {1} , {2} , {3} , {4} ", MyClass.PersianToEnglish(fdate), MyClass.PersianToEnglish(tdate), anbar, idkol, idtaf).ToList();
            return View("Printdaftar", lst);
        }


        public IActionResult Indextarazkol()
        {
            ViewData["listanbar"] = new SelectList(_context.Anbartb.ToList(), "Mid", "Namec");
            ViewData["fdate"] = MyClass.getpersiondate(DateTime.Now);
            ViewData["tdate"] = MyClass.getpersiondate(DateTime.Now);
            var min = _context.Mastersanadtb.Min(x => x.Datec);
            if (min != null)
                ViewData["fdate"] = min.ToString();
            var max = _context.Mastersanadtb.Max(x => x.Datec);
            if (max != null)
                ViewData["tdate"] = max.ToString();
            ViewBag.MID = 6;
            ViewBag.Tittle = "گزارش تراز کل";
            return View();
        }

        public IActionResult tarazkol(string fdate, string tdate, int anbar, int print, int gid)
        {
            try
            {
                List<Tarazkol> obj = new List<Tarazkol>();
                var lst = _context.Tarazkols.FromSql("exec sp_tarazkol {0}  , {1} , {2} , {3} ", MyClass.PersianToEnglish(fdate), MyClass.PersianToEnglish(tdate), anbar, gid).ToList();
                if (lst.Count == 0)
                    return Ok(new { msg = "داده ای برای نمایش وجود ندارد", state = 0 });
                foreach (var item in lst)
                {
                    obj.Add(new Tarazkol
                    {
                        Kid = item.Kid,
                        Mid = item.Mid,
                        Namekol = item.Namekol,
                        Codekol = item.Codekol,
                        Bed = item.Bed,
                        Bes = item.Bes,
                        Bednow = item.Bednow,
                        Besnow = item.Besnow,
                        Nameanbar = item.Nameanbar
                    });
                }
                ViewData["list"] = obj;
                ViewData["gid"] = gid;
                return View();
            }
            catch (Exception err)
            {
                return Ok(new { msg = err.Message.ToString(), state = 0 });
            }

        }

        public IActionResult Indextarazmoin()
        {
            ViewData["listanbar"] = new SelectList(_context.Anbartb.ToList(), "Mid", "Namec");
            ViewData["fdate"] = MyClass.getpersiondate(DateTime.Now);
            ViewData["tdate"] = MyClass.getpersiondate(DateTime.Now);
            var min = _context.Mastersanadtb.Min(x => x.Datec);
            if (min != null)
                ViewData["fdate"] = min.ToString();
            var max = _context.Mastersanadtb.Max(x => x.Datec);
            if (max != null)
                ViewData["tdate"] = max.ToString();
            ViewBag.MID = 6;
            ViewBag.Tittle = "گزارش تراز معین";
            return View();
        }

        public IActionResult tarazmoin(string fdate, string tdate, int print = 0, int kid = 0, int anbar = 0, int gid = 0)
        {
            ViewData["fdate"] = fdate;
            ViewData["tdate"] = tdate;
            List<Tarazmoin> obj = new List<Tarazmoin>();
            try
            {
                var lst = _context.Tarazmoins.FromSql("exec sp_tarazmoin {0}  , {1} , {2} , {3} ,{4}", fdate, tdate, kid, anbar, gid);
                foreach (var item in lst)
                {
                    obj.Add(new Tarazmoin
                    {
                        Mid = item.Mid,
                        Moinid = item.Moinid,
                        Kid = item.Kid,
                        Namekol = item.Namekol,
                        Codekol = item.Codekol,
                        Namemoin = item.Namemoin,
                        Codemoin = item.Codemoin,
                        Bed = item.Bed,
                        Bes = item.Bes,
                        Bednow = item.Bednow,
                        Besnow = item.Besnow,
                        Nameanbar = item.Nameanbar
                    });
                }
                ViewData["list"] = obj;
                ViewData["gid"] = gid;
                return View();
            }
            catch
            {
                return View();
            }
        }



        public IActionResult Indextaraztaf()
        {
            ViewData["listanbar"] = new SelectList(_context.Anbartb.ToList(), "Mid", "Namec");
            ViewData["fdate"] = MyClass.getpersiondate(DateTime.Now);
            ViewData["tdate"] = MyClass.getpersiondate(DateTime.Now);
            var min = _context.Mastersanadtb.Min(x => x.Datec);
            if (min != null)
                ViewData["fdate"] = min.ToString();
            var max = _context.Mastersanadtb.Max(x => x.Datec);
            if (max != null)
                ViewData["tdate"] = max.ToString();
            ViewBag.MID = 6;
            ViewBag.Tittle = "گزارش تراز تفصیلی";
            return View();
        }

        public IActionResult taraztaf(string fdate, string tdate, int print = 0, int anbar = 0, int idmoin = 0, int gid = 0)
        {
            ViewData["fdate"] = fdate;
            ViewData["tdate"] = tdate;
            List<Taraztaf> obj = new List<Taraztaf>();
            try
            {
                var lst = _context.Taraztafs.FromSql("exec sp_tarazsubmoin {0}  , {1} , {2} , {3} ,{4}", fdate, tdate, idmoin, anbar, gid).ToList();
                if (print == 1)
                {
                    //return RedirectToAction("Printtmoin", new { _fdate = fdate, _tdate = tdate, _kid = kid, _anbar = anbar });
                }
                foreach (var item in lst)
                {
                    obj.Add(new Taraztaf
                    {
                        Mid = item.Mid,
                        Moinid = item.Moinid,
                        Tafid = item.Tafid,
                        Namekol = item.Namekol,
                        Codekol = item.Codekol,
                        Namemoin = item.Namemoin,
                        Codemoin = item.Codemoin,
                        Nametaf = item.Nametaf,
                        Codetaf = item.Codetaf,
                        Bed = item.Bed,
                        Bes = item.Bes,
                        Bednow = item.Bednow,
                        Besnow = item.Besnow,
                        Nameanbar = item.Nameanbar
                    });
                }
                ViewData["list"] = obj;
                ViewData["gid"] = gid;
                return View();
            }
            catch
            {
                return View();
            }
        }




        public async Task<IActionResult> Indextarazperson()
        {
            ViewData["listanbar"] = new SelectList(_context.Anbartb.ToList(), "Mid", "Namec");
            ViewData["fdate"] = MyClass.getpersiondate(DateTime.Now);
            ViewData["tdate"] = MyClass.getpersiondate(DateTime.Now);
            var min = await _context.Mastersanadtb.MinAsync(x => x.Datec);
            if (min != null)
                ViewData["fdate"] = min.ToString();
            var max = await _context.Mastersanadtb.MaxAsync(x => x.Datec);
            if (max != null)
                ViewData["tdate"] = max.ToString();
            ViewBag.MID = 6;
            ViewBag.Tittle = "گزارش تراز اشخاص";
            ViewData["listgperson"] = new SelectList(await _context.Gorohpersontb.ToListAsync(), "Mid", "Title");
            return View();
        }

        public IActionResult tarazperson(string fdate, string tdate, int print = 0, int anbar = 0, int gid = 0, int gperson = 0)
        {
            if (print == 1)
            {
                return RedirectToAction("Printtarazperson", new { _fdate = fdate, _tdate = tdate, _gperson = gperson, _anbar = anbar, _gid = gid });
            }
            ViewData["fdate"] = fdate;
            ViewData["tdate"] = tdate;
            List<Tarazperson> obj = new List<Tarazperson>();
            try
            {
                var lst = _context.Tarazpersons.FromSql("exec sp_tarazperson {0}  , {1} , {2} , {3} ,{4}", MyClass.PersianToEnglish(fdate), MyClass.PersianToEnglish(tdate), gperson, anbar, gid).ToList();
                foreach (var item in lst)
                {
                    obj.Add(new Tarazperson
                    {
                        Mid = item.Mid,
                        Namekol = item.Namekol,
                        Codekol = item.Codekol,
                        Namemoin = item.Namemoin,
                        Codemoin = item.Codemoin,
                        Nametaf = item.Nametaf,
                        Codetaf = item.Codetaf,
                        Bed = item.Bed,
                        Bes = item.Bes,
                        Bednow = item.Bednow,
                        Besnow = item.Besnow,
                        Nameanbar = item.Nameanbar,
                        Moinid = item.Moinid,
                        Nameperson = item.Nameperson,
                        Pid = item.Pid,
                        Tafid = item.Tafid
                    });
                }
                ViewData["list"] = obj;
                ViewData["gid"] = gid;
                return View();
            }
            catch (Exception err)
            {
                return View();
            }
        }

        public IActionResult Printtarazperson(string _fdate, string _tdate, int anbar = 0, int gid = 0, int gperson = 0)
        {
            ViewData["fdate"] = _fdate;
            ViewData["tdate"] = _tdate;
            List<Tarazperson> obj = new List<Tarazperson>();
            try
            {
                var lst = _context.Tarazpersons.FromSql("exec sp_tarazperson {0}  , {1} , {2} , {3} ,{4}", MyClass.PersianToEnglish(_fdate), MyClass.PersianToEnglish(_tdate), gperson, anbar, gid).ToList();
                foreach (var item in lst)
                {
                    obj.Add(new Tarazperson
                    {
                        Mid = item.Mid,
                        Namekol = item.Namekol,
                        Codekol = item.Codekol,
                        Namemoin = item.Namemoin,
                        Codemoin = item.Codemoin,
                        Nametaf = item.Nametaf,
                        Codetaf = item.Codetaf,
                        Bed = item.Bed,
                        Bes = item.Bes,
                        Bednow = item.Bednow,
                        Besnow = item.Besnow,
                        Nameanbar = item.Nameanbar,
                        Moinid = item.Moinid,
                        Nameperson = item.Nameperson,
                        Pid = item.Pid,
                        Tafid = item.Tafid
                    });
                }
                ViewData["list"] = obj;
                ViewData["gid"] = gid;
                return View();
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message;
                return View();
            }
        }


        public class empfactkala
        {
            public string Namekala { get; set; }
            public string Vahedkala { get; set; }
            public string Vahedkala1 { get; set; }
            public double Tedadv { get; set; }
            public double Tedad1 { get; set; }
            public decimal? Price { get; set; }
            public double? Pricekol { get; set; }
            public decimal? dtakh { get; set; }
            public decimal? takhfif { get; set; }
            public int? act { get; set; }
        }
        public IActionResult getfact(int idfact)
        {
            List<empfactkala> _li = new List<empfactkala>();
            try
            {
                var qp = _context.Factkalatb.Include(x => x.IdfactNavigation).Include(x => x.IdkalaNavigation)
                    .Include(x => x.IdkalaNavigation.Idvahed1Navigation)
                    .Include(x => x.IdkalaNavigation.Idvahed2Navigation)
                    .Where(x => x.Idfact == idfact).ToList();
                foreach (var item in qp)
                {
                    _li.Add(new empfactkala
                    {
                        Namekala = item.IdkalaNavigation.Namekala,
                        Vahedkala = item.IdkalaNavigation.Idvahed2Navigation.Title,
                        Price = item.Price,
                        Pricekol = item.Pricekol,
                        Tedad1 = item.Tedadvahed1??0,
                        Vahedkala1 = (item.IdkalaNavigation.Idvahed1Navigation !=null ? item.IdkalaNavigation.Idvahed1Navigation.Title : ""),
                        Tedadv = (item.Tedadv == 0 ? Convert.ToDouble(item.Tedaddar) : item.Tedadv),
                        takhfif = item.Takhfif??0,
                        dtakh = item.Takhfifsh??0,
                        act = item.IdfactNavigation.Act
                    });
                }
                ViewData["list"] = _li;
                return View();
            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message;
                return View();
            }

        }
        public IActionResult Taraz(int id = 0, string typec = "-1", string fdate = "1111/11/11", string tdate = "9999/99/99"
            , int idkol = 0, int idmoin = 0, int idtaf = 0, int pid = 0, int typesearch = 0, int print = 0, int goroh = 0, int anbar = 0)
        {
            ViewData["fdate"] = fdate;
            ViewData["tdate"] = tdate;
            ViewData["typesearch"] = typesearch;
            ViewData["PageT"] = "گزارشات مالی";
            ViewData["listanbar"] = new SelectList(_context.Anbartb.FromSql("select * from anbartb where mid in "
                + " (select isnull(p.idanbar , 0)  from pertb p where p.userid = {0} )"
                , int.Parse(HttpContext.Session.GetString("PersonId"))).ToList(), "Mid", "Namec", anbar);

            if (TempData["errormsg1"] != null)
            {
                ViewBag.errormsg = TempData["errormsg1"].ToString();
            }
            else
            {
                ViewBag.errormsg = "";
            }
            ViewBag.MID = 6;

            if (id == 1)
            {
                ViewBag.Tittle = "تراز کل";
                ViewBag.TCode = "1";
            }
            if (id == 2)
            {
                ViewBag.Tittle = "تراز معین";
                ViewBag.TCode = "2";
            }
            if (id == 3)
            {
                ViewBag.Tittle = "تراز تفصیلی";
                ViewBag.TCode = "3";
            }
            if (id == 4)
            {
                ViewBag.Tittle = "لیست بدهکاران و بستانکاران  ";
                ViewBag.TCode = "4";
            }
            if (id == 5)
            {
                ViewBag.Tittle = "تراز یک فرد";
                ViewBag.TCode = "5";
            }

            try
            {
                var q = _context.Gorohpersontb.ToList();
                ViewData["list"] = new SelectList(q, "Mid", "Title", goroh);

                if (typec != "-1")
                {
                    ViewData["typec"] = typec;
                    if (typec == "1")
                        ViewData["caption"] = "تراز کل";
                    if (typec == "2")
                        ViewData["caption"] = "تراز معین";
                    if (typec == "3")
                        ViewData["caption"] = "تراز تفصیلی";
                    if (typec == "4")
                        ViewData["caption"] = "";
                    var lst = _context.Tarazs.FromSql(" sp_taraz {0}  , {1} , {2} , {3} ,0,0,0,0, {4} , {5}", typec, fdate, tdate, typesearch, anbar, goroh);
                    //if (goroh != 0)
                    //    lst = lst.Where(x => x.Gid == goroh);
                    if (print == 1)
                    {
                        //return RedirectToAction("PrintT");
                        return RedirectToAction("PrintT", new
                        {
                            _fdate = fdate,
                            _tdate = tdate,
                            _where = typesearch
                         ,
                            _goroh = goroh,
                            _anbar = anbar
                        });
                    }
                    return View(lst.ToList());
                }
                else
                {
                    List<Taraz> lst = null;
                    return View(lst);
                }
            }
            catch
            {
                return View();
            }
        }
        public IActionResult PrintT(string _fdate, string _tdate, int _where, int _goroh = 0, int _anbar = 0)
        {
            ViewData["fdate"] = _fdate;
            ViewData["tdate"] = _tdate;
            var lst = _context.Tarazs.FromSql(" sp_taraz {0}  , {1} , {2} , {3} ,0,0,0,0, {4} , {5}", 4, _fdate, _tdate, _where, _anbar, _goroh);
            //if (_goroh != 0)
            //    lst = lst.Where(x => x.Gid == _goroh);
            return View(lst.ToList());
        }
        public IActionResult Printtkol(string _fdate, string _tdate, int _anbar)
        {

            ViewData["fdate"] = _fdate;
            ViewData["tdate"] = _tdate;
            var lst = _context.Tarazs.FromSql(" sp_taraz {0}  , {1} , {2} , {3} ,0,0,0,0, {4}", 1, _fdate, _tdate, 0, _anbar);
            return View(lst.ToList());
        }
        public IActionResult Tarazkol1(int start = 0, string fdate = "1111/11/11", string tdate = "9999/99/99"
            , int print = 0, string err = "", int anbar = 0)
        {
            ViewData["listanbar"] = new SelectList(_context.Anbartb.FromSql("select * from anbartb where mid in "
                + " (select isnull(p.idanbar , 0)  from pertb p where p.userid = {0} )"
                , int.Parse(HttpContext.Session.GetString("PersonId"))).ToList(), "Mid", "Namec", anbar);

            ViewData["fdate"] = fdate;
            ViewData["tdate"] = tdate;
            ViewData["start"] = start;
            ViewData["PageT"] = "گزارشات مالی";
            ViewData["err"] = err;
            ViewBag.MID = 6;
            try
            {
                if (start == 1)
                {
                    var lst = _context.Tarazs.FromSql(" sp_taraz {0}  , {1} , {2} , {3} ,0,0,0,0, {4}"
                        , 1, fdate, tdate, 0, anbar);
                    if (print == 1)
                        return RedirectToAction("Printtkol", new { _fdate = fdate, _tdate = tdate, _anbar = anbar });
                    return View(lst.ToList());
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Printtaf(string _fdate, string _tdate, int _moinid = 0, int _anbar = 0)
        {
            ViewData["fdate"] = _fdate;
            ViewData["tdate"] = _tdate;
            var lst = _context.Tarazs.FromSql(" sp_taraz {0}  , {1} , {2} , {3} ,0,0,0,0, {4}", 3, _fdate, _tdate, 0, _anbar);
            if (_moinid != 0)
                lst = lst.Where(x => x.Moinid == _moinid);
            return View(lst.ToList());
        }
        public IActionResult Taraztaf1(int moinid = 0, int start = 0, string fdate = "1111/11/11", string tdate = "9999/99/99"
           , int print = 0, string err = "", int anbar = 0)
        {
            ViewData["fdate"] = fdate;
            ViewData["tdate"] = tdate;
            ViewData["start"] = start;
            ViewData["PageT"] = "گزارشات مالی";
            ViewData["err"] = err;
            ViewData["listanbar"] = new SelectList(_context.Anbartb.FromSql("select * from anbartb where mid in "
                + " (select isnull(p.idanbar , 0)  from pertb p where p.userid = {0} )"
                , int.Parse(HttpContext.Session.GetString("PersonId"))).ToList(), "Mid", "Namec", anbar);

            ViewBag.MID = 6;
            try
            {
                if (start == 1)
                {
                    var lst = _context.Tarazs.FromSql(" sp_taraz {0}  , {1} , {2} , {3} ,0,0,0,0, {4}", 3, fdate, tdate, 0, anbar);
                    if (moinid != 0)
                        lst = lst.Where(x => x.Moinid == moinid);
                    ViewData["moinid"] = moinid;
                    if (print == 1)
                    {
                        return RedirectToAction("Printtaf", new { _fdate = fdate, _tdate = tdate, _moinid = moinid, _anbar = anbar });
                    }
                    return View(lst.ToList());
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Printtmoin(string _fdate, string _tdate, int _kid = 0, int _anbar = 0)
        {
            ViewData["fdate"] = _fdate;
            ViewData["tdate"] = _tdate;
            var lst = _context.Tarazs.FromSql(" sp_taraz {0}  , {1} , {2} , {3},0,0,0,0 , {4}", 2, _fdate, _tdate, 0, _anbar);
            if (_kid != 0)
                lst = lst.Where(x => x.Kid == _kid);
            return View(lst.ToList());
        }
        public IActionResult Printp(string _fdate, string _tdate, int pid, int isf)
        {

            ViewData["fdate"] = _fdate;
            ViewData["tdate"] = _tdate;
            ViewData["isfact"] = isf;
            var person = _context.Persontb.Where(x => x.Mid == pid).FirstOrDefault();
            ViewData["namec"] = person.Namekamel;
            List<Daftar> q = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Daftar>>(HttpContext.Session.GetString("obj"));
            HttpContext.Session.SetString("obj", "");
            return View(q);
        }
        public IActionResult PrintH(string _fdate, string _tdate, int kid, int moinid, int tafid)
        {

            ViewData["fdate"] = _fdate;
            ViewData["tdate"] = _tdate;
            var kol = _context.Koltb.Where(x => x.Mid == kid).FirstOrDefault();
            var moin = _context.Mointb.Where(x => x.Mid == moinid).FirstOrDefault();
            var taf = _context.Submointb.Where(x => x.Mid == tafid).FirstOrDefault();
            ViewData["namec"] = kol.Namekol;
            ViewData["code"] = kol.Codekol;
            if (moin != null)
            {
                ViewData["namec"] = kol.Namekol + " - " + moin.Namemoin;
                ViewData["code"] = kol.Codekol + " - " + moin.Codemoin;
            }
            if (taf != null)
            {
                ViewData["namec"] = kol.Namekol + " - " + moin.Namemoin + " - " + taf.Nametaf;
                ViewData["code"] = kol.Codekol + " - " + moin.Codemoin + " - " + taf.Codetaf;
            }
            List<Daftar> q = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Daftar>>(HttpContext.Session.GetString("obj"));
            HttpContext.Session.SetString("obj", "");
            return View(q);
        }
        public class empG
        {
            public string Nosanad { get; set; }
            public string Datec { get; set; }
            public string Note { get; set; }
            public string Bed { get; set; }
            public string Bes { get; set; }
            public string Raw { get; set; }
            public string s { get; set; }
            public string Tittle { get; set; }
        }
        public IActionResult PrintPageT()
        {
            return View();
        }
        public class empT
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Bed { get; set; }
            public string Bednow { get; set; }
            public string Bes { get; set; }
            public string Besnow { get; set; }
            public string Tittle { get; set; }
        }

        public IActionResult tarazhesab(string typec = "-1", string fdate = "1111/11/11", string tdate = "9999/99/99"
            , int idkol = 0, int idmoin = 0, int idtaf = 0, int pid = 0, int typesearch = 0, int anbar = 0)
        {
            ViewData["fdate"] = fdate;
            ViewData["tdate"] = tdate;
            ViewData["typec"] = typec;
            ViewData["typesearch"] = typesearch;
            ViewData["Person"] = new SelectList(_context.Persontb.ToList(), "Mid", "Namekamel", pid);
            ViewData["Kol"] = new SelectList(_context.Koltb.ToList(), "Mid", "Namekol", idkol);

            ViewData["PageT"] = "گزارشات مالی";
            ViewData["listanbar"] = new SelectList(_context.Anbartb.FromSql("select * from anbartb where mid in "
                + " (select isnull(p.idanbar , 0)  from pertb p where p.userid = {0} )"
                , int.Parse(HttpContext.Session.GetString("PersonId"))).ToList(), "Mid", "Namec", anbar);

            if (TempData["errormsg1"] != null)
            {
                ViewBag.errormsg = TempData["errormsg1"].ToString();
            }
            else
            {
                ViewBag.errormsg = "";
            }
            ViewBag.MID = 6;

            try
            {
                if (typec != "-1")
                {
                    var lst = _context.Tarazs.FromSql(" sp_tarazpersonkol {0}  , {1} , {2} , {3} , {4} , {5} , {6} , {7}"
                        , fdate, tdate, typesearch, idkol, idmoin, idtaf, pid, anbar);
                    if (typec == "1")
                    {
                        return RedirectToAction("PrintT", new
                        {
                            _fdate = fdate,
                            _tdate = tdate,
                            _where = typesearch
                         ,
                            _anbar = anbar
                        });
                    }
                    return View(lst.ToList());
                }
                else
                {
                    List<Taraz> lst = null;
                    return View(lst);
                }
            }
            catch
            {
                return View();
            }
        }





        [HttpPost]
        public IActionResult reportperson(string fdate, string tdate, int idkol, int anbar, int print)
        {
            try
            {
                List<object> obj = new List<object>();
                if (print == 2)
                {
                    var lst = _context.Daftarkol.FromSql("exec sp_daftarkol {0} , {1} , {2} , {3} ", MyClass.PersianToEnglish(fdate), MyClass.PersianToEnglish(tdate), idkol, anbar).ToList();
                    if (lst.Count == 0)
                        return Ok(new { msg = "داده ای برای نمایش وجود ندارد", state = 0 });
                    List<object> objf = null;
                    foreach (var item in lst)
                    {
                        if (item.Factid != null)
                        {
                            var f = _context.Factkalatb.Where(x => x.Idfact == item.Factid)
                                .Include(x => x.IdkalaNavigation).ToList();
                            foreach (var itemf in f)
                            {
                                if (item.Note.IndexOf("تخفیف") < 0)
                                {
                                    objf = new List<object>();
                                    objf.Add(new
                                    {
                                        Namekala = itemf.IdkalaNavigation.Namekala,
                                        Tedadv = itemf.Tedadv,
                                        Tedaddar = itemf.Tedaddar,
                                        Tedadvahed = itemf.Tedadvahed1,
                                        Takh = (itemf.Takhfif == null ? "0" : string.Format("{0:n0}", itemf.Takhfif)),
                                        Takhsh = (itemf.Takhfifsh != null ? itemf.Takhfifsh : 0),
                                        Price = (itemf.Price == null ? "0" : string.Format("{0:n0}", itemf.Price)),
                                        Pricekol = (itemf.Pricekol == null ? "0" : string.Format("{0:n0}", itemf.Pricekol)),
                                    });
                                }
                            }

                        }
                        obj.Add(new
                        {
                            Mid = item.Mid,
                            Datec = item.Datec,
                            Note = item.Note,
                            Nosanad = item.Nosanad,
                            Factid = (item.Factid == null ? 0 : item.Factid),
                            Fact = objf,
                            Bed = (item.Bed == null ? "0" : string.Format("{0:n0}", item.Bed)),
                            Bes = (item.Bes == null ? "0" : string.Format("{0:n0}", item.Bes)),
                            Userid = item.Userid,
                            Idanbar = item.Idanbar
                        });
                    }
                }
                else if (print == 1)
                {
                    return RedirectToAction("PrintH", new
                    {
                        _fdate = fdate,
                        _tdate = tdate,
                        kid = idkol
                    });
                }
                return Ok(new { msg = obj, state = 1 });
            }
            catch (Exception err)
            {
                return Ok(new { msg = err.Message.ToString(), state = 0 });
            }

        }

        public IActionResult IndexForosh()
        {
            ViewData["fdate"] = _context.Facttb.Min(x => x.Datec);
            ViewData["tdate"] = _context.Facttb.Max(x => x.Datec);
            ViewData["listanbar"] = new SelectList(_context.Anbartb.FromSql("select * from anbartb where mid in "
        + " (select isnull(p.idanbar , 0)  from pertb p where p.userid = {0} )"
        , int.Parse(HttpContext.Session.GetString("PersonId"))).ToList(), "Mid", "Namec");
            return View();
        }
        public IActionResult Indexkharid()
        {
            ViewData["fdate"] = _context.Facttb.Min(x => x.Datec);
            ViewData["tdate"] = _context.Facttb.Max(x => x.Datec);
            ViewData["listanbar"] = new SelectList(_context.Anbartb.FromSql("select * from anbartb where mid in "
        + " (select isnull(p.idanbar , 0)  from pertb p where p.userid = {0} )"
        , int.Parse(HttpContext.Session.GetString("PersonId"))).ToList(), "Mid", "Namec");
            return View();
        }

        public IActionResult ListForosh(string fdate, string tdate, int anbar)
        {
            try
            {
                if (anbar > 0)
                {
                    var w = _context.Query<viewforosh>()
                   .Where(x => x.Datec.CompareTo(MyClass.PersianToEnglish(fdate)) >= 0 && x.Datec.CompareTo(MyClass.PersianToEnglish(tdate)) <= 0 && x.Idanbar == anbar).ToList();
                    ViewData["list"] = w;
                }
                else
                {
                    var w = _context.Query<viewforosh>()
                   .Where(x => x.Datec.CompareTo(MyClass.PersianToEnglish(fdate)) >= 0 && x.Datec.CompareTo(MyClass.PersianToEnglish(tdate)) <= 0).ToList();
                    ViewData["list"] = w;
                }
                return View();

            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message;
                return View();
            }
        }

        public IActionResult Listkharid(string fdate, string tdate, int anbar)
        {
            try
            {
                if (anbar > 0)
                {
                    var w = _context.Query<viewkharid>()
                   .Where(x => x.Datec.CompareTo(MyClass.PersianToEnglish(fdate)) >= 0 && x.Datec.CompareTo(MyClass.PersianToEnglish(tdate)) <= 0 && x.Idanbar == anbar).ToList();
                    ViewData["list"] = w;
                }
                else
                {
                    var w = _context.Query<viewkharid>()
                   .Where(x => x.Datec.CompareTo(MyClass.PersianToEnglish(fdate)) >= 0 && x.Datec.CompareTo(MyClass.PersianToEnglish(tdate)) <= 0).ToList();
                    ViewData["list"] = w;
                }
                return View();

            }
            catch (Exception err)
            {
                ViewData["err"] = err.Message;
                return View();
            }
        }

//        public IActionResult Indextarazperson()
  //      {
        //    ViewData["listanbar"] = new SelectList(_context.Anbartb.FromSql("select * from anbartb where mid in "
        //    + " (select isnull(p.idanbar , 0)  from pertb p where p.userid = {0} )"
        //    , int.Parse(HttpContext.Session.GetString("PersonId"))).ToList(), "Mid", "Namec", anbar);
        //    ViewData["PageT"] = "گزارشات مالی";
        //    if (TempData["errormsg1"] != null)
        //    {
        //        ViewBag.errormsg = TempData["errormsg1"].ToString();
        //    }
        //    else
        //    {
        //        ViewBag.errormsg = "";
        //    }

        //    ViewBag.MID = 6;
        //    ViewData["fdate"] = fdate;
        //    ViewData["tdate"] = tdate;
        //    ViewData["id"] = id;
        //    ViewData["isfact"] = isfact;
        //    if (id == 1)
        //    {
        //        ViewBag.Tittle = "گزارش دفتر کل";
        //        ViewBag.TCode = "1";
        //        ViewBag.mo = codemoin;
        //        ViewBag.ta = 0;
        //    }
        //    if (id == 2)
        //    {
        //        ViewBag.Tittle = "گزارش دفتر معین";
        //        ViewBag.TCode = "2";
        //        ViewBag.mo = codemoin;
        //        ViewBag.ta = codetaf;
        //        ViewBag.pe = codeperson;
        //    }
        //    if (id == 3)
        //    {
        //        ViewBag.Tittle = "گزارش دفتر تفصیلی";
        //        ViewBag.TCode = "3";
        //        ViewBag.mo = codemoin;
        //        ViewBag.ta = codetaf;
        //        ViewBag.pe = codeperson;
        //    }
        //    if (id == 4)
        //    {
        //        ViewBag.Tittle = "گزارش گردش حساب یک فرد";
        //        ViewBag.TCode = "4";
        //        ViewBag.mo = codemoin;
        //        ViewBag.ta = codetaf;
        //        ViewBag.pe = codeperson;
        //    }
        //    if (id == 5)
        //    {
        //        ViewBag.Tittle = "صورت حساب کلی یک فرد";
        //        ViewBag.TCode = "5";
        //        ViewBag.mo = codemoin;
        //        ViewBag.ta = codetaf;
        //        ViewBag.pe = codeperson;
        //    }

        //    try
        //    {
        //        var kol = _context.Koltb.ToList();
        //        var person = _context.Persontb.OrderBy(p => p.Namekamel).ToList();
        //        if (typec != "-1")
        //        {
        //            var lst = _context.Daftars.FromSql(" sp_daftar {0} , {1} , {2} , {3} , {4} , "
        //                + " {5} , {6} , {7} ", anbar, id, codekol, codemoin, codetaf, codeperson, fdate, tdate).ToList();
        //            ViewData["Kol"] = new SelectList(kol, "Mid", "Namekol", codekol);
        //            ViewData["Person"] = new SelectList(person, "Mid", "Namekamel", codeperson);
        //            if (print == 1)
        //            {
        //                //return RedirectToAction("PrintT");
        //                HttpContext.Session.SetString("obj", Newtonsoft.Json.JsonConvert.SerializeObject(lst));
        //                if (id >= 4)
        //                    return RedirectToAction("Printp", new { _fdate = fdate, _tdate = tdate, pid = codeperson, isf = isfact });
        //                else
        //                    return RedirectToAction("PrintH", new
        //                    {
        //                        _fdate = fdate,
        //                        _tdate = tdate
        //                        ,
        //                        kid = codekol,
        //                        moinid = codemoin,
        //                        tafid = codetaf
        //                    });
        //            }
        //            return View(lst);
        //        }
        //        else
        //        {
        //            List<Daftar> lst = null;
        //            ViewData["Kol"] = new SelectList(kol, "Mid", "Namekol");
        //            ViewData["Person"] = new SelectList(person, "Mid", "Namekamel");
        //            return View(lst);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        ViewData["err"] = err.Message.ToString();
        //        return View();
        //    }
        //}
    }
}