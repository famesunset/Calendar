using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models.ViewModels
{
    public class DropdownItem
    {
        public string Selector { get; set; }
        public string Value { get; set; }
        public object Data { get; set; }

        public DropdownItem(string selector, string value)
        {
            Selector = selector;
            Value = value;
        }

        public DropdownItem(string selector, string value, object data) 
            : this(selector, value)
        {
            Data = data;
        }
    }
}
