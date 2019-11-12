using System.Collections.Generic;
using System.Security.AccessControl;

namespace BusinessCore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public List<int> SelectedCalendars { get; set; }
        // календари которые отображаются
    }
}