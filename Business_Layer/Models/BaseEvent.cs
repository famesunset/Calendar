namespace Business_Layer.Models
{
    using System;

    public class BaseEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public string Color { get; set; }
        // HEX
        public bool IsAllDay { get; set; }
    }
}