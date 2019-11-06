using Microsoft.AspNetCore.Mvc;
using Business_Layer.Models;
using Business_Layer.Services;
using System.Collections.Generic;
using System;
using Business_Layer;

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
            int calendarId = 2;
            //service.AddEvent(null, calendarId, @event);
        }

        [HttpGet]
        public IActionResult GetEventList([FromServices] IEventService service)
        {
            // List<Event> events = new List<Event>(service.GetAllEvents(null, DateTime.Now, DateUnit.Month));
            // модели дата слоя пока не совпадают с бд
            List<Event> events = new List<Event>();

            for (int i = 0; i < 1; ++i)
            {
                events.Add(new Event()
                {
                    Id = i + 1,
                    Color = "#fff",
                    Description = $"Description {i + 1}",
                    Title = $"Title {i + 1}",
                    Start = DateTime.Now.AddHours(-1),
                    Finish = DateTime.Now
                });
            }

            return Json(events);
        }
    }
}
