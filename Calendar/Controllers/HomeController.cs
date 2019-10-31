using Microsoft.AspNetCore.Mvc;
using Business_Layer.Models;
using Business_Layer.Services;

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
            service.AddEvent(null, calendarId, @event);
        }
    }
}
