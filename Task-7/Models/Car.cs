using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_7.Models
{
    class Car
    {
        public string model { get; set; }
        public string mark { get; set; }
        public Color color { get; set; }
        public int year { get; set; }

        public override string ToString()
        {
            return "this car is a "+mark+" "+model+", and was built in "+year+". Its color is "+color;
        }
    }
}
