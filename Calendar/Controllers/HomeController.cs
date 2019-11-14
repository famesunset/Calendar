using Microsoft.AspNetCore.Mvc;
using Business_Layer.Models;
using Business_Layer.Services.Event;
using System.Collections.Generic;
using System;
using Business_Layer;
using System.Linq;

namespace Calendar.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetEvent([FromServices] IEventService service, int id)
        {
            Event @event = service.GetEvent(null, id);

            return Json(@event);
        }

        [HttpPost]
        public IActionResult CreateEvent([FromServices] IEventService service, [FromBody] Event @event)
        {
            @event.CalendarId = 2;
            int eventId = service.AddEvent(null, @event);

            return Json(eventId);
        }


        [HttpGet]
        public IActionResult GetEventList([FromServices] IEventService service, DateTime date)
        {
            var calendars = new List<Business_Layer.Models.Calendar>
              (service.GetEvents(null, date.Date, DateUnit.Day));

            List<BaseEvent> events = calendars.First(c => c.Id.Equals(2)).Events;

            return Json(events);
        }

        [HttpPost]
        public void EditEvent([FromServices] IEventService service, [FromBody] Event @event)
        {
            ;
        }

        [HttpGet]
        public IActionResult DeleteEvent([FromServices] IEventService service, int id)
        {
            // TODO: 
            ;
            service.DeleteEvent(null, id);

            return Json("success");
        }
    }
}
