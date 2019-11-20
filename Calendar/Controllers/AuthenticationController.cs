using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Business.Models;
using Business.Services.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public string LoginProvider { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult Index()
        {
            return RedirectToPage("/");
        }

        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        [HttpGet]
        public IActionResult GoogleLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action("GetCallback", "Authentication", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> GetCallback([FromServices]IUserService userService, string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return Redirect("/");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return Redirect("/");
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                var props = new AuthenticationProperties();
                props.StoreTokens(info.AuthenticationTokens);
                await _signInManager.SignInAsync(user, props, info.LoginProvider);

                return Redirect(returnUrl);
            }
            else
            {
                var userInfo = info.Principal.Identities.First();
                var email = userInfo.FindFirst(ClaimTypes.Email);
                var picture = userInfo.FindFirst("picture");
                var user = new IdentityUser { UserName = email.Value, Email = email.Value};
                userService.CreateUser(new User { IdentityId =  user.Id, Name = userInfo.Name, Email = email.Value, Picture = picture.Value });
                var identityResult = await _userManager.CreateAsync(user);
                if (identityResult.Succeeded)
                {
                    identityResult = await _userManager.AddLoginAsync(user, info);
                    if (identityResult.Succeeded)
                    {
                        var props = new AuthenticationProperties();
                        props.StoreTokens(info.AuthenticationTokens);
                        await _signInManager.SignInAsync(user, props, authenticationMethod: info.LoginProvider);
                        return Redirect(returnUrl);
                    }
                }
            }
            return Redirect("/");
        }
    }
}