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
        public int IdIdentity { get; set; }

        public User(int idUser, int idCalendarDefault)
        {
            this.IdUser = idUser;
            this.IdCalendarDefault = idCalendarDefault;
        }

        public User(string name, string mobile, string email, int idIdentity)
        {
            this.Name = name;
            this.Mobile = mobile;
            this.Email = email;
            this.IdIdentity = idIdentity;
        }

        public User(int idUser, string name, string mobile, string email, int idCalendarDefault, int idIdentity)
        {
            this.IdUser = idUser;
            this.Name = name;
            this.Mobile = mobile;
            this.Email = email;
            this.IdCalendarDefault = idCalendarDefault;
            this.IdIdentity = idIdentity;
        }

        public User(int idUser)
        {
            this.IdUser = idUser;
        }

        public User()
        {
            
        }
    }
}
