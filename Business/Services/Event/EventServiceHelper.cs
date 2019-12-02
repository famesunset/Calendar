namespace Business.Services.Event
{
    using System;
    using System.Collections.Generic;
    using Models;
    public partial class EventService
    {
        private static (DateTime Start, DateTime Finish) GetDateRange(DateTime beginning, DateUnit dateUnit, int timeOffset)
        {
            DateTime dateStart;
            DateTime dateFinish;
            beginning = beginning.AddMinutes(timeOffset);
            switch (dateUnit)
            {
                default:
                case DateUnit.Day:
                    {
                        dateStart = beginning + TimeSpan.FromSeconds(1);
                        dateFinish = dateStart.AddDays(1) - TimeSpan.FromSeconds(2);
                    }
                    break;
                case DateUnit.Week:
                    {
                        int startDay = beginning.Day - (int)beginning.DayOfWeek;
                        dateStart = new DateTime(beginning.Year, beginning.Month, startDay);
                        dateFinish = dateStart.AddDays(7);
                    }
                    break;
                case DateUnit.Month:
                    {
                        dateStart = new DateTime(beginning.Year, beginning.Month, 1);
                        dateFinish = dateStart.AddMonths(1);
                    }
                    break;
            }
            return (dateStart, dateFinish);
        }

        private IEnumerable<Data.Models.AllData> GetInfinityEvents
            (int userId, IEnumerable<Data.Models.Calendar> calendarList, DateTime beginning, DateUnit dateUnit, DateTime finish, int timeOffset)
        {
            var events = serviceHelper.WrapMethodWithReturn(() => bigEventRepos.GetInfinityEvents(userId, calendarList, finish),
                new List<Data.Models.AllData>());
            var result = new List<Data.Models.AllData>();
            foreach (var _event in events)
            {
                AddTimeOffset(_event, timeOffset);
                result.AddRange(FindEvents(beginning, dateUnit, _event));
            }
            return result;
        }

        private static void AddTimeOffset(Data.Models.AllData _event, int timeOffset)
        {
            _event.TimeStart = _event.TimeStart.AddMinutes(-timeOffset);
            _event.TimeFinish = _event.TimeFinish.AddMinutes(-timeOffset);
        }

        private static void AddTimeOffset(Event _event, int timeOffset)
        {
            _event.Start = _event.Start.AddMinutes(-timeOffset);
            _event.Finish = _event.Finish.AddMinutes(-timeOffset);
        }

        private static IEnumerable<Data.Models.AllData> FindEvents(DateTime beginning, DateUnit dateUnit, Data.Models.AllData _event)
        {
            switch (dateUnit)
            {
                default:
                case DateUnit.Day:
                    return FindEventsDayView(beginning, _event);
                case DateUnit.Week:
                    throw new NotImplementedException();
                case DateUnit.Month:
                    throw new NotImplementedException();
            }
        }

        private static IEnumerable<Data.Models.AllData> FindEventsDayView(DateTime beginning, Data.Models.AllData _event)
        {
            var result = new List<Data.Models.AllData>();
            switch (_event.RepeatId)
            {
                case (int)Interval.Day:
                    result.Add(_event);
                    break;
                case (int)Interval.Week:
                    if (beginning.DayOfWeek >= _event.TimeStart.DayOfWeek &&
                        beginning.DayOfWeek <= _event.TimeFinish.DayOfWeek)
                    {
                        result.Add(_event);
                    }
                    break;
                case (int)Interval.Month:
                    if (beginning.Day >= _event.TimeStart.Day &&
                        beginning.Day <= _event.TimeFinish.Day)
                    {
                        result.Add(_event);
                    }
                    break;
                case (int)Interval.Year:
                    if (beginning.Month >= _event.TimeStart.Month &&
                        beginning.Month <= _event.TimeFinish.Month &&
                        beginning.Day >= _event.TimeStart.Day &&
                        beginning.Day <= _event.TimeFinish.Day)
                    {
                        result.Add(_event);
                    }
                    break;
            }
            return result;
        }

        #region unused
        //private IEnumerable<Data.Models.AllData> BuildInfinityEvents(int idUser, IEnumerable<Data.Models.Calendar> @calendarsList, DateTime dateStart, DateTime dateFinish)
        //{
        //    IEnumerable<Data.Models.AllData> s = bigEventRepos.GetInfinityEvents(idUser, @calendarsList, dateFinish);
        //    var infinity = new List<Data.Models.AllData>();
        //    foreach (var t in s)
        //    {
        //        if (t.TimeStart < dateStart)
        //        {
        //            DateTime tempStart = t.TimeStart;
        //            DateTime tempFinish = t.TimeFinish;
        //            switch (t.RepeatId)
        //            {
        //                case 1:
        //                    {
        //                        do
        //                        {
        //                            infinity.Add(new Data.Models.AllData(t.CalendarId, t.CalendarName, t.AccessName, t.EventId,
        //                                t.Description, t.Title, t.EventId,
        //                                tempStart, tempFinish,
        //                                t.RepeatId));

        //                            tempStart = tempStart.AddDays(1);
        //                            tempFinish = tempFinish.AddDays(1);
        //                        } while (dateFinish.AddDays(1)! > tempStart);

        //                        break;
        //                    }

        //                case 7:
        //                    {
        //                        do
        //                        {
        //                            infinity.Add(new Data.Models.AllData(t.CalendarId, t.CalendarName, t.AccessName, t.EventId,
        //                                t.Description, t.Title, t.EventId,
        //                                tempStart, tempFinish,
        //                                t.RepeatId));

        //                            tempStart = tempStart.AddDays(7);
        //                            tempFinish = tempFinish.AddDays(7);
        //                        } while (dateFinish.AddDays(1)! > tempStart);

        //                        break;
        //                    }

        //                case 30:
        //                    {
        //                        do
        //                        {
        //                            infinity.Add(new Data.Models.AllData(t.CalendarId, t.CalendarName, t.AccessName, t.EventId,
        //                                t.Description, t.Title, t.EventId,
        //                                tempStart, tempFinish,
        //                                t.RepeatId));

        //                            tempStart = tempStart.AddDays(30);
        //                            tempFinish = tempFinish.AddDays(30);
        //                        } while (dateFinish.AddDays(1)! > tempStart);

        //                        break;
        //                    }

        //                case 365:
        //                    {
        //                        do
        //                        {
        //                            infinity.Add(new Data.Models.AllData(t.CalendarId, t.CalendarName, t.AccessName, t.EventId,
        //                                t.Description, t.Title, t.EventId,
        //                                tempStart, tempFinish,
        //                                t.RepeatId));

        //                            tempStart = tempStart.AddDays(365);
        //                            tempFinish = tempFinish.AddDays(365);
        //                        } while (dateFinish.AddDays(1)! > tempStart);

        //                        break;
        //                    }
        //            }
        //        }
        //    }
        //    return infinity;
        //}

        #endregion
    }
}
