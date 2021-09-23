using System;
using System.Collections.Generic;
using RentallCarsAPI.Models;
using RentallCarsAPI.Models.Request;

namespace RentallCarsAPI.Tools.Interfaces
{
    public interface ICarHelper
    {
        public string ValidateParams(CarRequest model);
        public List<Car> GetAll();
        public Car GetById(Guid id);
    }
}
