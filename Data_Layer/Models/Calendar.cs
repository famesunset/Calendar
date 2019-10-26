using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Data_Layer
{
    public class Calendar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccessId { get; set; }

        public Calendar(string name, int accessId)
        {
            this.Name = name;
            this.AccessId = accessId;
        }
        public Calendar(int id, string name, int accessId)
        {
            this.Id = id;
            this.Name = name;
            this.AccessId = accessId;
        }

        public void AddCalendar()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.Server))
            {
                var AddECalendar = connection.Query<Calendar>("AddCalendar", new { this.Name, this.AccessId },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
