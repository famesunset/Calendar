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
                        Color = new Color {Id = val.ColorId, Hex = val.ColorHex},
                        UserOwnerId = val.UserOwnerId
                    })
                    .ForMember(dest => dest.Name,
                        expression => expression.MapFrom(src => Encode(src.Name)));

                cfg.CreateMap<Calendar, Data.Models.Calendar>()
                    .ConstructUsing(val => new Data.Models.Calendar
                    {
                        Id = val.Id,
                        Name = val.Name,
                        AccessId = (int)val.Access,
                        ColorId = val.Color.Id,
                        ColorHex =  val.Color.Hex,
                        UserOwnerId = val.UserOwnerId
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
                       CalendarId = val.CalendarId,
                       Finish = val.TimeFinish,
                       Start = val.TimeStart,
                       IsAllDay = val.AllDay,
                       // Repeat = val
                   })
                   .ForMember(dest => dest.Title,
                       expression => expression.MapFrom(src => Encode(src.Title)))
                   .ForMember(dest => dest.Color,
                       expression => expression.MapFrom(src => Encode(null)))
                   .ForMember(dest => dest.Description,
                       expression => expression.MapFrom(src => Encode(src.Description)));

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
                        RepeatId = 0,
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

                cfg.CreateMap<Data.Models.Color, Color>()
                    .ForMember(dest => dest.Hex,
                        expression => expression.MapFrom(src => Encode(src.Hex)));

                cfg.CreateMap<Color, Data.Models.Color>();

            });
            Map = config.CreateMapper();
        }
        public static IMapper Map { get; }
    }
}