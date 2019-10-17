using AutoMapper;
using Business_Layer.Models;

namespace Business_Layer
{
    public static class Mapper
    {
        static Mapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // User
                cfg.CreateMap<User, Data_Layer.User>()
                    .ForMember(dest => dest.id_User, map => map.MapFrom(src => src.Id));

                cfg.CreateMap<Data_Layer.User, User>()
                    .ForMember(dest => dest.Id, map => map.MapFrom(src => src.id_User));

                // Calendar
                cfg.CreateMap<Calendar, Data_Layer.Calendar>()
                    .ForMember(dest => dest.id_Calendar, map => map.MapFrom(src => src.Id))
                    .ForMember(dest => dest.id_Access, map => map.MapFrom(src => src.Access));

                cfg.CreateMap<Data_Layer.Calendar, Calendar>()
                    .ForMember(dest => dest.Id, map => map.MapFrom(src => src.id_Calendar))
                    .ForMember(dest => dest.Access, map => map.MapFrom(src => src.id_Access));

                // Event
                cfg.CreateMap<Event, Data_Layer.Event>()
                    .ForMember(dest => dest.id, map => map.MapFrom(src => src.Id))
                    .ForMember(dest => dest.id_Calendar, map => map.MapFrom(src => src.CalendarId));

                cfg.CreateMap<Data_Layer.Event, Event>()
                    .ForMember(dest => dest.Id, map => map.MapFrom(src => src.id))
                    .ForMember(dest => dest.CalendarId, map => map.MapFrom(src => src.id_Calendar));
            });
            
            Map = config.CreateMapper();
            //Result = mapper.Map<Data_Layer.User, User>(new Data_Layer.User(){id_User = 1, Mobile = "123"});
        }
        public static IMapper Map { get; }
    }
}