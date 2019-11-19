using System.Collections.Generic;

namespace Business.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Access Access { get; set; }
        public List<User> Users { get; set; }
        public List<BaseEvent> Events { get; set; }
        public string Color { get; set; }
        public int UserOwnerId { get; set; }
        // HEX Color
    }
}