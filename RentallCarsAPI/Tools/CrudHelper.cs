using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RentallCarsAPI.Models;
using RentallCarsAPI.Models.Request;
using RentallCarsAPI.Tools.Interfaces;

namespace RentallCarsAPI.Tools
{
    public class CrudHelper : ICrudHelper
    {
        private readonly IConfiguration _configuration;

        public CrudHelper(IConfiguration iconfiguration)
        {
            _configuration = iconfiguration;
        }
        public string ValidateParams(CarRequest model)
        {
            if (!Enum.IsDefined(typeof(EnumTransmition), model.Transmition))
            {
                return "Invalid transmition";
            }
            if (!Enum.IsDefined(typeof(EnumMark), model.Mark))
            {
                return "Invalid mark";
            }
            return string.Empty;
        }
        public List<Car> GetAll()
        {
            var cars = new List<Car>();
            if (!System.IO.File.Exists(_configuration.GetValue<string>("MySettings:_path")))
            {
                return cars;
            }
            try
            {
                var list = System.IO.File.ReadAllText(_configuration.GetValue<string>("MySettings:_path"));
                if (list == "")
                    return cars;
                else
                    return JsonConvert.DeserializeObject<List<Car>>(list);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
