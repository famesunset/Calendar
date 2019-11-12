using AutoMapper;
using Business_Layer.Models;
using Data_Layer.Models;
using System;
using System.Collections.Generic;

namespace Business_Layer
{
    public static class Mapper
    {
        static Mapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // User
                //cfg.CreateMap<User, Data_Layer.User>()
                //    .ForMember(dest => dest.IdUser, map => map.MapFrom(src => src.Id));

                //cfg.CreateMap<Data_Layer.User, User>()
                //    .ForMember(dest => dest.Id, map => map.MapFrom(src => src.IdUser));

                // Calendar
                //cfg.CreateMap<Calendar, Data_Layer.Calendar>()
                //    .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
                //    .ForMember(dest => dest.AccessId, map => map.MapFrom(src => src.Access));

                //cfg.CreateMap<Data_Layer.Calendar, Calendar>()
                //    .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
                //    .ForMember(dest => dest.Access, map => map.MapFrom(src => src.AccessId));

                cfg.CreateMap<Data_Layer.Calendar, Calendar>()
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

                // Event
                //cfg.CreateMap<Event, Data_Layer.Event>()
                //    .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id));

                //cfg.CreateMap<Data_Layer.Event, Event>()
                //    .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id));

                //cfg.CreateMap<AllData, Business_Layer.Models.Event>()
                //    .ForMember(dest => dest.Id, map => map.MapFrom(src => src.EventId))
                //    .ForMember(dest => dest.Title, map => map.MapFrom(src => src.Title))
                //    .ForMember(dest => dest.Color, map => map.MapFrom(src => "#fff"))
                //    .ForMember(dest => dest.Description, map => map.MapFrom(src => src.Description))
                //    .ForMember(dest => dest.Start, map => map.MapFrom(src => src.TimeStart))
                //    .ForMember(dest => dest.Finish, map => map.MapFrom(src => src.TimeFinish))
                //    //.ForMember(dest => dest.IsAllDay, map => map.MapFrom(src => src.AllDay));
                //    ;

                //cfg.CreateMap<Data_Layer.Event, BaseEvent>()
                //.ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id));

                cfg.CreateMap<AllData, BaseEvent>()
                    .ConstructUsing(val => new BaseEvent()
                    {
                        Color = null,
                        Start = val.TimeStart,
                        Finish = val.TimeFinish,
                        Title = val.Title,
                        Id = val.EventId,
                        IsAllDay = false,
                            //IsAllDay = val.a
                        });

                cfg.CreateMap<AllData, Event>()
                   .ConstructUsing(val => new Event()
                   {
                       Id = val.EventId,
                       CalendarId = val.IdCalendar,
                       Color = null,
                       Description = val.Description,
                       Finish = val.TimeFinish,
                       Start = val.TimeStart,
                       //IsAllDay = val.IsAllDay,
                       Notify = new NotificationSchedule { Message = val.Notification, Time = val.NotificationTime },
                       // не хватает notificationId, но нужен ли он
                       //Repeat = val.Interval,
                       Schedule = new List<Models.EventSchedule>(),
                       Title = val.Title,
                    });

                cfg.CreateMap<Event, Data_Layer.Event>()
                    .ConstructUsing(val => new Data_Layer.Event()
                    {
                        Id = val.Id,
                        AllDay = val.IsAllDay,
                        CalendarId = val.CalendarId,
                        Description = val.Description,
                        Title = val.Title,
                        TimeFinish = val.Finish,
                        TimeStart = val.Start,
                        Notification = null,
                        RepeatId = 0,
                        Schedule = null,
                    });
            });
            
            Map = config.CreateMapper();
            //Result = mapper.Map<Data_Layer.User, User>(new Data_Layer.User(){id_User = 1, Mobile = "123"});
        }
        public static IMapper Map { get; }

        //public static Data_Layer.Event MapBussinesEvent(Event source)
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<Event, Data_Layer.Event>()
        //            .ConstructUsing(val => new Data_Layer.Event(0,
        //                val.CalendarId,
        //                val.Description ?? string.Empty,
        //                val.Notify != null ? val.Notify.Message ?? string.Empty : string.Empty,
        //                val.Title,
        //                val.Start,
        //                val.Finish));

        //    });
        //    var mapper = config.CreateMapper();
        //    var result = mapper.Map<Event, Data_Layer.Event>(source);

        //    // TODO: rework
        //    return result;
        //}
    }
}