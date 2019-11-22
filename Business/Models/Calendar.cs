namespace Business.Models
{
    using System.Collections.Generic;
    public class Calendar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Access Access { get; set; }
        public List<User> Users { get; set; }
        public List<BaseEvent> Events { get; set; }
        public Color Color { get; set; }
        public int UserOwnerId { get; set; }
    }
}