using AutoMapper;
using Business_Layer.Models;
using Data_Layer.Models;

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

                    cfg.CreateMap<Data_Layer.Event, Event>()
                    .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id));

                    cfg.CreateMap<AllData, Business_Layer.Models.Event>()
                        .ConstructUsing(val => new Event()
                        {
                            Color = null,
                            Description = val.Description,
                            Start = val.TimeStart,
                            Finish = val.TimeFinish,
                            Title = val.Title,
                            Id = val.EventId,
                            //IsAllDay = val.AllDay,
                            Notify = new NotificationSchedule()
                            {
                                Id = val.EventScheduledId,
                                Message = val.Notification,
                                Time = val.NotificationTime,
                            },
                        });
            });
            
            Map = config.CreateMapper();
            //Result = mapper.Map<Data_Layer.User, User>(new Data_Layer.User(){id_User = 1, Mobile = "123"});
        }
        public static IMapper Map { get; }

        public static Data_Layer.Event MapBussinesEvent(Event source, int calendarId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, Data_Layer.Event>()
                    .ConstructUsing(val => new Data_Layer.Event(0, 
                        calendarId, 
                        val.Description ?? string.Empty, 
                        val.Notify != null ? val.Notify.Message ?? string.Empty : string.Empty,
                        val.Title,
                        val.Start,
                        val.Finish));

            });

            var mapper = config.CreateMapper();
            var result = mapper.Map<Event, Data_Layer.Event>(source);

            return result;
        }
    }
}