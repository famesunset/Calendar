using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataCore
{
    public class UserCalendar
    {
        //public int id_User { get; set; }
        //public int id_Calendar { get; set; }

        //public UserCalendar(int idUser, int idCalendar)
        //{
        //    this.id_Calendar = idCalendar;
        //    this.id_User = idUser;
        //}

        //public void SetCalendarToUser()
        //{
        //}

        //public void SetCalendarToUser(List<UserCalendar> list)
        //{
        //    var CalendarsUsers = list.ConvertToDatatable();
        //    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.Server))
        //    {
        //        var AddEvent = connection.Query<UserCalendar>("SetCalendarToUser", new { CalendarsUsers },
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}

        //public void AddIdCalendarToList(int id_Calendar)
        //{
        //    this.ids_Calendars.Add(id_Calendar);
        //}
    }
}
