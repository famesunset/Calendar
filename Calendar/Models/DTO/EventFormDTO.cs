using Calendar.Models.ViewModelInitializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Models;

namespace Calendar.Models.DTO
{
    public class EventFormDTO
    {        
        public Event Event { get; set; }
        public EventScheduleDropdown Dropdown { get; set; }
        public CalendarDTO CalendarDTO { get; set; }        

        public EventFormDTO(
            Business.Models.Calendar _calendar,
            Event _event,
            EventScheduleDropdown _dropdown,            
            IEnumerable<Business.Models.Calendar> _calendars)
        {            
            Event = _event;
            Dropdown = _dropdown;
            CalendarDTO = new CalendarDTO(_calendar, _calendars);
        }
    }
}
