using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Models;
using Business.Services.Calendar;
using Business.Services.User;
using Calendar.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    public class CalendarController : Controller
    {        
        private readonly UserManager<IdentityUser> userManager;        
        private readonly ICalendarService calendarService;
        private readonly IUserService userService;

        public CalendarController(            
            UserManager<IdentityUser> userManager,            
            [FromServices] ICalendarService calendarService,
            [FromServices] IUserService userService)
        {            
            this.userManager = userManager;            
            this.calendarService = calendarService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult GetCalendar(int id)
        {
            string user = userManager.GetUserId(User);
            var calendar = calendarService.GetCalendar(user, id);

            return Json(calendar);
        }

        [HttpGet]        
        public IActionResult GetCalendarList()
        {
            string user = userManager.GetUserId(User);
            var calendars = calendarService.GetCalendars(user);            

            return Json(calendars);
        }

        [HttpPost]
        public IActionResult CreateCalendar([FromBody] CreateCalendarDTO calendarDTO)
        {
            string user = userManager.GetUserId(User);
            int id = calendarService.CreateCalendar(user, calendarDTO.Name, calendarDTO.ColorId, Access.Private);            
            return Json(id);
        }
    
        [HttpGet]
        public IActionResult DeleteCalendar(int id)
        {
            string user = userManager.GetUserId(User);
            calendarService.DeleteCalendar(user, id);

            return Json("success");
        }

        [HttpGet]
        public IActionResult UnsubscribeCalendar(int id)
        {
            string user = userManager.GetUserId(User);
            calendarService.UnsubscribeUser(user, id);

            return Json("success");
        }

        [HttpPost]
        public IActionResult SubscribeCalendar([FromBody] SubscribeCalendarDTO calendarDTO)
        {
            var user = userService.GetUserByEmail(calendarDTO.Email);
            if (user != null)
            {
                calendarService.SubscribeUser(user.Id, calendarDTO.CalendarId);
            }
            return Json("Success");
        }
    }
}