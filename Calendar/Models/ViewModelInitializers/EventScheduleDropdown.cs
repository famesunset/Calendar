using Calendar.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models.ViewModelInitializers
{
    public class EventScheduleDropdown
    {
        private DateTime today = DateTime.UtcNow;
        public List<DropdownItem> Items { get; set; }

        public EventScheduleDropdown()
        {
            Items = new List<DropdownItem>();

            InitContent();
        }

        private void InitContent()
        {
            var thTH = new System.Globalization.CultureInfo("en-US");

            string everyDay = "Everyday";
            string everyWeek = $"Every week on {today.ToString("dddd", thTH)}"; 
            string everyMonth = $"Every month of the {today.Day}"; 
            string everyYear = $"Every year on {today.ToString("MMM", thTH)} {today.Day}";

            Items.Add(new DropdownItem("everyday", everyDay));
            Items.Add(new DropdownItem("every-week", everyWeek));
            Items.Add(new DropdownItem("every-month", everyMonth));
            Items.Add(new DropdownItem("every-year", everyYear));
        }
    }
}
