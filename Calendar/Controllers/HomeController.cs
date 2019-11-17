using Microsoft.AspNetCore.Mvc;
using Business.Services.Event;
using Microsoft.AspNetCore.Identity;
using Business.Services.User;

namespace Calendar.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUserService userService;

        public HomeController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            [FromServices] IUserService userService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                var user = userService.GetUserByIdentityId(userManager.GetUserId(User));
                ViewBag.Avatar = user?.Picture;
            }

            return View();
        }
    }
}
