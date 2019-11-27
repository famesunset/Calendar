using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services.Calendar;
using Business.Services.User;
using Calendar.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    [Authorize]
    public class PopUpController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUserService userService;
        private readonly ICalendarService calendarService;

        public PopUpController(
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
        public IActionResult PopUpWindow()
        {
            return PartialView("PartialViews/PopUps/PopUpPartial");
        }

        [HttpGet]
        public IActionResult ShareCalendar(int calendarId, string email)
        {
            return Json("Success");
        }

        [HttpGet]
        public IActionResult DeleteCalendarMessage(int id)
        {
            string user = userManager.GetUserId(User);
            var calendar = calendarService.GetCalendar(user, id);

            return PartialView("PartialViews/PopUps/Content/DeleteCalendarPartial", calendar);
        }

        [HttpGet]
        public IActionResult ShareCalendarConfirm(string email, int calendarId)
        {
            var loginedUser = userManager.GetUserId(User);
            var user = userService.GetUserByEmail(email);
            var calendar = calendarService.GetCalendar(loginedUser, calendarId);

            return PartialView("PartialViews/PopUps/Content/ShareCalendarPartial", new ShareCalendarDTO(user, calendar));
        }
    }
}