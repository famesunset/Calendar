using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models.DTO
{
    public class EventWithTimeOffestDTO 
    {
        public Business.Models.Event Event { get; set; }
        public int Offset { get; set; }
    }
}
