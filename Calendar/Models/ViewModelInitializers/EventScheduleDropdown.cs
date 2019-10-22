using Calendar.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models.ViewModelInitializers
{
    public class EventScheduleDropdown
    {        
        public DateTime Today { get; set; }
        public List<DropdownItem> Items { get; set; }

        public EventScheduleDropdown()
        {
            Today = DateTime.UtcNow;
            Items = new List<DropdownItem>();

            InitContent();
        }

        private void InitContent()
        {
            var thTH = new System.Globalization.CultureInfo("en-US");

            string everyDay = "Everyday";
            string everyWeek = $"Every week on {Today.ToString("dddd", thTH)}"; 
            string everyMonth = $"Every month of the {Today.Day}"; 
            string everyYear = $"Every year on {Today.ToString("MMM", thTH)} {Today.Day}";

            Items.Add(new DropdownItem("everyday", everyDay));
            Items.Add(new DropdownItem("every-week", everyWeek));
            Items.Add(new DropdownItem("every-month", everyMonth));
            Items.Add(new DropdownItem("every-year", everyYear));

        }
    }
}
