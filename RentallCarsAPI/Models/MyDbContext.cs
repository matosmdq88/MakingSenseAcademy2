using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RentallCarsAPI.Models
{
    public class MyDbContext: DbContext
    {
        public DbSet<Car> Car { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Rental> Rental { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
    }

    /*public class CarDb
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public int Doors { get; set; }
        public Brand Brand { get; set; }
        public string Color { get; set; }
        public Transmition Transmition { get; set; }
        public bool IsFree { get; set; }
    }

    public class ClientDb
    {
        public int Id { get; set; }
        public int Dni { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public short PostalCode { get; set; }
        public DateTime LastModification { get; set; }
    }

    public class RentalDb
    {
        public string Id { get; set; }
        public int ClientId { get; set; }
        public ClientDb Client { get; set; }
        public string CarId { get; set; }
        public CarDb Car { get; set; }
        public DateTime StartRental { get; set; }
        public byte RentalDays { get; set; }
        public int Penalisation { get; set; }
        public DateTime FinishRental { get; set; }
    }*/
}
