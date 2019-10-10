using System.Collections.Generic;

namespace Business_Layer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public List<Calendar> Calendars { get; set; }
    }
}