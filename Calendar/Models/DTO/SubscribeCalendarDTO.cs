using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models.DTO
{
    public class SubscribeCalendarDTO
    {
        public string Email { get; set; }
        public int CalendarId { get; set; }
    }
}
