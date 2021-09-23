using System;

namespace RentallCarsAPI.Models
{
    public class Rental
    { 
        public Guid Id { get; set; } 
        public Client Client { get; set; }
        public Car Car { get; set; }
        public DateTime StartRental { get; set; }
        public byte RentalDays { get; set; }
        public int Penalisation { get; set; }
        public DateTime FinishRental { get; set; }
    }
}
