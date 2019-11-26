using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models.ViewModels
{
    public class IntervalDropdownItem
    {
        public string Selector { get; set; }
        public string Value { get; set; }
        public Interval Interval { get; set; }

        public IntervalDropdownItem(string selector, string value)
        {
            Selector = selector;
            Value = value;
        }

        public IntervalDropdownItem(string selector, string value, Interval interval) 
            : this(selector, value)
        {
            Interval = interval;
        }
    }
}
