﻿using Business.Models;
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

            return PartialView("PartialViews/CreateEventForms/CreateEventPartial",
                new EventFormDTO(null, new Event(), new EventScheduleDropdown(date), calendars));
        }

        public IActionResult EventInfo(int id)
        {
            string user = userManager.GetUserId(User);
            var @event = eventService.GetEvent(user, id);
            var calendar = calendarService.GetCalendar(user, @event.CalendarId);

            return PartialView("PartialViews/PopUps/EventInfoPartial", new EventCalendarDTO(@event, calendar));
        }

        [Authorize]
        public IActionResult EditEventForm(int id)
        {
            string user = userManager.GetUserId(User);
            Event @event = eventService.GetEvent(user, id);
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