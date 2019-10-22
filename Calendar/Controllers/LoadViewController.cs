using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Models.ViewModelInitializers;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    public class LoadViewController : Controller
    {
        public IActionResult EventMoreOptions()
        {           
            return PartialView("CreateEventForms/MoreOptionsPartial", new EventScheduleDropdown());
        }  
    }
}