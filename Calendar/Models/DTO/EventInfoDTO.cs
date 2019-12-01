using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Models;

namespace Calendar.Models.DTO
{
    public class EventInfoDTO
    {
        public Event Event { get; set; }
        public Business.Models.Calendar Calendar { get; set; }

        public User Creator { get; set; }

        public EventInfoDTO(Event _event, Business.Models.Calendar _calendar, User _creator)
        {
            Event = _event;
            Calendar = _calendar;
            Creator = _creator;
        }
    }
}
