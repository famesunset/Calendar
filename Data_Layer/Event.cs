using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public class Event
    {
        public int id { get; set; }
        public int id_Calendar { get; set; }
        public string Description { get; set; }
        public string Notification { get; set; }
        public string Title { get; set; }

        /// <summary>
        /// Get data from view to put it into the db.
        /// </summary>
        /// <param name="id_User">id of User</param>
        /// <param name="id_Calendar">id of Calendar</param>
        /// <param name="Title">Title of the Event</param>>
        /// <param name="Description">Description of the Event</param>
        /// <param name="EventTimeStart">The event starts at this time (day, hours and minutes)</param>
        /// <param name="EventTimeFinish">The Event starts at this time (day, hours and minutes)</param>
        /// <param name="EventPeriodStart">Datetime of the event when it is started (day period since)</param>
        /// <param name="EventPeriodFinish">Datetime of the event when it is finished (day period till)</param>
        public void GetEventData(int id_User, int id_Calendar, string Title, string Description, DateTime EventTimeStart, DateTime EventTimeFinish, DateTime EventPeriodStart, DateTime EventPeriodFinish)
        {
            
        }
    }
}
