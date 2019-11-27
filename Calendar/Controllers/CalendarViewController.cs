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
        private readonly ICalendarService calendarService;

        public CalendarViewController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            [FromServices] IUserService userService,
            [FromServices] ICalendarService calendarService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userService = userService;
            this.calendarService = calendarService;
        }

        [HttpGet]        
        public IActionResult GetList()
        {
            var calendars = calendarService.GetCalendars(userManager.GetUserId(User));

            return PartialView("PartialViews/SideBar/MyCalendarsPartial", calendars);
        }

        [HttpGet]
        public IActionResult GetCalendarView(int id)
        {
            string user = userManager.GetUserId(User);
            Business.Models.Calendar calendar = calendarService.GetCalendar(user, id);

            return PartialView("PartialViews/SideBar/CalendarPartial", calendar);
        }

        [HttpGet]
        public IActionResult GetCreateCalendarForm()
        {
            List<Color> colors = calendarService.GetCalendarColors().ToList();

            return PartialView("PartialViews/PopUps/CreateCalendarPartial", colors);
        }

        [HttpGet]
        public IActionResult ShareCalendarForm(int id)
        {
            string user = userManager.GetUserId(User);
            Business.Models.Calendar calendar = calendarService.GetCalendar(user, id);

            return PartialView("PartialViews/PopUps/ShareCalendarFormPartial", calendar);
        }
    }
}