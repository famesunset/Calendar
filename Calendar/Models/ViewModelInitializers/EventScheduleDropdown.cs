﻿using Business.Models;
using Calendar.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models.ViewModelInitializers
{
    public class EventScheduleDropdown
    {
        public Interval Interval { get; set; }
        public DateTime Day { get; set; }        
        public List<IntervalDropdownItem> Items { get; set; }

        public EventScheduleDropdown(DateTime date)
        {
            Day = date;
            Items = new List<IntervalDropdownItem>();

            InitContent();
        }

        public EventScheduleDropdown(DateTime date, Interval interval) : this(date)
        {
            Interval = interval;
        }

        private void InitContent()
        {
            var thTH = new System.Globalization.CultureInfo("en-US");

            string noRepeat = "Does not repeat";
            string everyDay = "Daily";
            string everyWeek = $"Weekly on {Day.ToString("dddd", thTH)}"; 
            string everyMonth = $"Monthly on the {Day.Day}"; 
            string everyYear = $"Annually on {Day.ToString("MMM", thTH)} {Day.Day}";

            Items.Add(new IntervalDropdownItem("no-repeat", noRepeat, Interval.NoRepeat));
            Items.Add(new IntervalDropdownItem("everyday", everyDay, Interval.Day));
            Items.Add(new IntervalDropdownItem("every-week", everyWeek, Interval.Week));
            Items.Add(new IntervalDropdownItem("every-month", everyMonth, Interval.Month));
            Items.Add(new IntervalDropdownItem("every-year", everyYear, Interval.Year));
        }
    }
}
