using Business.Models;
using Business.Services.Event;
using Calendar.Models.ViewModelInitializers;
using Microsoft.AspNetCore.Mvc;
using Calendar.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Calendar.Controllers
{
    public class EventViewController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IEventService eventService;

        public EventViewController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            [FromServices] IEventService eventService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.eventService = eventService;
        }

        public IActionResult CreateEventForm()
        {
            return PartialView("PartialViews/CreateEventForms/CreateEventPartial",
                new EventAndOptionsDropdownDTO(new Event(), new EventScheduleDropdown()));
        }

        [Authorize]
        public IActionResult EditEventForm(int id)
        {
            Event @event = eventService.GetEvent(userManager.GetUserId(User), id);
            return PartialView("PartialViews/CreateEventForms/CreateEventPartial",
                                new EventAndOptionsDropdownDTO(@event, new EventScheduleDropdown()));
        }

        public IActionResult DeleteEventPopUp()
        {
            return PartialView("PartialViews/PopUps/DeleteEventPartial");
        }
    }
}