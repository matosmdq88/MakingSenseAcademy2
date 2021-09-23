using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentallCarsAPI.Models;

namespace RentallCarsAPI.Tools.Interfaces
{
    public interface IRentalHelper
    {
        public Car GetCar(Guid idCar);
        public Client GetClient(int dni);
        public List<Rental> GetAll();
    }
}
