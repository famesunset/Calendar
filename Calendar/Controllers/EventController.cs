using System;
using Business.Models;
using Business.Services.Event;
using Business.Services.User;
using Calendar.Models;
using Calendar.Models.DTO;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IEventService eventService;
        private readonly HangfireEvent hangfire;

        public EventController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            [FromServices] IEventService eventService,
            [FromServices] IUserService userService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.eventService = eventService;
            this.hangfire = new HangfireEvent(userService);
        }


        public IActionResult GetEvent(int id)
        {
            Event @event = eventService.GetEvent(userManager.GetUserId(User), id);
            return Json(@event);
        }

        [HttpGet]
        public IActionResult GetEventList(DateTime date, int[] calendars, int timeOffset)
        {
            var events = eventService.GetEvents(userManager.GetUserId(User), date, DateUnit.Day, calendars, timeOffset);
            return Json(events);
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] EventWithTimeOffsetDTO @event)
        {   
            int eventId = eventService.CreateEvent(userManager.GetUserId(User), @event.Event, @event.Offset);
            if (eventId > 0)
            {
                @event.Event.Id = eventId;
                this.hangfire.ScheduleNotification(@event.Event, @event.Offset);
            }
            return Json(eventId);
        }

        [HttpGet]
        public IActionResult GenerateEventLink(int id)
        {
            string domainName = $"{Request.Scheme}://{Request.Host}";
            var link = eventService.GetEventLink(userManager.GetUserId(User), id, domainName);

            return Json(link);
        }

        [HttpPost]
        public IActionResult EditEvent([FromBody] EventWithTimeOffsetDTO @event)
        {
            eventService.UpdateEvent(userManager.GetUserId(User), @event.Event);
            return Json("success");
        }

        [HttpGet]

        public IActionResult DeleteEvent(int id)
        {
            eventService.DeleteEvent(userManager.GetUserId(User), id);
            return Json("success");
        }
    }
}