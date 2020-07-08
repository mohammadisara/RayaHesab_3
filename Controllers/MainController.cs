using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RayaHesab.Models;
using System.IO;

namespace RayaHesab.Controllers
{
    public class MainController : Controller
    {
        private readonly malisContext _context;

        public MainController(IConfiguration configuration, malisContext context)
        {
            Configuration = configuration;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public string getdateshamshi()
        {
            try
            {
                var q = _context.Dateshamsis.FromSql("select dbo.getShamsiDate(getdate()) as Datec ").FirstOrDefault();
                return q.Datec;
            }
            catch
            {
                return "1398/04/01";
            }
        }



        public static string mtext(string s)
        {
            string str = "0";
            if (s != "")
            {
                str = s.Replace(",", "");
            }
            return str;
        }
        public static string Totext(string s)
        {
            string str = "0";
            if (s != "")
            {
                str = string.Format("{0:#,##0}", double.Parse(s));
            }
            return str;
        }

        public IConfiguration Configuration { get; }

        [HttpPost]
        public string getmoinwithkol(string kid)
        {
            var list = _context.Mointb.Where(k => k.Kid == int.Parse(kid)).Include(p => p.K).ToList();
            List<RayaHesab.Models.Mointb> tmpmoin = new List<RayaHesab.Models.Mointb>();

            foreach (var item in list)
            {
                tmpmoin.Add(new RayaHesab.Models.Mointb()
                {
                    Mid = item.Mid,
                    Namemoin = item.Namemoin,
                    Codemoin = item.Codemoin
                });
            }
            return JsonConvert.SerializeObject(tmpmoin);
        }

        [HttpPost]
        public string gettafwithmoin(string moinid)
        {
            var list = _context.RlateMstb.Where(k => k.Moinid == int.Parse(moinid)).Include(p => p.Taf).ToList();
            List<RayaHesab.Models.Submointb> tmpmoin = new List<RayaHesab.Models.Submointb>();

            foreach (var item in list)
            {
                tmpmoin.Add(new RayaHesab.Models.Submointb()
                {
                    Mid = item.Tafid,
                    Nametaf = item.Taf.Nametaf,
                    Codetaf = item.Taf.Codetaf
                });
            }
            return JsonConvert.SerializeObject(tmpmoin);
        }

        [HttpPost]
        public string getmoin()
        {
            //            var list = _context.Mointb.Join(_context.Koltb, m => m.Kid, k => k.Mid, (m, k) => new { Mymoin = m, Mykol = k }).ToList();
            //ViewBag.Moin = await _context.Query<vcoding>().ToListAsync();

            var list = _context.Query<vcoding>().ToList();//  Mointb.Join(_context.Koltb, m => m.Kid, k => k.Mid, (m, k) => new { Mymoin = m, Mykol = k }).ToList();
            List<empmoin> tmpmoin = new List<empmoin>();

            foreach (var item in list)
            {
                tmpmoin.Add(new empmoin()
                {
                    mid = item.mid.ToString(),
                    codetaf = item.codetaf,
                    tafname = item.nametaf,
                    tafid = item.idtaf.ToString(),
                    codekol = item.codekol,
                    codemoin = item.codemoin,
                    kolid = item.kid.ToString(),
                    kolname = item.namekol,
                    moinid = item.moinid.ToString(),
                    moinname = item.namemoin,
                    label = item.codekol + "-" + item.codemoin + "-" + item.codetaf + "-" +
                    item.namekol + "-" + item.namemoin + "-" + item.nametaf,
                    id = item.moinid.ToString() + "*" + item.idtaf.ToString(),
                    value = item.codekol + " - " + item.codemoin + " - " + item.codetaf

                    //codekol = item.Mykol.Codekol,
                    //codemoin = item.Mymoin.Codemoin,
                    //kolid = item.Mykol.Mid.ToString(),
                    //kolname = item.Mykol.Namekol,
                    //moinid = item.Mymoin.Mid.ToString(),
                    //moinname = item.Mymoin.Namemoin,
                    //label = item.Mykol.Codekol + "-" + item.Mymoin.Codemoin + "-" +
                    //item.Mykol.Namekol + "-" + item.Mymoin.Namemoin,
                    //id = item.Mymoin.Mid.ToString(),
                    //value = item.Mykol.Codekol + " - " + item.Mymoin.Codemoin

                });
            }
            return JsonConvert.SerializeObject(tmpmoin);
        }
        private class empmoin
        {
            public string mid { get; set; }
            public string tafid { get; set; }
            public string moinid { get; set; }
            public string kolid { get; set; }
            public string tafname { get; set; }
            public string moinname { get; set; }
            public string kolname { get; set; }
            public string codekol { get; set; }
            public string codemoin { get; set; }
            public string codetaf { get; set; }
            public string label { get; set; }
            public string value { get; set; }
            public string id { get; set; }
        }

        [HttpPost]
        public string gettaf()
        {
            var list = _context.Submointb.ToList();
            List<emptaf> tmptaf = new List<emptaf>();

            foreach (var item in list)
            {
                tmptaf.Add(new emptaf()
                {
                    mid = item.Mid.ToString(),
                    codetaf = item.Codetaf,
                    nametaf = item.Nametaf,
                    label = item.Codetaf + "-" + item.Nametaf,
                    id = item.Mid.ToString(),
                    value = item.Codetaf
                });
            }
            return JsonConvert.SerializeObject(tmptaf);
        }
        private class emptaf
        {
            public string codetaf { get; set; }
            public string nametaf { get; set; }
            public string mid { get; set; }
            public string label { get; set; }
            public string value { get; set; }
            public string id { get; set; }
        }
        //public static string getcn = "Data Source=176.9.214.221,2016;Initial Catalog=malis;User ID=sa;Password=Mm55478816_;Persist Security Info=True;Max Pool Size=3000000;Connect Timeout=60";
        [HttpPost]
        public string getperson()
        {
            var list = _context.Persontb.ToList();
            List<empperson> tmpper = new List<empperson>();

            foreach (var item in list)
            {
                tmpper.Add(new empperson()
                {
                    mid = item.Mid.ToString(),
                    namekamel = item.Namekamel + " " + item.Addressc + " " + item.Mobile,
                    label = item.Namekamel + " " + item.Addressc + " " + item.Mobile,
                    id = item.Mid.ToString(),
                    value = item.Namekamel
                });
            }
            return JsonConvert.SerializeObject(tmpper);
        }
        private class empperson
        {
            public string namekamel { get; set; }
            public string mid { get; set; }
            public string label { get; set; }
            public string value { get; set; }
            public string id { get; set; }
        }

        public string getVisitorName(int id)
        {
            var list = _context.Persontb.Where(p => p.Mid == id).FirstOrDefault();
            return list.Namekamel;
        }
        public string getVisitor()
        {
            var list = _context.Persontb.Where(p => p.Typeperson == 1).ToList();
            List<empvisitor> tmpper = new List<empvisitor>();

            foreach (var item in list)
            {
                tmpper.Add(new empvisitor()
                {
                    mid = item.Mid.ToString(),
                    namekamel = item.Namekamel,
                    label = item.Namekamel,
                    id = item.Mid.ToString(),
                    value = item.Namekamel,
                    typeperson = item.Typeperson.ToString()
                });
            }
            return JsonConvert.SerializeObject(tmpper);
        }
        private class empvisitor
        {
            public string namekamel { get; set; }
            public string mid { get; set; }
            public string label { get; set; }
            public string value { get; set; }
            public string id { get; set; }
            public string typeperson { get; set; }
        }


        [HttpPost]
        public string getanbar()
        {

            var list = _context.Anbartb.FromSql("select * from anbartb where mid in "
    + " (select isnull(p.idanbar , 0)  from pertb p where p.userid = {0} )"
    , int.Parse(HttpContext.Session.GetString("PersonId"))).ToList();
            List<empanbar> tmpa = new List<empanbar>();

            foreach (var item in list)
            {
                tmpa.Add(new empanbar()
                {
                    mid = item.Mid.ToString(),
                    namec = item.Namec,
                    id = item.Mid.ToString(),
                    label = item.Mid + " - " + item.Namec,
                    value = item.Mid.ToString()
                });
            }

            return JsonConvert.SerializeObject(tmpa);
        }
        private class empanbar
        {
            public string mid { get; set; }
            public string namec { get; set; }
            public string id { get; set; }
            public string value { get; set; }
            public string label { get; set; }

        }

        private double getmojodikala(int idkala , int idanbar)
        {
            var q = _context.getmojodikala.FromSql("select [dbo].[getmojodikala]({0},{1}) as tedadv ", idkala , idanbar).FirstOrDefault();
            if (q != null)
                return q.tedadv;
            return 0;
        }
        [HttpPost]
        public string getkala(int i = 0, string str = "" , int anbar = 0)
        {
            var list = _context.Kalatb.FromSql("select k.* from kalatb k inner join paneltb p on p.mid = k.gid where p.fcode in "
                + " (select gkalaid from pertb pr where pr.userid = {0})", int.Parse(HttpContext.Session.GetString("PersonId")))
                .Include(x => x.G)
                .ToList();
            if (!string.IsNullOrEmpty(str))
            {
                string[] ch = str.Split(" ");
                foreach (var item in ch)
                {
                    list = list.Where(x => ((x.Namekala) + (x.Codekala.ToString())).Contains(item)).ToList();
                }
            }
            List<empkala> tmpk = new List<empkala>();
            foreach (var item in list)
            {
                if (i == 0)
                {
                    tmpk.Add(new empkala()
                    {
                        mid = item.Mid,
                        namekala = item.Namekala,
                        codekala = item.Codekala,
                        barcode = item.Barcode,
                        vahed1 = item.Vahed1,
                        mojodi = getmojodikala(item.Mid, anbar) ,
                        vahed2 = item.Vahed2,
                        price1 = Totext(item.Price1.ToString()),
                        price2 = Totext(item.Price2.ToString()),
                        id = (Int32)item.Mid,
                        label = item.Codekala.ToString() + " - " + item.Namekala + " / " + item.G.Fullname,
                        value = item.Codekala.ToString()
                    });
                }
                if (i == 1)
                {
                    if (item.Price2 != null)
                        if (item.Price2 != 0)
                            tmpk.Add(new empkala()
                            {
                                mid = item.Mid,
                                namekala = item.Namekala,
                                codekala = item.Codekala,
                                vahed1 = item.Vahed1,
                                vahed2 = item.Vahed2,
                                barcode = item.Barcode,
                                id = (Int32)item.Mid,
                                label = item.Barcode + " - " + item.Namekala,
                                value = item.Barcode
                            });

                }
            }
           return JsonConvert.SerializeObject(tmpk);
        }
        private class empkala
        {
            public int mid { get; set; }
            public int? codekala { get; set; }
            public string namekala { get; set; }
            public string barcode { get; set; }
            public double? vahed1 { get; set; }
            public double? mojodi { get; set; }
            public double? vahed2 { get; set; }
            public string price1 { get; set; }
            public string price2 { get; set; }
            public int id { get; set; }
            public string value { get; set; }
            public string label { get; set; }

        }

        private class emppanel
        {
            public int? codeparent { get; set; }
            public int mid { get; set; }
            public string Namep { get; set; }
            public string Namekamel { get; set; }
            public int id { get; set; }
            public int c { get; set; }
            public string value { get; set; }
            public string label { get; set; }

        }
        [HttpPost]
        public string getpanel()
        {
            var list = _context.PanelTb.OrderBy(s => s.Codeparent).ToList();
            List<emppanel> tmpmoin = new List<emppanel>();
            string c = "0";
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.Codeparent.ToString()))
                    c = "0";
                else
                    c = item.Codeparent.ToString();
                tmpmoin.Add(new emppanel()
                {
                    codeparent = int.Parse(c),
                    mid = item.Mid,
                    Namep = item.Namep
                });
            }
            return JsonConvert.SerializeObject(tmpmoin);
        }

        [HttpPost]
        public string getgorohkala(string code = "0")
        {
            int? codep = int.Parse(code);
            List<emppanel> tmpmoin = new List<emppanel>();
            if (codep == 0)
            {
                var list = _context.Query<vogorohkala>().FromSql("select * from vogorohkala where mid in (select gkalaid from pertb where pertb.userid = {0})", HttpContext.Session.GetString("PersonId")).Where(x => x.codeparent == null).OrderBy(s => s.mid).ToList();
                foreach (var item in list)
                {
                    tmpmoin.Add(new emppanel()
                    {
                        codeparent = item.codeparent,
                        mid = item.mid,
                        Namep = item.namep,
                        c = item.c
                    });
                }
            }
            else
            {
                var list = _context.Query<vogorohkala>().Where(x => x.codeparent == codep).OrderBy(s => s.mid).ToList();
                foreach (var item in list)
                {
                    tmpmoin.Add(new emppanel()
                    {
                        codeparent = item.codeparent,
                        mid = item.mid,
                        Namep = item.namep,
                        c = item.c
                    });
                }

            }
            return JsonConvert.SerializeObject(tmpmoin);
        }

        [HttpPost]
        public string getendpanel()
        {
            var list = _context.Query<vpanel>().ToList();
            List<emppanel> tmpmoin = new List<emppanel>();
            string c = "0";
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.codeparent.ToString()))
                    c = "0";
                else
                    c = item.codeparent.ToString();
                tmpmoin.Add(new emppanel()
                {
                    codeparent = int.Parse(c),
                    mid = item.mid,
                    Namep = item.namep,
                    id = item.mid,
                    value = item.namekamel,
                    label = item.namekamel,
                    Namekamel = item.namekamel
                });
            }
            return JsonConvert.SerializeObject(tmpmoin);

        }



        [HttpPost]
        public string getmaxkol(string gid)
        {
            try
            {
                var mdata = _context.Koltb.Where(x => x.Gid == int.Parse(gid)).Max(x => x.Codekol);
                string q = gid + "01";
                int i = 0;
                try
                {
                    i = int.Parse(mdata);
                    i = i + 1;
                }
                catch
                {
                    i = int.Parse(q);
                }
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.InnerException.Message;
            }

        }
        [HttpPost]
        public string getmaxmoin(string kid)
        {

            var mdata = _context.Mointb.Where(x => x.Kid == int.Parse(kid)).Max(x => x.Codemoin);
            string q = kid + "01";
            int i = 0;
            string s = "001";
            try
            {
                i = int.Parse(mdata);
                string MyString = ("000" + (i + 1).ToString());
                s = MyString.Substring(MyString.Length - 3);
            }
            catch
            {
            }
            return s;

        }


        [HttpPost]
        public string getmaxnofact(string typec)
        {
            var t = _context.Facttb.Where(x => x.Typec == int.Parse(typec)).OrderByDescending(x => x.Mid).FirstOrDefault();
            int? maxid = 1;
            try
            {
                maxid = t.Nofact + 1;
            }
            catch
            {
            }
            return maxid.ToString();
        }

        [HttpPost]
        public string printfact(string mid)
        {
            List<viewprintfact> _li = new List<viewprintfact>();
            var list = _context.Query<viewprintfact>().Where(p => p.mid == int.Parse(mid)).ToList();
            foreach (var item in list)
            {
                _li.Add(new viewprintfact()
                {
                    mid = item.mid,
                    codekol = item.codekol,
                    codemoin = item.codemoin,
                    codetaf = item.codemoin,
                    datec = item.datec,
                    idanbar = item.idanbar,
                    idkala = item.idkala,
                    idmoin = item.idmoin,
                    idperson = item.idperson,
                    idtaf = item.idtaf,
                    maliyat = item.maliyat,
                    namekala = item.namekala,
                    namekamel = item.namekamel,
                    namekol = item.namekol,
                    namemoin = item.namemoin,
                    nametaf = item.nametaf,
                    namevahed = item.namevahed,
                    nofact = item.nofact,
                    note = item.note,
                    price = item.price,
                    pricekharid = item.pricekharid,
                    pricekol = item.pricekol,
                    takhfif = item.takhfif,
                    tedadv = item.tedadv,
                    typec = item.typec,
                    sarbarg = item.sarbarg,
                    fforosh = item.fforosh,
                    namebazar = item.namebazar,
                    sahmbazar = item.sahmbazar,
                    codekala = item.codekala,
                    fullname = item.fullname,
                    nameanbar = item.nameanbar,
                    pricepar = item.pricepar

                });

            }
            return JsonConvert.SerializeObject(_li);
        }
        private class empfactkala
        {
            public int? mid { get; set; }
            public int? idmoin { get; set; }
            public int? idtaf { get; set; }
            public int? idperson { get; set; }
            public int? kalaid { get; set; }
            public int? idkala { get; set; }
            public int? typec { get; set; }
            public int? idanbar { get; set; }
            public int? nofact { get; set; }
            public string datec { get; set; }
            public string note { get; set; }
            public string namekala { get; set; }
            public string namevahed { get; set; }
            public string codemoin { get; set; }
            public string namemoin { get; set; }
            public string codetaf { get; set; }
            public string nametaf { get; set; }
            public string namekamel { get; set; }
            public string codekol { get; set; }
            public string namekol { get; set; }
            public float? tedadv { get; set; }
            public decimal? price { get; set; }
            public decimal? takhfif { get; set; }
            public decimal? maliyat { get; set; }
            public decimal? pricekharid { get; set; }
            public decimal? pricekol { get; set; }

        }
        [HttpPost]
        public string getnamekala()
        {
            var list = _context.Kalatb.Where(d => d.Namekala != null || d.Namekala != "").Select(x => new { x.Namekala }).ToList();
            List<empanbar> tmpa = new List<empanbar>();
            foreach (var item in list)
            {
                tmpa.Add(new empanbar()
                {
                    namec = item.Namekala,
                    id = item.Namekala,
                    label = item.Namekala,
                    value = item.Namekala
                });
            }
            return JsonConvert.SerializeObject(tmpa);
        }
        [HttpPost]
        public string getcapkala(int gid)
        {
            List<Tintb> tmpk = new List<Tintb>();
            if (gid == -1)
            {
                int? lmax = _context.Kalatb.Max(x => x.Codekala);
                if (lmax != null)
                    tmpk.Add(new Tintb()
                    {
                        Mid = int.Parse((lmax + 1).ToString())
                    });
                else
                    tmpk.Add(new Tintb()
                    {
                        Mid = 1
                    });
                return JsonConvert.SerializeObject(tmpk);
            }
            var list = _context.Tintb.FromSql("select top 10 mid from tintb where mid >= "
            + " (select min(isnull(codekala, 0)) from kalatb where gid = {0}) "
            + " and mid <= (select max(isnull(codekala, 0)) from kalatb where gid = {0}) "
            + " and mid not in(select isnull(codekala, 0) from kalatb where gid = {0}) ", gid).ToList();
            var fcode = _context.Kalatb.Include(x => x.G).Where(x => x.Gid == gid).Select(x => x.G.Fcode).FirstOrDefault();
            if (fcode == null)
            {
                var tmpcode = _context.PanelTb.Where(x => x.Mid == gid).Select(x => x.Fcode).FirstOrDefault();
                if (tmpcode != null)
                {
                    var lgmaxu = _context.Kalatb.Include(x => x.G).Where(x => x.G.Fcode == tmpcode).Max(x => x.Codekala);
                    
                    if (lgmaxu != null)
                    {
                        tmpk.Add(new Tintb()
                        {
                            Mid = Convert.ToInt32(lgmaxu + 1)
                        });
                    }
                    else
                    {
                        var codeg = _context.PanelTb.Where(x => x.Mid == tmpcode).Select(x => x.Codegroup).FirstOrDefault();
                        tmpk.Add(new Tintb()
                        {
                            Mid =int.Parse(codeg.ToString() + "1000")
                        });
                    }

                }
                else
                {
                    tmpk.Add(new Tintb()
                    {
                        Mid = 1
                    });
                }
            }
            else
            {
                var lgmax = _context.Kalatb.Include(x => x.G).Where(x => x.G.Fcode == fcode).Max(x => x.Codekala);
                if (lgmax != null)

                    tmpk.Add(new Tintb()
                    {
                        Mid = int.Parse((lgmax + 1).ToString())
                    });
                else
                    tmpk.Add(new Tintb()
                    {
                        Mid = 1
                    });
            }
            foreach (var item in list)
            {
                tmpk.Add(new Tintb()
                {
                    Mid = item.Mid
                });
            }
            return JsonConvert.SerializeObject(tmpk);
        }

        [HttpPost]
        public async Task<IActionResult> addperson(string sname ,string family , int gid , string mobile)
        {
            try
            {
                Persontb p = new Persontb();
                p.Namec = sname;
                p.Family = family;
                p.Mobile = mobile;
                p.Gid = gid;
                _context.Persontb.Add(p);
                await _context.SaveChangesAsync();
                return Ok(new { mid = p.Mid , msg = "", err = "" , namec = p.Namekamel });
            }
            catch (Exception err)
            {
                return Ok(new { msg = "خطا در ایجاد شخص", err = err.Message });
            }
        }
        [HttpPost]
        public string getonekala(Int32 mid)
        {
            var list = _context.Kalatb
                .Where(x => x.Mid == mid).FirstOrDefault();

            //List<empkala> tmpk = new List<empkala>();
            //string s = "";
            //foreach (var item in list)
            //{
            //    s = item.Idvahed1Navigation.Title;
            //    tmpk.Add(new empkala()
            //    {
            //        mid = item.Mid,
            //        namekala = item.Namekala,
            //        codekala = item.Codekala,
            //        vahed1 = item.Idvahed1Navigation.Title,
            //        price1 = Totext(item.Price1.ToString()),
            //        price2 = Totext(item.Price2.ToString()),
            //        id = (Int32)item.Mid,
            //        label = item.Codekala.ToString() + " - " + item.Namekala + " / " + item.G.Fullname,
            //        value = item.Codekala.ToString()
            //    });
            //}

            return JsonConvert.SerializeObject(list);
        }
    }
}
//https://github.com/zzzprojects/EntityFramework.Extended
//Scaffold-DbContext "Data Source=176.9.214.221,2016;Initial Catalog=malis;User ID=sa;Password=Mm55478816_;Persist Security Info=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
//public virtual DbSet<Daftar> Daftars { get; set; }
//public virtual DbSet<Taraz> Tarazs { get; set; }
//public virtual DbQuery<Dateshamsi> Dateshamsis { get; set; }
//public virtual DbQuery<Sp_gardeshkala> Sp_gardeshkala { get; set; }
//public virtual DbQuery<Sp_mojodikala> Sp_mojodikala { get; set; }

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Query<vcoding>().ToView("vcoding");
//    modelBuilder.Query<vpanel>().ToView("vpanel");
//    modelBuilder.Query<vogorohkala>().ToView("vogorohkala");
//    modelBuilder.Query<viewprintfact>().ToView("viewprintfact");
//    modelBuilder.Query<vmastersanad>().ToView("vmastersanad");
//    modelBuilder.Query<viewkala>().ToView("viewkala");
//    modelBuilder.Query<ViewFact>().ToView("viewfact");
//    modelBuilder.Query<Viewkalabarcode>().ToView("viewkalabarcode");
//    modelBuilder.Query<Vlistnobarcode>().ToView("vlistnobarcode");
//    modelBuilder.Query<Kalatahvilview>().ToView("kalatahvilview");
//Scaffold-DbContext "Data Source=95.216.56.89,2016;Initial Catalog=accfiyan_data;User ID=accfiyan_sa;Password=Mm55478816_;Persist Security Info=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force