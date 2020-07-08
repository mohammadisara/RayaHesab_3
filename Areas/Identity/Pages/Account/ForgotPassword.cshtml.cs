using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RayaHesab.Models;

namespace RayaHesab.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MyConfig _config;

        public ForgotPasswordModel(UserManager<IdentityUser> userManager, IOptions<MyConfig> Config)
        {
            _userManager = userManager;
            _config = Config.Value;
        }

        public async Task OnGetAsync()
        {
            ViewData["layoutlogin"] = _config.layoutlogin;
        }


        public async Task<int> Send(string mob, string text)
        {
            var api = await new SmsApi.SendSMSSoapClient(
                new SmsApi.SendSMSSoapClient.EndpointConfiguration() { }).SendAsync(
                _config.keysms, mob, text, "");

            //int _retStatus = p.SendGroupSMS("AFC3BD9D-9A09-498D-8948-339D78C8AF3E", "10009474", new string[] { mob }, note, false, string.Empty, ref _successCount, ref _ReturnStr);
            return api.Body.SendResult;
            //    return Ok(new { msg = "خطا در ارسال", err = "مجدد تلاش نمایید" });
            //return Ok(new { msg = "", err = "" });

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {
            var user = await _userManager.FindByNameAsync(Id);
            if (user == null)
            {
                // Don't reveal that the user does not exist or is not confirmed
                ModelState.AddModelError(string.Empty, "کاربر یافت نشد");
                return Page();
            }

            // For more information on how to enable account confirmation and password reset please 
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            try
            {
                string phonenumber = await _userManager.GetPhoneNumberAsync(user);
                string codec = new Random().Next(100000, 999999).ToString();
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, code, codec);
                if (result.Succeeded)
                {
                    int i = await Send(phonenumber, "کلمه عبور شما" + Environment.NewLine + codec);
                    if (i == 0)
                    {
                        ModelState.AddModelError(string.Empty, "خطا در ارسال پیامک لطفا مجدد تلاش نمایید");
                        return Page();

                    }
                    return RedirectToPage("/Account/Login", new { area = "Identity" , msg = "پیامک به تلفن همراه شما پیامک شد" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return Page();
            }

            //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var callbackUrl = Url.Page(
            //    "/Account/ResetPassword",
            //    pageHandler: null,
            //    values: new { code },
            //    protocol: Request.Scheme);

            //await _emailSender.SendEmailAsync(
            //    Input.Email,
            //    "Reset Password",
            //    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            //return RedirectToPage("./ResetPassword", new { Id = Id, Code = code });
        }
    }
}
