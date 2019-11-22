using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models 
{
    public class Color 
    {
        public int Id { get; set; }
        public string Hex { get; set; }

        public Color(int id, string hex)
        {
            Id = id;
            Hex = hex;
        }

        public Color()
        {
            
        }
    }
}
