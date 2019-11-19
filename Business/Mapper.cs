namespace Business
{
    using AutoMapper;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using System.Text.Unicode;

    internal static class Mapper
    {
        private static HtmlEncoder htmlEncoder;
        private static string Encode(string html)
        {
            return htmlEncoder.Encode(html ?? string.Empty);
        }

        static Mapper()
        {
            htmlEncoder = HtmlEncoder.Create(UnicodeRanges.Cyrillic, UnicodeRanges.BasicLatin);
            var config = new MapperConfiguration(cfg =>
            {
                // Calendar
                cfg.CreateMap<Data.Models.Calendar, Calendar>()
                    .ConstructUsing(val => new Calendar
                    {
                        Access = (Access)Enum.ToObject(typeof(Access), val.AccessId),
                        Events = new List<BaseEvent>(),
                        Id = val.Id,
                        Users = new List<User>(),
                    })
                    .ForMember(dest => dest.Color,
                        expression => expression.MapFrom(src => Encode(null)))
                    .ForMember(dest => dest.Name,
                        expression => expression.MapFrom(src => Encode(src.Name)));

                cfg.CreateMap<Calendar, Data.Models.Calendar>()
                    .ConstructUsing(val => new Data.Models.Calendar
                    {
                        Id = val.Id,
                        Name = val.Name,
                        AccessId = (int)val.Access
                    });


                // Event
                cfg.CreateMap<Data.Models.AllData, BaseEvent>()
                    .ConstructUsing(val => new BaseEvent
                    {
                        Start = val.TimeStart,
                        Finish = val.TimeFinish,
                        Id = val.EventId,
                        IsAllDay = val.AllDay
                    })
                    .ForMember(baseEvent => baseEvent.Title,
                        expression => expression.MapFrom(data => Encode(data.Title)))
                    .ForMember(baseEvent => baseEvent.Color,
                        expression => expression.MapFrom(data => Encode(null)));

                cfg.CreateMap<Data.Models.AllData, Event>()
                   .ConstructUsing(val => new Event
                   {
                       Id = val.EventId,
                       CalendarId = val.IdCalendar,
                       Finish = val.TimeFinish,
                       Start = val.TimeStart,
                       IsAllDay = val.AllDay,
                       Notify = new NotificationSchedule { Time = val.NotificationTime },
                       //Repeat = val.Interval,
                       Schedule = new List<EventSchedule>(),
                   })
                   .ForMember(dest => dest.Title,
                       expression => expression.MapFrom(src => Encode(src.Title)))
                   .ForMember(dest => dest.Color,
                       expression => expression.MapFrom(src => Encode(null)))
                   .ForMember(dest => dest.Description,
                       expression => expression.MapFrom(src => Encode(src.Description)))
                   .ForPath(dest => dest.Notify.Message,
                       expression => expression.MapFrom(src => Encode(src.Notification)));

                cfg.CreateMap<Event, Data.Models.Event>()
                    .ConstructUsing(val => new Data.Models.Event
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

                // User
                cfg.CreateMap<User, Data.Models.User>()
                    .ConvertUsing(val => new Data.Models.User
                    {
                        IdUser = val.Id,
                        Email = val.Email,
                        Mobile = val.Mobile,
                        Name = val.Name,
                        Picture = val.Picture,
                        IdIdentity = val.IdentityId,
                    });

                cfg.CreateMap<Data.Models.User, User>()
                    .ConstructUsing(val => new User()
                    {
                        Id = val.IdUser,
                        IdentityId = val.IdIdentity,
                        SelectedCalendars = new List<int>()
                    })
                    .ForMember(dest => dest.Name,
                       expression => expression.MapFrom(src => Encode(src.Name)))
                    .ForMember(dest => dest.Mobile,
                       expression => expression.MapFrom(src => Encode(src.Mobile)))
                    .ForMember(dest => dest.Email,
                       expression => expression.MapFrom(src => Encode(src.Email)))
                    .ForMember(dest => dest.Picture,
                       expression => expression.MapFrom(src => Encode(src.Picture)));
            });
            Map = config.CreateMapper();
        }
        public static IMapper Map { get; }
    }
}