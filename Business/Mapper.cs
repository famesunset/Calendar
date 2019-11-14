namespace Business
{
    using AutoMapper;
    using Models;
    using System;
    using System.Collections.Generic;
    public static class Mapper
    {
        static Mapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Data.Models.Calendar, Calendar>()
                    .ConstructUsing(val => new Calendar()
                    {
                        Access = (Access)Enum.GetValues(typeof(Access)).GetValue(val.AccessId),
                        Color = null,
                        // todo
                        Events = new List<BaseEvent>(),
                        Id = val.Id,
                        Name = val.Name,
                        Users = new List<User>(),
                    });

              
                cfg.CreateMap<Data.Models.AllData, BaseEvent>()
                    .ConstructUsing(val => new BaseEvent()
                    {
                        Color = null,
                        Start = val.TimeStart,
                        Finish = val.TimeFinish,
                        Title = val.Title,
                        Id = val.EventId,
                        IsAllDay = val.AllDay
                        });

                cfg.CreateMap<Data.Models.AllData, Event>()
                   .ConstructUsing(val => new Event()
                   {
                       Id = val.EventId,
                       CalendarId = val.IdCalendar,
                       Color = null,
                       Description = val.Description,
                       Finish = val.TimeFinish,
                       Start = val.TimeStart,
                       IsAllDay = val.AllDay,
                       Notify = new NotificationSchedule { Message = val.Notification, Time = val.NotificationTime },
                       // не хватает notificationId, но нужен ли он
                       //Repeat = val.Interval,
                       Schedule = new List<Models.EventSchedule>(),
                       Title = val.Title,
                    });

                cfg.CreateMap<Event, Data.Models.Event>()
                    .ConstructUsing(val => new Data.Models.Event()
                    {
                        Id = val.Id,
                        AllDay = val.IsAllDay,
                        CalendarId = val.CalendarId,
                        Description = val.Description,
                        Title = val.Title,
                        TimeFinish = val.Finish.ToUniversalTime(),
                        TimeStart = val.Start.ToUniversalTime(),
                        Notification = null,
                        RepeatId = 0,
                        Schedule = null,
                    });
            });
            
            Map = config.CreateMapper();
        }
        public static IMapper Map { get; }
    }
}