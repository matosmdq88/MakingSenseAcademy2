using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RentallCarsAPI.Models;
using RentallCarsAPI.Tools.Interfaces;

namespace RentallCarsAPI.Tools
{
    public class RentalHelper : IRentalHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IClientHelper _clientHelper;
        private readonly ICarHelper _carHelper;

        public RentalHelper(IConfiguration configuration, ICarHelper carHelper, IClientHelper clientHelper)
        {
            _configuration = configuration;
            _clientHelper = clientHelper;
            _carHelper = carHelper;
        }

        public Car GetCar(Guid idCar)
        {
            return _carHelper.GetById(idCar);
        }

        public Client GetClient(int dni)
        {
            return _clientHelper.GetByDni(dni);
        }

        public List<Rental> GetAll()
        {
            var rentals = new List<Rental>();
            if (!System.IO.File.Exists(_configuration.GetValue<string>("MySettings:_pathrentals")))
            {
                return rentals;
            }
            try
            {
                var list = System.IO.File.ReadAllText(_configuration.GetValue<string>("MySettings:_pathrentals"));
                if (list == "")
                    return rentals;
                else
                    return JsonConvert.DeserializeObject<List<Rental>>(list);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
