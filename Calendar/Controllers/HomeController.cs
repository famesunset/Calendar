using Microsoft.AspNetCore.Mvc;
using Business_Layer.Models;
using Business_Layer.Services;
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

        [HttpPost]
        public void CreateEvent([FromServices] IEventService service, [FromBody] Event @event)
        {
            @event.CalendarId = 2;
            service.AddEvent(null, 0, @event);
        }

        [HttpGet]
        public IActionResult GetEventList([FromServices] IEventService service)
        {
            var calendars = new List<Business_Layer.Models.Calendar>
                (service.GetEvents(null, DateTime.Now, DateUnit.Day));
            
            List<BaseEvent> events = calendars.First(c => c.Id.Equals(2)).Events;

            for (int i = 0; i < 1; ++i)
            {
                //events.Add(new Event()
                //{
                //    Id = i + 1,
                //    Color = "#fff",
                //    Description = $"Description {i + 1}",
                //    Title = $"Title {i + 1}",
                //    TimeStart = DateTime.Now.AddHours(-1),
                //    TimetFinish = DateTime.Now
                //});
            }

            return Json(events);
        }
    }
}
