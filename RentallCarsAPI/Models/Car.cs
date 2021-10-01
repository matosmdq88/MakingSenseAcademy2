using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace RentallCarsAPI.Models
{
    public class Car
    {
        public  Guid Id { get; set; }
        public string Model{ get; set; }
        public int Doors { get; set; }
        public Brand Brand { get; set; }
        public string Color { get; set; }
        public Transmition Transmition { get; set; }
        public bool IsFree { get; set; }
        public bool Available { get; set; }
    }
}
