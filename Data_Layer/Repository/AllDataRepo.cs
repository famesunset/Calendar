using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Data_Layer.Models;
using Data_Layer.Repository.Interfaces;

namespace Data_Layer.Repository
{
    public class AllDataRepo : BaseRepository<AllData>, IAllData
    {
        public IEnumerable<AllData> GetDataEvents(User @user, IEnumerable<Calendar> @calendarList, DateTime dateTimeStart, DateTime dateTimeFinish)
        {
            DataTable idsCalendars = (@calendarList.Select(x => x.Id).ToList()).ConvertToDatatable("idsCalendars");
            using (SqlConnection connection = new SqlConnection(Data_Layer.Properties.Settings.Default.Server))
            {
                IEnumerable<AllData> s = connection.Query<AllData>("uspGetDataEvents", new { @user.IdUser, id_Calendar = idsCalendars, dateTimeStart, dateTimeFinish },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}