using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Models;
using Business.Services.Calendar;
using Business.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    [Authorize]
    public class CalendarViewController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUserService userService;

        public CalendarViewController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            [FromServices] IUserService userService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userService = userService;
        }

        [HttpGet]        
        public IActionResult GetList([FromServices] ICalendarService service)
        {
            var calendars = service.GetCalendars(userManager.GetUserId(User));

            return PartialView("PartialViews/SideBar/MyCalendarsPartial", calendars);
        }

        [HttpGet]
        public IActionResult GetCreateCalendarForm()
        {
            List<Color> colors = new List<Color>()
            {
                new Color(1, "#9E69AF"),
                new Color(2, "#039BE5"),
                new Color(3, "#3F51B5"),
                new Color(4, "#0B8043"),
                new Color(5, "#F6BF26"),
                new Color(6, "#D50000"),  //Red
            };

            return PartialView("PartialViews/PopUps/CreateCalendarPartial", colors);
        }
    }
}