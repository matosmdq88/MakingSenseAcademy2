using System;

namespace RentallCarsAPI.Models.Request
{
    public class RentalRequest
    {
        public Guid Id { get; set; }
        public int Client { get; set; }
        public Guid Car { get; set; }
        public DateTime StartRental { get; set; }
        public byte RentalDays { get; set; }
        public int Penalisation { get; set; }
        public DateTime FinishRental { get; set; }
    }
}
