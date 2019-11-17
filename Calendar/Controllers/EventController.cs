using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Models;
using Business.Services.Event;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    public class EventController : Controller
    {

        public IActionResult GetEvent([FromServices] IEventService service, int id)
        {
            Event @event = service.GetEvent(null, id);

            return Json(@event);
        }

        [HttpGet]
        public IActionResult GetEventList([FromServices] IEventService service, DateTime date)
        {
            var calendars = new List<Business.Models.Calendar>
              (service.GetEvents(null, date.Date, DateUnit.Day));

            List<BaseEvent> events = calendars.First(c => c.Id.Equals(2)).Events;

            return Json(events);
        }

        [HttpGet]
        public IActionResult GetEventListByCalendarId([FromServices] IEventService service, int calendarId)
        {
            return Json("success");
        }

        [HttpPost]
        public IActionResult CreateEvent([FromServices] IEventService service, [FromBody] Event @event)
        {
            @event.CalendarId = 2;
            int eventId = service.AddEvent(null, @event);

            return Json(eventId);
        }

        [HttpPost]
        public IActionResult EditEvent([FromServices] IEventService service, [FromBody] Event @event)
        {
            service.UpdateScheduledEvent(@event);

            return Json("success");
        }

        [HttpGet]
        public IActionResult DeleteEvent([FromServices] IEventService service, int id)
        {
            service.DeleteEvent(null, id);

            return Json("success");
        }
    }
}