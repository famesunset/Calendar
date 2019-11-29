namespace Business.Tests.FakeRepositories.Models
{
    using System;
    using System.Collections.Generic;

    public class FakeCalendar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Business.Models.Access Access { get; set; }
        public FakeUser Owner { get; set; }
        public List<FakeUser> Users { get; set; }
        public List<FakeEvent> Events { get; set; }
        public ColorFake Color { get; set; }

        internal void Select()
        {
            throw new NotImplementedException();
        }
    }
}
