using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RayaHesab.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ResetPasswordModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Id { get; set; }
            public string code { get; set; }
        }

        public IActionResult OnGet(string Id , string Code)
        {
            if (Id == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                ViewData["id"] = Id;
                ViewData["code"] = Code;
                var user =  _userManager.FindByIdAsync(Id).Result;
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return BadRequest("A code must be supplied for password reset.");
                }
                ViewData["user"] = user.UserName;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(Input.Id);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return BadRequest("A code must be supplied for password reset.");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Usertbs");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            ViewData["id"] = Input.Id;
            ViewData["code"] = Input.code;
            return Page();
            //return RedirectToPage("./ResetPassword", new  {Id = Input.Id , Code = Input.code } );
        }
    }
}
