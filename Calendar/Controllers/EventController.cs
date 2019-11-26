﻿using System;
using Business.Models;
using Business.Services.Event;
using Calendar.Models.DTO;
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

        [Authorize]
        [HttpGet]
        public IActionResult GetEventList(DateTime date, int[] calendars, int timeOffset)
        {
            var events = eventService.GetEvents(userManager.GetUserId(User), date, DateUnit.Day, calendars);
            return Json(events);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateEvent([FromBody] EventWithTimeOffsetDTO @event)
        {   
            int eventId = eventService.CreateEvent(userManager.GetUserId(User), @event.Event, @event.Offset);
            return Json(eventId);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditEvent([FromBody] EventWithTimeOffsetDTO @event)
        {
            eventService.UpdateEvent(userManager.GetUserId(User), @event.Event);
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