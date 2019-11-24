using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Models;
using Business.Services.Calendar;
using Business.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    public class CalendarController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUserService userService;
        private readonly ICalendarService calendarService;

        public CalendarController(
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
        public IActionResult GetCalendar(int id)
        {
            string user = userManager.GetUserId(User);
            var calendar = calendarService.GetCalendar(user, id);

            return Json(calendar);
        }

        public IActionResult GetCalendarList()
        {
            string user = userManager.GetUserId(User);
            var calendars = calendarService.GetCalendars(user);            

            return Json(calendars);
        }

        [HttpGet]
        public IActionResult CreateCalendar(string name, int colorId)
        {
            var color = calendarService.GetCalendarColors().Where(c => c.Id == colorId).FirstOrDefault();
            var calendar = new Business.Models.Calendar()
            {
                Name = name,
                Color = color,
                Access = Access.Private
            };

            string user = userManager.GetUserId(User);
            int id = calendarService.CreateCalendar(user, calendar);            

            return Json(id);
        }
    
        [HttpGet]
        public IActionResult DeleteCalendar(int id)
        {
            string user = userManager.GetUserId(User);
            calendarService.DeleteCalendar(user, id);

            return Json("success");
        }
    }
}