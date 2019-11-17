using System;

namespace Data.Models
{
    public class EventSchedule
    {
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }

        public EventSchedule(DateTime timeStart, DateTime timeFinish)
        {
            this.TimeStart = timeStart;
            this.TimeFinish = timeFinish;
        }

        public EventSchedule()
        {
                
        }
    }
}