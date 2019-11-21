using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Models;
using Business.Services.Calendar;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    public class CalendarController : Controller
    {

        public IActionResult GetCalendarList([FromServices] ICalendarService service)
        {
            var calendars = service.GetCalendars(null);

            return Json(calendars);
        }

        [HttpGet]
        public IActionResult GetCalendarColorsList()
        {
            List<Color> colors = new List<Color>()
            {
                new Color(1, "#D50000"),  //Red
                new Color(2, "#0B8043"),
                new Color(3, "#039BE5"),
                new Color(4, "#3F51B5"),
                new Color(5, "#F6BF26"),
                new Color(6, "#9E69AF")
            };

            return Json(colors);
        }
    }
}