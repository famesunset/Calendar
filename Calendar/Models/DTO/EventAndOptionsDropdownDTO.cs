using Calendar.Models.ViewModelInitializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Models;

namespace Calendar.Models.DTO
{
    public class EventAndOptionsDropdownDTO
    {
        public Event Event { get; set; }
        public EventScheduleDropdown Dropdown { get; set; }

        public EventAndOptionsDropdownDTO(Event _event, EventScheduleDropdown _dropdown)
        {
            Event = _event;
            Dropdown = _dropdown;
        }
    }
}
