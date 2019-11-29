namespace Business.Tests.FakeRepositories
{
    using System;
    using Data.Models;
    using Data.Repository.Interfaces;
    using System.Linq;

    public class FakeEventRepository : IEvent
    {
        private static int eventId = 0;
        public int CreateEvent(Event _event)
        {
            var fakeCalendar = FakeRepository.Get.Calendars.SingleOrDefault(c => c.Id.Equals(_event.CalendarId));
            if (fakeCalendar != null)
            {
                _event.Id = ++eventId;
                var fakeEvent = FakeConverters.EventToFakeEvent(_event, fakeCalendar);
                FakeRepository.Get.Events.Add(fakeEvent);
                fakeCalendar.Events.Add(fakeEvent);
                return fakeEvent.Id;
            }
            return -1;
        }

        public void UpdateEvent(Event newEvent)
        {
            var fakeEvent = FakeRepository.Get.Events.SingleOrDefault(e => e.Id.Equals(newEvent.Id));
            if (!newEvent.AllDay.Equals(fakeEvent.IsAllDay))
            {
                fakeEvent.IsAllDay = newEvent.AllDay;
            }
            if (newEvent.Description != null && !newEvent.Description.Equals(fakeEvent.Description))
            {
                fakeEvent.Description = newEvent.Description;
            }
            if (newEvent.Title != null && !newEvent.Title.Equals(fakeEvent.Title))
            {
                fakeEvent.Title = newEvent.Title;
            }
            if (newEvent.TimeStart != DateTime.MinValue && newEvent.TimeStart != fakeEvent.Start)
            {
                fakeEvent.Start = newEvent.TimeStart;
            }
            if (newEvent.TimeFinish != DateTime.MinValue && newEvent.TimeFinish != fakeEvent.Finish)
            {
                fakeEvent.Finish = newEvent.TimeFinish;
            }
            if (newEvent.RepeatId != (int)fakeEvent.Interval)
            {
                fakeEvent.Interval = (Business.Models.Interval)Enum.ToObject(typeof(Business.Models.Interval), newEvent.RepeatId);
            }
        }

        public void Delete(int eventId)
        {
            var fakeEvent = FakeRepository.Get.Events.SingleOrDefault(e => e.Id.Equals(eventId));
            FakeRepository.Get.Events.Remove(fakeEvent);
            fakeEvent.Calendar.Events.Remove(fakeEvent);
        }
    }
}