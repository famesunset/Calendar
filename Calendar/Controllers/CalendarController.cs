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
        private readonly UserManager<IdentityUser> userManager;        
        private readonly ICalendarService calendarService;

        public CalendarController(            
            UserManager<IdentityUser> userManager,            
            [FromServices] ICalendarService calendarService)
        {            
            this.userManager = userManager;            
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
            string user = userManager.GetUserId(User);
            int id = calendarService.CreateCalendar(user, name, colorId, Access.Private);            
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
            // TODO

            return Json("success");
        }
    }
}