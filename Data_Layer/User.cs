using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int IdCalendarDefault { get; set; }
        public int IdIdentity { get; set; }

        public void CreateUser(string name, string email, string mobile)
        {
            //sp 
        }
    }
}
