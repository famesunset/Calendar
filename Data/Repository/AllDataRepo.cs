﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Data.Models;
using Data.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class AllDataRepo : BaseRepository, IAllData
    {
        public IEnumerable<AllData> GetDataEvents(int userId, IEnumerable<Calendar> @calendarList, DateTime dateTimeStart, DateTime dateTimeFinish)
        {
            DataTable idsCalendars = (@calendarList.Select(x => x.Id).ToList()).ConvertToDatatable("idsCalendars");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<AllData> events = connection.Query<AllData>("uspGetDataEvents", new { IdUser = userId, id_Calendar = idsCalendars, dateTimeStart, dateTimeFinish },
                    commandType: CommandType.StoredProcedure);
                return events;
            }
        }

        public IEnumerable<AllData> GetInfinityEvents(int userId, IEnumerable<Calendar> @calendarList, DateTime finish)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataTable idsCalendars = (@calendarList.Select(x => x.Id).ToList()).ConvertToDatatable("idsCalendars");
                IEnumerable<AllData> events = connection.Query<AllData>("uspGetDataInfinityEvents",
                    new {IdUser = userId, id_Calendar = idsCalendars, date = finish},
                    commandType: CommandType.StoredProcedure);
                return events;
            }
        }

        public AllData GetEvent(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                AllData _event = connection.Query<AllData>("uspGetEventById", new { eventId },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return _event;
            }
        }
    }
}