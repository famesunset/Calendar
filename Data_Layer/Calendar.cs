using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public class Calendar
    {
        public int id_Calendar { get; set; }
        public string Name { get; set; }
        public int id_Access { get; set; }

        public Calendar()
        {

        }

        public Calendar(string name, int id_access)
        {
            this.Name = name;
            this.id_Access = id_access;
        }

        public void AddCalendar()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.Server))
            {
                var AddECalendar = connection.Query<Calendar>("AddCalendar", new { this.Name, this.id_Access },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
