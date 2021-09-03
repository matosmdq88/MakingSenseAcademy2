using System.Collections.Generic;
using RentallCarsAPI.Models;
using RentallCarsAPI.Models.Request;

namespace RentallCarsAPI.Tools.Interfaces
{
    public interface ICrudHelper
    {
        public string ValidateParams(CarRequest model);
        public List<Car> GetAll();
    }
}
