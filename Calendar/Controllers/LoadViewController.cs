using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_Layer.Models;
using Business_Layer.Services.Event;
using Calendar.Models.ViewModelInitializers;
using Microsoft.AspNetCore.Mvc;
using Calendar.Models.DTO;

namespace Calendar.Controllers
{
    public class LoadViewController : Controller
    {
        public IActionResult CreateEventForm()
        {
            return PartialView("PartialViews/CreateEventForms/CreateEventPartial",
                new EventAndOptionsDropdownDTO(new Event(), new EventScheduleDropdown()));
        }

        public IActionResult UpdateEventForm([FromServices] IEventService service, int id)
        {
            // TODO: Pull the event from database by id

            Event @event = new Event()
            {
                Id = id,
                Description = "Test description",
                Title = "Test title",
                Start = DateTime.Now,
                Finish = DateTime.Now.AddHours(1),
                IsAllDay = false
            };

            return PartialView("PartialViews/CreateEventForms/CreateEventPartial", 
                                new EventAndOptionsDropdownDTO(@event, new EventScheduleDropdown()));
        }

        public IActionResult EventMoreOptions()
        {           
            return PartialView("PartialViews/CreateEventForms/MoreOptionsPartial", new EventScheduleDropdown());
        }    
        
        public IActionResult DeleteEventPopUp()
        {
            return PartialView("PartialViews/PopUps/DeleteEventPartial");
        }
    }
}