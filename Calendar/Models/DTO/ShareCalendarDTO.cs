using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models.DTO
{
    public class ShareCalendarDTO
    {
        public Business.Models.User User { get; set; }
        public Business.Models.Calendar Calendar { get; set; }

        public ShareCalendarDTO(Business.Models.User _user, Business.Models.Calendar _calendar)
        {
            User = _user;
            Calendar = _calendar;
        }
    }
}
