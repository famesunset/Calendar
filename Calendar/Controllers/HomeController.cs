using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Business_Layer;
using Business_Layer.Models;

namespace Calendar.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public void CreateEvent([FromServices] IService service, [FromBody] Event @event)
        {
            @event.CalendarId = 2;
            service.AddEvent(null, @event);
        }
    }
}
