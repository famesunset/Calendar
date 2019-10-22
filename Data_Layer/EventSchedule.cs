using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    class EventSchedule
    {
        public int Id { get; set; }
        public int IdEvent { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public DateTime TimePeriodStart { get; set; }
        public DateTime TimePeriodFinish { get; set; }
        public EventSchedule()
        {

        }
    }
}
