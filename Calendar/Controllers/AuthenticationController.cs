using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Models;
using Business.Services.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUserService userService;

        public AuthenticationController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            [FromServices] IUserService userService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;

            this.userService = userService;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult Index()
        {
            return RedirectToPage("/");
        }

        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await signInManager.SignOutAsync();
            // TODO: удалить browserId из бд
            return Redirect(returnUrl);
        }

        [HttpGet]
        public IActionResult GoogleLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action("GetCallback", "Authentication", values: new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> GetCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return Redirect("/");
            }
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return Redirect("/");
            }

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            // есть ли пользователь в бд
            if (result.Succeeded)
            {
                var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                var props = new AuthenticationProperties();
                props.StoreTokens(info.AuthenticationTokens);
                await signInManager.SignInAsync(user, props, info.LoginProvider);

                return Redirect(returnUrl);
            }
            return await Register(returnUrl, info);
        }

        private async Task<IActionResult> Register(string returnUrl, ExternalLoginInfo info)
        {
            var userInfo = info.Principal.Identities.First();
            var email = userInfo.FindFirst(ClaimTypes.Email);
            var picture = userInfo.FindFirst("picture");
            var user = new IdentityUser { UserName = email.Value, Email = email.Value };
            userService.CreateUser(new User { IdentityId = user.Id, Name = userInfo.Name, Email = email.Value, Picture = picture.Value });
            var identityResult = await userManager.CreateAsync(user);
            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddLoginAsync(user, info);
                if (identityResult.Succeeded)
                {
                    var props = new AuthenticationProperties();
                    props.StoreTokens(info.AuthenticationTokens);
                    await signInManager.SignInAsync(user, props, authenticationMethod: info.LoginProvider);
                    return Redirect(returnUrl);
                }
            }
            return Redirect("/");
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddBrowserId(string token)
        {
            userService.AddBrowser(userManager.GetUserId(User), token);
            return Ok();
        }
    }
}