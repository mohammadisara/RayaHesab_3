using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RayaHesab.Models;
using System.IO;
using Microsoft.Extensions.Options;

namespace RayaHesab.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly malisContext _context;
        private readonly MyConfig _config;

        public LoginModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> UserManager, malisContext Context, IOptions<MyConfig> Config)
        {
            _signInManager = signInManager;
            _userManager = UserManager;
            _context = Context;
            _config = Config.Value;
        }

        [BindProperty]
        public InputModel Input { get; set; }


        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [StringLength(4)]
            public string CaptchaCode { get; set; }
        }

        public async Task OnGetAsync( string msg , string returnUrl = null )
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            if (!string.IsNullOrEmpty(msg))
            {
                ModelState.AddModelError(string.Empty, msg);
            }
            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["layoutlogin"] = _config.layoutlogin ;
            ReturnUrl = returnUrl;
        }

        private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }
        }
        private void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.UtcNow.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.UtcNow.AddMinutes(60);
            Response.Cookies.Append(key, value, option);
        }
        private async Task<string> BuildToken(IdentityUser user)
        {
            var securityKey = Encoding.UTF8.GetBytes("7GardonApiCfaa48dsfdfgc4a69a444aa4564154b06b95a4Morteza");
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>{
                new Claim (JwtRegisteredClaimNames.Sub, user.Id),
                new Claim (JwtRegisteredClaimNames.Jti, System.Guid.NewGuid ().ToString ()),
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString())
            };
            AddRolesToClaims(claims, roles);

            var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            var signingKey = new SymmetricSecurityKey(securityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "http://localhost:55074/",
                Audience = "7Gardon",
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.Now.AddDays(365),
                //Subject = new ClaimsIdentity(claims),
                Subject = claimsIdentity,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            Set("token", token, 480);
            var u = await _context.AspNetUsers.FindAsync(user.Id);
            Set("fullname", u.Firstname + " " + u.Lastname, 480);
            Set("username", user.UserName, 480);
            //Set("logo", (p.LogoNavigation == null ? "" : p.LogoNavigation.Mpath), 600);
            Set("idanbar", u.Idanbar.ToString(), 480);
            Set("layoutlogin", _config.layoutlogin, 480);
            Set("layoutmain",_config.layoutmain, 480);
            return token;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                if (!Captcha.ValidateCaptchaCode(Input.CaptchaCode, HttpContext))
                {
                    ModelState.AddModelError(string.Empty, "کد امنیتی صحیح نمی باشد");
                    return Page();
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var user = await _userManager.FindByNameAsync(Input.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "نام کاربری و کلمه عبور اشتباه می باشد");
                    return Page();
                }
                var result = await _signInManager.PasswordSignInAsync(user, Input.Password,
                    false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    //if (user.PhoneNumberConfirmed != true)
                    //{
                    //    ModelState.AddModelError(string.Empty, "کاربر فعال نمی باشد");
                    //    return Page();
                    //}
                    var newRefreshToken = await BuildToken(user);
                    await _userManager.SetAuthenticationTokenAsync(user, "Default", "RefreshToken", newRefreshToken);
                    return RedirectToAction(_config.Index, "User");
                    //return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "ورود مجاز نمی باشد");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
