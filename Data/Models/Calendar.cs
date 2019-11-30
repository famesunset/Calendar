namespace Data.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccessId { get; set; }
        public int UserOwnerId { get; set; }
        public int ColorId { get; set; }
        public string ColorHex { get; set; }
        public bool IsAccepted { get; set; }
    }
}