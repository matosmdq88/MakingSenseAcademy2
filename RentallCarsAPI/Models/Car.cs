using System;

namespace RentallCarsAPI.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Model{ get; set; }
        public int Doors { get; set; }
        public EnumMark Mark { get; set; }
        public string Color { get; set; }
        public EnumTransmition Transmition { get; set; }
    }
}
