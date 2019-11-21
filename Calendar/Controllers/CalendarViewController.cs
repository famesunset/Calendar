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
    }
}