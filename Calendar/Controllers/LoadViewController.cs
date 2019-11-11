﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Models.ViewModelInitializers;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    public class LoadViewController : Controller
    {
        public IActionResult CreateEventForm()
        {
            return PartialView("PartialViews/CreateEventForms/CreateEventPartial");
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