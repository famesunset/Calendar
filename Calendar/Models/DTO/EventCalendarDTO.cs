using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Models;

namespace Calendar.Models.DTO
{
    public class EventCalendarDTO
    {
        public Event Event { get; set; }
        public Business.Models.Calendar Calendar { get; set; }

        public EventCalendarDTO(Event _event, Business.Models.Calendar _calendar)
        {
            Event = _event;
            Calendar = _calendar;
        }
    }
}
