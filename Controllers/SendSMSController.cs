using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RayaHesab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace SendSMS.Asp.Net.Core.Controllers
{
    public class SendSMSController : Controller
    {
        private readonly malisContext _context;
        private readonly MyConfig _config;

        public SendSMSController(malisContext context, IOptions<MyConfig> Config)
        {
            _context = context;
            _config = Config.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Send(string mob, string text)
        {
            var api = await new SmsApi.SendSMSSoapClient(
                new SmsApi.SendSMSSoapClient.EndpointConfiguration() { }).SendAsync(
                _config.keysms, mob, text, "");

            //int _retStatus = p.SendGroupSMS("AFC3BD9D-9A09-498D-8948-339D78C8AF3E", "10009474", new string[] { mob }, note, false, string.Empty, ref _successCount, ref _ReturnStr);
            if (api.Body.SendResult == 0)
            {
                return Ok(new { msg = "خطا در ارسال", err = "مجدد تلاش نمایید" });
            }
            return Ok(new { msg = "", err = "" });

        }
    }
    //[HttpPost]
    //public async Task<IActionResult> Send(SendSMS.Asp.Net.Core.Models.SendSms_RequestModel req)
    //{
    //    var api = await new SmsApi.SendSMSSoapClient(
    //        new SmsApi.SendSMSSoapClient.EndpointConfiguration() { }).SendAsync(
    //        "3579C9C7-6527-4D03-9402-E91275F3A429", req.Mobile, req.Text, "");
    //    ViewBag.SendResult = api.Body.SendResult;
    //    ViewBag.retStr = api.Body.retStr;

    //    return View();
    //}

}
