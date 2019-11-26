namespace Data.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        //Get new id default calendar during creating a new user through stored procedure
        public int IdCalendarDefault { get; set; }
        public string IdIdentity { get; set; }
        public string Picture { get; set; }
        public string BrowserId { get; set; }
    }
}
