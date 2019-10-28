using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public class Access
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Access(int id)
        {
            this.Id = id;
        }

        public Access(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }

}
