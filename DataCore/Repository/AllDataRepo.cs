using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DataCore.Models;
using DataCore.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataCore.Repository
{
    public class AllDataRepo : BaseRepository<AllData>, IAllData
    {
        private readonly string connectionString;
        public AllDataRepo()
        {
            var builder = new ConfigurationBuilder()   
                .AddJsonFile("launchSettings.json");
            var config = builder.Build();
            connectionString = config["profiles:DataCore:environmentVariables:Server"];
        }
        public IEnumerable<AllData> GetDataEvents(User @user, IEnumerable<Calendar> @calendarList, DateTime dateTimeStart, DateTime dateTimeFinish)
        {
            DataTable idsCalendars = (@calendarList.Select(x => x.Id).ToList()).ConvertToDatatable("idsCalendars");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                IEnumerable<AllData> s = connection.Query<AllData>("uspGetDataEvents", new { @user.IdUser, id_Calendar = idsCalendars, dateTimeStart, dateTimeFinish },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }

        public AllData GetEvent(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                AllData s = connection.Query<AllData>("uspGetEventById", new { eventId },
                    commandType: CommandType.StoredProcedure).First();
                return s;
            }
        }
    }
}