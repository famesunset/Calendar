using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Hex { get; set; }

        public Color(int id, string hex)
        {
            this.Id = id;
            this.Hex = hex;
        }

        public Color()
        {

        }
    }
}
