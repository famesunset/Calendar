using Business.Models;
using Business.Services.Event;
using Calendar.Models.ViewModelInitializers;
using Microsoft.AspNetCore.Mvc;
using Calendar.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Business.Services.User;
using Business.Services.Calendar;
using System;

namespace Calendar.Controllers
{
    [Authorize]
    public class EventViewController : Controller
    {        
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUserService userService;
        private readonly ICalendarService calendarService;
        private readonly IEventService eventService;

        public EventViewController(            
            UserManager<IdentityUser> userManager,
            [FromServices] IUserService userService,
            [FromServices] ICalendarService calendarService,
            [FromServices] IEventService eventService)
        {            
            this.userManager = userManager;
            this.userService = userService;
            this.calendarService = calendarService;
            this.eventService = eventService;
        }

        public IActionResult CreateEventForm(DateTime date)
        {
            string user = userManager.GetUserId(User);
            var calendars = calendarService.GetCalendars(user);
            var _event = new Event();

            return PartialView("PartialViews/CreateEventForms/CreateEventPartial",
                new EventFormDTO(null, _event, new EventScheduleDropdown(date), calendars));
        }

        public IActionResult EventInfo(int id, int timeOffset)
        {
            string user = userManager.GetUserId(User);
            var @event = eventService.GetEvent(user, id, timeOffset);
            var calendar = calendarService.GetCalendar(user, @event.CalendarId);  
            User creator = null;

            return PartialView("PartialViews/PopUps/EventInfoPartial", new EventInfoDTO(@event, calendar, creator));
        }

        [HttpPost]
        public IActionResult OpenSharedEventForm([FromBody] Event @event)
        {
            string user = userManager.GetUserId(User);            
            var calendars = calendarService.GetCalendars(user);

            return PartialView("PartialViews/CreateEventForms/CreateEventPartial",
                                new EventFormDTO(null, @event, new EventScheduleDropdown(@event.Start, @event.Repeat), calendars));
        }
        
        public IActionResult EditEventForm(int id, int timeOffset)
        {
            string user = userManager.GetUserId(User);
            Event @event = eventService.GetEvent(user, id, timeOffset);
            var calendar = calendarService.GetCalendar(user, @event.CalendarId);
            var calendars = calendarService.GetCalendars(user);

            return PartialView("PartialViews/CreateEventForms/CreateEventPartial",
                                new EventFormDTO(calendar, @event, new EventScheduleDropdown(@event.Start, @event.Repeat), calendars));
        }

        public IActionResult DeleteEventPopUp()
        {
            return PartialView("PartialViews/PopUps/DeleteEventPartial");
        }
    }
}