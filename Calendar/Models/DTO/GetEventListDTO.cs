using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models.DTO
{
    public class GetEventListDTO
    {
        public DateTime Date { get; set; }
        public int[] Calendars { get; set; }
        public int TimeOffset { get; set; }
    }
}
