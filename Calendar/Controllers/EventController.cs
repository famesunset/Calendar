using System;
using System.Collections.Generic;
using System.Linq;
using Business.Models;
using Business.Services.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    public class EventController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IEventService eventService;

        public EventController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            [FromServices] IEventService eventService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.eventService = eventService;
        }

        [Authorize]
        public IActionResult GetEvent(int id)
        {
            Event @event = eventService.GetEvent(userManager.GetUserId(User), id);
            return Json(@event);
        }

        [HttpGet]
        public IActionResult GetEventList(DateTime date)
        {
            if (signInManager.IsSignedIn(User))
            {
                var calendars = new List<Business.Models.Calendar>
                              (eventService.GetEvents(userManager.GetUserId(User), date.Date, DateUnit.Day));

                var events = calendars.SelectMany(c => c.Events).ToList();
                return Json(events);
            }
            return Json(new BaseEvent[0]);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetEventListByCalendarId(int calendarId)
        {
            return Json("success");
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateEvent([FromBody] Event @event)
        {
            @event.CalendarId = 2;
            int eventId = eventService.CreateEvent(userManager.GetUserId(User), @event);

            return Json(eventId);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditEvent([FromBody] Event @event)
        {
            eventService.UpdateScheduledEvent(userManager.GetUserId(User), @event);
            return Json("success");
        }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteEvent(int id)
        {
            eventService.DeleteEvent(userManager.GetUserId(User), id);
            return Json("success");
        }
    }
}