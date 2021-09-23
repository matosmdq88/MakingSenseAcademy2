using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RentallCarsAPI.Models;
using RentallCarsAPI.Models.Request;
using RentallCarsAPI.Tools.Interfaces;

namespace RentallCarsAPI.Tools
{
    public class CarHelper : ICarHelper
    {
        private readonly IConfiguration _configuration;

        public CarHelper(IConfiguration iconfiguration)
        {
            _configuration = iconfiguration;
        }
        public string ValidateParams(CarRequest model)
        {
            if (!Enum.IsDefined(typeof(Transmition), model.Transmition))
            {
                return "Invalid transmition";
            }
            if (!Enum.IsDefined(typeof(Brand), model.Brand))
            {
                return "Invalid mark";
            }
            return string.Empty;
        }
        public List<Car> GetAll()
        {
            var cars = new List<Car>();
            if (!System.IO.File.Exists(_configuration.GetValue<string>("MySettings:_pathcars")))
            {
                return cars;
            }
            try
            {
                var list = System.IO.File.ReadAllText(_configuration.GetValue<string>("MySettings:_pathcars"));
                if (list == "")
                    return cars;
                else
                    return JsonConvert.DeserializeObject<List<Car>>(list);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Car GetById(Guid id)
        {
            var cars = GetAll();
            if (cars == null)
            {
                return null;
            }
            return cars.FirstOrDefault(car => car.Id == id);
        }
    }
}
