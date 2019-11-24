using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models.DTO
{
    public class CalendarDTO
    {
        public Business.Models.Calendar Calendar { get; set; }
        public IEnumerable<Business.Models.Calendar> Calendars { get; set; }

        public CalendarDTO (
            Business.Models.Calendar _calendar,
            IEnumerable<Business.Models.Calendar> _calendars)
        {
            Calendar = _calendar;
            Calendars = _calendars;
        }
    }
}
