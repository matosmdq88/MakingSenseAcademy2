using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentallCarsAPI.Models
{
    public class Car
    {
        public string model{ get; set; }
        public int doors { get; set; }
        public EnumMark mark { get; set; }
        public string color { get; set; }
        public EnumTransmition transmition { get; set; }
    }
}
