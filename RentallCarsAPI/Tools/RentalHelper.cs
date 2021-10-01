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
        private readonly MyDbContext _context;

        public RentalHelper(IConfiguration configuration, ICarHelper carHelper, IClientHelper clientHelper,MyDbContext context)
        {
            _configuration = configuration;
            _clientHelper = clientHelper;
            _carHelper = carHelper;
            _context = context;
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
                return JsonConvert.DeserializeObject<List<Rental>>(list);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string UpdateIsFree(Guid idCar, bool condition)
        {
            var cars = _carHelper.GetAll();
            foreach (var car in cars)
            {
                if (car.Id == idCar)
                {
                    car.IsFree = condition;
                    break;
                }
            }

            try
            {
                var writer = JsonConvert.SerializeObject(cars, Formatting.Indented);
                System.IO.File.WriteAllText(_configuration.GetValue<string>("MySettings:_pathcars"), writer);
                return string.Empty;
            }
            catch (Exception)
            {
                return "Error while update car";
            }
        }

        public Rental GetById(Guid id)
        {
            var rentals = GetAll();
            if (rentals==null)
            {
                return null;
            }

            return rentals.FirstOrDefault(rental => rental.Id == id);
        }

        public string SaveRental(List<Rental> rentals)
        {
            try
            {
                var writer = JsonConvert.SerializeObject(rentals, Formatting.Indented);
                System.IO.File.WriteAllText(_configuration.GetValue<string>("MySettings:_pathrentals"), writer);
                return string.Empty;
            }
            catch (Exception)
            {
                return "Error while update rental";
            }
        }
    }
}
