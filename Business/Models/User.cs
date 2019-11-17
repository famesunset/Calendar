using System.Collections.Generic;

namespace Business.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public List<int> SelectedCalendars { get; set; }
        public string IdentityId { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
    }
}