namespace Business.Tests.FakeRepositories
{
    using System.Collections.Generic;
    using Models;
    
    public class FakeRepository
    {
        private static FakeRepository instanse;
        public static FakeRepository Get => instanse ??= new FakeRepository();
        public List<FakeCalendar> Calendars { get; }
        public List<FakeEvent> Events { get; }
        public List<FakeUser> Users { get; }

        private FakeRepository()
        {
            Calendars = new List<FakeCalendar>();
            Events = new List<FakeEvent>();
            Users = new List<FakeUser>();
        }

        private void Init()
        {
            // todo: initial values
        }
    }
}
