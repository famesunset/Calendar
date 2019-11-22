﻿using System;
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
    }
}