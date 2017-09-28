using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Blog.Core.Users.Model;

namespace Blog.Admin.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<User> _userManager;

        public LoginModel(SignInManager<User> signInManager, ILogger<LoginModel> logger, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "必填")]
            [EmailAddress(ErrorMessage = "邮箱格式不正确")]
            public string Email { get; set; }

            [Required(ErrorMessage = "必填")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        //public async Task OnGetAsync(string returnUrl = null)
        //{
        //    if (!string.IsNullOrEmpty(ErrorMessage))
        //    {
        //        ModelState.AddModelError(string.Empty, ErrorMessage);
        //    }

        //    // Clear the existing external cookie to ensure a clean login process
        //    await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        //    ReturnUrl = returnUrl;
        //}

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? "/Index";

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var user = await _userManager.FindByNameAsync(Input.Email);
                var result = await _signInManager.PasswordSignInAsync(user, Input.Password,false,false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(Url.Page(ReturnUrl));
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "登入失败");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
