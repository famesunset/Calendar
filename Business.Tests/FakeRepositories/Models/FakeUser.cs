using System.Collections.Generic;

namespace Business.Tests.FakeRepositories.Models
{
    public class FakeUser
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public FakeCalendar DefaultCalendar { get; set; }
        public string PictureURL { get; set; }
        public List<FakeCalendar> Calendars { get; set; }
        public List<FakeBrowser> Browsers { get; set; }
    }
}