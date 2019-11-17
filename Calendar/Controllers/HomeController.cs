using Microsoft.AspNetCore.Mvc;
using Business.Models;
using Business.Services.Event;
using System.Collections.Generic;
using System;
using Business;
using System.Linq;

namespace Calendar.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
