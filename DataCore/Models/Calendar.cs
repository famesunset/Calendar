using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataCore
{
    public class Calendar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccessId { get; set; }

        public Calendar(int id, string name, int accessId)
        {
            this.Id = id;
            this.Name = name;
            this.AccessId = accessId;
        }

        public Calendar(int id)
        {
            this.Id = id;
        }
    }
}