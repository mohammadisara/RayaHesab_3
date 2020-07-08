using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RayaHesab.Models;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Http;
namespace RayaHesab.Controllers
{
    public class APIController : Controller
    {
        private readonly malisContext _context;
        public IActionResult Index()
        {
            return View();
        }
        public APIController(malisContext context)
        {
            _context = context;
        }
        [HttpPost]
        public string login(string uname, string pass)
        {
            var usertb = _context.Persontb.Where(x => x.Mobile == uname && x.Mobile == pass).FirstOrDefault();
            if (usertb == null)
            {
                return "0";
            }
            else
            {
                return usertb.Mid.ToString();
            }
        }



        /// <summary>
        /// For Mobile get Goroh kala from lookupsubtb
        /// </summary>
        /// <param name="mid"></param>
        /// <returns>ok 200</returns>
        [HttpPost]
        public IActionResult getpanel(int mid)
        {
            try
            {
                var q = (from k in _context.Kalatb
                         join l in _context.Looupsubtb on k.Titleprint equals l.Mid
                         where k.Gid == mid
                         select new { title = l.Title , mid = l.Mid }).ToList().Distinct();
                return Ok(new { res = q, msg = "" });
            }
            catch (Exception err)
            {
                return Ok(new { msg = err.Message });
            }
        }
        /// <summary>
        /// For Mobile show Group kala
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult showkala(int mid)
        {
            try
            {
                var q = (from k in _context.Kalatb
                         join l in _context.Looupsubtb on k.Idclass equals l.Mid
                         join f in _context.Filetb on k.Mid equals f.Idkala
                         where k.Titleprint == mid
                         select new { title = l.Title, mid = l.Mid , fname = f.Namec }).ToList().Distinct();
                var kalagroup = _context.Looupsubtb.Where(x => x.Mid == mid).FirstOrDefault();
                string gname = "";
                if (kalagroup != null)
                    gname = kalagroup.Title;
                return Ok(new { res = q, msg = ""  , nameg = gname });
            }
            catch (Exception err)
            {
                return Ok(new { msg = err.Message });
            }
        }


        private class empmenu
        {
            public string mid { get; set; }
            public string namep { get; set; }
            public string codeparent { get; set; }
            public string typec { get; set; }
            public string css { get; set; }
            public string iskala { get; set; }
        }
        [HttpPost]
        public string getmenumain()
        {
            var paneltb = _context.PanelTb.Where(x => x.Codeparent == null).ToList();
            List<empmenu> _li = new List<empmenu>();
            foreach (var item in paneltb)
            {
                empmenu emp = new empmenu();
                emp.css = "";
                emp.codeparent = item.Codeparent.ToString();
                emp.mid = item.Mid.ToString();
                emp.namep = item.Namep.ToString();
                emp.typec = item.Typec.ToString();
                _li.Add(emp);
            }
            return JsonConvert.SerializeObject(_li);
        }

    }
}