using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Data_Layer.Models;
using Data_Layer.Repository.Interfaces;

namespace Data_Layer.Repository
{
    public class AllDataRepo : BaseRepository<AllData>, IAllData
    {
        public IEnumerable<AllData> GetDataEvents(int userId, List<int> idCalendarList, DateTime dateTimeStart, DateTime dateTimeFinish)
        {
            DataTable idsCalendars = idCalendarList.ConvertToDatatable();
            using (SqlConnection connection = new SqlConnection(Data_Layer.Properties.Settings.Default.Server))
            {
                IEnumerable<AllData> s = connection.Query<AllData>("uspGetDataEvents", new { userId, idsCalendars, dateTimeStart, dateTimeFinish },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}
