using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public class UserCalendar
    {
        public int id_User { get; set; }
        public int id_Calendar { get; set; }
        public UserCalendar(int idUser, int idCalendar)
        {
            this.id_Calendar = idCalendar;
            this.id_User = idUser;
        }

        public void SetCalendarToUser(UserCalendar userCalendar)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "20.188.35.217",
                UserID = "sa",
                Password = "Sunsetfame05!",
                InitialCatalog = "Calendar"
            };
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            var AddEvent = connection.Query<UserCalendar>("SetCalendarToUser", new { userCalendar.id_Calendar, userCalendar.id_User },
                commandType: CommandType.StoredProcedure);
        }

        public void SetCalendarToUser()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "20.188.35.217",
                UserID = "sa",
                Password = "Sunsetfame05!",
                InitialCatalog = "Calendar"
            };
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            var AddEvent = connection.Query<UserCalendar>("SetCalendarToUser", new { this.id_Calendar, this.id_User },
                commandType: CommandType.StoredProcedure);
        }
    }
}
