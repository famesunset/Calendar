using Business.Models;
using Calendar.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models.ViewModelInitializers
{
    public class EventScheduleDropdown
    {        
        public DateTime Day { get; set; }
        public List<IntervalDropdownItem> Items { get; set; }

        public EventScheduleDropdown(DateTime date)
        {
            Day = date;
            Items = new List<IntervalDropdownItem>();

            InitContent();
        }

        private void InitContent()
        {
            var thTH = new System.Globalization.CultureInfo("en-US");

            string noRepeat = "Don't repeat";
            string everyDay = "Everyday";
            string everyWeek = $"Every week on {Day.ToString("dddd", thTH)}"; 
            string everyMonth = $"Every month of the {Day.Day}"; 
            string everyYear = $"Every year on {Day.ToString("MMM", thTH)} {Day.Day}";

            Items.Add(new IntervalDropdownItem("no-repeat", noRepeat, Interval.NoRepeat));
            Items.Add(new IntervalDropdownItem("everyday", everyDay, Interval.Day));
            Items.Add(new IntervalDropdownItem("every-week", everyWeek, Interval.Week));
            Items.Add(new IntervalDropdownItem("every-month", everyMonth, Interval.Month));
            Items.Add(new IntervalDropdownItem("every-year", everyYear, Interval.Year));
        }
    }
}
